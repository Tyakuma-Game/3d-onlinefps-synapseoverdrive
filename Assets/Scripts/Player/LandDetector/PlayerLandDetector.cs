using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player�̒��n������s���N���X
/// </summary>
public class PlayerLandDetector : MonoBehaviour
{
    [Tooltip("�n�ʂ��ƔF�����郌�C���[")]
    [SerializeField] LayerMask groundLayers;

    [Tooltip("���n���Ă��邩�̃t���O")]
    bool isGrounded = true;

    /// <summary>
    /// �n�ʂɒ��n���Ă��邩
    /// </summary>
    public bool IsGrounded
    {
        get { return isGrounded; }
    }

    void OnCollisionEnter(Collision collision)
    {
        // �n�ʂɐڐG���Ă��Ȃ� & �Փ˂����I�u�W�F�N�g���w�肳�ꂽ�n�ʂ̃��C���[�Ɋ܂܂�Ă��邩�`�F�b�N
        if (isGrounded == false && ((1 << collision.gameObject.layer) & groundLayers) != 0)
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // �n�ʂɐڐG���Ă��� & �Փ˂��痣�ꂽ�I�u�W�F�N�g���w�肳�ꂽ�n�ʂ̃��C���[�Ɋ܂܂�Ă��邩�`�F�b�N
        if (isGrounded == true && ((1 << collision.gameObject.layer) & groundLayers) != 0)
        {
            isGrounded = false;
        }
    }
}