using UnityEngine;

/// <summary>
/// �X�v���C����Ԃ��s���ÓI�N���X
/// </summary>
public static class HermiteSpline
{
    /// <summary>
    /// 2�̓_�Ƒ��x��񂩂��Ԓl���v�Z
    /// </summary>
    /// <param name="p1">�J�n�_�̒l</param>
    /// <param name="p2">�I���_�̒l</param>
    /// <param name="v1">�J�n�_�̑��x���</param>
    /// <param name="v2">�I���_�̑��x���</param>
    /// <param name="t">��Ԃ̐i�s�x�����i0����1�͈̔́j</param>
    /// <returns>��Ԃ��ꂽ�l</returns>
    public static float Interpolate(float p1, float p2, float v1, float v2, float t)
    {
        float a = 2f * p1 - 2f * p2 + v1 + v2;
        float b = -3f * p1 + 3f * p2 - 2f * v1 - v2;
        return t * (t * (t * a + b) + v1) + p1;
    }

    /// <summary>
    /// 2�̃x�N�g���Ƒ��x��񂩂��ԃx�N�g�����v�Z
    /// </summary>
    /// <param name="p1">�J�n�_�̃x�N�g��</param>
    /// <param name="p2">�I���_�̃x�N�g��</param>
    /// <param name="v1">�J�n�_�̑��x���x�N�g��</param>
    /// <param name="v2">�I���_�̑��x���x�N�g��</param>
    /// <param name="t">��Ԃ̐i�s�x�����i0����1�͈̔́j</param>
    public static Vector2 Interpolate(Vector2 p1, Vector2 p2, Vector2 v1, Vector2 v2, float t)
    {
        return new Vector2(
            Interpolate(p1.x, p2.x, v1.x, v2.x, t),
            Interpolate(p1.y, p2.y, v1.y, v2.y, t)
        );
    }

    /// <summary>
    /// 2�̃x�N�g���Ƒ��x��񂩂��ԃx�N�g�����v�Z
    /// </summary>
    /// <param name="p1">�J�n�_�̃x�N�g��</param>
    /// <param name="p2">�I���_�̃x�N�g��</param>
    /// <param name="v1">�J�n�_�̑��x���x�N�g��</param>
    /// <param name="v2">�I���_�̑��x���x�N�g��</param>
    /// <param name="t">��Ԃ̐i�s�x�����i0����1�͈̔́j</param>
    /// <returns>��Ԃ��ꂽ�x�N�g��</returns>
    public static Vector3 Interpolate(Vector3 p1, Vector3 p2, Vector3 v1, Vector3 v2, float t)
    {
        return new Vector3(
            Interpolate(p1.x, p2.x, v1.x, v2.x, t),
            Interpolate(p1.y, p2.y, v1.y, v2.y, t),
            Interpolate(p1.z, p2.z, v1.z, v2.z, t)
        );
    }
}