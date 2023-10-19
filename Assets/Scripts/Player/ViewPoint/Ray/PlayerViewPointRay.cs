using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�̃J�������甭�˂��郌�C���Ǘ�����N���X
/// </summary>
public class PlayerViewPointRay : MonoBehaviour
{
    /// <summary>
    /// �J�����̎w�肵���ꏊ���烌�C�𔭎�
    /// </summary>
    /// <param name="camera">���C�𔭎˂���J����</param>
    /// <param name="generationPos">���˂���ꏊ</param>
    public Ray GenerateRay(Camera camera, Vector2 generationPos)
    {
        return camera.ViewportPointToRay(generationPos);
    }
}