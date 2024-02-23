using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

/// <summary>
/// �v���C���[�̈ړ����Ǘ�����N���X
/// </summary>
public class PlayerMove : MonoBehaviourPunCallbacks
{
    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float dashSpeed = 8f;
    
    Vector2 moveDirection = Vector2.zero;
    float currentSpeed;
    InputAction moveAction;

    void Start()
    {
        // ���g�����삷��I�u�W�F�N�g�łȂ���Ώ������X�L�b�v
        if (!photonView.IsMine)
            return;

        // InputAction�̎Q�Ǝ擾
        moveAction = InputManager.Controls.Player.Move;

        // ���̓V�X�e���̃C�x���g�Ɉړ������̍X�V���\�b�h��o�^
        moveAction.started += UpdateMoveDirection;
        moveAction.performed += UpdateMoveDirection;
        moveAction.canceled += UpdateMoveDirection;

        // �v���C���[��Event�ɒǉ�
        PlayerEvent.onWalk += OnWalk;
        PlayerEvent.onDash += OnDash;

        // �ړ����x������
        currentSpeed = walkSpeed;
    }

    void OnDestroy()
    {
        // ���g�����삷��I�u�W�F�N�g�łȂ���Ώ������X�L�b�v
        if (!photonView.IsMine)
            return;

        // �o�^�����C�x���g������
        moveAction.started -= UpdateMoveDirection;
        moveAction.performed -= UpdateMoveDirection;
        moveAction.canceled -= UpdateMoveDirection;

        // �v���C���[��Event����폜
        PlayerEvent.onWalk -= OnWalk;
        PlayerEvent.onDash -= OnDash;
    }

    void FixedUpdate()
    {
        // ���g�����삷��I�u�W�F�N�g�łȂ���Ώ������X�L�b�v
        if (!photonView.IsMine)
            return;

        // �ړ����������s
        Move(moveDirection);
    }

    /// <summary>
    /// �ړ����x��������ɕύX����
    /// </summary>
    void OnWalk() => currentSpeed = walkSpeed;

    /// <summary>
    /// �ړ����x���_�b�V�����ɕύX����
    /// </summary>
    void OnDash() => currentSpeed = dashSpeed;

    /// <summary>
    /// ���͂Ɋ�Â��Ĉړ��������X�V����
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
        // �ړ��������[���̏ꍇ�͏����X�L�b�v
        if (direction == Vector2.zero)
            return;

        // �ړ��ʌv�Z (��2�����͂Ȃ��߁A[Y��(����) = Z��(���W�ړ�)]�Ƃ��Ĉ����Ă���I)
        Vector3 movement = ((transform.forward * direction.y)
                            + (transform.right * direction.x)).normalized * currentSpeed * Time.fixedDeltaTime;

        // �v�Z�����ړ��ʂŃv���C���[�̈ʒu���X�V
        transform.position += movement;
    }
}