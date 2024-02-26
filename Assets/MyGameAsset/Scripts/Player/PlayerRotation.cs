using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Player�̉�]���Ǘ�����N���X
/// </summary>
public class PlayerRotation : MonoBehaviourPunCallbacks
{
    [Header(" Settings ")]
    [SerializeField] RotationSettings rotationSettings; //TODO�F ���t�@�N�^�����O����I

    Vector2 rotationInput = Vector2.zero;
    InputAction lookAction;

    void Start()
    {
        if (!photonView.IsMine)
            return;

        // �擾
        lookAction = InputManager.Controls.Player.Look;

        // ���\�b�h���C�x���g�ɓo�^
        lookAction.started += OnLookPerformed;
        lookAction.performed += OnLookPerformed;
        lookAction.canceled += OnLookCanceled;
    }

    void OnDestroy()
    {
        if (!photonView.IsMine)
            return;

        // �C�x���g���烁�\�b�h�̓o�^������
        lookAction.started -= OnLookPerformed;
        lookAction.performed -= OnLookPerformed;
        lookAction.canceled -= OnLookCanceled;
    }

    void FixedUpdate()
    {
        if (!photonView.IsMine)
            return;

        if (rotationInput == Vector2.zero)
            return;

        Rotate(rotationInput);
    }

    /// <summary>
    /// ���͎��ɌX���l���󂯎��
    /// </summary>
    void OnLookPerformed(InputAction.CallbackContext context)
    {
        rotationInput = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// ���͏I�����ɌĂяo��
    /// </summary>
    void OnLookCanceled(InputAction.CallbackContext context)
    {
        rotationInput = Vector2.zero;
    }

    /// <summary>
    /// Player�̉�]����
    /// </summary>
    /// <param name="rotaInput">��]�̂��߂̓��͏��</param>
    void Rotate(Vector2 rotaInput)
    {
        // �v�Z
        Vector2 rotation = new Vector2(rotaInput.x * rotationSettings.rotationSpeed, 0);

        //����]�𔽉f
        transform.rotation = Quaternion.Euler           // �I�C���[�p�Ƃ��Ă̊p�x���Ԃ����
                (transform.eulerAngles.x,
                transform.eulerAngles.y + rotation.x,   // x���̓��͂𑫂�
                transform.eulerAngles.z);
    }
}