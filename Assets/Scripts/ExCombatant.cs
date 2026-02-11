using System;
using UnityEngine;

[Serializable]
public struct ExStats
{
    public int health;
    public int mana;
    public int power;
    public int defense;
    public int speed;
}

[Serializable]
public struct ExCombatantSFX
{
    public AudioClip hurt;
}

public class ExCombatant : MonoBehaviour
{
#region Variables
    [SerializeField] ExStats baseStats;
    ExStats currentStats;

    [SerializeField] ExCombatantSFX sfx;

    GameObject model;
#endregion

#region Functions
    void Awake()
    {
        currentStats = baseStats; // TODO: this is not true for player health
    }
    
    public ExStats GetCurrentStats() { return currentStats; }
#endregion
}
