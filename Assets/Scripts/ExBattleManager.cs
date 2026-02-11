using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ExBattleState
{
    START,
    PLAYER_SELECTING,
    ENEMY_SELECTING,
    EXECUTING_ACTION,
    END
}

public class ExBattleManager : MonoBehaviour
{
#region Variables
    List<ExCombatant> allCombatants;
    List<ExCombatant> turnQueue;
    List<ExCombatAction> actionQueue;
    List<ExCombatAction> interruptActionQueue;
#endregion

#region Functions
    void Start()
    {
        allCombatants = FindObjectsByType<ExCombatant>(FindObjectsSortMode.None).ToList();
        turnQueue = allCombatants;
        CalculateTurnOrder(ref turnQueue);
    }

    // optimized bubble sort from GeeksForGeeks
    // https://www.geeksforgeeks.org/dsa/bubble-sort-algorithm/
    void CalculateTurnOrder(ref List<ExCombatant> combatants)
    {
        ExCombatant temp;
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

    public void OnCombatActionSelected(ExCombatAction action) { actionQueue.Add(action); }
    public void OnInterruptActionSelected(ExCombatAction action) { interruptActionQueue.Add(action); }

    void ExecuteCombatActions()
    {
        // TODO: check for interrupt actions, execute in order
        // TODO: execute actions in order
    }
#endregion
}
