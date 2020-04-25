using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Config
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] int health = 100;
    [SerializeField] AudioClip hurtSFX;

    // Cached component references
    Rigidbody2D myRigidBody;
    AudioSource myAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFacingRight())
        {
            myRigidBody.velocity = new Vector2(moveSpeed, 0f);
        }
        else
        {
            myRigidBody.velocity = new Vector2(-moveSpeed, 0f);
        }
    }

    bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        AudioSource.PlayClipAtPoint(hurtSFX, Camera.main.transform.position);
        Destroy(gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
    }
}
