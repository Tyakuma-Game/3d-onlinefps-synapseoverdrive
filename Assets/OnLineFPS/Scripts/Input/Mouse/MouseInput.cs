using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �}�E�X�̓��͏������Ǘ�����N���X
/// </summary>
public class MouseInput : MonoBehaviour, IMouseInput
{
    /// <summary>
    /// �}�E�X�̈ړ����擾
    /// </summary>
    /// <returns>�}�E�X�̈ړ�</returns>
    public Vector2 GetMouseMove()
    {
        return new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
    }

    /// <summary>
    /// �Y�[��Click���s���Ă��邩�擾
    /// </summary>
    /// <returns>���</returns>
    public bool GetZoomClickStayt()
    {
        return Input.GetMouseButton(1);
    }
}