using UnityEngine;

/// <summary>
/// �}�E�X���͂̃C���^�t�F�[�X
/// </summary>
public interface IMouseInput
{
    /// <summary>
    /// �}�E�X�̈ړ����擾
    /// </summary>
    /// <returns>�}�E�X�̈ړ�</returns>
    Vector2 GetMouseMove();

    /// <summary>
    /// �Y�[��Click���s���Ă��邩�擾
    /// </summary>
    /// <returns>���</returns>
    bool GetZoomClickStayt();
}