using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ShooterController : MonoBehaviour
{
    GameObject playerCharacter;
    PlayerController playerController;
    [SerializeField] float distanceToPlayer;
    [SerializeField] bool attacking = false;
    float attackTimer;
    float attackInterval = 0.5f;
    float bulletSpeed = 10f;
    float attackDistance = 5f;
    [SerializeField] GameObject bulletPrefab;

    private void Start()
    {
        playerCharacter = transform.GetComponentInParent<EnemyController>().playerCharacter;
        playerController = playerCharacter.GetComponent<PlayerController>();
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackInterval)
        {
            attackTimer = 0;
            TryAttack();
        }

        distanceToPlayer = Vector2.Distance(playerCharacter.transform.position, this.transform.position);

        if (distanceToPlayer < attackDistance)
        {
            attacking = true;
        }
        else
        {
            attacking = false;
        }
    }

    void TryAttack()
    {
        if (attacking)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
        bullet.GetComponent<BulletController>().bulletType = "enemy";
        // Get the Rigidbody2D of the bullet
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        // Calculate the direction to the mouse position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Set z to 0 if your game is 2D

        Vector2 direction = (playerCharacter.transform.position - transform.position).normalized;

        // Set the bullet's velocity based on the direction to the mouse
        bulletRb.velocity = direction * bulletSpeed;


        //knockback to player
        //float knockbackForce = 10f; // Adjust the force as needed
        //rb.AddForce(-direction * knockbackForce, ForceMode2D.Impulse);

        AudioManager.instance.PlayShootSplat();
    }
}
