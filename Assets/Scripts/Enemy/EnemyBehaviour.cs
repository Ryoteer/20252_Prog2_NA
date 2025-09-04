using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour, IDamage
{
    public void TakeDamage()
    {
        Debug.Log($"<color=yellow>DIO</color>: WRYYYY!");
    }
}
