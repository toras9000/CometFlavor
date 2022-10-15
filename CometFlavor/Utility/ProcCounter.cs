using System;

namespace CometFlavor.Utility;

/// <summary>
/// カウント情報
/// </summary>
public interface ICountInfo
{
    /// <summary>合計のカウント数</summary>
    uint Total { get; set; }

    /// <summary>成功したカウント数</summary>
    uint Successful { get; set; }

    /// <summary>失敗したカウント数</summary>
    uint Failed { get; set; }

    /// <summary>不明なカウント数</summary>
    uint Unknown { get; set; }
}

/// <summary>
/// 処理数カウンタ
/// </summary>
/// <remarks>
/// このクラスはシーケンシャルに実施される処理とその成否をカウントする。
/// 単一のスレッドコンテキスト上での利用が想定されており、スレッドセーフではない。
/// 
/// また、指定の頻度で現在状態を通知するイベントを発する。
/// このイベントはカウントメソッドの呼び出しコンテキスト上での発生となる。
/// 
/// このクラスでのカウントする値には大きく2種類がある。
/// <list type="bullet">
/// <item><see cref="Entry"/>メソッドによる処理数(処理開始数)のカウント(オプショナル)</item>
/// <item><see cref="Success"/>および<see cref="Fail"/>メソッドによる結果数のカウント</item>
/// </list>
/// 
/// 基本的な考え方としては処理の開始時に処理数をカウントし、
/// 処理を完了して結果を得たら結果数をカウントする利用方法を想定している。
/// ただし、処理はシーケンシャルであるとみなされるため、
/// 結果をカウントせずに処理数をカウントすると前回の結果が不明な結果だったものとしてカウントする。
/// また、処理数のカウントは結果未確定の処理数を表現する目的のオプショナルなカウント値であり、
/// 処理数をカウントせずに結果だけをカウントすると処理数は結果数に追従する。
/// </remarks>
public class ProcCounter
{
    // 構築
    #region コンストラクタ
    /// <summary>通知閾値を指定するコンストラクタ</summary>
    /// <param name="threshold">
    /// 通知イベント(<see cref="Progress"/>)の発生閾値。
    /// 結果数がこの値に達するごとに通知を発生させる。
    /// セロを指定すると通知を発生させない。
    /// </param>
    public ProcCounter(uint threshold = 10)
    {
        this.status = new CountInfo();
        this.indicater = 0;

        this.Status = this.status;
        this.Threshold = threshold;
    }
    #endregion

    // 公開プロパティ
    #region コンストラクタ
    /// <summary>通知閾値</summary>
    public uint Threshold { get; }

    /// <summary>現在のカウント情報</summary>
    public ICountInfo Status { get; }
    #endregion

    // 公開イベント
    #region 進捗閾値
    /// <summary>進捗通知イベント</summary>
    public event Action<ICountInfo>? Progress;
    #endregion

    // 公開メソッド
    #region カウント
    /// <summary>処理対象(処理の開始)をカウントする</summary>
    /// <remarks>結果数をカウントせずに連続で呼び出した場合、前回分を不明な結果としてカウントする。</remarks>
    public void Entry()
    {
        // 前の対象数カウントに対する結果をカウントする前に再度カウントされた場合、前回分を不明な結果としてカウントする。
        if (this.status.Result < this.status.Total)
        {
            this.status.Unknown++;
            this.status.Result++;
            reportCount();
        }

        // 対象数をカウントする
        checked { this.status.Total++; }
    }

    /// <summary>処理成功をカウントする</summary>
    public void Success()
    {
        // 処理成功をカウントする
        checked { this.status.Successful++; this.status.Result++; }

        // 対象数カウント無しで結果だけカウントされた場合、対象数(総数)を追従する。
        if (this.status.Total < this.status.Result)
        {
            this.status.Total = this.status.Result;
        }

        // 必要に応じた通知処理
        reportCount();
    }

    /// <summary>処理失敗をカウントする</summary>
    public void Fail()
    {
        // 処理失敗をカウントする
        checked { this.status.Failed++; this.status.Result++; }

        // 対象数カウント無しで結果だけカウントされた場合、対象数(総数)を追従する。
        if (this.status.Total < this.status.Result)
        {
            this.status.Total = this.status.Result;
        }

        // 必要に応じた通知処理
        reportCount();
    }
    #endregion

    // 非公開型
    #region 状態管理
    /// <summary>カウント情報</summary>
    private class CountInfo : ICountInfo
    {
        /// <summary>合計のカウント数</summary>
        public uint Total { get; set; }

        /// <summary>結果のカウント数(Successful/Failed/Unknown の合計)</summary>
        public uint Result { get; set; }

        /// <summary>成功したカウント数</summary>
        public uint Successful { get; set; }

        /// <summary>失敗したカウント数</summary>
        public uint Failed { get; set; }

        /// <summary>不明なカウント数</summary>
        public uint Unknown { get; set; }
    }
    #endregion

    // 非公開フィールド
    #region 状態管理
    /// <summary>カウント情報</summary>
    private readonly CountInfo status;

    /// <summary>通知までのカウンタ</summary>
    private uint indicater;
    #endregion

    // 非公開フィールド
    #region 状態管理
    /// <summary>必要に応じてカウント情報の通知を行う</summary>
    private void reportCount()
    {
        // 通知が無効の場合は何もせず。
        if (this.Threshold <= 0) return;

        // 通知頻度カウンタをカウント
        this.indicater++;

        // 閾値に達したら通知する。
        if (this.Threshold <= this.indicater)
        {
            this.Progress?.Invoke(this.Status);
            this.indicater = 0;
        }
    }
    #endregion
}
