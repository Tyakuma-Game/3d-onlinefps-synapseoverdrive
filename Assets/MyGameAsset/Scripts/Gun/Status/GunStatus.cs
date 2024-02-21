using ExitGames.Client.Photon.StructWrapping;
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



    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/
    // �Q�b�^�[
    //�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|/

    /// <summary>
    /// �e�̔��˃C���^�[�o������
    /// </summary>
    public float ShootInterval
    {
        get { return shootInterval; }
    }

    /// <summary>
    /// �e�̈З�
    /// </summary>
    public int ShotDamage
    {
        get { return shotDamage; }
    }

    /// <summary>
    /// �Y�[���{��
    /// </summary>
    public float AdsZoom
    {
        get { return adsZoom; }
    }

    /// <summary>
    /// �Y�[�����x
    /// </summary>
    public float AdsSpeed
    {
        get { return adsSpeed; }
    }

}