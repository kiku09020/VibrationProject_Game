using System;
using UnityEngine;

/// <summary>
/// オブジェクトのコアインターフェース。
/// コンポーネントとのイベント通知で利用する
/// </summary>
public abstract class ObjectCore :MonoBehaviour
{
    /// <summary> Startで呼び出されるイベント		</summary>
    public virtual event Action OnStartEvent;

    /// <summary> Updateで呼び出されるイベント		</summary>
    public virtual event Action OnUpdateEvent;

	/// <summary> FixedUpdateで呼び出されるイベント </summary>
	public virtual event Action OnFixedUpdateEvent;

	/// <summary> OnDestroyで呼び出されるイベント	</summary>
	public virtual event Action OnDestroiedEvent;

	//--------------------------------------------------

	protected virtual void Start()
	{
		OnStartEvent?.Invoke();
	}

	protected virtual void Update()
	{
		OnUpdateEvent?.Invoke();
	}

	protected virtual void FixedUpdate()
	{
		OnFixedUpdateEvent?.Invoke();
	}

	protected virtual void OnDestroy()
	{
		OnDestroiedEvent?.Invoke();
	}

	//--------------------------------------------------
	/// <summary>
	/// Coreの付属のコンポーネントを取得
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
