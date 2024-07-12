using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player movement
    private Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    private Animator animator;
    [SerializeField] GameObject bulletPrefab;
    float bulletSpeed = 10f;
    Vector3 offsetVector = new Vector3(0, -0.2f,0);
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody component attached to the player
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component attached to the player
        animator = GetComponent<Animator>(); // Get the Animator component attached to the player

    }

    void Update()
    {
        DoMovement();

        if (Input.GetKeyDown(KeyCode.J))
            Shoot();
    }

    void DoMovement()
    {
        // Get input from WASD keys
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Create a Vector3 direction based on the input
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        // Apply the movement to the Rigidbody
        rb.velocity = movement * moveSpeed;

        // Flip the sprite based on the movement direction
        if (moveHorizontal > 0 && spriteRenderer.flipX)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveHorizontal < 0 && !spriteRenderer.flipX)
        {
            spriteRenderer.flipX = true;
        }

        bool isMoving = movement.magnitude > 0;
        animator.SetBool("isMoving", isMoving);
    }

    void Shoot()
    {

        // Instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, transform.position + offsetVector, Quaternion.identity);

        // Get the Rigidbody2D of the bullet
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        // Set the bullet's velocity based on the player's facing direction
        float bulletDirection = spriteRenderer.flipX ? -1f : 1f;
        bulletRb.velocity = new Vector2(bulletDirection * bulletSpeed, 0);
    }
}
