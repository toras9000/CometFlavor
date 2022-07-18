using System;
using System.Linq.Expressions;
using System.Reflection;

namespace CometFlavor.Reflection;

/// <summary>
/// プロパティアクセサユーティリティ
/// </summary>
public class MemberAccessor
{
    /// <summary>プロパティのアクセスデリゲートを作成する</summary>
    /// <typeparam name="T">プロパティを持つ型</typeparam>
    /// <param name="name">プロパティ名</param>
    /// <param name="flags">プロパティ情報参照フラグ</param>
    /// <returns>プロパティへのアクセスデリゲート</returns>
    public static Func<T?, object?> CreatePropertyGetter<T>(string name, BindingFlags flags = BindingFlags.Instance | BindingFlags.Public)
    {
        // パラメータの検証
        if (name == null) throw new ArgumentNullException(nameof(name));

        // ターゲットとなるプロパティの情報を取得
        var propInfo = typeof(T).GetProperty(name, flags) ?? throw new ArgumentException("Cannot get property info");

        // プロパティ情報から作成するバージョンに
        return CreatePropertyGetter<T>(propInfo, (flags & BindingFlags.NonPublic) != 0);
    }

    /// <summary>プロパティのアクセスデリゲートを作成する</summary>
    /// <typeparam name="T">プロパティを持つ型</typeparam>
    /// <param name="propInfo">プロパティ情報</param>
    /// <param name="nonPublic">非公開プロパティを参照するか否か</param>
    /// <returns>プロパティへのアクセスデリゲート</returns>
    public static Func<T?, object?> CreatePropertyGetter<T>(PropertyInfo propInfo, bool nonPublic = false)
    {
        // パラメータの検証
        if (propInfo == null) throw new ArgumentNullException(nameof(propInfo));
        if (!typeof(T).IsAssignableFrom(propInfo.DeclaringType)) throw new InvalidOperationException("Receiver type mismatch");

        // プロパティのゲッターメソッド情報取得
        var propGetterInfo = propInfo.GetGetMethod(nonPublic) ?? throw new ArgumentException("Cannot get getter");

        // ゲッターメソッド呼び出すデリゲートを作成
        var getterType = propGetterInfo.IsStatic
                        ? typeof(Func<>).MakeGenericType(propGetterInfo.ReturnType)
                        : typeof(Func<,>).MakeGenericType(typeof(T), propGetterInfo.ReturnType);
        var getter = propGetterInfo.CreateDelegate(getterType);

        // プロパティの型が値型であるかを判定
        if (propGetterInfo.ReturnType.IsValueType)
        {
            // 値型の場合、ゲッターメソッドからobjectを返すデリゲートを直接生成出来ない。(boxingを挟む必要がある。)
            // そのため、object型を返すデリゲートにラップするジェネリックメソッドにプロパティ戻り値型をバインドして呼び出し、ラッパーデリゲートを得る。
            var genericWrapper = typeof(MemberAccessor).GetMethod(nameof(boxingWrapper), BindingFlags.Static | BindingFlags.NonPublic) ?? throw new ArgumentException("Cannot get box wrapper");
            var typedWrapper = genericWrapper.MakeGenericMethod(typeof(T), propGetterInfo.ReturnType);
            var boxingGetter = typedWrapper.Invoke(null, new object[] { getter, }) ?? throw new ArgumentException("Cannot generate wrapper delegate");
            return (Func<T?, object?>)boxingGetter;
        }

        // 対象のプロパティが静的プロパティの場合は型合わせのためにラップする。
        if (propGetterInfo.IsStatic)
        {
            var staticGetter = (Func<object?>)getter;
            return new Func<T?, object?>(o => staticGetter());
        }

        // インスタンスプロパティの場合はそのままキャストして返却
        return (Func<T?, object?>)getter;
    }

    /// <summary>プロパティを取得するデリゲートを構築する。</summary>
    /// <remarks>
    /// このメソッドによるデリゲート生成自体はかなり重い処理となる。
    /// CreatePropertyGetter と比較すると、生成されたデリゲートを少なくとも10万回以上利用するような場合でなければ、生成のオーバーヘッドのほうが大きくなりかえってパフォーマンスを落とす可能性がある。
    /// </remarks>
    /// <typeparam name="T">プロパティを持つ型</typeparam>
    /// <param name="propInfo">プロパティ情報</param>
    /// <param name="nonPublic">非公開メンバを参照するか否か</param>
    /// <returns>プロパティへのアクセスデリゲート</returns>
    public static Func<T?, object?> CompilePropertyGetter<T>(PropertyInfo propInfo, bool nonPublic = false)
    {
        // パラメータの検証
        if (propInfo == null) throw new ArgumentNullException(nameof(propInfo));
        if (!typeof(T).IsAssignableFrom(propInfo.DeclaringType)) throw new InvalidOperationException("Receiver type mismatch");

        var getMethod = propInfo.GetGetMethod(nonPublic) ?? throw new ArgumentException("Cannot get getter");

        var param = Expression.Parameter(typeof(T), "o");
        var member = Expression.Property(getMethod.IsStatic ? null : param, propInfo);
        var lambda = Expression.Lambda<Func<T?, object?>>(
            (member.Type.IsValueType) ? Expression.Convert(member, typeof(object)) : member,
            param
        );

        return lambda.Compile();
    }

    /// <summary>フィールドを取得するデリゲートを構築する。</summary>
    /// <remarks>
    /// このメソッドによるデリゲート生成自体はかなり重い処理となる。(リフレクションによる値の取得数百回分の処理時間を要する)
    /// そのため、生成されたデリゲートを少なくとも数千回以上利用するような場合でなければ、生成のオーバーヘッドのほうが大きくなりかえってパフォーマンスを落とす可能性がある。
    /// </remarks>
    /// <typeparam name="T">フィールドを持つ型</typeparam>
    /// <param name="fieldInfo">フィールド情報</param>
    /// <param name="nonPublic">非公開メンバを参照するか否か</param>
    /// <returns>フィールドへのアクセスデリゲート</returns>
    public static Func<T?, object?> CompileFieldGetter<T>(FieldInfo fieldInfo, bool nonPublic = false)
    {
        // パラメータの検証
        if (fieldInfo == null) throw new ArgumentNullException(nameof(fieldInfo));
        if (!typeof(T).IsAssignableFrom(fieldInfo.DeclaringType)) throw new InvalidOperationException("Receiver type mismatch");
        if (!nonPublic && !fieldInfo.IsPublic) throw new ArgumentException("Not public");

        var param = Expression.Parameter(typeof(T), "o");
        var member = Expression.Field(fieldInfo.IsStatic ? null : param, fieldInfo);
        var lambda = Expression.Lambda<Func<T?, object?>>(
            (member.Type.IsValueType) ? Expression.Convert(member, typeof(object)) : member,
            param
        );

        return lambda.Compile();
    }

    /// <summary>プロパティゲッターデリゲートをobject返却にラップしたデリゲートを作成する。</summary>
    /// <remarks>TResult が値型の場合に、boxingによってobjectにして返すデリゲートにラップする事を目的としている。</remarks>
    /// <typeparam name="TReceiver">レシーバ型</typeparam>
    /// <typeparam name="TResult">プロパティ戻り値型</typeparam>
    /// <param name="valueGetter">ラップ対象のゲッターデリゲート</param>
    /// <returns>ラップしたデリゲート</returns>
    private static Func<TReceiver?, object?> boxingWrapper<TReceiver, TResult>(Delegate valueGetter)
    {
        // 対象がstaticの場合は引数無しデリゲートのはずなので、その形にキャストしてラップする。
        if (valueGetter.Method.IsStatic)
        {
            var staticGetter = (Func<TResult>)valueGetter;
            return new Func<TReceiver?, object?>(o => staticGetter());
        }

        // そうでなければ普通にラップする。
        var instanceGetter = (Func<TReceiver?, TResult>)valueGetter;
        return new Func<TReceiver?, object?>(o => instanceGetter(o));
    }

}
