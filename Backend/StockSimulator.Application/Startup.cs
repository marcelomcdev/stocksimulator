using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using StockSimulator.Application.Helpers.Identity;
using StockSimulator.CrossCutting.Configuration;
using StockSimulator.Data.Context;
using StockSimulator.Data.Repository;
using StockSimulator.Domain.Entities;
using StockSimulator.Domain.Interfaces.Repository;
using StockSimulator.Domain.Interfaces.Services;
using StockSimulator.Service.QuoteSimulator;
using StockSimulator.Service.Services;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StockSimulator.Application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("config.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();
        }

        private string ConnectionString { 
            get 
            {
                var conn = Configuration.GetConnectionString("StockSimulatorDB");
                return conn.Replace("[DB_ENV]", System.Environment.GetEnvironmentVariable("DB_ENV"));
            } 
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddDbContext<StockContext>(option => option.UseSqlServer(ConnectionString, m => m.MigrationsAssembly("StockSimulator.Data")));

            #region Identity

            services.AddIdentity<User, IdentityRole>(options=>
                    {
                        options.Password.RequiredLength = 8;
                        options.Password.RequireLowercase = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequireDigit = false;
                        options.User.RequireUniqueEmail = true;
                        options.User.AllowedUserNameCharacters = string.Empty;
                    })
                    .AddRoles<IdentityRole>()
                    .AddErrorDescriber<LocalizedIdentityErrorDescriber>()
                    .AddEntityFrameworkStores<StockContext>()
                    .AddDefaultTokenProviders();

            #endregion Identity

            #region JWT Bearer Security
            // JWT
            var appSettingsSection = Configuration.GetSection("GeneralConfig");
            services.Configure<GeneralConfig>(appSettingsSection);

            var appSettings = appSettingsSection.Get<GeneralConfig>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => 
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudiences = appSettings.ValidIn,
                    ValidIssuer = appSettings.Issuer
                };
            });
            
            #endregion


            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<IOperationRepository, OperationRepository>();
            services.AddScoped<IOperationService, OperationService>();

            services.AddScoped<IListenerService, Listener>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region UseWebSocketsOptions
            var webSocketOptions = new WebSocketOptions()
            {
                KeepAliveInterval = TimeSpan.FromSeconds(120),
                ReceiveBufferSize = 4 * 1024
            };
            app.UseWebSockets(webSocketOptions);
            #endregion

            #region Accept Websocket

            app.Use(async (context, next) => { 
                if(context.Request.Path == "/ws")
                {
                    if(context.WebSockets.IsWebSocketRequest)
                    {
                        WebSocket websocket = await context.WebSockets.AcceptWebSocketAsync();
                        await Echo(context, websocket);
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                    }
                }
                else
                {
                    await next();
                }
            });

            #endregion

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                //c.AllowCredentials();
                c.AllowAnyOrigin();
                //c.WithOrigins("http://localhost:4200");
            });

            //app.UseHttpsRedirection();
            app.UseHttpMethodOverride();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


        #region Echo
        private async Task Echo(HttpContext context, WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }
        #endregion

    }
}
