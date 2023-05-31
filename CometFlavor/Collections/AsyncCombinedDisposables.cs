#if NETCOREAPP3_1_OR_GREATER
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CometFlavor.Collections;

/// <summary>
/// 破棄対象リソース管理コレクション
/// </summary>
public class AsyncCombinedDisposables : AsyncCombinedDisposables<IAsyncDisposable>
{
    // 構築
    #region コンストラクタ
    /// <summary>
    /// デフォルトコンストラクタ。
    /// コレクションの逆順破棄、除去時破棄無しで初期化する。
    /// </summary>
    public AsyncCombinedDisposables() : base() { }

    /// <summary>
    /// 逆順破棄動作を指定して構築するコンストラクタ。
    /// 除去時破棄は無しで初期化する。
    /// </summary>
    /// <param name="reverse">一括破棄時に逆順で破棄するか否か</param>
    public AsyncCombinedDisposables(bool reverse) : base(reverse) { }

    /// <summary>
    /// 逆順破棄動作と除去時破棄動作を指定して構築するコンストラクタ。
    /// </summary>
    /// <param name="reverse">一括破棄時に逆順で破棄するか否か</param>
    /// <param name="removeDispose">コレクションから取り除いた要素を破棄するか否か</param>
    /// <param name="continueContext">一括破棄時にコンテキスト維持するか否か</param>
    public AsyncCombinedDisposables(bool reverse, bool removeDispose, bool continueContext = true)
         : base(reverse, removeDispose, continueContext) { }
    #endregion
}

/// <summary>
/// 破棄対象リソース管理コレクション
/// </summary>
/// <remarks>
/// 破棄予定のIAsyncDisposableオブジェクトをコレクションとして管理し、
/// コレクションをDisposeした際に管理している全ての要素に対してDisposeを呼び出す。
/// 管理コレクションの内容はDispose時にクリアされる。
/// 破棄済みのコレクションに対して要素を追加仕様とした場合、要素に対して即座にDisposeを呼び出す。
/// なお、このコレクションはスレッドセーフではない。
/// </remarks>
public class AsyncCombinedDisposables<T> : IAsyncDisposable, ICollection<T> where T : IAsyncDisposable
{
    // 構築
    #region コンストラクタ
    /// <summary>
    /// デフォルトコンストラクタ。
    /// コレクションの逆順破棄、除去時破棄無しで初期化する。
    /// </summary>
    public AsyncCombinedDisposables()
        : this(reverse: true, removeDispose: false, continueContext: true)
    { }

    /// <summary>
    /// 逆順破棄動作を指定して構築するコンストラクタ。
    /// 除去時破棄は無しで初期化する。
    /// </summary>
    /// <param name="reverse">一括破棄時に逆順で破棄するか否か</param>
    public AsyncCombinedDisposables(bool reverse)
       : this(reverse, removeDispose: false, continueContext: true)
    { }

    /// <summary>
    /// 逆順破棄動作と除去時破棄動作を指定して構築するコンストラクタ。
    /// </summary>
    /// <param name="reverse">一括破棄時に逆順で破棄するか否か</param>
    /// <param name="removeDispose">コレクションから取り除いた要素を破棄するか否か</param>
    /// <param name="continueContext">一括破棄時にコンテキスト維持するか否か</param>
    public AsyncCombinedDisposables(bool reverse, bool removeDispose, bool continueContext = true)
    {
        this.Disposables = new List<T>();
        this.ReverseDispose = reverse;
        this.DisposeOnRemove = removeDispose;
        this.ContinueuDisposeContext = continueContext;
    }
    #endregion

    // 公開プロパティ
    #region コレクション情報
    /// <summary>コレクション要素数</summary>
    public int Count => this.Disposables.Count;

    /// <summary>コレクションが読み取り専用であるか否か。常に false を返却する。</summary>
    public bool IsReadOnly => false;
    #endregion

    #region リソース管理
    /// <summary>一括破棄時に逆順で破棄するか否か</summary>
    public bool ReverseDispose { get; }

    /// <summary>コレクションから取り除いた要素を破棄するか否か</summary>
    /// <remarks><see cref="RemoveAsync"/> または <see cref="ClearAsync"/> の呼び出し時に適用される。</remarks>
    public bool DisposeOnRemove { get; }

    /// <summary>一括破棄時にコンテキスト維持するか否か</summary>
    public bool ContinueuDisposeContext { get; }
    #endregion

    #region 状態
    /// <summary>コレクションがDispose済みであるか否か</summary>
    public bool IsDisposed { get; private set; }

    /// <summary>最後に発生した例外</summary>
    /// <remarks>
    /// <see cref="DisposeOnRemove"/> が true の場合の <see cref="RemoveAsync"/>, <see cref="ClearAsync"/> や <see cref="DisposeAsync()"/> の呼び出しで要素が破棄される際、
    /// 要素のDisposeメソッドが発した例外をこのプロパティに反映する。
    /// (Disposeは本来例外を発してはならないため、このクラスでは発行された例外をプロパティに保存した後に握りつぶす。)
    /// ClearやDisposeによって複数のオブジェクトが破棄される際、複数の例外が発生した場合には AggregateException でラップする。
    /// このプロパティは破棄が行われるたびに更新される。
    /// 一度例外が発生する呼び出しを行い例外が保存された後、別の呼び出しで破棄が正常に行われた場合はプロパティ値が null に更新される。
    /// </remarks>
    public Exception? LatestException { get; private set; }
    #endregion

    // 公開メソッド
    #region 要素管理
    /// <summary>コレクションに要素を追加する。</summary>
    /// <param name="item">追加する要素</param>
    /// <exception cref="ArgumentNullException">引数がnullである場合</exception>
    public async ValueTask AddAsync(T item)
    {
        // パラメータチェック
        if (item == null) throw new ArgumentNullException(nameof(item));

        // 既にコレクションが破棄済みの場合は指定の要素をすぐに破棄する
        if (this.IsDisposed)
        {
            await item.DisposeAsync().ConfigureAwait(false);
            return;
        }

        // コレクションも追加
        this.Disposables.Add(item);
    }

    /// <summary>
    /// コレクションから要素を取り除く。
    /// <see cref="DisposeOnRemove"/> が true の場合、コレクションに登録されていたか否かに関わらず指定した要素をDisposeする。
    /// </summary>
    /// <param name="item">取り除く要素</param>
    /// <returns>要素を取り除いたか否か</returns>
    /// <exception cref="ArgumentNullException">引数がnullである場合</exception>
    public async ValueTask<bool> RemoveAsync(T item)
    {
        // パラメータチェック
        if (item == null) throw new ArgumentNullException(nameof(item));

        // コレクションから要素を取り除く
        var removed = this.Disposables.Remove(item);

        // 除去時破棄が有効であればDisposeする。
        // 取り除いて破棄することが目的なのだから、登録されていたか否かには関わらず破棄は行う。
        if (this.DisposeOnRemove)
        {
            var error = default(Exception);
            try { await item.DisposeAsync().ConfigureAwait(false); }
            catch (Exception ex) { error = ex; }
            this.LatestException = error;
        }

        return removed;
    }

    /// <summary>
    /// コレクションから全ての要素を取り除く。
    /// <see cref="DisposeOnRemove"/> が true の場合、コレクションに含まれていた全ての要素をDisposeする。
    /// このメソッドを呼び出してもコレクション自体が破棄されるわけでは無く、コレクションは継続して利用可能となる。
    /// </summary>
    public ValueTask ClearAsync()
    {
        return clearResourcesAsync(this.DisposeOnRemove);
    }
    #endregion

    #region インターフェース明示実装：要素管理(同期)
    /// <summary>このメソッドを利用するべきではない</summary>
    [Obsolete("このメソッドを利用するべきではない")]
    void ICollection<T>.Add(T item)
    {
        // パラメータチェック
        if (item == null) throw new ArgumentNullException(nameof(item));

        // 既にコレクションが破棄済みの場合は指定の要素をすぐに破棄する
        if (this.IsDisposed)
        {
            item.DisposeAsync().GetAwaiter().GetResult();
            return;
        }

        // コレクションも追加
        this.Disposables.Add(item);
    }

    /// <summary>このメソッドを利用するべきではない</summary>
    [Obsolete("このメソッドを利用するべきではない")]
    bool ICollection<T>.Remove(T item)
    {
        // パラメータチェック
        if (item == null) throw new ArgumentNullException(nameof(item));

        // コレクションから要素を取り除く
        var removed = this.Disposables.Remove(item);

        // 除去時破棄が有効であればDisposeする。
        // 取り除いて破棄することが目的なのだから、登録されていたか否かには関わらず破棄は行う。
        if (this.DisposeOnRemove)
        {
            var error = default(Exception);
            try { item.DisposeAsync().GetAwaiter().GetResult(); }
            catch (Exception ex) { error = ex; }
            this.LatestException = error;
        }

        return removed;
    }

    /// <summary>このメソッドを利用するべきではない</summary>
    [Obsolete("このメソッドを利用するべきではない")]
    void ICollection<T>.Clear()
    {
        clearResourcesAsync(this.DisposeOnRemove).GetAwaiter().GetResult();
    }
    #endregion

    #region コレクション情報
    /// <summary>
    /// コレクションに要素が含まれるかを判定する。
    /// 要素の一致判定は List{T}.Contains に準ずる。
    /// </summary>
    /// <param name="item"></param>
    /// <returns>指定の要素がコレクションに含まれているか否か</returns>
    /// <exception cref="ArgumentNullException">引数がnullである場合</exception>
    public bool Contains(T item)
    {
        // パラメータチェック
        if (item == null) throw new ArgumentNullException(nameof(item));

        // コレクションに含まれるかを判定
        return this.Disposables.Contains(item);
    }

    /// <summary>コレクション反復子を取得する。</summary>
    /// <returns>コレクション反復子</returns>
    public IEnumerator<T> GetEnumerator()
    {
        return this.Disposables.GetEnumerator();
    }

    /// <summary>コレクション反復子を取得する。</summary>
    /// <returns>コレクション反復子</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.Disposables.GetEnumerator();
    }
    #endregion

    #region 補助
    /// <summary>コレクション内容をコピーする</summary>
    /// <param name="array">コピー先配列</param>
    /// <param name="arrayIndex">コピー先配列の格納開始位置</param>
    void ICollection<T>.CopyTo(T[] array, int arrayIndex)
    {
        this.Disposables.CopyTo(array, arrayIndex);
    }
    #endregion

    #region 破棄
    /// <summary>リソースを破棄する</summary>
    public async ValueTask DisposeAsync()
    {
        await this.DisposeAsync(true).ConfigureAwait(false);
        GC.SuppressFinalize(this);
    }
    #endregion

    // 保護プロパティ
    #region リソース管理
    /// <summary>破棄予定IAsyncDisposableを管理するコレクション</summary>
    protected List<T> Disposables { get; }
    #endregion

    // 保護メソッド
    #region 破棄
    /// <summary>インスタンス及び管理要素を破棄する。</summary>
    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            await clearResourcesAsync(true).ConfigureAwait(false);
            this.IsDisposed = true;
        }
    }
    #endregion

    // 非公開メソッド
    #region 破棄
    /// <summary>管理しているコレクションをクリアする。</summary>
    /// <param name="disposeElements">要素を破棄するか否か</param>
    private async ValueTask clearResourcesAsync(bool disposeElements)
    {
        // 要素破棄が必要な場合、保持する要素を破棄
        if (disposeElements && (0 < this.Disposables.Count))
        {
            // 破棄中の例外を保持するリスト
            var errors = new List<Exception>();

            // 破棄の順序を決定
            var resources = this.Disposables.AsEnumerable();
            if (this.ReverseDispose)
            {
                resources = resources.Reverse();
            }

            // 要素をすべて破棄
            foreach (var item in resources)
            {
                try { await item.DisposeAsync().ConfigureAwait(this.ContinueuDisposeContext); }
                catch (Exception ex) { errors.Add(ex); }
            }

            // 破棄中にの例外発生状態をプロパティに反映
            switch (errors.Count)
            {
            case 0: this.LatestException = null; break;
            case 1: this.LatestException = errors[0]; break;
            default: this.LatestException = new AggregateException(errors); break;
            }
        }

        // コレクションをクリア
        this.Disposables.Clear();
    }
    #endregion
}
#endif