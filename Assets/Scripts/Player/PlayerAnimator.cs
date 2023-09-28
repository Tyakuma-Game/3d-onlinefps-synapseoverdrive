using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// �v���C���[�̃A�j���[�V�����Ǘ��N���X
/// </summary>
public class PlayerAnimator : MonoBehaviourPunCallbacks
{
    [Header("�Q��")]

    [Tooltip("�v���C���[�̈ړ��N���X")]
    [SerializeField] PlayerMovement playerMovement;

    [Tooltip("Player�̃A�j���[�^�[")]
    [SerializeField] Animator animator;


    void Update()
    {
        // �����ȊO�̏ꍇ��
        if (!photonView.IsMine)
        {
            //�����I��
            return;
        }

        //�A�j���[�V�����̏�ԍX�V
        AnimatorUpdate();
    }


    /// <summary>
    /// �A�j���[�V�����̏�ԍX�V
    /// </summary>
    void AnimatorUpdate()
    {
        //��������
        animator.SetBool("walk", playerMovement.GetMoveDir() != Vector3.zero);

        //���蔻��
        animator.SetBool("run", Input.GetKey(KeyCode.LeftShift));
    }
}