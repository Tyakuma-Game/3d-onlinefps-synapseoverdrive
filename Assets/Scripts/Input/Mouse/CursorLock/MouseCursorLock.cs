using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �}�E�X�̃��b�N��Ԃ��Ǘ�����N���X
/// </summary>
public class MouseCursorLock : MonoBehaviour, IMouseCursorLock
{
    void Start()
    {
        LockScreen();
    }

    void OnDestroy()
    {
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