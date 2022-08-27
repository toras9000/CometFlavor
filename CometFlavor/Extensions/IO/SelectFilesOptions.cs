namespace CometFlavor.Extensions.IO;

#if NET5_0_OR_GREATER

/// <summary>ファイル処理オプション</summary>
/// <param name="Recurse">再帰検索を行うか否か</param>
/// <param name="Sort">ファイル名/ディレクトリ名でソートするか否か。</param>
/// <param name="Buffered">検索結果をバッファリングしてから列挙するか否か</param>
public record SelectFilesOptions(bool Recurse = true, bool Sort = true, bool Buffered = true);

#endif