using System;

/// <summary>
/// ����C�x���g�Fbyte�͈����f�[�^(0 �` 255)
/// </summary>
[Serializable]
public enum EventCodes : byte
{
    NewPlayer,      //�V�����v���C���[�����}�X�^�[�ɑ��M
    ListPlayers,    //�S���Ƀv���C���[�������L
    UpdateStat,     //�L���f�X���̍X�V
}