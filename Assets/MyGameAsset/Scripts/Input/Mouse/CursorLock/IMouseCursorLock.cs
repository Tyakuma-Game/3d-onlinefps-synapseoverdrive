
/// <summary>
/// �}�E�X�̃��b�N��Ԃ��Ǘ��̃C���^�[�t�F�[�X
/// </summary>
public interface IMouseCursorLock
{
    /// <summary>
    /// ���b�N��Ԃɂ���
    /// </summary>
    void LockScreen();

    /// <summary>
    /// ���b�N��Ԃ�����
    /// </summary>
    void UnlockScreen();

    /// <summary>
    /// ���݂̃��b�N��Ԃ��擾
    /// </summary>
    /// <returns>���݂̃��b�N���</returns>
    bool IsLocked();
}