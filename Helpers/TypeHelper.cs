using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace dkab.Helpers
{
    class TypeHelper
    {
        public static List<Type> FindDerivedTypes<T>()
        {
            var derivedType = typeof(T);
            return Assembly.GetAssembly(typeof(T))
                .GetTypes()
                .Where(t =>
                    t != derivedType &&
                    derivedType.IsAssignableFrom(t)
                    ).ToList();
        }
    }
}
