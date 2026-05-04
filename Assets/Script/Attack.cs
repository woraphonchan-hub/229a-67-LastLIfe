using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage = 20;
    public float attackRange = 1.5f;
    public LayerMask enemyLayer;

    public Transform attackPoint;   // จุดตี
    public Animator animator;       // Animator ของ Player

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // คลิกซ้าย
        {
            Attack();
        }
    }

    void Attack()
    {
        // เล่นอนิเมชั่นตี
        animator.SetTrigger("Attack");

        // หา enemy ในระยะ
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRange,
            enemyLayer
        );

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("ตีโดน: " + enemy.name);

            EnemyHealth e = enemy.GetComponent<EnemyHealth>();
            if (e != null)
            {
                e.TakeDamage(damage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}