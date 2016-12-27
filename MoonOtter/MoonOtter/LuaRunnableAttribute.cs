using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MoonOtter
{
    public class LuaRunnableAttribute : Attribute
  {
    public static IEnumerable<MethodBase> GetAllMethods()
    {
      return Assembly.GetExecutingAssembly().GetTypes()
        .SelectMany(type =>
        {
          if (type.GetCustomAttribute<LuaRunnableAttribute>(true) != null)
          {
            return type.GetMethods(BindingFlags.Static | BindingFlags.Public).Concat(type.GetConstructors().Cast<MethodBase>());
          }
          else
          {
            return type.GetMethods(BindingFlags.Static | BindingFlags.Public).Where(_ => _.GetCustomAttribute<LuaRunnableAttribute>() != null);
          }
        });
    }
  }
}
