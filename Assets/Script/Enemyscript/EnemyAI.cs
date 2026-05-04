using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;

    [Header("Movement")]
    public float speed = 2f;
    public float detectRange = 5f;
    public float attackRange = 1.2f;

    [Header("Attack")]
    public float attackCooldown = 1.5f;
    public int damage = 10;

    private float lastAttackTime;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        sr.flipX = (player.position.x < transform.position.x);

        if (distance <= detectRange)
        {
            if (distance > attackRange)
            {
                Vector2 dir = (player.position - transform.position).normalized;
                rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);

                anim.SetFloat("Speed", Mathf.Abs(dir.x));
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                anim.SetFloat("Speed", 0);

                if (Time.time >= lastAttackTime + attackCooldown)
                {
                    Attack();
                    lastAttackTime = Time.time;
                }
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            anim.SetFloat("Speed", 0);
        }
    }

    void Attack()
    {
        anim.SetTrigger("Attack");

        PlayerHealth hp = player.GetComponent<PlayerHealth>();

        if (hp != null)
        {
            hp.TakeDamage(damage);
        }

        Debug.Log("Enemy Hit Player: " + damage);
    }
}