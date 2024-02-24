using Photon.Pun;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Player�̃W�����v�Ɋւ���N���X
/// </summary>
public class PlayerJump : MonoBehaviourPunCallbacks
{
    /// <summary>
    /// �n�ʂɐڐG���Ă��邩�ǂ�����ʒm����C�x���g
    /// </summary>
    public static event Action<bool> OnGroundContactChange;

    [Header(" Settings ")]
    [SerializeField] Vector3 jumpForce;
    [SerializeField] LayerMask groundLayers;

    Rigidbody rb;
    InputAction jumpAction;
    bool isGround = true;

    void Start()
    {
        if (!photonView.IsMine)
            return;

        // �擾
        rb = GetComponent<Rigidbody>();
        jumpAction = InputManager.Controls.Player.Jump;

        // �����o�^
        jumpAction.started += OnJump;
    }

    void OnDestroy()
    {
        if (!photonView.IsMine)
            return;
        
        // ��������
        jumpAction.started -= OnJump;
    }

    void OnCollisionEnter(Collision collision)
    {
        // �n�ʂɐڐG���Ă��Ȃ� & �Փ˂����I�u�W�F�N�g���w�肳�ꂽ�n�ʂ̃��C���[�Ɋ܂܂�Ă��邩�`�F�b�N
        if (!isGround && ((1 << collision.gameObject.layer) & groundLayers) != 0)
        {
            // �ύX�ƒʒm
            isGround = true;
            OnGroundContactChange?.Invoke(isGround);
        }
    }

    /// <summary>
    /// �W�����v����
    /// </summary>
    void OnJump(InputAction.CallbackContext context)
    {
        if (isGround)
        {
            rb.AddForce(jumpForce, ForceMode.VelocityChange);

            // �ύX�ƒʒm
            isGround = false;
            OnGroundContactChange?.Invoke(isGround);
        }
    }
}