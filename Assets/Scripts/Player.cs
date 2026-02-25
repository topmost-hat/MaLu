using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Splines;

public enum TurnAction { NONE, ATTACK, HEAL }

public class Player : MonoBehaviour
{
    [SerializeField] BattleManager manager;
    [SerializeField] TurnTaker turnTakerID;

    [SerializeField] SplineContainer jumpPath;
    int health = 5;

    bool takingTurn = false;
    TurnAction queuedAction = TurnAction.NONE;

    void Update()
    {
        if(takingTurn)
        {
            if(InputSystem.actions.FindAction("Previous").WasPressedThisFrame()) { queuedAction = TurnAction.ATTACK; }
            else if(InputSystem.actions.FindAction("Next").WasPressedThisFrame()) { queuedAction = TurnAction.HEAL; }

            switch(queuedAction)
            {
                case TurnAction.ATTACK:
                    takingTurn = false;
                    StartCoroutine(Attack(manager.GetRandomEnemy()));
                    break;
                case TurnAction.HEAL:
                    takingTurn = false;
                    StartCoroutine(Heal());
                    break;
            }
        }
    }

    public void TakeTurn()
    {
        print("It is " + turnTakerID + "'s turn.");
        takingTurn = true;
        queuedAction = TurnAction.NONE;
    }

    IEnumerator Attack(Enemy target)
    {
        // TODO: jump & ground-pound attack

        target.Damage();
        yield return new WaitForSeconds(2.0f);
        manager.NextTurn();
    }

    IEnumerator Heal()
    {
        health = 5;
        print(turnTakerID + " healed themself!");
        yield return new WaitForSeconds(2.0f);
        manager.NextTurn();
    }

    public void Damage()
    {
        --health;
        print("Ouch! " + turnTakerID + " has " + health + " health left.");
        if(0 >= health)
        {
            print(turnTakerID + " was defeated!");
            manager.RemoveFromTurnOrder(turnTakerID);
            Destroy(gameObject);
        }
    }
}
