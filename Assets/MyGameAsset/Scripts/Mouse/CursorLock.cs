using UnityEngine;
using UnityEngine.InputSystem;

// TODO: �����I�ɉ��z�}�E�X����������

/// <summary>
/// �}�E�X�̃��b�N��Ԃ��Ǘ�����N���X
/// </summary>
public class CursorLock : MonoBehaviour
{
    InputAction lockAction;

    void Start()
    {
        // ��\���ɐݒ�
        LockCursor();

        // �擾
        lockAction = InputManager.Controls.Mouse.Cursorlock;

        // �����o�^
        lockAction.performed += ToggleCursorLockState;
    }

    void OnDestroy()
    {
        // �\���ɐݒ�
        UnlockCursor();

        // ��������
        lockAction.performed -= ToggleCursorLockState;
    }

    /// <summary>
    /// �}�E�X�����b�N���J�[�\����\��
    /// </summary>
    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    /// <summary>
    /// �}�E�X�̃��b�N���������J�[�\���\��
    /// </summary>
    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    /// <summary>
    /// �}�E�X�̃��b�N��Ԃ�؂�ւ�
    /// </summary>
    void ToggleCursorLockState(InputAction.CallbackContext context)
    {
        // �}�E�X�̃��b�N��Ԃ�؂�ւ�
        if (Cursor.lockState == CursorLockMode.Locked)
            UnlockCursor();     // �}�E�X���b�N����
        else
            LockCursor();       // �}�E�X���b�N
    }
}