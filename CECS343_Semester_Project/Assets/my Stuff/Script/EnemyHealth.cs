using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Health
    [SerializeField]
    private float enemyMaxHealth;
    float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = enemyMaxHealth;
    }

    public void AddDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            TriggerDeath();
        }
    }

    // Triggers death for Animator Trigger
    void TriggerDeath()
    {
        // Gathers all children of this gameObject level components and looks for tag that matches to disable collider
        Collider2D[] children = gameObject.GetComponentsInChildren<Collider2D>();
        if(children != null)
        {
            foreach (Collider2D child in children)
            {
                if(child.CompareTag("Enemy Attack"))
                {
                    child.enabled = false;
                }
            }
        }
        gameObject.GetComponent<Animator>().SetTrigger("die");
    }

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
