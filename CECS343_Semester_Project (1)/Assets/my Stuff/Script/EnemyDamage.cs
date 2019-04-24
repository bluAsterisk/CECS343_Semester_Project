using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField]
    protected float damage;
    [SerializeField]
    protected float pushBackForce;
    [SerializeField]
    protected bool damageContinue;

    private void Start()
    {
        damageContinue = false;
    }

    protected virtual IEnumerator InvulnWearOff(Collider2D other)
    {
        // print("Character enter Coroutine"); // See if character started the Coroutine
        if (other.tag == "Player")
        {
            while (damageContinue)
            {
                //print("damageContinue"); // See if Damage Continues
                yield return new WaitUntil(() => other.gameObject.GetComponent<PlayerHealth>().CanHurt() == true);
                other.gameObject.GetComponent<PlayerHealth>().AddDamage(damage);
                PushBack(other.transform);
            }
            
        }

    }
    
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        damageContinue = true;
        //print("damageContinue True");  //See if trigger true
        StartCoroutine("InvulnWearOff", other);
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        StopCoroutine("InvulnWearOff");
        //StopAllCoroutines();
        damageContinue = false;
        //print("damageContinue False"); //See if trigger false
    }

    protected void PushBack(Transform pushedObject)
    {
        //print(pushedObject.gameObject.GetComponent<Rigidbody2D>().velocity);
        pushedObject.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, pushBackForce);
    }
}
