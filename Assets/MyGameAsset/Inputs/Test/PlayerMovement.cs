using UnityEngine;

namespace NewInputTest
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] float moveForce = 5;
        Rigidbody _rigidbody;
        Vector2 _moveInputValue;

        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="rigidbody">�����R���|�[�l���g�擾</param>
        public void Initialize(Rigidbody rigidbody)
        {
            _rigidbody = rigidbody;
        }

        /// <summary>
        /// �ړ�����
        /// </summary>
        /// <param name="input">�ړ��̓��͒l</param>
        public void Move(Vector2 input)
        {
            _moveInputValue = input;
            Vector3 moveDirection = new Vector3(_moveInputValue.x, 0, _moveInputValue.y);
            _rigidbody.AddForce(moveDirection * moveForce * Time.deltaTime);
        }
    }
}