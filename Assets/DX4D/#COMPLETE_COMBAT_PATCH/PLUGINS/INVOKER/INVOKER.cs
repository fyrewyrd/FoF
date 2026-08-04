using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public static class Invoker {

    public static void InvokeMany(Type type, object onObject, string methodPrefix, params object[] args)
    {
        foreach (MethodInfo method in FetchMethods(type, methodPrefix))
            method.Invoke(onObject, args.ToArray());
    }


    // invoke multiple functions by prefix via reflection.
    // -> works for static classes too if object = null
    // -> cache it so it's fast enough for Update calls
    // -> C# only has Tuple support in 4.6, so we use KeyValuePair instead
    static Dictionary<KeyValuePair<Type, string>, MethodInfo[]> lookup = new Dictionary<KeyValuePair<Type, string>, MethodInfo[]>();
    static MethodInfo[] FetchMethods(Type methodType, string methodPrefix)
    {
        KeyValuePair<Type, string> key = new KeyValuePair<Type, string>(methodType, methodPrefix);
        if (!lookup.ContainsKey(key))
        {
            MethodInfo[] methods = methodType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance)
                                       .Where(m => m.Name.StartsWith(methodPrefix))
                                       .ToArray();
            lookup[key] = methods;
        }
        return lookup[key];
    }
}
