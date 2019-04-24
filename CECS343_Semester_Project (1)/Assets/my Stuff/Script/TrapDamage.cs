using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamage : EnemyDamage
{
    // overrides method to allow attacking Enemies
    override protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            damageContinue = true;
            //print("damageContinue True");  //See if trigger true
            StartCoroutine("InvulnWearOff", other);
        }
        else if (other.tag == "Enemy")
        {
            //print("Enemy Enters");
            other.gameObject.GetComponent<EnemyHealth>().AddDamage(damage);
        }

    }
}
