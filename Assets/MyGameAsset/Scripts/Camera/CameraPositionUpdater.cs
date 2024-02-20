using UnityEngine;

namespace CameraControl
{
    public class CameraPositionUpdater : MonoBehaviour
    {
        [SerializeField] private Transform viewPoint;
        private Camera myCamera;

        void Start()
        {
            myCamera = Camera.main;
        }

        public void UpdatePosition()
        {
            myCamera.transform.position = viewPoint.position; // ���W
            myCamera.transform.rotation = viewPoint.rotation; // ��]
        }
    }
}