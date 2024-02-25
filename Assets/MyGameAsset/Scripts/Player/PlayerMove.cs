using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;
using System;

/// <summary>
/// �v���C���[�̈ړ����Ǘ�����N���X
/// </summary>
public class PlayerMove : MonoBehaviourPunCallbacks
{
    /// <summary>
    /// �ړ����x�ύX��Callback
    /// </summary>
    public static event Action<float> OnSpeedChanged;

    [Header(" Settings ")]
    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float dashSpeed = 8f;

    Vector2 moveDirection = Vector2.zero;
    float currentSpeed;
    InputAction moveAction;
    InputAction sprintAction;

    void Start()
    {
        if (!photonView.IsMine)
            return;

        // �擾
        moveAction = InputManager.Controls.Player.Move;
        sprintAction = InputManager.Controls.Player.Sprint;

        // �����o�^
        moveAction.started += UpdateMoveDirection;
        moveAction.performed += UpdateMoveDirection;
        moveAction.canceled += UpdateMoveDirection;

        sprintAction.started += OnDash;
        sprintAction.canceled += OnWalk;

        // ������
        currentSpeed = walkSpeed;
    }

    void OnDestroy()
    {
        if (!photonView.IsMine)
            return;

        // ��������
        moveAction.started -= UpdateMoveDirection;
        moveAction.performed -= UpdateMoveDirection;
        moveAction.canceled -= UpdateMoveDirection;

        sprintAction.started -= OnDash;
        sprintAction.canceled -= OnWalk;

    }

    void FixedUpdate()
    {
        if (!photonView.IsMine)
            return;

        // �ړ����������s
        Move(moveDirection);

        // Animation�̊֌W��...
        OnSpeedChanged?.Invoke(currentSpeed * moveDirection.magnitude);
    }

    /// <summary>
    /// �ړ����x��������ɕύX����
    /// </summary>
    void OnWalk(InputAction.CallbackContext context)
    {
        currentSpeed = walkSpeed;
    }

    /// <summary>
    /// �ړ����x���_�b�V�����ɕύX����
    /// </summary>
    void OnDash(InputAction.CallbackContext context)
    {
        currentSpeed = dashSpeed;
    }

    /// <summary>
    /// ���͂Ɋ�Â��Ĉړ������X�V
    /// </summary>
    /// <param name="context">���͂̃R���e�L�X�g</param>
    void UpdateMoveDirection(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }
        

    /// <summary>
    /// �v���C���[���w�肳�ꂽ�����Ƒ��x�ňړ�������
    /// </summary>
    /// <param name="direction">�ړ�����</param>
    void Move(Vector2 direction)
    {
        if (direction == Vector2.zero)
            return;

        // �v�Z (��2�����͂Ȃ��߁A[Y��(����) = Z��(���W�ړ�)]�Ƃ��Ĉ����Ă���I)
        Vector3 movement = ((transform.forward * direction.y)
                            + (transform.right * direction.x)).normalized * currentSpeed * Time.fixedDeltaTime;

        // ���W�X�V
        transform.position += movement;
    }
}