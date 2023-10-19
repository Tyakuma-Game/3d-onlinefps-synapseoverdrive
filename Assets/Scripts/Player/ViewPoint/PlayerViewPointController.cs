using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�̎��_�������܂Ƃ߂ĊǗ�����N���X
/// </summary>
public class PlayerViewPointController : MonoBehaviour
{
    PlayerViewPointRotation playerViewPointRotation;
    PlayerViewPointRay playerViewPointRay;

    PlayerViewPointController()
    {
        playerViewPointRotation = new PlayerViewPointRotation();
        playerViewPointRay = new PlayerViewPointRay();
    }

    /// <summary>
    /// Player�̎��_��]����
    /// </summary>
    /// <param name="rotaInput">��]�̂��߂̓��͏��</param>
    /// <param name="viewPoint">���_���W</param>
    /// <param name="rotationRange">��]�͈�</param>
    /// <return>�V�������_���W</return>
    public Transform Rotation(Vector2 rotaInput, Transform viewPoint, float rotationRange)
    {
        return playerViewPointRotation.Rotation(rotaInput, viewPoint, rotationRange);
    }

    /// <summary>
    /// �J�����̒�������Ray�𐶐�
    /// </summary>
    /// <param name="camera">���C�𔭎˂���J����</param>
    public Ray GenerateRayFromCameraCenter(Camera camera)
    {
        return playerViewPointRay.GenerateRay(camera,new Vector2(.5f, .5f));
    }

    // Effect

}