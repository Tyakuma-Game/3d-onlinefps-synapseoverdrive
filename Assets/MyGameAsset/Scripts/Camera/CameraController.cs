using UnityEngine;
using Photon.Pun;

/// <summary>
/// カメラに関する処理をまとめて管理するクラス
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

        // 位置更新
        myCamera.transform.position = viewPoint.position;
        myCamera.transform.rotation = viewPoint.rotation;
    }

    //−−−−−−−−−−−−−−−−−−−−−−−−−−−/
    //　Ray生成
    //−−−−−−−−−−−−−−−−−−−−−−−−−−−/

    // TODO:
    // この処理を別クラスに分割する！

    /// <summary>
    /// カメラから場所を指定してRayを生成
    /// </summary>
    /// <param name="generationPos">生成する座標</param>
    /// <returns>生成したRay</returns>
    public Ray GenerateRay(Vector2 generationPos)
    {
         return myCamera.ViewportPointToRay(generationPos);
    }
}