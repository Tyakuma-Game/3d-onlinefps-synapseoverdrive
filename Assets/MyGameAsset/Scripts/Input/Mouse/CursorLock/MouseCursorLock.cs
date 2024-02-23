using Photon.Pun;
using UnityEngine;

/// <summary>
/// �}�E�X�̃��b�N��Ԃ��Ǘ�����N���X
/// </summary>
public class MouseCursorLock : MonoBehaviourPunCallbacks, IMouseCursorLock
{
    void Start()
    {
        // ���g�����삷��I�u�W�F�N�g�łȂ���Ώ������X�L�b�v
        if (!photonView.IsMine)
            return;

        LockScreen();
    }

    void OnDestroy()
    {
        // ���g�����삷��I�u�W�F�N�g�łȂ���Ώ������X�L�b�v
        if (!photonView.IsMine)
            return;

        UnlockScreen();
    }

    /// <summary>
    /// ���b�N��Ԃɕω�
    /// </summary>
    public void LockScreen()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    /// <summary>
    /// ���b�N��Ԃ�����
    /// </summary>
    public void UnlockScreen()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    /// <summary>
    /// ���݂̃��b�N��Ԃ��擾
    /// </summary>
    /// <returns>���݂̃��b�N���</returns>
    public bool IsLocked()
    {
        return Cursor.visible;
    }
}