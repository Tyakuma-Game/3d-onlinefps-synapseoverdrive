using Photon.Pun;
using System;
using UnityEngine;

/// <summary>
/// ��蒼���I
/// </summary>
public class CameraRay : MonoBehaviourPunCallbacks
{
    /// <summary>
    /// Ray���܂ރJ�X�^���C�x���g����
    /// </summary>
    public class RayEventArgs : EventArgs
    {
        public Ray Ray { get; set; }
    }

    /// <summary>
    /// Ray�����C�x���g
    /// </summary>
    public event EventHandler<RayEventArgs> OnRayCreated;

    Camera myCamera;

    void Start()
    {
        myCamera = Camera.main;
    }

    public void CreateRay(Vector2 screenPoint)
    {
        Ray ray = myCamera.ScreenPointToRay(screenPoint);

        // �C�x���g������Ray���Z�b�g���ăC�x���g�𔭉�
        OnRayCreated?.Invoke(this, new RayEventArgs { Ray = ray });
    }
}
