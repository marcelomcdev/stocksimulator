﻿using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockSimulator.Domain.DependencyInjection
{
    public static class Dependencies
    {
        private static IResolver _resolver;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", Justification = "É necessário por ser o resolvedor")]
        public static IResolver Resolver
        {
            get
            {
                if (_resolver == null)
                    throw new ResolverNotConfiguredException();

                return _resolver;
            }

            set
            {
                _resolver = value;
            }
        }


        public static IMemoryCache MemoryCache
        {
            get { return Resolver.GetService<IMemoryCache>(); }
        }


    }
}
