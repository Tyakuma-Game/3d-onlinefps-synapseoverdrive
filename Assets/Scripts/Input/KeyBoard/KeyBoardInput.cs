using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �L�[�{�[�h�̓��͏������Ǘ�����N���X
/// </summary>
public class KeyBoardInput : MonoBehaviour, IKeyBoardInput
{
    /// <summary>
    /// WASD�L�[�{���L�[�̓��͂��擾
    /// </summary>
    /// <returns>WASD�L�[�{���L�[�̓���</returns>
    public Vector3 GetWASDAndArrowKeyInput()
    {
        return new Vector3(Input.GetAxisRaw("Horizontal"),
                           0, Input.GetAxisRaw("Vertical"));
    }

    /// <summary>
    /// �W�����v�L�[�̓��͏�Ԏ擾
    /// </summary>
    /// <returns>�W�����v�L�[�̓��͏��</returns>
    public bool GetJumpKeyInput()
    {
        return Input.GetKey(KeyCode.Space);
    }

    /// <summary>
    /// �_�b�V���L�[�̓��͏�Ԏ擾
    /// </summary>
    /// <returns>�_�b�V���L�[�̓��͏��</returns>
    public bool GetRunKeyInput()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }
}