using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �I�u�W�F�N�g�̃R�A�C���^�[�t�F�[�X�B
/// �R���|�[�l���g�Ƃ̃C�x���g�ʒm�ŗ��p����
/// </summary>
public interface IObjectCore 
{
    /// <summary>
    /// ���������̃C�x���g
    /// </summary>
    public event Action OnStartEvent;

    /// <summary>
    /// �X�V���̃C�x���g
    /// </summary>
    public event Action OnUpdateEvent;
}
