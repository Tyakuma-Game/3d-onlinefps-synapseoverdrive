using UnityEngine;

/// <summary>
/// Player�̎��_��]�Ɋւ���C���^�[�t�F�[�X
/// </summary>
public interface IPlayerViewPointRotation
{
    /// <summary>
    /// Player�̎��_��]����
    /// </summary>
    /// <param name="rotaInput">��]�̂��߂̓��͏��</param>
    /// <param name="rotaSpeed">��]���x</param>
    /// <param name="viewPoint">���_���W</param>
    /// <param name="rotationRange">��]�͈�</param>
    public Transform Rotation(Vector2 rotaInput, float rotaSpeed, Transform viewPoint, float rotationRange);
}