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
    public void TriggerDeath()
    {
        EnemyDamage children = gameObject.GetComponentInChildren<EnemyDamage>();
        children.TurnOffAttack(); // Enemy can't retaliate when dead.
        gameObject.GetComponent<Animator>().SetTrigger("die");
    }

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
