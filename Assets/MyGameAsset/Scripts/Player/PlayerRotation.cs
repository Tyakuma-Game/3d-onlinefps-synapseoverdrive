using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Player�̉�]���Ǘ�����N���X
/// </summary>
public class PlayerRotation : MonoBehaviour
{
    [Header(" Settings ")]
    [SerializeField] RotationSettings rotationSettings; //TODO�F ���t�@�N�^�����O����I

    Vector2 rotationInput = Vector2.zero;
    InputAction lookAction;

    void Awake()
    {
        // �����o�^
        PlayerEvent.OnPlayerInstantiated += HandlePlayerInstantiated;
    }

    void OnDestroy()
    {
        // ��������
        PlayerEvent.OnPlayerInstantiated -= HandlePlayerInstantiated;
        if (lookAction != null)
        {
            lookAction.started -= OnLookPerformed;
            lookAction.performed -= OnLookPerformed;
            lookAction.canceled -= OnLookCanceled;
        }
    }

    void FixedUpdate()
    {
        if (rotationInput == Vector2.zero)
            return;

        Rotate(rotationInput);
    }

    /// <summary>
    /// �v���C���[���C���X�^���X�����ꂽ�ۂɌĂ΂�鏈��
    /// </summary>
    void HandlePlayerInstantiated()
    {
        // �擾
        lookAction = InputManager.Controls.Player.Look;

        // ���\�b�h���C�x���g�ɓo�^
        lookAction.started += OnLookPerformed;
        lookAction.performed += OnLookPerformed;
        lookAction.canceled += OnLookCanceled;
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
        Vector2 rotation = new Vector2(rotaInput.x * rotationSettings.rotationSpeed, 0f);

        //����]�𔽉f
        transform.rotation = Quaternion.Euler           // �I�C���[�p�Ƃ��Ă̊p�x���Ԃ����
                (transform.eulerAngles.x,
                transform.eulerAngles.y + rotation.x,   // x���̓��͂𑫂�
                transform.eulerAngles.z);
    }
}