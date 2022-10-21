using System.IO;

namespace CometFlavor.Extensions.IO;

#if NET5_0_OR_GREATER
/// <summary>
/// 列挙処理でのファイル/ディレクトリに対する処理情報
/// </summary>
/// <remarks>
/// この情報はファイル列挙処理にて、ファイルまたはディレクトリに対する処理デリゲートのパラメータとして渡される。
/// 処理デリゲートではこのオブジェクトに対してフラグを設定することで列挙動作の中断を指示することができる。
/// 列挙オプションでディレクトリに対するハンドリングが有効な場合、処理デリゲートはディレクトリに対しても呼び出される。
/// ディレクトリに対する処理デリゲートの呼び出しでは<see cref="File"/>プロパティがnullとなるため、それによってディレクトリ対象であるかを判断できる。
/// </remarks>
public interface IFileWalker
{
    /// <summary>列挙対象ディレクトリ</summary>
    DirectoryInfo Directory { get; }
    /// <summary>列挙対象ファイル。ディレクトリ対象の場合は null となる。</summary>
    FileInfo? File { get; }
    /// <summary>「現在の階層」の列挙中断を指定するフラグ。対象がファイルかディレクトリかによって動作は異なる。</summary>
    /// <remarks>
    /// ファイルに対する処理デリゲート呼び出しで中断フラグが立てられた場合、同階層の残りのファイル列挙を中断し、(存在するならば)サブディレクトリ列挙に進む。
    /// ディレクトリに対する処理デリゲート呼び出しで中断フラグが立てられた場合、そのディレクトリ配下の列挙に入らずに同一階層の次ディレクトリの列挙に進む。
    /// </remarks>
    bool Break { get; set; }
    /// <summary>列挙処理自体の終了を指定するフラグ。</summary>
    bool Exit { get; set; }
}

/// <summary>
/// 列挙処理でのファイル/ディレクトリに対する処理情報と結果設定
/// </summary>
public interface IFileConverter<TResult> : IFileWalker
{
    /// <summary>列挙対象のファイル/ディレクトリに対する処理結果を設定する。</summary>
    /// <param name="value">結果値</param>
    void SetResult(TResult value);
}
#endif
