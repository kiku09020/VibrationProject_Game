using System;
using UnityEngine;

/// <summary>
/// �I�u�W�F�N�g�̃R�A�C���^�[�t�F�[�X�B
/// �R���|�[�l���g�Ƃ̃C�x���g�ʒm�ŗ��p����
/// </summary>
public abstract class ObjectCore :MonoBehaviour
{
    /// <summary>
    /// ���������̃C�x���g
    /// </summary>
    public abstract event Action OnStartEvent;

    /// <summary>
    /// �X�V���̃C�x���g
    /// </summary>
    public abstract event Action OnUpdateEvent;
}
