using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject prefabProjectile;
    [SerializeField] public Vector2 respawnPosition;
    [SerializeField] AudioClip throwSFX;
    [SerializeField] AudioClip jumpSFX;

    // State   
    public bool isAlive = true;
    float gravityScaleAtStart;
    bool lookRight = true;
    float timingShoot = 0.5f;
    float waitingShoot = 0f;

    // Cached component references
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider2D;
    BoxCollider2D myFeetCollider2D;
    LevelManager levelManager;

    // Methods
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider2D = GetComponent<CapsuleCollider2D>();
        myFeetCollider2D = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
        respawnPosition = transform.position;
        levelManager = FindObjectOfType<LevelManager>();
    }

    void Update()
    {
        if (!isAlive) { return; }

        Run();
        ClimbLadder();
        Jump();
        FlipSprite();
        Shoot();
        Die();
    }

    private void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);
    }

    private void ClimbLadder()
    {
        if (!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myAnimator.SetBool("Climbing", false);
            myRigidbody.gravityScale = gravityScaleAtStart;
            return;
        }

        float controlThrow = Input.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, controlThrow * climbSpeed);
        myRigidbody.velocity = climbVelocity;
        myRigidbody.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("Climbing", playerHasVerticalSpeed);
    }

    private void Jump()
    {
        if (!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Foreground"))) { return; }

        if (Input.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            AudioSource.PlayClipAtPoint(jumpSFX, Camera.main.transform.position);
            myRigidbody.velocity += jumpVelocityToAdd;
        }
    }

    private void Die()
    {
        if (myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            levelManager.Respawn();
        }
    }

    private void FlipSprite()
    {
        if (myRigidbody.velocity.x > 0f && !lookRight)
        {
            transform.Rotate(0f, 180f, 0f);
            lookRight = true;
        }
        else if (myRigidbody.velocity.x < 0f && lookRight)
        {
            transform.Rotate(0f, 180f, 0f);
            lookRight = false;
        }
    }

    private void Shoot()
    {
        float fire = Input.GetAxis("Fire1");
        if (fire != 0f && CanShoot())
        {
            myAnimator.SetTrigger("Shooting");
            AudioSource.PlayClipAtPoint(throwSFX, Camera.main.transform.position);
            Instantiate(prefabProjectile, firePoint.position, firePoint.rotation);
            waitingShoot = Time.time + timingShoot;
        }
    }

    private bool CanShoot()
    {
        return Time.time > this.waitingShoot;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Checkpoint")
        {
            respawnPosition = other.transform.position;
        }
    }
}
