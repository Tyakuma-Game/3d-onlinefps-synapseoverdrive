using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// �v���C���[�̈ړ����Ǘ�����N���X
/// </summary>
public class PlayerMove : MonoBehaviour
{
    /// <summary>
    /// �ړ����x�ύX��Callback
    /// </summary>
    public static event Action<float> OnSpeedChanged;

    /// <summary>
    /// �ړ���ԕύX�o�^��Callback
    /// </summary>
    public static event Action<bool> OnStateChanged;

    [Header(" Settings ")]
    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float dashSpeed = 8f;

    Vector2 moveDirection = Vector2.zero;
    float currentSpeed;
    InputAction moveAction;
    InputAction sprintAction;

    void Awake()
    {
        // �����o�^
        PlayerEvent.OnPlayerInstantiated += HandlePlayerInstantiated;
    }

    void OnDestroy()
    {
        // ��������
        PlayerEvent.OnPlayerInstantiated -= HandlePlayerInstantiated;
        if (moveAction != null)
        {
            moveAction.started -= UpdateMoveDirection;
            moveAction.performed -= UpdateMoveDirection;
            moveAction.canceled -= UpdateMoveDirection;
        }

        if(sprintAction != null)
        {
            sprintAction.started -= OnDash;
            sprintAction.canceled -= OnWalk;
        }
    }

    void FixedUpdate()
    {
        // �ړ����������s
        Move(moveDirection);

        // Animation�̊֌W��...
        OnSpeedChanged?.Invoke(currentSpeed * moveDirection.magnitude);
    }

    /// <summary>
    /// �v���C���[���C���X�^���X�����ꂽ�ۂɌĂ΂�鏈��
    /// </summary>
    void HandlePlayerInstantiated()
    {
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

    /// <summary>
    /// �ړ����x��������ɕύX����
    /// </summary>
    void OnWalk(InputAction.CallbackContext context)
    {
        currentSpeed = walkSpeed;
        OnStateChanged?.Invoke(false);
    }

    /// <summary>
    /// �ړ����x���_�b�V�����ɕύX����
    /// </summary>
    void OnDash(InputAction.CallbackContext context)
    {
        currentSpeed = dashSpeed;
        OnStateChanged?.Invoke(true);
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