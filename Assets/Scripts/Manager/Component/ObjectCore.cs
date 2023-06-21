using System;
using UnityEngine;

/// <summary>
/// オブジェクトのコアインターフェース。
/// コンポーネントとのイベント通知で利用する
/// </summary>
public abstract class ObjectCore :MonoBehaviour
{
    /// <summary>
    /// 初期化時のイベント
    /// </summary>
    public abstract event Action OnStartEvent;

    /// <summary>
    /// 更新時のイベント
    /// </summary>
    public abstract event Action OnUpdateEvent;
}
