using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;

/// <summary>
/// �v���C���[�̓ޗ�����Ǘ��N���X
/// </summary>
public class PlayerAbyssRespawner : MonoBehaviourPunCallbacks
{
    [Header("�萔")]
    [Tooltip("�ޗ�������s������")]
    [SerializeField] float PITFALL_COORDINATE = -25f;
    bool isRespawns = false;

    private void Awake()
    {
        //�����ȊO�̏ꍇ��
        if (!photonView.IsMine)
        {
            //�����I��
            return;
        }
    }


    void Update()
    {
        //�����ȊO�̏ꍇ��
        if (!photonView.IsMine)
        {
            //�����I��
            return;
        }

        //�����˔j���Ă���Ȃ�
        if (transform.position.y <= PITFALL_COORDINATE && isRespawns == false)
        {
            isRespawns = true;
            AbyssRespawn();
        }
    }


    /// <summary>
    /// Player�̓ޗ�����
    /// </summary>
    public void AbyssRespawn()
    {
        //���S�֐����Ăяo��
        SpawnManager.instance.Die();

        //���SUI���X�V
        UIManager.instance.UpdateDeathUI();
    }
}