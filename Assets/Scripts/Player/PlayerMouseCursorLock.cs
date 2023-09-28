using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// �v���C���[�̃}�E�X�J�[�\�����b�N�Ǘ��N���X
/// </summary>
public class PlayerMouseCursorLock : MonoBehaviourPunCallbacks
{
    [Tooltip("���݃J�[�\�������b�N���Ă��邩")]
    bool isCursorLock = true;

    void Update()
    {
        //�����ȊO�̏ꍇ��
        if (!photonView.IsMine)
        {
            //�����I��
            return;
        }

        //�}�E�X�J�[�\���̃��b�N��Ԃ��X�V
        UpdateCursorLock();
    }

    /// <summary>
    /// �}�E�X�J�[�\���̃��b�N��Ԃ��X�V
    /// </summary>
    public void UpdateCursorLock()
    {
        //Control�L�[��������Ă��邩�`�F�b�N
        bool controlKeyPressed = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);

        //���b�N��ԍX�V
        if (controlKeyPressed)
        {
            isCursorLock = false;
        }
        else
        {
            isCursorLock = true;
        }

        //�\���ؑ�
        if (isCursorLock)
        {
            //�J�[�\���𒆉��ɌŒ肵��\����
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            //�J�[�\����\��
            Cursor.lockState = CursorLockMode.None;
        }
    }
}