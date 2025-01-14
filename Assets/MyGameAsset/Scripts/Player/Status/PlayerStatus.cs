using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: ìè¼·
// f[^NXÍXNv^uIuWFNgÅìè¼·

/// <summary>
/// vC[ÌXe[^XlðÇ·éNX
/// </summary>
public class PlayerStatus: MonoBehaviour
{
    [Tooltip("vC[ÌèNX")]
    [SerializeField] PlayerConstants playerConstants;

    [Tooltip("HPcÊ")]
    int currentHp;

    [Tooltip("Ú®¬x")]
    float activeMoveSpeed;

    [Tooltip("WvÍ")]
    Vector3 activeJumpForth;

    
    /// <summary>
    /// Xe[^Xú»
    /// </summary>
    public void Init()
    {
        currentHp = playerConstants.MaxHP;            // ÌÍ
        activeMoveSpeed = playerConstants.WalkSpeed;  // Ú®¬x
        activeJumpForth = playerConstants.JumpForce;  // WvÍ
    }

    /// <summary>
    /// _[W
    /// </summary>
    /// <param name="damage"></param>
    public void OnDamage(int damage)
    {
        currentHp -= damage;
        if (currentHp < 0)
        {
            currentHp = 0;
        }
    }

    //|||||||||||||||||||||/
    // Qb^[
    //|||||||||||||||||||||/

    /// <summary>
    /// PLAYERÌèæ¾
    /// </summary>
    public PlayerConstants Constants
    {
        get { return playerConstants; }
    }

    /// <summary>
    /// »ÝÌHPÊ
    /// </summary>
    public int CurrentHP { get { return currentHp; } }

    /// <summary>
    /// »ÝÌÚ®¬x
    /// </summary>
    public float ActiveMoveSpeed { get { return activeMoveSpeed; } }

    /// <summary>
    /// »ÝÌWvÍ
    /// </summary>
    public Vector3 ActiveJumpForth { get { return activeJumpForth; } }
}