using UnityEngine;

/// <summary>
/// Player�̃W�����v�C���^�[�t�F�[�X
/// </summary>
public interface IPlayerJump
{
    /// <summary>
    /// ����������
    /// </summary>
    /// <param name="rigidbody"></param>
    void Init(Rigidbody rigidbody);
    
    /// <summary>
    /// �W�����v����
    /// </summary>
    /// <param name="jumpForth">�W�����v��</param>
    void Jump(Vector3 jumpForth);
}