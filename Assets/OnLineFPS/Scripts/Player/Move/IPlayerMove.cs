using UnityEngine;

/// <summary>
/// Player�̈ړ��Ɋւ���C���^�[�t�F�[�X
/// </summary>
public interface IPlayerMove
{
    /// <summary>
    /// ����������
    /// </summary>
    /// <param name="rigidbody">�����v�Z�p</param>
    void Init(Rigidbody rigidbody);

    /// <summary>
    /// �ړ�����
    /// </summary>
    /// <param name="direction">�ړ��̂��߂̓��͏��</param>
    /// <param name="moveSpeed">�ړ����x</param>
    void Move(Vector3 direction,float moveSpeed);
}