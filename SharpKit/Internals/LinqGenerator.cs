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
    private static class FuncCache<T> { public static readonly MethodInfo Invoke = typeof(Func<T, bool>).GetMethod("Invoke")!; }

    /// <summary>
    /// A thread-safe cache for storing delegates associated with specific LINQ method configurations.
    /// </summary>
    /// <remarks>
    /// The cache uses a composite key consisting of the method name, overload index, target element type, and target element type.
    /// </remarks>
    private static readonly ConcurrentDictionary<(LinqMethod name, int overload, Type targetType), Delegate> _cache = [];

    /// <summary>
    /// Retrieves a delegate that represents the specified LINQ-like operation for a given type and rank.
    /// </summary>
    /// <remarks>This method dynamically generates a delegate for the specified LINQ operation using IL generation. The generated delegate is cached for reuse to improve performance.</remarks>
    /// <typeparam name="T">The type of elements in the array to be processed by the LINQ operation.</typeparam>
    /// <param name="method">The LINQ method to generate.</param>
    /// <param name="rank">The rank of the array to be processed.</param>
    /// <param name="overload">Specifies the overload of the LINQ method to generate.</param>
    /// <returns>A delegate that performs the specified LINQ operation on an array of type <typeparamref name="T"/>. The delegate's signature is equivalent to the original LINQ return.</returns>
    /// <exception cref="NotImplementedException">Thrown if the specified <paramref name="method"/> is not implemented.</exception>
    public static Delegate GetMethod<T>(LinqMethod method, int overload)
    {
        if (_cache.TryGetValue((method, overload, typeof(T)), out Delegate? cachedMethod)) return cachedMethod;

        Type returnType; Type[] parameters; Func<DynamicMethod, Delegate> emitter;

        switch ((method, overload))
        {
            case (All, 1):
                parameters = [typeof(Array), typeof(Func<T, bool>)];
                returnType = typeof(bool);
                emitter = EmitAll<T>;
                break;

            case (Any, 1): goto noGeneration;

            case (Any, 2):
                parameters = [typeof(Array), typeof(Func<T, bool>)];
                returnType = typeof(bool);
                emitter = EmitAny<T>;
                break;

            case (Count, 1): goto noGeneration;

            case (Count, 2):
                parameters = [typeof(Array), typeof(Func<T, bool>)];
                returnType = typeof(int);
                emitter = EmitCount<T>;
                break;

            noGeneration: throw new ArgumentException($"Linq method {method}_{overload} doesn't need IL generation, please use the appropriate overload.");
            default: throw new NotImplementedException($"Linq method {method}_{overload} is not implemented, or does not exist.");
        }

        Delegate linq = emitter(new($"Mx{method}_{overload}_{typeof(T).Name}", returnType, parameters, typeof(LinqGenerator).Module, true));

        _cache[(method, overload, typeof(T))] = linq;

        return linq;

    }

    #region IL Generation

    private static Delegate EmitAll<T>(DynamicMethod method)
    {
        ILGenerator generator = method.GetILGenerator();

        LocalBuilder localLen = generator.DeclareLocal(typeof(int)), localI = generator.DeclareLocal(typeof(int));

        Label labelLoop = generator.DefineLabel(), labelCheck = generator.DefineLabel(), labelFalse = generator.DefineLabel();

        #region IL

        // length = array.Length
        generator.Emit(Ldarg_0);
        generator.Emit(Callvirt, typeof(Array).GetProperty("Length")!.GetGetMethod()!);
        generator.Emit(Stloc, localLen);

        // i = 0
        generator.Emit(Ldc_I4_0);
        generator.Emit(Stloc, localI);

        // goto check
        generator.Emit(Br, labelCheck);

        // loop:
        generator.MarkLabel(labelLoop);

        // call predicate
        generator.Emit(Ldarg_1);
        generator.Emit(Dup);
        generator.Emit(Ldvirtftn, FuncCache<T>.Invoke);
        generator.Emit(Ldarg_0);
        generator.Emit(Ldloc, localI);
        generator.Emit(Ldelem_Ref);
        generator.Emit(Castclass, typeof(T));
        generator.EmitCalli(Calli, CallingConventions.HasThis, typeof(bool), [typeof(T)], null);
        generator.Emit(Brfalse, labelFalse);

        // ++i
        generator.Emit(Ldloc, localI);
        generator.Emit(Ldc_I4_1);
        generator.Emit(Add);
        generator.Emit(Stloc, localI);

        generator.MarkLabel(labelCheck);
        generator.Emit(Ldloc, localI);
        generator.Emit(Ldloc, localLen);
        generator.Emit(Blt, labelLoop);

        // return true
        generator.Emit(Ldc_I4_1);
        generator.Emit(Ret);

        // return false
        generator.MarkLabel(labelFalse);
        generator.Emit(Ldc_I4_0);
        generator.Emit(Ret);

        #endregion

        return method.CreateDelegate(typeof(Func<Array, Func<T, bool>, bool>));
    }

    private static Delegate EmitAny<T>(DynamicMethod method)
    {
        ILGenerator generator = method.GetILGenerator();

        LocalBuilder localLen = generator.DeclareLocal(typeof(int)), localI = generator.DeclareLocal(typeof(int));

        Label labelLoop = generator.DefineLabel(), labelCheck = generator.DefineLabel(), labelTrue = generator.DefineLabel();

        #region IL

        // length = array.Length
        generator.Emit(Ldarg_0);
        generator.Emit(Callvirt, typeof(Array).GetProperty("Length")!.GetGetMethod()!);
        generator.Emit(Stloc, localLen);

        // i = 0
        generator.Emit(Ldc_I4_0);
        generator.Emit(Stloc, localI);
        generator.Emit(Br, labelCheck);

        // call predicate
        generator.MarkLabel(labelLoop);
        generator.Emit(Ldarg_1);
        generator.Emit(Dup);
        generator.Emit(Ldvirtftn, FuncCache<T>.Invoke);
        generator.Emit(Ldarg_0);
        generator.Emit(Ldloc, localI);
        generator.Emit(Ldelem_Ref);
        generator.Emit(Castclass, typeof(T));
        generator.EmitCalli(Calli, CallingConventions.HasThis, typeof(bool), [typeof(T)], null);
        generator.Emit(Brtrue, labelTrue);

        // ++i
        generator.Emit(Ldloc, localI);
        generator.Emit(Ldc_I4_1);
        generator.Emit(Add);
        generator.Emit(Stloc, localI);

        generator.MarkLabel(labelCheck);
        generator.Emit(Ldloc, localI);
        generator.Emit(Ldloc, localLen);
        generator.Emit(Blt, labelLoop);

        // return false
        generator.Emit(Ldc_I4_0);
        generator.Emit(Ret);

        // return true
        generator.MarkLabel(labelTrue);
        generator.Emit(Ldc_I4_1);
        generator.Emit(Ret);

        #endregion

        return method.CreateDelegate(typeof(Func<Array, Func<T, bool>, bool>));
    }

    private static Delegate EmitCount<T>(DynamicMethod method)
    {
        ILGenerator generator = method.GetILGenerator();

        LocalBuilder localLen = generator.DeclareLocal(typeof(int)), localI = generator.DeclareLocal(typeof(int)), localCount = generator.DeclareLocal(typeof(int));

        Label labelNext = generator.DefineLabel(), labelLoop = generator.DefineLabel(), labelCheck = generator.DefineLabel();

        #region IL

        // length = array.Length
        generator.Emit(Ldarg_0);
        generator.Emit(Callvirt, typeof(Array).GetProperty("Length")!.GetGetMethod()!);
        generator.Emit(Stloc, localLen);

        // i = 0; count = 0
        generator.Emit(Ldc_I4_0);
        generator.Emit(Stloc, localI);
        generator.Emit(Ldc_I4_0);
        generator.Emit(Stloc, localCount);
        generator.Emit(Br, labelCheck);

        // call predicate
        generator.MarkLabel(labelLoop);
        generator.Emit(Ldarg_1);
        generator.Emit(Dup);
        generator.Emit(Ldvirtftn, FuncCache<T>.Invoke);
        generator.Emit(Ldarg_0);
        generator.Emit(Ldloc, localI);
        generator.Emit(Ldelem_Ref);
        generator.Emit(Castclass, typeof(T));
        generator.EmitCalli(Calli, CallingConventions.HasThis, typeof(bool), [typeof(T)], null);
        generator.Emit(Brfalse, labelNext);

        // count++
        generator.Emit(Ldloc, localCount);
        generator.Emit(Ldc_I4_1);
        generator.Emit(Add);
        generator.Emit(Stloc, localCount);

        // ++i
        generator.MarkLabel(labelNext);
        generator.Emit(Ldloc, localI);
        generator.Emit(Ldc_I4_1);
        generator.Emit(Add);
        generator.Emit(Stloc, localI);

        generator.MarkLabel(labelCheck);
        generator.Emit(Ldloc, localI);
        generator.Emit(Ldloc, localLen);
        generator.Emit(Blt, labelLoop);

        // return count
        generator.Emit(Ldloc, localCount);
        generator.Emit(Ret);

        #endregion

        return method.CreateDelegate(typeof(Func<Array, Func<T, bool>, int>));
    }

    #endregion
}

#endif