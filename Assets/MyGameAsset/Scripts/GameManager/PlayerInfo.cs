using System;

/// <summary>
/// Player���Ǘ��N���X
/// </summary>
[Serializable]
public class PlayerInfo
{
    public string name;             //���O
    public int actor, kills, deaths;//�ԍ��A�L���A�f�X

    //���i�[
    public PlayerInfo(string _name, int _actor, int _kills, int _death)
    {
        name = _name;
        actor = _actor;
        kills = _kills;
        deaths = _death;
    }
}