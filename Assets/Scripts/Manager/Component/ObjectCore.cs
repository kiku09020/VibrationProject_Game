using System;
using UnityEngine;

/// <summary>
/// �I�u�W�F�N�g�̃R�A�C���^�[�t�F�[�X�B
/// �R���|�[�l���g�Ƃ̃C�x���g�ʒm�ŗ��p����
/// </summary>
public abstract class ObjectCore :MonoBehaviour
{
    /// <summary> Start�ŌĂяo�����C�x���g		</summary>
    public virtual event Action OnStartEvent;

    /// <summary> Update�ŌĂяo�����C�x���g		</summary>
    public virtual event Action OnUpdateEvent;

	/// <summary> FixedUpdate�ŌĂяo�����C�x���g </summary>
	public virtual event Action OnFixedUpdateEvent;

	/// <summary> OnDestroy�ŌĂяo�����C�x���g	</summary>
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
	/// Core�̕t���̃R���|�[�l���g���擾
	/// </summary>
	/// <typeparam name="T">�R���|�[�l���g</typeparam>
	/// <param name="checkChildren">�q�̃R���|�[�l���g���܂߂邩</param>
	/// <returns>�R���|�[�l���g</returns>
	/// <exception cref="Exception"></exception>
	public T GetCoreComponent<T>(bool checkChildren = false) 
	{
		// �Q�[���I�u�W�F�N�g�̃R���|�[�l���g���擾����
		if (GetComponent<T>() is T comp) {
			return comp;
		}

		// �q�I�u�W�F�N�g�̃R���|�[�l���g���擾����
		if (checkChildren) {
			T childComp;

			for (int i = 0; i < transform.childCount; i++) {
				childComp = transform.GetChild(i).GetComponent<T>();        // �擾

				if (childComp != null) {        // null����Ȃ���ΕԂ�
					return childComp;
				}
			}
		}

		// ��O
		throw new Exception("�R���|�[�l���g��������܂���ł���");
	}
}
