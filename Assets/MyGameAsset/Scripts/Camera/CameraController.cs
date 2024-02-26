using UnityEngine;
using Photon.Pun;

/// <summary>
/// �J�����Ɋւ��鏈�����܂Ƃ߂ĊǗ�����N���X
/// </summary>
public class CameraController : MonoBehaviourPunCallbacks
{
    [Header(" Elements ")]
    [SerializeField] Transform viewPoint;
    Camera myCamera;

    void Start()
    {
        if (!photonView.IsMine)
            return;

        myCamera = Camera.main;
    }

    void Update()
    { 
        if (!photonView.IsMine)
            return;

        // �ʒu�X�V
        myCamera.transform.position = viewPoint.position;
        myCamera.transform.rotation = viewPoint.rotation;
    }

    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/
    //�@Ray����
    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/

    // TODO:
    // ���̏�����ʃN���X�ɕ�������I

    /// <summary>
    /// �J��������ꏊ���w�肵��Ray�𐶐�
    /// </summary>
    /// <param name="generationPos">����������W</param>
    /// <returns>��������Ray</returns>
    public Ray GenerateRay(Vector2 generationPos)
    {
         return myCamera.ViewportPointToRay(generationPos);
    }
}