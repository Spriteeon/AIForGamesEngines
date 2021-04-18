using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int startingHealth = 100;

    private int currentHealth;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
        currentHealth = startingHealth;
    }

    public void GetShot(int damage, ShootingAgent shooter)
	{
        ApplyDamage(damage, shooter);
	}

    private void ApplyDamage(int damage, ShootingAgent shooter)
	{
        currentHealth -= damage;

        if (currentHealth <= 0)
		{
            Die(shooter);
		}
	}

    private void Die(ShootingAgent shooter)
	{
        shooter.RegisterKill();
        Debug.Log(message: "I Died!");
        Respawn();
	}

    private void Respawn()
	{
        currentHealth = startingHealth;
        transform.position = startPosition;
	}
}
