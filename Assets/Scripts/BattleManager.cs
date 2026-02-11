using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
#region Variables
    List<Combatant> allCombatants;
    List<Combatant> turnQueue;
    List<CombatAction> actionQueue;
#endregion

#region Functions
    void Start()
    {
        allCombatants = FindObjectsByType<Combatant>(FindObjectsSortMode.None).ToList();
        turnQueue = allCombatants;
        CalculateTurnOrder(ref turnQueue);
    }

    // optimized bubble sort from GeeksForGeeks
    // https://www.geeksforgeeks.org/dsa/bubble-sort-algorithm/
    void CalculateTurnOrder(ref List<Combatant> combatants)
    {
        Combatant temp;
        int i, j;
        bool swapped;

        for(i = 0; i < combatants.Count - 1; i++)
        {
            swapped = false;
            for(j = 0; j < combatants.Count - i - 1; j++)
            {
                if(combatants[j].GetCurrentStats().speed > combatants[j+1].GetCurrentStats().speed)
                {
                    temp = combatants[j];
                    combatants[j] = combatants[j+1];
                    combatants[j+1] = temp;
                    swapped = true;
                }
            }

            if(!swapped) { break; } // break if inner loop did not swap any elements
        }
    }

    public void OnCombatActionQueued(CombatAction action) { actionQueue.Add(action); }

    void ExecuteCombatActions()
    {
        // TODO: execute actions in order
    }
#endregion
}
