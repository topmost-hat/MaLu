using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] BattleManager manager;
    [SerializeField] TurnTaker turnTakerID;

    int health = 3;

    public void TakeTurn()
    {
        StartCoroutine(Attack(manager.GetRandomPlayer()));
    }

    IEnumerator Attack(Player target)
    {
        // TODO: charge attack

        target.Damage();
        yield return new WaitForSeconds(2.0f);
        manager.NextTurn();
    }

    public void Damage()
    {
        print("Nice! You hit " + turnTakerID + ".");
        --health;
        if(0 >= health)
        {
            print(turnTakerID + " was defeated!");
            manager.RemoveFromTurnOrder(turnTakerID);
            Destroy(gameObject);
        }
    }
}
