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

	/// <summary>
	/// Coreに付属のコンポーネントを取得
	/// </summary>
	/// <typeparam name="T">コンポーネント</typeparam>
	/// <param name="checkChildren">子のコンポーネントを含めるか</param>
	/// <returns>コンポーネント</returns>
	/// <exception cref="Exception"></exception>
	public T GetCoreComponent<T>(bool checkChildren = false) 
	{
		// ゲームオブジェクトのコンポーネントを取得する
		if (GetComponent<T>() is T comp) {
			return comp;
		}

		// 子オブジェクトのコンポーネントを取得する
		if (checkChildren) {
			T childComp;

			for (int i = 0; i < transform.childCount; i++) {
				childComp = transform.GetChild(i).GetComponent<T>();        // 取得

				if (childComp != null) {        // nullじゃなければ返す
					return childComp;
				}
			}
		}

		// 例外
		throw new Exception("コンポーネントが見つかりませんでした");
	}
}
