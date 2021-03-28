using System;
using System.Collections.Generic;
using System.Text;

namespace StockSimulator.Domain.DependencyInjection
{
    [Serializable]
    public class ResolverNotConfiguredException : Exception
    {
        public ResolverNotConfiguredException() : base(
            string.Format("No one instance of \"{0}\" has configured in domain.", typeof(IResolver).Name))
        {
        }
    }
}

    
