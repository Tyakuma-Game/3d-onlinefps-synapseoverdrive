using System.Collections;
using UnityEngine;

/// <summary>
/// �e��Effect�Ǘ��p�N���X
/// �A�j���[�V����������Ăяo�����s��
/// </summary>
public class GunEffects : MonoBehaviour
{
    [Header(" Settings ")]
    [SerializeField] float lightDuration = 0.1f;
    [SerializeField] float particlesDuration = 1.0f;
    [SerializeField] float particlesStartDelay = 0.2f;

    [Header(" Elements ")]
    [SerializeField] Light shootingLight;
    [SerializeField] ParticleSystem shootingParticles;

    /// <summary>
    /// ���˗p���C�g��ON�ɂ��A�w�莞�Ԍ��OFF�ɂ���
    /// </summary>
    void TurnOnShootingLight()
    {
        StartCoroutine(TurnOffLightAfterDelay(lightDuration));
        shootingLight.enabled = true;
    }

    /// <summary>
    /// �w�莞�Ԍ�ɔ��˗p���C�g��OFF�ɂ���R���[�`��
    /// </summary>
    IEnumerator TurnOffLightAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        shootingLight.enabled = false;
    }

    /// <summary>
    /// �p�[�e�B�N�����Đ����A�w�莞�Ԍ�ɒ�~����
    /// </summary>
    void PlayShootingParticles()
    {
        StartCoroutine(StopParticlesAfterDelay(particlesDuration));
        shootingParticles.Simulate(particlesStartDelay); // �t���[���̊֌W��w�莞�ԃX�L�b�v
        shootingParticles.Play();
    }

    /// <summary>
    /// �w�莞�Ԍ�Ƀp�[�e�B�N�����~����R���[�`��
    /// </summary>
    IEnumerator StopParticlesAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        shootingParticles.Stop();
    }
}