using System;
using Photon.Pun;
using UnityEngine;

/// <summary>
/// �v���C���[�Ɋ֘A����C�x���g���Ǘ�����N���X
/// Photon�l�b�g���[�N���g�p���ăv���C���[���������ꂽ�ۂɓ���̃C�x���g���g���K�[����
/// </summary>
public class PlayerEvent : MonoBehaviour, IPunInstantiateMagicCallback
{
    /// <summary>
    /// ���[�J���v���C���[���C���X�^���X�����ꂽ�Ƃ��ɔ�������C�x���g
    /// </summary>
    public static event Action OnPlayerInstantiated;

    /// <summary>
    /// �v���C���[���_���[�W���󂯂����ɔ�������C�x���g
    /// </summary>
    public static Action OnDamage;

    /// <summary>
    /// �v���C���[���Q�[���ɃX�|�[���i�����j�������ɔ�������C�x���g
    /// </summary>
    public static Action onSpawn;

    /// <summary>
    /// �v���C���[���Q�[��������Łi�폜�j�������ɔ�������C�x���g
    /// </summary>
    public static Action onDisappear;

    /// <summary>
    /// �l�b�g���[�N��ŃI�u�W�F�N�g���������ꂽ�ۂ�Photon���玩���I�ɌĂяo����郁�\�b�h
    /// ���[�J���v���C���[�̐������Ɋ֘A�C�x���g�𔭉΂�����
    /// </summary>
    /// <param name="info">�I�u�W�F�N�g���</param>
    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        if (info.Sender.IsLocal)
            OnPlayerInstantiated?.Invoke();
    }
}