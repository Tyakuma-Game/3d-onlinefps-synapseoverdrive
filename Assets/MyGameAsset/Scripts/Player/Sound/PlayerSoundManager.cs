using UnityEngine;

/// <summary>
/// �v���C���[�̈ړ����
/// </summary>
public enum PlayerMoveState
{
    Idol,
    Walk,
    Run
}


/// <summary>
/// �v���C���[�̏�Ԉꗗ
/// </summary>
public enum PlayerAnimationState
{
    Idol,   // �ҋ@���
    Walk,   // �������
    Run,    // ������
    Jump    // �W�����v���
}


public class PlayerSoundManager : MonoBehaviour
{
    [SerializeField] AudioSource walkSound;
    [SerializeField] AudioSource runSound;
    [SerializeField] AudioSource damegeSound;

    public void DamageSound()
    {
        damegeSound.Stop();
        damegeSound.Play();
    }

    public void SoundPlays(PlayerAnimationState playerAnimationState)
    {
        // TO DO : ����Sound�Ǘ��i��ŏC������j
        if (playerAnimationState == PlayerAnimationState.Run)
        {
            if (!runSound.isPlaying)
            {
                runSound.loop = true;
                runSound.Play();
            }
        }
        else
        {
            if (runSound.isPlaying)
            {
                runSound.loop = false;
                runSound.Stop();
            }
        }

        if (playerAnimationState == PlayerAnimationState.Walk)
        {
            if (!walkSound.isPlaying)
            {
                walkSound.loop = true;
                walkSound.Play();
            }
        }
        else
        {
            if (walkSound.isPlaying)
            {
                walkSound.loop = false;
                walkSound.Stop();
            }
        }
    }
}