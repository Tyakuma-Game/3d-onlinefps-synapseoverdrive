using UnityEngine;

/// <summary>
/// �J������Ray������S������C���^�[�t�F�[�X
/// </summary>
public interface ICameraRay
{
    /// <summary>
    /// �J��������ꏊ���w�肵��Ray�𐶐�
    /// </summary>
    /// <param name="camera">��������J����</param>
    /// <param name="generationPos">����������W</param>
    /// <returns>��������Ray</returns>
    Ray GenerateRay(Camera camera, Vector2 generationPos);
}