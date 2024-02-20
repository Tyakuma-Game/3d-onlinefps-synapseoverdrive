using UnityEngine;

namespace CameraControl
{
    public class RayGenerator : MonoBehaviour
    {
        Camera myCamera;

        void Start()
        {
            myCamera = Camera.main;
        }

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
}