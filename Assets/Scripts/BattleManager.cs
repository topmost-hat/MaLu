using System.Collections.Generic;
using UnityEngine;

public enum GameState { BATTLE_START, SELECTING_ACTION, PERFORMING_ACTION, PLAYER_WINS, PLAYER_LOSES }
public enum TurnTaker { PLAYER_1, PLAYER_2, ENEMY_1, ENEMY_2, ENEMY_3 }

public class BattleManager : MonoBehaviour
{
    [SerializeField] Player p1;
    [SerializeField] Player p2;
    [SerializeField] Enemy e1;
    [SerializeField] Enemy e2;
    [SerializeField] Enemy e3; // not anymore
    
    GameState state;
    List<TurnTaker> turnOrder;
    uint currentTurn;

    void Start()
    {
        state = GameState.BATTLE_START;
        turnOrder = new() { TurnTaker.PLAYER_1, TurnTaker.PLAYER_2,
            TurnTaker.ENEMY_1, TurnTaker.ENEMY_2, TurnTaker.ENEMY_3 };
        ShuffleTurnOrder();
    }

    void NextTurn()
    {
        ++currentTurn;
        if( turnOrder.Count <= currentTurn) { ShuffleTurnOrder(); }
    }

    // Modified super simple shuffling algorithm by Smooth_P (second post)
    // https://discussions.unity.com/t/clever-way-to-shuffle-a-list-t-in-one-line-of-c-code/535113/2
    void ShuffleTurnOrder()
    {
        for(int i = 0; i < turnOrder.Count; i++)
        {
            int randomIndex = Random.Range(i, turnOrder.Count);
            (turnOrder[randomIndex], turnOrder[i]) = (turnOrder[i], turnOrder[randomIndex]);
        }

        currentTurn = 0;
    }

    public void RemoveFromTurnOrder(TurnTaker target)
    {
        for(int i = 0; i < turnOrder.Count; i++)
        {
            if(target == turnOrder[i])
            {
                turnOrder.RemoveAt(i);
                if(i < currentTurn) { --currentTurn; }
            }
        }
    }
}
