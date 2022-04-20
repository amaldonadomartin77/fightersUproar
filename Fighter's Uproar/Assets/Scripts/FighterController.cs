using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class FighterController : MonoBehaviour
{
    //Movement Variables
    Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    private bool movingPos, movingNeg;

    public bool isGrounded = true;

    public GameObject player, target;

    //Combat Variables
    public LayerMask enemyFighter;
    public Animator playerAnim;
    public PlayerInput playerInput;

    private bool isAttacking;
    private bool isCrouching;
    private bool isShoryu;

    public Transform punchPos;
    public float punchRangeX;
    public float punchRangeY;
    public int punchDamage;
    private float timeBetweenPunch = 0;
    public float startTimeBetweenPunch;

    public Transform kickPos;
    public float kickRangeX;
    public float kickRangeY;
    public int kickDamage;
    private float timeBetweenKick;
    public float startTimeBetweenKick;

    public Transform specialPunchPos;
    public float specialPunchRangeX;
    public float specialPunchRangeY;
    public int specialPunchDamage;
    private float timeBetweenSpecialPunch;
    public float startTimeBetweenSpecialPunch;
    private bool specialActive;

    public Fireball fireballPrefab;
    public Poison poisonPrefab;
    public Beam beamPrefab;
    public Transform launchOffset;
    public Transform beamOffset;
    public Transform poisonLocation;

    public float specialKickRangeX;
    public float specialKickRangeY;
    public int specialKickDamage;
    private float timeBetweenSpecialKick;
    public float startTimeBetweenSpecialKick;

    public AudioSource audioSource;
    public AudioClip footstepSound, punchSound, kickSound, specialHitSound;

    private HealthSystem healthSystem;
    private MeterSystem meterSystem;

    private GameController gameController;
    private float stunTime;

    private void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthSystem = transform.GetComponent<HealthSystem>();
        meterSystem = transform.GetComponent<MeterSystem>();
    }

    void Update()
    {
        if (timeBetweenPunch <= 0 && timeBetweenKick <= 0 && timeBetweenSpecialPunch <= 0)
            isAttacking = false;

        if (!isGrounded && gameController.movementAllowed)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        }
        else if (isAttacking)
        {
            rb.velocity = new Vector2(0, 0);
        }
        else if (movingPos && gameController.movementAllowed && isGrounded && stunTime <= 0 && !isCrouching)
        {
            float moveBy = 1 * speed;
            rb.velocity = new Vector2(moveBy, rb.velocity.y);

            GetComponent<Animator>().SetBool("IsRunning", true);
        }
        else if (movingNeg && gameController.movementAllowed && isGrounded && stunTime <= 0 && !isCrouching)
        {
            float moveBy = -1 * speed;
            rb.velocity = new Vector2(moveBy, rb.velocity.y);

            GetComponent<Animator>().SetBool("IsRunning", true);
        }
        else if (stunTime <= 0 && !isShoryu)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            GetComponent<Animator>().SetBool("IsRunning", false);
        }

        timeBetweenPunch -= Time.deltaTime;
        timeBetweenSpecialPunch -= Time.deltaTime;
        timeBetweenKick -= Time.deltaTime;
        stunTime -= Time.deltaTime;

        if (EnemyToRight())
            GetComponent<SpriteRenderer>().flipX = false;
        else
            GetComponent<SpriteRenderer>().flipX = true;

        if (rb.velocity.y > 0 && !isShoryu)
            GetComponent<Animator>().SetBool("IsJumping", true);
        else
            GetComponent<Animator>().SetBool("IsJumping", false);

        if (rb.velocity.y < 0)
        {
            GetComponent<Animator>().SetBool("IsFalling", true);
            isShoryu = false;
        }
        else
        {
            GetComponent<Animator>().SetBool("IsFalling", false);
        }            

        
        if (isGrounded)
        {
            GetComponent<Animator>().SetBool("IsJumping", false);
            GetComponent<Animator>().SetBool("IsFalling", false);
        }
        
        if (isCrouching)
            GetComponent<SpriteRenderer>().flipY = true;
        else
            GetComponent<SpriteRenderer>().flipY = false;

        Die();
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        var inputValue = ctx.ReadValue<float>();
        if (ctx.performed && inputValue > 0)
            movingPos = true;
        if (ctx.performed && inputValue < 0)
            movingNeg = true;
        if (ctx.canceled)
        {
            movingPos = false;
            movingNeg = false;
        }
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (gameController.movementAllowed && ctx.performed && isGrounded && stunTime <= 0)
        {
            rb.velocity = new Vector2(rb.velocity.x * 1.2f, jumpForce);
            isGrounded = false;
        }
    }

    public void Crouch(InputAction.CallbackContext ctx)
    {
        if (stunTime <= 0)
        {
            if (ctx.performed)
                isCrouching = true;
            else
                isCrouching = false;
        }
    }

    public void Weak(InputAction.CallbackContext ctx)
    {
        if (gameController.movementAllowed && ctx.performed && isGrounded && stunTime <= 0)
        {
            if (!specialActive)
            {
                if (timeBetweenPunch <= 0)
                {
                    isAttacking = true;
                    GetComponent<Animator>().SetTrigger("standingWeak");
                    Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(punchPos.position, new Vector2(punchRangeX, punchRangeY), 0, enemyFighter);
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        enemiesToDamage[i].GetComponent<FighterController>().TakeDamage(punchDamage, startTimeBetweenPunch, isCrouching);
                        StartCoroutine(playPunchSound());
                    }
                    timeBetweenPunch = startTimeBetweenPunch;
                }
            }
            else
            {
                if (timeBetweenSpecialPunch <= 0)
                {
                    if (meterSystem.GetMeterNormalized() >= 0.20f)
                    {
                        isAttacking = true;
                        GetComponent<Animator>().SetTrigger("specialWeak");
                        meterSystem.Spend(20);
                        if (gameObject.tag == "Bella")
                        {
                            if (EnemyToRight())
                            {
                                Instantiate(fireballPrefab, launchOffset.position, transform.rotation);
                            }
                            else
                            {
                                Instantiate(fireballPrefab, launchOffset.position, Quaternion.Euler(new Vector3(0, 180, 0)));
                            }
                        }
                        else       //Ace special weak
                        {
                            StartCoroutine(Beam());
                            
                        }
                    }
                    timeBetweenSpecialPunch = startTimeBetweenSpecialPunch;
                }
            }
        }
    }

    IEnumerator Beam()
    {
        yield return new WaitForSeconds(0.9f);  
        if (EnemyToRight())
        {
            Instantiate(beamPrefab, beamOffset.position, transform.rotation);
        }
        else
        {
            Instantiate(beamPrefab, beamOffset.position, Quaternion.Euler(new Vector3(0, 180, 0)));
        }
        Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(specialPunchPos.position, new Vector2(specialPunchRangeX, specialPunchRangeY), 0, enemyFighter);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<FighterController>().TakeDamage(specialPunchDamage, startTimeBetweenSpecialPunch, false);
        }
    }

    public void Strong(InputAction.CallbackContext ctx)
    {
        if (gameController.movementAllowed && ctx.performed && isGrounded && stunTime <= 0)
        {
            if (!specialActive)
            {
                if (timeBetweenKick <= 0)
                {
                    isAttacking = true;
                    GetComponent<Animator>().SetTrigger("standingStrong");
                    Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(kickPos.position, new Vector2(kickRangeX, kickRangeY), 0, enemyFighter);
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        enemiesToDamage[i].GetComponent<FighterController>().TakeDamage(kickDamage, startTimeBetweenKick, isCrouching);
                        StartCoroutine(playKickSound());
                    }
                    timeBetweenKick = startTimeBetweenKick;
                }
            }
            else
            {
                if (gameObject.tag == "Bella")
                {
                    if (!isShoryu)
                    {
                        if (meterSystem.GetMeterNormalized() >= 0.20f)
                        {
                            meterSystem.Spend(20);
                            isShoryu = true;
                            if (EnemyToRight())
                            {
                                rb.AddForce(new Vector2(4, 7), ForceMode2D.Impulse);
                            }
                            else
                            {
                                rb.AddForce(new Vector2(-4, 7), ForceMode2D.Impulse);
                            }
                            isShoryu = true;
                            GetComponent<Animator>().SetTrigger("specialStrong");

                            Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(kickPos.position, new Vector2(kickRangeX, kickRangeY), 0, enemyFighter);
                            for (int i = 0; i < enemiesToDamage.Length; i++)
                            {
                                enemiesToDamage[i].GetComponent<FighterController>().TakeDamage(kickDamage, startTimeBetweenKick, false);
                                StartCoroutine(playKickSound());
                            }
                        }
                        timeBetweenSpecialKick = startTimeBetweenSpecialKick;
                    }
                }
                else
                {
                    if (timeBetweenSpecialKick <= 0)
                    {
                        GetComponent<Animator>().SetTrigger("specialStrong");
                        if (EnemyToRight())
                        {
                            Instantiate(poisonPrefab, poisonLocation.position, Quaternion.Euler(new Vector3(0, 180, 0)));
                        }
                        else
                        {
                            Instantiate(poisonPrefab, poisonLocation.position, transform.rotation);
                        }
                        timeBetweenSpecialKick = startTimeBetweenSpecialKick;
                    }
                }
            }
        }
    }

    public void Special(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            specialActive = true;
        else
            specialActive = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(punchPos.position, new Vector3(punchRangeX, punchRangeY, 1));
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(specialPunchPos.position, new Vector3(specialPunchRangeX, specialPunchRangeY, 1));
    }

    public void TakeDamage(float damage, float stunValue, bool crouching)
    {
        if (((EnemyToRight() && movingNeg) || (!EnemyToRight() && movingPos)) && ((isCrouching && crouching) || (!isCrouching && !crouching)))
            Block(0, stunValue);
        else
        {
            GetComponent<Animator>().SetTrigger("Hit");
            Knockback(damage * 0.5f);
            healthSystem.Damage(damage);
            stunTime = stunValue + 0.25f;
        }
    }

    public void TakeDamageNoHitStun(float damage)
    {
        healthSystem.Damage(damage);
    }

    private void Block(int damage, float stunValue)
    {
        //GetComponent<Animator>().SetTrigger("Block");
        Knockback(damage * 0.5f);
        healthSystem.Damage(damage);
        stunTime = stunValue - 0.2f;
    }

    private void Knockback(float strength)
    {
        rb.velocity = new Vector3(0, rb.velocity.y);
        if (EnemyToRight())
            rb.velocity = new Vector3(-strength, rb.velocity.y);
        else
            rb.velocity = new Vector3(strength, rb.velocity.y);
    }

    private void Die()
    {
        if (healthSystem.GetHealthNormalized() == 0.0f)
        {
            GetComponent<Animator>().SetTrigger("Die");
        }
    }

    private bool EnemyToRight()
    {
        Vector3 pos = player.transform.InverseTransformPoint(target.transform.position);
        if (pos.x < 0)
            return false;
        else
            return true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Entered Ground");
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Exited Ground");
            isGrounded = false;
        }
    }

    IEnumerator playPunchSound()
    {
        yield return new WaitForSeconds(0.2f);
        audioSource.PlayOneShot(punchSound);
    }

    IEnumerator playKickSound()
    {
        yield return new WaitForSeconds(0.2f);
        audioSource.PlayOneShot(kickSound);
    }

    IEnumerator playSpecialHitSound()
    {
        yield return new WaitForSeconds(0.2f);
        audioSource.PlayOneShot(specialHitSound);
    }
}
