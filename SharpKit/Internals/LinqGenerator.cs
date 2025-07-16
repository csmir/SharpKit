#if NET8_0_OR_GREATER
using static System.Reflection.Emit.OpCodes;
using static SharpKit.Internals.LinqMethod;
using System.Collections.Concurrent;
using System.Reflection.Emit;
using SharpKit.Internals;

namespace SharpKit.Arrays;

/// <summary>
/// Provides dynamic generation of LINQ-like operations for <see cref="Array"/> types using IL generation.
/// </summary>
/// <remarks>
/// This class supports LINQ-like methods for arrays of any type and rank, using a caching mechanism to store generated delegates for improved performance.
/// The delegates are dynamically created using <see cref="System.Reflection.Emit"/> to handle array processing efficiently.
/// </remarks>
internal static class LinqGenerator
{
    public static readonly ConcurrentDictionary<(LinqMethod, Type, int), Delegate> MethodCache = [];

    /// <summary>
    /// Retrieves a delegate that represents the specified LINQ-like operation for a given type and rank.
    /// </summary>
    /// <remarks>This method dynamically generates a delegate for the specified LINQ-like operation using IL generation. The generated delegate is cached for re-use to improve performance.</remarks>
    /// <typeparam name="T">The type of elements in the array to be processed by the LINQ-like operation.</typeparam>
    /// <param name="method">The LINQ-like method to generate.</param>
    /// <param name="rank">The rank of the array to be processed.</param>
    /// <returns>A delegate that performs the specified LINQ-like operation on an array of type <typeparamref name="T"/>. The delegate's signature is equivalent to the original LINQ return.</returns>
    /// <exception cref="NotImplementedException">Thrown if the specified <paramref name="method"/> is not implemented.</exception>
    public static Delegate GetMethod<T>(LinqMethod method, int rank, int overload = 1)
    {
        if (MethodCache.TryGetValue((method, typeof(T), rank), out Delegate? cachedMethod)) return cachedMethod;

        Type returnType; Type[] parameters;

        switch (method)
        {
            default: throw new NotImplementedException($"Linq method {method} is not implemented.");

            case All:
                parameters = [typeof(Array), typeof(Func<T, bool>)];
                returnType = typeof(bool);
                break;

            case Any:
                parameters = [typeof(Array), typeof(Func<T, bool>)];
                returnType = typeof(bool);
                break;

            case Count:
                parameters = [typeof(Array), typeof(Func<T, bool>)];
                returnType = typeof(int);
                break;
        }

        DynamicMethod generated = new($"Mx{method}_{typeof(T).Name}_{rank}D", returnType, parameters, typeof(LinqGenerator).Module, true);

        Delegate linq = method switch
        {   
              All =>   EmitAll<T>(generated),
              Any =>   EmitAny<T>(generated),
            Count => EmitCount<T>(generated),

            _ => throw new NotImplementedException($"Linq method {method} is not implemented."),
        };

        MethodCache[(method, typeof(T), rank)] = linq;

        return linq;

    }

    #region IL Generation

    private static Delegate EmitAll<T>(DynamicMethod method)
    {
        ILGenerator generator = method.GetILGenerator();

        LocalBuilder current = generator.DeclareLocal(typeof(object)), length = generator.DeclareLocal(typeof(int)), i = generator.DeclareLocal(typeof(int));

        Label jump = generator.DefineLabel(), start = generator.DefineLabel(), check = generator.DefineLabel();

        #region IL

        // totalLength = array.Length
        generator.Emit(Ldarg_0);
        generator.Emit(Callvirt, typeof(Array).GetProperty("Length")!.GetGetMethod()!);
        generator.Emit(Stloc, length);

        // flatIndex = 0
        generator.Emit(Ldc_I4_0);
        generator.Emit(Stloc, i);

        generator.Emit(Br, check);

        // loopStart:
        generator.MarkLabel(start);

        // Load reference to array and flat index
        generator.Emit(Ldarg_0);
        generator.Emit(Ldloc, i);
        generator.Emit(Ldelema, typeof(T));
        generator.Emit(Ldobj, typeof(T));
        generator.Emit(Box, typeof(T));
        generator.Emit(Stloc, current);

        // if (!predicate((T)currentValue)) return false;
        generator.Emit(Ldarg_1);
        generator.Emit(Ldloc, current);
        generator.Emit(Unbox_Any, typeof(T));
        generator.Emit(Callvirt, typeof(Func<T, bool>).GetMethod("Invoke")!);
        generator.Emit(Brfalse, jump);

        // flatIndex++
        generator.Emit(Ldloc, i);
        generator.Emit(Ldc_I4_1);
        generator.Emit(Add);
        generator.Emit(Stloc, i);

        // loopCheck:
        generator.MarkLabel(check);
        generator.Emit(Ldloc, i);
        generator.Emit(Ldloc, length);
        generator.Emit(Blt, start);

        // return true
        generator.Emit(Ldc_I4_1);
        generator.Emit(Ret);

        // return false
        generator.MarkLabel(jump);
        generator.Emit(Ldc_I4_0);
        generator.Emit(Ret);

        #endregion

        return method.CreateDelegate(typeof(Func<Array, Func<T, bool>, bool>));
    }

    private static Delegate EmitAny<T>(DynamicMethod method)
    {
        ILGenerator generator = method.GetILGenerator();

        LocalBuilder current = generator.DeclareLocal(typeof(object)), length = generator.DeclareLocal(typeof(int)), i = generator.DeclareLocal(typeof(int));

        Label jump = generator.DefineLabel(), start = generator.DefineLabel(), check = generator.DefineLabel();

        #region IL

        // totalLength = array.Length
        generator.Emit(Ldarg_0);
        generator.Emit(Callvirt, typeof(Array).GetProperty("Length")!.GetGetMethod()!);
        generator.Emit(Stloc, length);

        // flatIndex = 0
        generator.Emit(Ldc_I4_0);
        generator.Emit(Stloc, i);

        generator.Emit(Br, check);

        // loopStart:
        generator.MarkLabel(start);

        // Load reference to array and flat index
        generator.Emit(Ldarg_0);
        generator.Emit(Ldloc, i);
        generator.Emit(Ldelema, typeof(T));
        generator.Emit(Ldobj, typeof(T));
        generator.Emit(Box, typeof(T));
        generator.Emit(Stloc, current);

        // if (predicate((T)currentValue)) return true;
        generator.Emit(Ldarg_1);
        generator.Emit(Ldloc, current);
        generator.Emit(Unbox_Any, typeof(T));
        generator.Emit(Callvirt, typeof(Func<T, bool>).GetMethod("Invoke")!);
        generator.Emit(Brtrue, jump);

        // flatIndex++
        generator.Emit(Ldloc, i);
        generator.Emit(Ldc_I4_1);
        generator.Emit(Add);
        generator.Emit(Stloc, i);

        // loopCheck:
        generator.MarkLabel(check);
        generator.Emit(Ldloc, i);
        generator.Emit(Ldloc, length);
        generator.Emit(Blt, start);

        // return false
        generator.Emit(Ldc_I4_0);
        generator.Emit(Ret);

        // return true
        generator.MarkLabel(jump);
        generator.Emit(Ldc_I4_1);
        generator.Emit(Ret);

        #endregion

        return method.CreateDelegate(typeof(Func<Array, Func<T, bool>, bool>));
    }

    private static Delegate EmitCount<T>(DynamicMethod method)
    {
        ILGenerator generator = method.GetILGenerator();

        LocalBuilder current = generator.DeclareLocal(typeof(object)), length = generator.DeclareLocal(typeof(int)), i = generator.DeclareLocal(typeof(int)), count = generator.DeclareLocal(typeof(int));

        Label jump = generator.DefineLabel(), start = generator.DefineLabel(), check = generator.DefineLabel();

        #region IL

        // totalLength = array.Length
        generator.Emit(Ldarg_0);
        generator.Emit(Callvirt, typeof(Array).GetProperty("Length")!.GetGetMethod()!);
        generator.Emit(Stloc, length);

        // flatIndex = 0
        generator.Emit(Ldc_I4_0);
        generator.Emit(Stloc, i);

        generator.Emit(Br, check);

        // loopStart:
        generator.MarkLabel(start);

        // Load reference to array and flat index
        generator.Emit(Ldarg_0);
        generator.Emit(Ldloc, i);
        generator.Emit(Ldelema, typeof(T));
        generator.Emit(Ldobj, typeof(T));
        generator.Emit(Box, typeof(T));
        generator.Emit(Stloc, current);

        // if (predicate((T)currentValue)) count++
        generator.Emit(Ldarg_1);
        generator.Emit(Ldloc, current);
        generator.Emit(Unbox_Any, typeof(T));
        generator.Emit(Callvirt, typeof(Func<T, bool>).GetMethod("Invoke")!);
        generator.Emit(Brfalse, jump);
        generator.Emit(Ldloc, count);
        generator.Emit(Ldc_I4_1);
        generator.Emit(Add);
        generator.Emit(Stloc, count);

        // flatIndex++
        generator.MarkLabel(jump);
        generator.Emit(Ldloc, i);
        generator.Emit(Ldc_I4_1);
        generator.Emit(Add);
        generator.Emit(Stloc, i);

        // loopCheck:
        generator.MarkLabel(check);
        generator.Emit(Ldloc, i);
        generator.Emit(Ldloc, length);
        generator.Emit(Blt, start);

        // return count
        generator.Emit(Ldloc, count);
        generator.Emit(Ret);

        #endregion

        return method.CreateDelegate(typeof(Func<Array, Func<T, bool>, int>));

    }

    #endregion
}

#endif