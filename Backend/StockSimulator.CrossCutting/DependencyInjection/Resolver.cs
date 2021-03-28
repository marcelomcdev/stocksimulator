using Microsoft.Extensions.DependencyInjection;
using StockSimulator.Domain.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;
using Unity;
using Unity.Lifetime;

namespace StockSimulator.CrossCutting.DependencyInjection
{
    public sealed class Resolver : IResolver
    {
        private readonly IUnityContainer _container;
        private static IServiceProvider _serviceProvider;

        [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "O resolvedor é a classe aonde fica o 'acoplamento' da aplicação")]
        public Resolver(IServiceCollection services)
        {
            _serviceProvider = services.BuildServiceProvider();
        }

        public T GetService<T>()
        {
            return _serviceProvider.GetService<T>();
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        private void Singleton<T>(T o)
        {
            _container.RegisterInstance<T>(o);
        }

        private void Singleton<T1, T2>() where T2 : T1
        {
            _container.RegisterType<T1, T2>(new ContainerControlledLifetimeManager());
        }

        private void Transient<T1, T2>() where T2 : T1 => _container.RegisterType<T1, T2>(new TransientLifetimeManager());

        public void Dispose() => _container.Dispose();
    }
}
