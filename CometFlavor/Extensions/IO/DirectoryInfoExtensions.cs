using System;
using System.IO;

namespace CometFlavor.Extensions.IO;

/// <summary>
/// DirectoryInfo に対する拡張メソッド
/// </summary>
public static class DirectoryInfoExtensions
{
    /// <summary>
    /// ディレクトリからの相対パス位置に対する FileInfo を取得する。
    /// </summary>
    /// <param name="self">基準となるディレクトリの DirectoryInfo</param>
    /// <param name="relativePath">基準ディレクトリからのパス。もし絶対パスの場合は基準ディレクトリは無関係にこの絶対パスが利用される。</param>
    /// <returns>対象ファイルパスの FileInfo</returns>
    public static FileInfo GetRelativeFile(this DirectoryInfo self, string relativePath)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return new FileInfo(Path.Combine(self.FullName, relativePath));
    }

    /// <summary>
    /// ディレクトリからの相対パス位置に対する DirectoryInfo を取得する。
    /// </summary>
    /// <param name="self">基準となるディレクトリのDirectoryInfo</param>
    /// <param name="relativePath">基準ディレクトリからのパス。もし絶対パスの場合は基準ディレクトリは無関係にこの絶対パスが利用される。</param>
    /// <returns>対象ディレクトリパスの DirectoryInfo</returns>
    public static DirectoryInfo GetRelativeDirectory(this DirectoryInfo self, string relativePath)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return new DirectoryInfo(Path.Combine(self.FullName, relativePath));
    }
}
