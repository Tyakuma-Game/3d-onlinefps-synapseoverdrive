using UnityEngine;

// TODO: �ً}�Ŏ����������炱���������`�ɂȂ��Ă��܂��B
// ��قǂ�������ƃf�[�^�N���X�𐧍삵���t�@�N�^�����O���s���܂��B

/// <summary>
/// ��]���x�̎w��I�u�W�F�N�g
/// </summary>
[CreateAssetMenu(fileName = "RotationSettings", menuName = "MyFPSGameDate/RotationSettings")]
public class RotationSettings : ScriptableObject
{
    public float rotationSpeed = 1f; // �f�t�H���g�l��1f
}