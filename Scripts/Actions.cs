using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.SceneManagement;
using GlobalState;
using UnityEngine.UI;

namespace Character
{
    public class Actions : MonoBehaviour
    {
        [SerializeField] private float Speed = 5f;
        // [SerializeField] private const float Jump = 6f;
        private bool isFacingRight = true;
        [SerializeField] public bool doubleJump;
        private Vector2 _flip;
        private Rigidbody2D _rb;
        private Animator _animator;

        private float attackRate = 3f;
        private float nextTimeAttack = 0f;

        public Transform attackPoint;
        public float attackRange = 1f;

        public LayerMask enemyLayers;

        private bool isRunning = false;

        public bool _isFacingRight = true;

        private bool isCrouching = false;
        private bool isJumping = false;

        [SerializeField] HealthBarCharacter healthBar;
        public int currentHealPlayer;

        public GameObject objRaycast;

        private void Awake()
        {
            // GameManager.HP = PlayerState.maxHealthPlayer;
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }
        private void Start()
        {
            // PlayerPrefs.SetInt("currentHeal", 1000);
            healthBar.UpdateHealthBar(PlayerPrefs.GetInt("currentHeal"), PlayerState.maxHealthPlayer);
            currentHealPlayer = PlayerPrefs.GetInt("currentHeal");
            Debug.Log("start health = " + currentHealPlayer);
            Debug.Log("scene = " + PlayerPrefs.GetInt("currentScene"));
        }


        private void Update()
        {


            PlayerRun();
            HandleInputAction();
            _animator.SetFloat(PlayerState.YVelocity, _rb.velocity.y);
            RaycastHit2D hit2dY = Physics2D.Raycast(objRaycast.transform.position, -Vector2.up);
            Debug.DrawRay(objRaycast.transform.position, -Vector2.up * hit2dY.distance, Color.red);
            if (hit2dY && hit2dY.distance >= 0.3)
                doubleJump = false;
            else
                doubleJump = true;
        }
        private void FixedUpdate()
        {

        }
        public void HandleInputAction()
        {
            //hàm xử lý thay đổi tốc độ nhân vật để xét chạy hay đi bộ
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                isRunning = !isRunning;
                switch (isRunning)
                {
                    case true: //nâng speed để chuyển sang trạng thái Run
                        Speed = 10f;
                        break;
                    case false: //chuyển về mặc định là đi bộ
                        Speed = 5f;
                        break;
                }
            }


            //nhân vật nhảy
            if (Input.GetButtonDown("Vertical") && doubleJump)
            {
                _rb.AddForce(new Vector2(0f, PlayerState.NomalJump), ForceMode2D.Impulse);
                _animator.SetTrigger("JumpAction");
            }
            if (Input.GetKey(KeyCode.Space))
            {
                if (!isCrouching && !isJumping)
                {
                    Crouch();
                }
            }
            else if (isCrouching)
            {
                Uncrouch();
            }

            // Xử lý khi thả space
            if (Input.GetKeyUp(KeyCode.Space) && !isJumping)
            {
                PowerJump();
            }
            if (Input.GetButtonDown("Fire3")) //Player using bow
            {

                _animator.SetTrigger(PlayerState.attack4);
                // nextTimeAttack = Time.time + 2f / attackRate;

            }
            if (Time.time >= nextTimeAttack)
            {
                if (Input.GetButtonDown("Fire1")) //Player using combo sword attack
                {
                    Speed = 0f;
                    _animator.SetTrigger(PlayerState.attack2);
                    nextTimeAttack = Time.time + 2f / attackRate;
                }
                if (Input.GetButtonDown("Fire2")) //Player using combo sword attack
                {
                    Speed = 0f;
                    _animator.SetTrigger(PlayerState.attack);
                    nextTimeAttack = Time.time + 2f / attackRate;
                }

                // if (Input.GetKey(KeyCode.K))
                // {
                //     Speed = 0f;
                //     _animator.SetTrigger(PlayerState.attack2);
                //     StartCoroutine(WaitAndAttack(0.3f, PlayerState.kickAttackDamage));
                //     //PlayerAttack(PlayerState.kickAttackDamage);
                //     nextTimeAttack = Time.time + 2f / attackRate;
                // }
            }
            if (Input.GetButtonDown("Cancel"))
            {
                SceneManager.LoadSceneAsync("PauseGame");
            }
        }

        public void ReturnSpeed()
        {
            Speed = 5f;
        }
        //hàm xử lý chờ sau 1 thời gian thực hiện animation thì mới gây sát thương

        public void PlayerAttack(float Damage)
        {
            //detect enemies in range of attack
            Collider2D[] hitenemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hitenemies)
            {
                Debug.Log("hit enemy " + enemy.name);
                enemy.GetComponent<EnemyNameSpace.Enemy2>().TakeDamage(Damage);
            }
        }

        private void Crouch()
        {
            isCrouching = true;
            // Thực hiện các thay đổi để nhân vật crouch
            // Ví dụ: Giảm chiều cao của nhân vật
            // transform.localScale = new Vector3(transform.localScale.x, 0.5f, transform.localScale.z);
            Speed = 3.5f;
            _animator.SetBool(PlayerState.Crouch, true);
        }
        private void Uncrouch()
        {
            isCrouching = false;
            // Thực hiện các thay đổi để nhân vật ngừng crouch và trở lại trạng thái bình thường
            // Ví dụ: Tăng chiều cao của nhân vật
            // transform.localScale = new Vector3(transform.localScale.x, 1f, transform.localScale.z);
            Speed = 5f;
            _animator.SetBool(PlayerState.Crouch, false);
        }
        private void PowerJump()
        {
            isJumping = true;
            // _animator.SetTrigger("JumpAction");
            _rb.AddForce(new Vector2(0f, PlayerState.powerJump), ForceMode2D.Impulse);
        }
        private void PlayerRun()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            //hàm xử lý nhân vật chạy ngang dọc

            RaycastHit2D hit2dR = Physics2D.Raycast(objRaycast.transform.position, transform.TransformDirection(Vector2.right));
            // Debug.DrawRay(objRaycast.transform.position, transform.TransformDirection(Vector2.right) * hit2dR.distance, Color.red);
            if (hit2dR)
            {
                if (hit2dR.distance <= 0.2 && hit2dR.collider.CompareTag("Ground"))
                {
                    _rb.velocity = new Vector2(0, _rb.velocity.y);
                }
                else _rb.velocity = new Vector2(horizontalInput * Speed, _rb.velocity.y);
            }


            //player đang từ đứng yên chuyển sang chạy
            if (horizontalInput != 0 && Math.Abs(Speed - 10) == 0)
            {
                _animator.SetBool(PlayerState.OnRun, true);
                _animator.SetBool(PlayerState.OnWalk, false);
            }
            //player từ đứng yên chuyển sang đi bộ
            if (horizontalInput != 0 && Math.Abs(Speed - 5) == 0)
            {
                _animator.SetBool(PlayerState.OnRun, false);
                _animator.SetBool(PlayerState.OnWalk, true);
            }

            //về trạng thái đứng yên nếu không di chuyển gì cả
            if (horizontalInput == 0)
            {
                _animator.SetBool(PlayerState.OnRun, false);
                _animator.SetBool(PlayerState.OnWalk, false);
            }




            //xử lý animation khi nhân vật quay phải hoặc quay trái
            if (isFacingRight && horizontalInput < 0 || !isFacingRight && horizontalInput > 0)
            {
                isFacingRight = !isFacingRight;
                // Vector3 flip = transform.localScale;
                // flip.x *= -1;
                // transform.localScale = flip;
                transform.Rotate(0f, 180, 0f);
            }

        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                isJumping = false;
                // doubleJump = true;
                _animator.SetBool(PlayerState.OnJump, false);
                _animator.SetBool(PlayerState.IsGrounded, true);
            }
            if (other.gameObject.CompareTag("NextMap"))
            {
                int currentScene = PlayerPrefs.GetInt("currentScene");
                currentScene += 1;
                PlayerPrefs.SetInt("currentScene", currentScene);
                SceneManager.LoadScene(currentScene);
            }
            if (other.gameObject.CompareTag("candy"))
            {
                currentHealPlayer += 40;
                PlayerPrefs.SetInt("currentHeal", currentHealPlayer);
                healthBar.UpdateHealthBar(PlayerPrefs.GetInt("currentHeal"), PlayerState.maxHealthPlayer);
            }

        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                // doubleJump = false;
                _animator.SetBool(PlayerState.OnJump, false);
                _animator.SetBool(PlayerState.IsGrounded, false);
            }
        }

        public void PlayerTakeDamages(int damage)
        {
            currentHealPlayer -= damage;
            _animator.SetTrigger(PlayerState.hurt);
            PlayerPrefs.SetInt("currentHeal", currentHealPlayer);
            // GameManager.Instance.HPValue = currentHealPlayer;
            healthBar.UpdateHealthBar(PlayerPrefs.GetInt("currentHeal"), PlayerState.maxHealthPlayer);
            if (currentHealPlayer <= 0)
                PlayerDied();
        }
        private void OnDestroy()
        {
            Destroy(gameObject);

        }

        private void PlayerDied()
        {
            _animator.SetTrigger(PlayerState.IsDead);
            this.enabled = true;
            GetComponent<Collider2D>().enabled = false;
            Invoke(nameof(OnDestroy), 1f);
            SceneManager.LoadScene(4);

        }
    }
}

