using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �J������Ray�������s���N���X
/// </summary>
public class CameraRay : MonoBehaviour,ICameraRay
{
    /// <summary>
    /// �J��������ꏊ���w�肵��Ray�𐶐�
    /// </summary>
    /// <param name="camera">��������J����</param>
    /// <param name="generationPos">����������W</param>
    /// <returns>��������Ray</returns>
    public Ray GenerateRay(Camera camera, Vector2 generationPos)
    {
        return camera.ViewportPointToRay(generationPos);
    }
}