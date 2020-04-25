using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Config
    [SerializeField] int damage = 40;
    [SerializeField] GameObject deathExplosion;

    // State
    private float speed = 15f;

    // Cached component references
    Rigidbody2D myRigidbody;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myRigidbody.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Instantiate(deathExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Background")
        {
            Destroy(gameObject);
        }
    }
}
