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

    public void GetShot(int damage)
	{
        ApplyDamage(damage);
	}

    private void ApplyDamage(int damage)
	{
        currentHealth -= damage;

        if (currentHealth <= 0)
		{
            Die();
		}
	}

    private void Die()
	{
        Debug.Log(message: "I Died!");
        Respawn();
	}

    private void Respawn()
	{
        currentHealth = startingHealth;
        transform.position = startPosition;
	}

	#region Debug
	private void OnMouseDown()
	{
        GetShot(startingHealth);
	}
	#endregion
}
