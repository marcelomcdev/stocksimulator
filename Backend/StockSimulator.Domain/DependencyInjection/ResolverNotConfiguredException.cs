using System;
using System.Collections.Generic;
using System.Text;

namespace StockSimulator.Domain.DependencyInjection
{
    [Serializable]
    public class ResolverNotConfiguredException : Exception
    {
        public ResolverNotConfiguredException() : base($"No one instance of name {typeof(IResolver).Name} has configured in domain.")
        {

        }
    }
}
