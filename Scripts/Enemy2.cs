using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GlobalState;
using Character;
namespace EnemyNameSpace
{
    public class Enemy2 : MonoBehaviour
    {
        public Animator animator;

        [SerializeField] float currentHealth;
        [SerializeField] float maxHealth = 100;
        List<GameObject> ListPrefab;
        [SerializeField] HealthBar healthBar;
        Rigidbody2D _rigidbody2d;
        BoxCollider2D boxCollider;
        [SerializeField] float enemySpeed = 4f;

        public Transform attackPoint;
        public float attackRange = 0.5f;
        public int attackDamage = 30;
        public LayerMask enemyLayers;
        private void Awake()
        {
            Debug.Log("your Point" + PlayerPrefs.GetInt("YourPoint"));
            animator = GetComponent<Animator>();
            healthBar = GetComponentInChildren<HealthBar>();
            _rigidbody2d = GetComponent<Rigidbody2D>();

            currentHealth = maxHealth;
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
        }
        public GameObject objRaycast;
        private int flipRay = 1;
        void Update()
        {
            if (IsFacingRight()) // nhan vat di chuyen ve phia ben trai
            {
                _rigidbody2d.velocity = new Vector2(enemySpeed, 0f);
                //animation here
            }
            else // nhan vat di chuyen ve phia ben phai
            {
                _rigidbody2d.velocity = new Vector2(-enemySpeed, 0f);
            }
            RaycastHit2D hit2dR = Physics2D.Raycast(objRaycast.transform.position, transform.TransformDirection(flipRay * Vector2.right), 5f);
            // Debug.DrawRay(objRaycast.transform.position, transform.TransformDirection(flipRay * Vector2.right) * hit2dR.distance);
            if (hit2dR && hit2dR.collider.CompareTag("Ground") && hit2dR.distance <= 0.5)
            {
                transform.localScale = new Vector2(-Mathf.Sign(_rigidbody2d.velocity.x), transform.localScale.y);
                flipRay *= -1;
            }
        }

        private bool IsFacingRight()
        {
            return transform.localScale.x > Mathf.Epsilon;
        }
        public void TakeDamage(float damage)
        {
            enemySpeed = 0f;
            currentHealth -= damage;
            animator.SetTrigger(EnemyState.hurt);
            healthBar.UpdateHealthBar(currentHealth, maxHealth);

            if (currentHealth <= 0)
                EnemyDie();
        }
        public void ReturnSpeedEnemy()
        {
            enemySpeed = 4f;
        }
        private void EnemyDie()
        {
            int YourPoint = PlayerPrefs.GetInt("YourPoint");
            YourPoint += 1;
            PlayerPrefs.SetInt("YourPoint", YourPoint);
            Debug.Log("your Point" + PlayerPrefs.GetInt("YourPoint"));
            animator.SetBool(EnemyState.IsDead, true);
            this.enabled = true;
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject);
            Debug.Log("Enemy died !");
        }

        private void OnDestroy()
        {
            Destroy(gameObject);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                animator.SetBool(EnemyState.KnightAttack, false);
                enemySpeed = 4f;
            }
            else
            {


                enemySpeed = 4f;
                // transform.Rotate(0f, 180, 0f);
                flipRay *= -1;
                transform.localScale = new Vector2(-Mathf.Sign(_rigidbody2d.velocity.x), transform.localScale.y);
            }
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                enemySpeed = 0;
                animator.SetBool(EnemyState.KnightAttack, true);
            }
        }

        private void EnemyAttack()
        {
            enemySpeed = 0;
            if (animator.GetBool(EnemyState.KnightAttack) == true)
            {
                Collider2D[] player = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
                foreach (Collider2D p in player)
                {
                    p.GetComponent<Actions>().PlayerTakeDamages(attackDamage);
                }
            }
        }
    }
}

