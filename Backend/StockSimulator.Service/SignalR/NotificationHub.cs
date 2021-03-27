using Microsoft.AspNetCore.SignalR;
using StockSimulator.Domain.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace StockSimulator.Service.SignalR
{
    public class NotificationHub //: Hub
    {
        //private readonly static ConnectionMapping<string> _connections = new ConnectionMapping<string>();
        //public static ConnectionMapping<string> Connections { get { return _connections; } }
        //private readonly IUserService _userService;
        //public NotificationHub(IUserService userService)
        //{
        //    _userService = userService;
        //}


        //public override Task OnConnectedAsync()
        //{
        //    _connections.Add(Context.ConnectionId, "1".ToString());
        //    return base.OnConnectedAsync();
        //}

        //public override Task OnDisconnectedAsync(Exception exception)
        //{
        //    _connections.Remove(Context.ConnectionId, "1".ToString());
        //    return base.OnDisconnectedAsync(exception);
        //}

        //[Authorize]
        //public override Task OnConnectedAsync()
        //{

        //    //var accessToken = Context.GetHttpContext().Request.Query["access_token"];
        //    //if (accessToken.Count() > 0)
        //    //{
        //    //    int userId = Convert.ToInt32(Context.GetHttpContext().User.Identity.Name ?? $"{AuthorizeActionFilterAttribute.RetornaIdDoUsuario(accessToken)}");
        //        //_connections.Add(Context.ConnectionId, userId.ToString());

        //        //var usuario = _usuarioService.FindBy(u => u.Id == userId).FirstOrDefault();
        //        ////var perfilSupervisor = usuario.PerfisUsuario.Where(f => f.Perfil != null && f.Perfil.Nome.ToLower().Contains("superv"));
        //        //var perfis = usuario.PerfisUsuario.ToList();
        //        //if (perfis.Any(f => f.IdPerfil == 15))
        //        //    Groups.AddToGroupAsync(Context.ConnectionId, "Administrador");
        //        //else if (perfis.Any(f => f.Perfil != null && f.Perfil.Nome.ToLower().Contains("superv")))
        //        //    Groups.AddToGroupAsync(Context.ConnectionId, $"supervisor_operacao_id_{usuario.IdOperacao}");
        //    }


        //    //if (perfilSupervisor.Any())
        //    //{
        //    //    Groups.AddToGroupAsync(Context.ConnectionId, $"supervisor_operacao_id_{usuario.IdOperacao}");
        //    //}
        //    //else if(usuario.PerfisUsuario.Any(f => f.Perfil == null))
        //    //    Groups.AddToGroupAsync(Context.ConnectionId, "Administrador");

        //    return base.OnConnectedAsync();
        //}

        //public override Task OnDisconnectedAsync(Exception exception)
        //{
        //    //var accessToken = Context.GetHttpContext().Request.Query["access_token"];
        //    //if (accessToken.Count() > 0)
        //    //{
        //    //    int userId = Convert.ToInt32(Context.GetHttpContext().User.Identity.Name ?? $"{AuthorizeActionFilterAttribute.RetornaIdDoUsuario(accessToken)}");
        //    //    _connections.Remove(Context.ConnectionId, userId.ToString());

        //    //    var usuario = _usuarioService.FindBy(u => u.Id == userId).FirstOrDefault();
        //    //    var perfis = usuario.PerfisUsuario.ToList();
        //    //    if (perfis.Any(f => f.IdPerfil == 15))
        //    //        Groups.AddToGroupAsync(Context.ConnectionId, "Administrador");
        //    //    else if (perfis.Any(f => f.Perfil != null && f.Perfil.Nome.ToLower().Contains("superv")))
        //    //        Groups.AddToGroupAsync(Context.ConnectionId, $"supervisor_operacao_id_{usuario.IdOperacao}");
        //    //}


        //    //var perfilSupervisor = usuario.PerfisUsuario.Where(f => f.Perfil.Nome.ToLower().Contains("superv"));
        //    //if (perfilSupervisor.Any())
        //    //{
        //    //    Groups.RemoveFromGroupAsync(Context.ConnectionId, $"supervisor_operacao_id_{usuario.IdOperacao}");
        //    //}
        //    //else if (usuario.PerfisUsuario.Any(f => f.Perfil == null))
        //    //    Groups.RemoveFromGroupAsync(Context.ConnectionId, "Administrador");

        //    return base.OnDisconnectedAsync(exception);
        //}

    }
}
