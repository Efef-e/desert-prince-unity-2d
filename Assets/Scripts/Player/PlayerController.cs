using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    private float movementDirection;
    public float speed;
    public float JumpPower;
    public float groundCheckRadius;
    public float attackRate = 2f;
    float nextAttack = 0;

    private bool isFacingRight = true;
    private bool isGrounded;
    Rigidbody2D rb;
    Animator anim;

    public GameObject groundCheck;
    public LayerMask groundLayer;

    public Transform attackPoint;
    public float attackDistance;
    public LayerMask enemyLayers;
    public float damage;

    public GameObject ninjaStar;
    public Transform firePoint;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckRotation();
        Jump();
        CheckSurface();
        CheckAnimations();

        if (Time.time > nextAttack)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                Attack();
                nextAttack = Time.time + 1f / attackRate;
            }
        }
        Shoot();
    }
    private void FixedUpdate()
    {
        Movement();
    }
    void Movement()
    {
        movementDirection = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(movementDirection * speed, rb.velocity.y);
        anim.SetFloat("runSpeed", Mathf.Abs(movementDirection * speed));
    }

    public void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if (StarBank.instance.bankStar > 0)
            {
                Instantiate(ninjaStar, firePoint.position, firePoint.rotation);
                StarBank.instance.bankStar -= 1;
            }

        }

    }

    void CheckAnimations()
    {
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
    }

    void CheckRotation()
    {
        if (isFacingRight && movementDirection < 0)
        {
            Flip();
        }
        else if (!isFacingRight && movementDirection > 0)
            Flip();
        {

        }
    }
    void CheckSurface()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckRadius, groundLayer);
    }
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void Jump()
    {
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, JumpPower);
            }
        }
    }

    public void Attack()
    {
        float numb = Random.Range(0, 2);

        if (numb == 0)
        {
            anim.SetTrigger("Attack1");
        }
        else if (numb == 1)
        {
            anim.SetTrigger("Attack2");
        }

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackDistance, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyStats>().TakeDamage(damage);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.transform.position, groundCheckRadius);
        Gizmos.DrawWireSphere(attackPoint.position, attackDistance);
    }
}
