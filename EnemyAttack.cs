using System.Collections;
using System.Collections.Generic;
using Character;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<Actions>().PlayerTakeDamages(30);

        Debug.Log("trigger " + other.name);
    }


}
