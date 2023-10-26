using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �e��Status���Ǘ�����N���X
/// </summary>
public class GunStatus : MonoBehaviour
{
    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/
    // �萔
    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/
    [Header("�萔")]
    [Tooltip("�ˌ��Ԋu")]
    [SerializeField] float shootInterval = 0.1f;

    

    [Tooltip("�З�")]
    [SerializeField] int shotDamage;

    

    [Tooltip("�`�����ݎ��̃Y�[��")]
    [SerializeField] float adsZoom;

    

    [Tooltip("�`�����ݎ��̑��x")]
    [SerializeField] float adsSpeed;

    public float ShootInterval
    {
        get { return shootInterval; }
    }
    public int ShotDamage
    {
        get { return shotDamage; }
    }
    public float AdsZoom
    {
        get { return adsZoom; }
    }
    public float AdsSpeed
    {
        get { return adsSpeed; }
    }

    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/
    // �Q��
    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/
    [Header("�Q��")]
    [SerializeField] AudioSource shotSE;    //�@�e�̔��C��
    [SerializeField] Light shotLight;       //�@���C���̏e��
    [SerializeField] GameObject shotEffect; //�@���C���̃p�[�e�B�N��
    [SerializeField] GameObject hitEffect;  //�@�e�������������̃p�[�e�B�N��

    public void ActiveShotEffect()
    {
        shotEffect.SetActive(true);
    }

    public void ShotEffectNotActive()
    {
        shotEffect.SetActive(false);
    }



    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/
    // �Q�b�^�[
    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/

    /// <summary>
    /// �e�̔��C�����擾
    /// </summary>
    /// <returns>�e�̔��C��</returns>
    public AudioSource GetShotSE()
    {
        return shotSE;
    }

    /// <summary>
    /// �e���ˎ��̉��o�p���C�g���擾
    /// </summary>
    /// <returns>�e���ˎ��̉��o�p���C�g</returns>
    public Light GetShotLight()
    {
        return shotLight;
    }

    /// <summary>
    /// �e���C���̃G�t�F�N�g���擾
    /// </summary>
    /// <returns>�e���C���̃G�t�F�N�g</returns>
    public GameObject GetShotEffect()
    {
        return shotEffect;
    }

    /// <summary>
    /// �e���e����Effect���擾
    /// </summary>
    /// <returns>�e���e����Effect</returns>
    public GameObject GetHitEffect()
    {
        return hitEffect;
    }
}