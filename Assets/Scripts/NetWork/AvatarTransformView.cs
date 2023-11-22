using System.Xml;
using UnityEngine;
using Photon.Pun;

///<summary>
/// ���̃v���C���[�̍��W�Ɖ�]���Ԃ���N���X
///</summary>
public class AvatarTransformView : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField, Tooltip("��Ԃɂ����鎞��")]
    float INTERPOLATION_PERIOD = 0.1f;

    [SerializeField, Tooltip("���̃v���C���[���ړ����Ă��邩�̔���l")]
    float MIN_MOVEMENT_THRESHOLD = 0.01f;

    // �o�ߎ���
    float elapsedTime;

    // ���̃v���C���[����~���Ă��邩
    bool isOtherPlayerMoving = true;

    // ��Ԃ̍��W
    Vector3 startPosition;
    Vector3 endPosition;

    // ��Ԃ̈ړ����x
    Vector3 startSpeed;
    Vector3 endSpeed;

    // ��Ԃ̉�]���
    Quaternion startRotation;
    Quaternion endRotation;

    ///<summary>
    /// ����������
    ///</summary>
    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition;
        startSpeed = Vector3.zero;
        endSpeed = startSpeed;
        startRotation = transform.rotation;
        endRotation = startRotation;
        elapsedTime = Time.deltaTime;
    }

    ///<summary>
    /// �t���[�����̍X�V����
    ///</summary>
    void Update()
    {
        if (photonView.IsMine)
        {
            // ���g�́A���t���[���̈ړ��ʂƌo�ߎ��Ԃ��L�^
            startPosition = endPosition;
            endPosition = transform.position;
            startRotation = endRotation;
            endRotation = transform.rotation;
            elapsedTime = Time.deltaTime;
        }
        else
        {
            // ���v���C���[�́A��ԏ���
            elapsedTime += Time.deltaTime;

            // ���W�ړ��̕��
            if (isOtherPlayerMoving)
            {
                if (elapsedTime < INTERPOLATION_PERIOD)
                {
                    transform.position = HermiteSpline.Interpolate(startPosition, endPosition, startSpeed, endSpeed, elapsedTime / INTERPOLATION_PERIOD);
                }
                else
                {
                    transform.position = Vector3.LerpUnclamped(startPosition, endPosition, elapsedTime / INTERPOLATION_PERIOD);
                }
            }

            // ��]�̕��
            transform.rotation = Quaternion.SlerpUnclamped(startRotation, endRotation, elapsedTime / INTERPOLATION_PERIOD);
        }
    }

    ///<summary>
    /// �l�b�g���[�N����
    ///</summary>
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // ���g�̃f�[�^���M
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);

            // ���t���[���̈ړ��ʂƌo�ߎ��Ԃ���A�b�������߂đ��M
            stream.SendNext((endPosition - startPosition) / elapsedTime);
        }
        else
        {
            // ���v���C���[�̃f�[�^��M
            var networkPosition = (Vector3)stream.ReceiveNext();
            var networkRotation = (Quaternion)stream.ReceiveNext();
            var networkVelocity = (Vector3)stream.ReceiveNext();
            var lag = Mathf.Max(0f, unchecked(PhotonNetwork.ServerTimestamp - info.SentServerTimestamp) / 1000f);

            // ���W
            startPosition = transform.position;                     // ��M���̍��W���A��Ԃ̊J�n���W�ɂ���
            endPosition = networkPosition + networkVelocity * lag;  // ���ݎ����ɂ�����\�����W���A��Ԃ̏I�����W�ɂ���

            // ���x
            startSpeed = endSpeed;                              // �O��̕�Ԃ̏I�����x���A��Ԃ̊J�n���x�ɂ���
            endSpeed = networkVelocity * INTERPOLATION_PERIOD;  // ��M�����b�����A��Ԃɂ����鎞�Ԃ�����̑��x�ɕϊ����āA��Ԃ̏I�����x�ɂ���

            // ��]
            startRotation = transform.rotation; // ��M���̉�]�����A��Ԃ̊J�n��]�ɂ���
            endRotation = networkRotation;      // ���ݎ����ɂ�����\����]�����A��Ԃ̏I����]�ɂ���

            // �o�ߎ��ԃ��Z�b�g
            elapsedTime = 0f;

            // ���̃v���C���[����~���Ă��邩�ǂ�������
            isOtherPlayerMoving = networkVelocity.magnitude > MIN_MOVEMENT_THRESHOLD;
        }
    }
}