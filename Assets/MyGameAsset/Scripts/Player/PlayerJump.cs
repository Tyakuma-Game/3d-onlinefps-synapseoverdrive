using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Player�̃W�����v�Ɋւ���N���X
/// </summary>
public class PlayerJump : MonoBehaviour
{
    /// <summary>
    /// �n�ʂƂ̐ڐG��ԕύX�ʒm�C�x���g
    /// </summary>
    public static event Action<bool> OnGroundContactChange;

    [Header(" Settings ")]
    [SerializeField] Vector3 jumpForce;
    [SerializeField] LayerMask groundLayers;

    Rigidbody rb;
    InputAction jumpAction;
    bool isGround = true;

    void Awake()
    {
        // �����o�^
        PlayerEvent.OnPlayerInstantiated += HandlePlayerInstantiated;
    }

    void OnDestroy()
    {
        // ��������
        PlayerEvent.OnPlayerInstantiated -= HandlePlayerInstantiated;
        if (jumpAction != null)
            jumpAction.started -= OnJump;
    }

    /// <summary>
    /// �v���C���[���C���X�^���X�����ꂽ�ۂɌĂ΂�鏈��
    /// </summary>
    void HandlePlayerInstantiated()
    {
        // �擾
        rb = GetComponent<Rigidbody>();
        jumpAction = InputManager.Controls.Player.Jump;

        // �o�^
        jumpAction.started += OnJump;
    }

    /// <summary>
    /// �n�ʂƂ̐ڐG������s���A�ڐG��Ԃ��ω������ꍇ�̓C�x���g��ʂ��Ēʒm
    /// </summary>
    /// <param name="collision">�Փ˂����I�u�W�F�N�g�̏��</param>
    void OnCollisionEnter(Collision collision)
    {
        // �n�ʂɐڐG���Ă��Ȃ� & �Փ˂����I�u�W�F�N�g���w�肳�ꂽ�n�ʂ̃��C���[�Ɋ܂܂�Ă��邩
        if (!isGround && ((1 << collision.gameObject.layer) & groundLayers) != 0)
        {
            isGround = true;
            OnGroundContactChange?.Invoke(isGround);
        }
    }

    /// <summary>
    /// �W�����v�̓��͂��������ۂɒn�ʂɐڐG���Ă���ꍇ�A�W�����v���������s
    /// </summary>
    /// <param name="context">���͂̃R���e�L�X�g</param>
    void OnJump(InputAction.CallbackContext context)
    {
        if (isGround)
        {
            rb.AddForce(jumpForce, ForceMode.VelocityChange);
            isGround = false;
            OnGroundContactChange?.Invoke(isGround);
        }
    }
}