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
    GameObject hand;
    SpriteRenderer handSpriteRenderer;
    public Sprite defaultHandSprite;
    public Sprite ShootHandSprite;
    bool canAttack = true;
    float attackInterval = 0.15f;

    [SerializeField] int playerHealth = 10;
    int maxPlayerHealth = 10;

    private void Awake()
    {
        hand = transform.Find("Hand").gameObject;
        handSpriteRenderer = hand.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody component attached to the player
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component attached to the player
        animator = GetComponent<Animator>(); // Get the Animator component attached to the player
        playerHealth = maxPlayerHealth;
    }

    void Update()
    {

        DoMovement();

        if (Input.GetKeyDown(KeyCode.J) || Input.GetMouseButtonDown(0))
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

        bool isMoving = movement.magnitude > 0.05f;
        animator.SetBool("isMoving", isMoving);

        if(isMoving)
            AudioManager.instance.PlayWalkSound();
    }

    void Shoot()
    {
        if (!canAttack)
            return;

        handSpriteRenderer.sprite = ShootHandSprite;
        Invoke("RefreshHandSprite", 0.2f);
        // Instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, hand.transform.position, Quaternion.identity);
        bullet.GetComponent<BulletController>().bulletType = "friendly";

        // Get the Rigidbody2D of the bullet
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        // Calculate the direction to the mouse position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Set z to 0 if your game is 2D

        Vector2 direction = (mousePosition - transform.position).normalized;

        // Set the bullet's velocity based on the direction to the mouse
        bulletRb.velocity = direction * bulletSpeed;


        //knockback to player
        //float knockbackForce = 10f; // Adjust the force as needed
        //rb.AddForce(-direction * knockbackForce, ForceMode2D.Impulse);

        StartCoroutine(ResetAttackInterval());

        AudioManager.instance.PlayShootSplat();
    }

    IEnumerator ResetAttackInterval()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackInterval);
        canAttack = true;
    }

    public void ReceiveDamage(int amount)
    {
        playerHealth -= amount;
        UIManager.instance.RefreshHealthSlider(playerHealth);

        if (playerHealth<=0)
        {
            
            GameMasterManager.instance.PlayerDies();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "NextMapTrigger")
        {
            Debug.Log("Triggering next map");
            GameMasterManager.instance.EnterDoor();
        }
    }

    void RefreshHandSprite()
    {
        handSpriteRenderer.sprite = defaultHandSprite;

    }

}
