using UnityEngine;

/// <summary>
/// Player�̉�]�Ɋւ���C���^�[�t�F�[�X
/// </summary>
public interface IPlayerViewPointRotation
{
    /// <summary>
    /// Player�̎��_��]����
    /// </summary>
    /// <param name="rotaInput">��]�̂��߂̓��͏��</param>
    /// /// <param name="rotaSpeed">��]���x</param>
    public void Rotation(Vector2 rotaInput, float rotaSpeed);
}