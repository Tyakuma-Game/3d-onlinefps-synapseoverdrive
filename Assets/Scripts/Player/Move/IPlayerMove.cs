using UnityEngine;

/// <summary>
/// Player�̈ړ��Ɋւ���C���^�[�t�F�[�X
/// </summary>
public interface IPlayerMove
{
    /// <summary>
    /// �ړ�����
    /// </summary>
    /// <param name="direction">�ړ��̂��߂̓��͏��</param>
    /// <param name="moveSpeed">�ړ����x</param>
    void Move(Vector3 direction,float moveSpeed);
}