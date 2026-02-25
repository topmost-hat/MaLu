using System.Collections.Generic;
using UnityEngine;

public enum GameState { BATTLING, BATTLE_OVER }
public enum TurnTaker { PLAYER_1, PLAYER_2, ENEMY_1, ENEMY_2, ENEMY_3 }

public class BattleManager : MonoBehaviour
{
    [SerializeField] Player p1;
    [SerializeField] Player p2;
    [SerializeField] Enemy e1;
    [SerializeField] Enemy e2;
    [SerializeField] Enemy e3; // not anymore
    
    List<TurnTaker> turnOrder;
    int currentTurn;

    GameState state = GameState.BATTLING;

    void Start()
    {
        turnOrder = new() { TurnTaker.PLAYER_1, TurnTaker.PLAYER_2,
            TurnTaker.ENEMY_1, TurnTaker.ENEMY_2, TurnTaker.ENEMY_3 };
        ShuffleTurnOrder();

        InitiateTurn();
    }

    void InitiateTurn()
    {
        switch(turnOrder[currentTurn])
        {
            case TurnTaker.PLAYER_1:
                p1.TakeTurn();
                break;
            case TurnTaker.PLAYER_2:
                p2.TakeTurn();
                break;
            case TurnTaker.ENEMY_1:
                e1.TakeTurn();
                break;
            case TurnTaker.ENEMY_2:
                e2.TakeTurn();
                break;
            case TurnTaker.ENEMY_3:
                e3.TakeTurn();
                break;
        }
    }

    public void NextTurn()
    {
        if(GameState.BATTLING != state) { return; }

        ++currentTurn;
        if( turnOrder.Count <= currentTurn) { ShuffleTurnOrder(); }

        InitiateTurn();
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
        // remove from turn order
        for(int i = 0; i < turnOrder.Count; i++)
        {
            if(target == turnOrder[i])
            {
                turnOrder.RemoveAt(i);
                if(i < currentTurn) { --currentTurn; }
            }
        }

        // get rid of reference (object is about to be destroyed)
        switch(target)
        {
            case TurnTaker.PLAYER_1:
                p1 = null;
                break;
            case TurnTaker.PLAYER_2:
                p2 = null;
                break;
            case TurnTaker.ENEMY_1:
                e1 = null;
                break;
            case TurnTaker.ENEMY_2:
                e2 = null;
                break;
            case TurnTaker.ENEMY_3:
                e3 = null;
                break;
        }

        // check if player has won or lost
        if(null == e1 && null == e2 && null == e3)
        {
            state = GameState.BATTLE_OVER;
            print("All enemies defeated. You win!");
        }
        else if(null == p1 && null == p2)
        {
            state = GameState.BATTLE_OVER;
            print("All players defeated. You lose...");
        }
    }

    public Player GetRandomPlayer()
    {
        Player player = null;

        do
        {
            player = Random.Range(0, 99) % 2 == 1 ? p1 : p2;
        } while(null == player);

        return player;
    }
    public Enemy GetRandomEnemy()
    {
        Enemy enemy = null;

        do
        {
            switch(Random.Range(0, 99) % 3)
            {
                case 1:
                    enemy = e1;
                    break;
                case 2:
                    enemy = e2;
                    break;
                case 0:
                    enemy = e3;
                    break;
            }
        } while(null == enemy);

        return enemy;
    }
}
