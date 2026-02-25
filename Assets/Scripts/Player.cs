using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    int health = 3;

    IEnumerator Attack()
    {
        yield return null;
    }

    IEnumerator Heal()
    {
        ++health;
        yield return new WaitForSeconds(3.0f);
    }
}
