using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Variables
    private GameObject player;
    private Rigidbody2D enemyRb;
    public float speed = 200;
    public int health = 100;

    private void Start()
    {
        player = GameObject.Find("Player");
        enemyRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }

    // Move function
    void Move()
    {
        // Set enemy direction towards player goal and move there
        Vector2 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed * Time.deltaTime);
    }

    // Enemy's health control
    public void takeDamage(int damage)
    {
        health -= damage;
        if(health < 0)
        {
            Die();
        }
    }

    // Destroy enemies if dies
    void Die()
    {
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
