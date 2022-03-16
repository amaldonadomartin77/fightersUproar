using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter2D_2_Movement : MonoBehaviour
{
    //Movement Variables
    Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    private float moveX;
    private float moveY;

    public bool isGrounded = true;
    //public float distToGround = 1.0f;
    //public LayerMask GroundLayer;
    //PauseMenu pauseSystem = new PauseMenu();
    //public PauseMenu pauseSystem;

    public GameObject player, target;

    //Combat Variables
    public LayerMask enemyFighter;
    public Animator playerAnim;

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

    public AudioSource audioSource;
    public AudioClip footstepSound, punchSound, kickSound, specialHitSound;

    private HealthSystem healthSystem;
    private MeterSystem meterSystem;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthSystem = transform.GetComponent<HealthSystem>();
        meterSystem = transform.GetComponent<MeterSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (pauseSystem.GetIsPaused()) { return; }
        Debug.Log(timeBetweenPunch);
        Move();
        Jump();
        SpecialPunch();
        Punch();
        Kick();
        Die();
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal2");
        float moveBy = x * speed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);

        //PLAYER DIRECTION
        moveX = Input.GetAxis("Horizontal2");
        Vector3 localPos = player.transform.InverseTransformPoint(target.transform.position);
        if (localPos.x < 0)
            GetComponent<SpriteRenderer>().flipX = true;
        else
            GetComponent<SpriteRenderer>().flipX = false;

        //Animations
        if (moveBy != 0)
        {
            GetComponent<Animator>().SetBool("IsRunning", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("IsRunning", false);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            GetComponent<Animator>().SetBool("IsJumping", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("IsJumping", false);
        }
        if (rb.velocity.y < 0)
        {
            GetComponent<Animator>().SetBool("IsFalling", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("IsFalling", false);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.I) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    /*
    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distToGround, GroundLayer);
        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }
    */

    void Punch()
    {
        if (timeBetweenPunch <= 0)
        {
            if (Input.GetKeyDown(KeyCode.O) && !Input.GetKey(KeyCode.LeftBracket) && isGrounded)
            {
                GetComponent<Animator>().SetTrigger("standingPunch");
                Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(punchPos.position, new Vector2(punchRangeX, punchRangeY), 0, enemyFighter);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Fighter2D_Movement>().TakeDamage(punchDamage);
                    StartCoroutine(playPunchSound());
                }
                timeBetweenPunch = startTimeBetweenPunch;
            }
        }
        else
        {
            timeBetweenPunch -= Time.deltaTime;
        }
    }

    void Kick()
    {
        if (timeBetweenKick <= 0)
        {
            if (Input.GetKeyDown(KeyCode.P) && !Input.GetKey(KeyCode.LeftBracket) && isGrounded)
            {
                GetComponent<Animator>().SetTrigger("standingKick");
                Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(kickPos.position, new Vector2(kickRangeX, kickRangeY), 0, enemyFighter);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Fighter2D_Movement>().TakeDamage(kickDamage);
                    StartCoroutine(playKickSound());
                }
                timeBetweenKick = startTimeBetweenKick;
            }
        }
        else
        {
            timeBetweenKick -= Time.deltaTime;
        }
    }

    void SpecialPunch()
    {
        if (timeBetweenSpecialPunch <= 0)
        {
            if (Input.GetKey(KeyCode.LeftBracket) && Input.GetKeyDown(KeyCode.O) && isGrounded)
            {
                if (meterSystem.GetMeterNormalized() >= 0.20f)
                {
                    GetComponent<Animator>().SetTrigger("specialPunch");
                    meterSystem.Spend(20);
                    Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(specialPunchPos.position, new Vector2(specialPunchRangeX, specialPunchRangeY), 0, enemyFighter);
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        enemiesToDamage[i].GetComponent<Fighter2D_Movement>().TakeDamage(specialPunchDamage);
                        StartCoroutine(playSpecialHitSound());
                    }
                }
                else
                {
                    Punch();
                    GetComponent<Animator>().SetTrigger("standingPunch");
                }
                timeBetweenSpecialPunch = startTimeBetweenSpecialPunch;
            }
        }
        else
        {
            timeBetweenSpecialPunch -= Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(punchPos.position, new Vector3(punchRangeX, punchRangeY, 1));
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(specialPunchPos.position, new Vector3(specialPunchRangeX, specialPunchRangeY, 1));
    }

    public void TakeDamage(int damage)
    {
        GetComponent<Animator>().SetTrigger("Hit");
        healthSystem.Damage(damage);
    }

    public void Die()
    {
        if (healthSystem.GetHealthNormalized() == 0.0f)
        {
            GetComponent<Animator>().SetTrigger("Die");
        }
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
