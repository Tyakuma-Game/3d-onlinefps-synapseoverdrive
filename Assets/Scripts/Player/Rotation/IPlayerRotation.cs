using UnityEngine;

/// <summary>
/// Player�̉�]�Ɋւ���C���^�[�t�F�[�X
/// </summary>
public interface IPlayerRotation
{
    /// <summary>
    /// Player�̉�]����
    /// </summary>
    /// <param name="rotaInput">��]�̂��߂̓��͏��</param>
    /// /// <param name="rotaSpeed">��]���x</param>
    void Rotation(Vector2 rotaInput, float rotaSpeed);
}