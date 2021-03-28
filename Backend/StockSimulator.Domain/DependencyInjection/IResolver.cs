using System;
using System.Collections.Generic;
using System.Text;

namespace StockSimulator.Domain.DependencyInjection
{
    public interface IResolver
    {
        T Resolve<T>();
        T GetService<T>();
    }
}
