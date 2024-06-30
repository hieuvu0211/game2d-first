using System.Collections;
using System.Collections.Generic;
using EnemyNameSpace;
using UnityEngine;
using GlobalState;
public class Arrow : MonoBehaviour
{
    public float moveSpeed = 20f;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        StartCoroutine(WaitAndDestroy(1f));
        rb.velocity = transform.right * moveSpeed;
    }



    private IEnumerator WaitAndDestroy(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy2 enemy = other.GetComponent<Enemy2>();
        if (enemy != null)
        {
            enemy.TakeDamage(PlayerState.BowDamage);
        }
        Debug.Log("hit " + other.name);
        Destroy(gameObject);
    }
}
