using UnityEngine;

/// <summary>
/// �L�[�{�[�h���͂̃C���^�t�F�[�X
/// </summary>
public interface IKeyBoardInput
{
    /// <summary>
    /// WASD�L�[�{���L�[�̓��͂��擾
    /// </summary>
    /// <returns>WASD�L�[�{���L�[�̓���</returns>
    Vector3 GetWASDAndArrowKeyInput();

    /// <summary>
    /// �W�����v�L�[�̓��͏�Ԏ擾
    /// </summary>
    /// <returns>�W�����v�L�[�̓��͏��</returns>
    bool GetJumpKeyInput();

    /// <summary>
    /// �_�b�V���L�[�̓��͏�Ԏ擾
    /// </summary>
    /// <returns>�_�b�V���L�[�̓��͏��</returns>
    bool GetRunKeyInput();

    /// <summary>
    /// �J�[�\�����b�N�L�[�̓��͏�Ԏ擾
    /// </summary>
    /// <returns>�J�[�\�����b�N�L�[�̓��͏��</returns>
    bool GetCursorLockKeyInput();
}