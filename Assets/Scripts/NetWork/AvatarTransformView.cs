using Photon.Pun;
using UnityEngine;

public class AvatarTransformView : MonoBehaviourPunCallbacks, IPunObservable
{
    // ��Ԃɂ����鎞��
    const float InterpolationPeriod = 0.1f;

    Vector3 p1;
    Vector3 p2;
    Vector3 v1;
    Vector3 v2;
    Quaternion r1;
    Quaternion r2;
    float elapsedTime;

    // ���̃v���C���[����~���Ă��邩�ǂ����������t���O
    bool isOtherPlayerMoving = true;

    void Start()
    {
        p1 = transform.position;
        p2 = p1;
        v1 = Vector3.zero;
        v2 = v1;
        r1 = transform.rotation;
        r2 = r1;
        elapsedTime = Time.deltaTime;
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            // ���g�̃l�b�g���[�N�I�u�W�F�N�g�́A���t���[���̈ړ��ʂƌo�ߎ��Ԃ��L�^����
            p1 = p2;
            p2 = transform.position;
            r1 = r2;
            r2 = transform.rotation;
            elapsedTime = Time.deltaTime;
        }
        else
        {
            // ���v���C���[�̃l�b�g���[�N�I�u�W�F�N�g�́A��ԏ������s��
            elapsedTime += Time.deltaTime;

            // ���W�ړ�
            if (isOtherPlayerMoving)
            {
                if (elapsedTime < InterpolationPeriod)
                {
                    transform.position = HermiteSpline.Interpolate(p1, p2, v1, v2, elapsedTime / InterpolationPeriod);
                }
                else
                {
                    transform.position = Vector3.LerpUnclamped(p1, p2, elapsedTime / InterpolationPeriod);
                }
            }

            // ��]����
            transform.rotation = Quaternion.SlerpUnclamped(r1, r2, elapsedTime / InterpolationPeriod);
        }
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            // ���t���[���̈ړ��ʂƌo�ߎ��Ԃ���A�b�������߂đ��M����
            stream.SendNext((p2 - p1) / elapsedTime);
        }
        else
        {
            var networkPosition = (Vector3)stream.ReceiveNext();
            var networkRotation = (Quaternion)stream.ReceiveNext();
            var networkVelocity = (Vector3)stream.ReceiveNext();
            var lag = Mathf.Max(0f, unchecked(PhotonNetwork.ServerTimestamp - info.SentServerTimestamp) / 1000f);

            // ��M���̍��W���A��Ԃ̊J�n���W�ɂ���
            p1 = transform.position;
            // ���ݎ����ɂ�����\�����W���A��Ԃ̏I�����W�ɂ���
            p2 = networkPosition + networkVelocity * lag;
            // �O��̕�Ԃ̏I�����x���A��Ԃ̊J�n���x�ɂ���
            v1 = v2;
            // ��M�����b�����A��Ԃɂ����鎞�Ԃ�����̑��x�ɕϊ����āA��Ԃ̏I�����x�ɂ���
            v2 = networkVelocity * InterpolationPeriod;
            // ��M���̉�]�����A��Ԃ̊J�n��]�ɂ���
            r1 = transform.rotation;
            // ���ݎ����ɂ�����\����]�����A��Ԃ̏I����]�ɂ���
            r2 = networkRotation;
            // �o�ߎ��Ԃ����Z�b�g����
            elapsedTime = 0f;

            // ���̃v���C���[����~���Ă��邩�ǂ����𔻒�
            isOtherPlayerMoving = networkVelocity.magnitude > 0.01f;
        }
    }
}