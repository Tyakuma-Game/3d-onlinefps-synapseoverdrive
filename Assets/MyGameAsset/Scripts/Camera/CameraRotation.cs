using UnityEngine;
using Photon.Pun;
using UnityEngine.InputSystem;

/// <summary>
/// �J�����̉�]���Ǘ�����N���X
/// </summary>
public class CameraRotation : MonoBehaviourPunCallbacks
{
    [Header(" Settings ")]
    [SerializeField] float verticalRotationRange = 60f;
    [SerializeField] float sensitivity = 1f;

    [Header(" Elements ")]
    [SerializeField] Transform viewPoint;

    float verticalMouseInput;
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

        // �C�x���g���烁�\�b�h���폜
        lookAction.started += OnLookPerformed;
        lookAction.performed -= OnLookPerformed;
        lookAction.canceled -= OnLookCanceled;
    }

    void FixedUpdate()
    {
        // ���g�����삷��I�u�W�F�N�g�łȂ���Ώ������X�L�b�v
        if (!photonView.IsMine)
            return;

        if (rotationInput == Vector2.zero)
            return;

        // �Œ�t���[�����[�g�ŉ�]���������s
        Rotate();
    }

    /// <summary>
    /// ���͂��s��ꂽ���ɌĂ΂�鏈��
    /// </summary>
    void OnLookPerformed(InputAction.CallbackContext context)
    {
        rotationInput = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// ���̓L�����Z�����ɌĂяo������
    /// </summary>
    /// <param name="context"></param>
    void OnLookCanceled(InputAction.CallbackContext context)
    {
        rotationInput = Vector2.zero;
    }

    /// <summary>
    /// ���_��]
    /// </summary>
    void Rotate()
    {
        // ��]�v�Z
        verticalMouseInput += rotationInput.y * sensitivity;
        verticalMouseInput = Mathf.Clamp(verticalMouseInput,
                                -verticalRotationRange, verticalRotationRange);

        // �c�̎��_��]�𔽉f
        viewPoint.rotation = Quaternion.Euler(-verticalMouseInput, // -��t���Ȃ��Ə㉺���]
            viewPoint.transform.rotation.eulerAngles.y,
            viewPoint.transform.rotation.eulerAngles.z);
    }
}