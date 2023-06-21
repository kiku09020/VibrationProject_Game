using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// オブジェクトのコアインターフェース。
/// コンポーネントとのイベント通知で利用する
/// </summary>
public interface IObjectCore 
{
    /// <summary>
    /// 初期化時のイベント
    /// </summary>
    public event Action OnStartEvent;

    /// <summary>
    /// 更新時のイベント
    /// </summary>
    public event Action OnUpdateEvent;
}
