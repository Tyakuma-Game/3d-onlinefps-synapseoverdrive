using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Player�̉�]�N���X
/// </summary>
public class PlayerRotation : MonoBehaviourPunCallbacks
{
    [Header("Settings")]
    [SerializeField] float rotationSpeed = 100f;
    Vector2 rotationInput = Vector2.zero;
    InputAction lookAction;

    void Start()
    {
        // ���g�����삷��I�u�W�F�N�g�łȂ���Ώ������X�L�b�v
        if (!photonView.IsMine)
            return;

        lookAction = InputManager.Controls.Player.Look;

        // ���\�b�h���C�x���g�ɓo�^
        lookAction.started += OnLookPerformed;
        lookAction.performed += OnLookPerformed;
        lookAction.canceled += OnLookCanceled;
    }

    void OnDestroy()
    {
        // ���g�����삷��I�u�W�F�N�g�łȂ���Ώ������X�L�b�v
        if (!photonView.IsMine)
            return;

        // �C�x���g���烁�\�b�h�̓o�^������
        lookAction.started -= OnLookPerformed;
        lookAction.performed -= OnLookPerformed;
        lookAction.canceled -= OnLookCanceled;
    }

    void FixedUpdate()
    {
        // ���g�����삷��I�u�W�F�N�g�łȂ���Ώ������X�L�b�v
        if (!photonView.IsMine)
            return;

        if (rotationInput == Vector2.zero) return;

        Rotate(rotationInput);
    }

    void OnLookPerformed(InputAction.CallbackContext context)
    {
        rotationInput = context.ReadValue<Vector2>();
    }

    void OnLookCanceled(InputAction.CallbackContext context)
    {
        rotationInput = Vector2.zero;
    }

    /// <summary>
    /// Player�̉�]����
    /// </summary>
    /// <param name="rotaInput">��]�̂��߂̓��͏��</param>
    public void Rotate(Vector2 rotaInput)
    {
        // �v�Z
        Vector2 rotation = new Vector2(rotaInput.x * rotationSpeed, 0);

        //����]�𔽉f
        transform.rotation = Quaternion.Euler           //�I�C���[�p�Ƃ��Ă̊p�x���Ԃ����
                (transform.eulerAngles.x,
                transform.eulerAngles.y + rotation.x,   //�}�E�X��x���̓��͂𑫂�
                transform.eulerAngles.z);
    }
}