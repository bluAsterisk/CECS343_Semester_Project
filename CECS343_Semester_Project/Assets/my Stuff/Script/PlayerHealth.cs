﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    // Health Variables
    [SerializeField]
    private float fullHealth;
    [SerializeField]
    private float currentHealth;

    // Health UI
    [SerializeField]
    private Slider healthSlider;

    // Damage variables
    [SerializeField]
    private float invulnerabilityTime;
    [SerializeField]
    private bool canDamage;

    // Animation field for player flashing when damaged
    private SpriteRenderer playerSprite;
    private Color newColor;

    // Flashing of player
    [SerializeField] [Range(0f, 1f)]
    private float colorTransparency;
    [SerializeField] [Range(0f, 1f)]
    private float intervalFlashTime;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = fullHealth;
        canDamage = true;
        newColor = Color.white;
        playerSprite = GetComponent<SpriteRenderer>();

        // Health UI Initilization
        healthSlider = PlayerHPSingleton.Instance.GetObject().GetComponentInChildren<Slider>();
        healthSlider.maxValue = fullHealth;
        healthSlider.value = fullHealth;

    }

    // Adds damage to player.
    // When player has no more health; player dies.
    // Also changes healthSlider.
    public void AddDamage(float damage)
    {
        if (canDamage)
        {
            if(damage <= 0)
            {
                return;
            }
            gameObject.GetComponent<Animator>().SetTrigger("hurtTrigger"); // animator Set trigger for wound
            currentHealth -= damage;
            healthSlider.value = currentHealth;
            if (currentHealth <= 0)
            {
                TriggerDeath();
            }
            StartCoroutine("Invulnerability");
        }
        
    }

    // How long player stays invulnerable
    IEnumerator Invulnerability()
    {
        canDamage = false;
        StartCoroutine(FlashWounded());
        yield return new WaitForSeconds(invulnerabilityTime);
        canDamage = true;
        yield return null;
    }

    // Indicator that player is still invulnerable
    IEnumerator FlashWounded()
    {
        while(!canDamage)
        {
            newColor.a = colorTransparency;
            playerSprite.color = newColor;
            yield return new WaitForSeconds(intervalFlashTime);
            newColor.a = 1f;
            playerSprite.color = newColor;
            yield return new WaitForSeconds(intervalFlashTime);
        }
        yield return null;
    }

    // Returns bool to see if Player can be damaged.
    public bool CanHurt()
    {
        return canDamage;
    }

    // Assigns healther Slider from input
    public void SetSlider(Slider HPS)
    {
        healthSlider = HPS;
    }

    // Triggers Death even though it doesn't have any triggers to set
    public void TriggerDeath()
    {
        DestroyPlayer();
    }

    // Destroys player and then respawns player
    void DestroyPlayer()
    {
        Destroy(gameObject);
        PlayerRespawn.Instance.RespawnPlayer();
    }
}

