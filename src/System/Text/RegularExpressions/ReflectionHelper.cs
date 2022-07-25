#if NETFRAMEWORK || NETSTANDARD
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IndieSystem.Text.RegularExpressions
{
    internal static class ReflectionHelper
    {
        public static MethodInfo GetMethodLiberalMatching(this Type t, string methodName, Type[] parameterTypes) 
        {
            var methods = t.GetMethods().Where(x => x.Name == methodName);

            bool AllParametersMatch(ParameterInfo[] parameterInfos)
            {
                if (parameterTypes.Length != parameterInfos.Length)
                {
                    return false;
                }
                for (int i = 0; i < parameterInfos.Length; i++)
                {
                    if (parameterInfos[i].ParameterType.Name != parameterTypes[i].Name)
                    {
                        return false;
                    }
                }

                return true;
            }

            return methods.First(x => AllParametersMatch(x.GetParameters()));
        }
    }
}
#endif