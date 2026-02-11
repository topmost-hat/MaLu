using System;
using UnityEngine;

[Serializable]
public struct Stats
{
    public int health;
    public int mana;
    public int power;
    public int defense;
    public int speed;
}

[Serializable]
public struct CombatantSFX
{
    public AudioClip hurt;
    public AudioClip defeated;

}

public class Combatant : MonoBehaviour
{
#region Variables
    [SerializeField] Stats baseStats;
    Stats currentStats;

    [SerializeField] CombatAction[] actions;

    [SerializeField] CombatantSFX sfx;

    Character character;
#endregion

#region Functions
    void Awake()
    {
        currentStats = baseStats; // TODO: this is not true for at least player health
        // get character
    }
    
    public Stats GetCurrentStats() { return currentStats; }
#endregion
}
