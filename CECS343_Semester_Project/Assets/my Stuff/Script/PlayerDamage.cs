﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    // Damage
    [SerializeField]
    private float damage;
    [SerializeField]
    private float pushbackForce;
    // Body
    [SerializeField]
    private Rigidbody2D pRB;
    private void Start()
    {
        pRB = gameObject.GetComponent<Rigidbody2D>();
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && pRB.velocity.y < 0)
        {
            other.gameObject.GetComponent<EnemyHealth>().AddDamage(damage);
            pRB.velocity = new Vector2(0, pushbackForce); // Because the velocity is always updated in player Controller
        }
    }
}
