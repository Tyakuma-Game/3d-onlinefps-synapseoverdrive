using UnityEngine;

// TODO:
// ��U��񂵂ŉ��o���ɓ����
// �ʂ̃N���X�Ɉȍ~����H

/// <summary>
/// �v���C���[�̉��Ɋւ��鏈�����Ǘ�����N���X
/// </summary>
public class PlayerSoundManager : MonoBehaviour
{
    // �ǉ���
    // �W�����v
    // ���n

    [SerializeField] AudioSource walkSound;
    [SerializeField] AudioSource sprintSound;
    [SerializeField] AudioSource damageSound;


    void Start()
    {
        // �����o�^
        PlayerEvent.OnDamage += OnDamage;
    }

    void OnDestroy()
    {
        // ��������
        PlayerEvent.OnDamage -= OnDamage;
    }

    /// <summary>
    /// �_���[�W���󂯂��ۂ̉�
    /// </summary>
    void OnDamage()
    {
        damageSound.Stop();
        damageSound.Play();
    }

    /// <summary>
    /// ������Ԃ̉�
    /// </summary>
    void OnWalk()
    {
        // ��~
        sprintSound.loop = false;
        sprintSound.Stop();

        // �Đ�
        walkSound.loop = true;
        walkSound.Play();
    }

    /// <summary>
    /// ������Ԃ̉�
    /// </summary>
    void OnSprint()
    {
        // ��~
        walkSound.loop = false;
        walkSound.Stop();

        // �����̍Đ�
        sprintSound.loop = true;
        sprintSound.Play();
    }
}