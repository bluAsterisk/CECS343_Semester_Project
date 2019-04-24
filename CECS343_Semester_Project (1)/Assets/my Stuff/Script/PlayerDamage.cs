using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField]
    private float damage;
    private Rigidbody2D prb;
    private void Start()
    {
        prb = gameObject.GetComponent<Rigidbody2D>();
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" && prb.velocity.y < 0)
        {
            other.gameObject.GetComponent<EnemyHealth>().AddDamage(damage);
            prb.velocity = new Vector2(0, 3);
        }
    }
}
