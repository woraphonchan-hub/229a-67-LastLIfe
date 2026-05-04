using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;

    public HealthDisplay healthDisplay;

    void Start()
    {
        currentHealth = maxHealth;
        healthDisplay.UpdateHearts(currentHealth, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
            currentHealth = 0;

        healthDisplay.UpdateHearts(currentHealth, maxHealth);

        Debug.Log("เลือดเหลือ: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player ตาย");
        Destroy(gameObject);
    }
}