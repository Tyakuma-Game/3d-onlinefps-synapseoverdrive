using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �e��Status���Ǘ�����N���X
/// </summary>
public class GunStatus : MonoBehaviour
{
    [SerializeField] AudioSource shotSE;    //�@�e�̔��C��
    [SerializeField] Light shotLight;       //�@�e�̌�
    [SerializeField] GameObject shotEffect; //�@�e�������������̃p�[�e�B�N��

    public AudioSource GetShotSE()
    {
        return shotSE;
    }

    public Light GetShotLight()
    {
        return shotLight;
    }

    public void ActiveShotEffect()
    {
        shotEffect.SetActive(true);
    }

    public void ShotEffectNotActive()
    {
        shotEffect.SetActive(false);
    }
}