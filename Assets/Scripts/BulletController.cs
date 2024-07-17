using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public string bulletType = "friendly";
    [SerializeField] GameObject bulletExplosionPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Enemy controlled bullet
        if (collision.tag == "PlayerCharacter" && bulletType == "enemy")
        {
            collision.GetComponent<PlayerController>().ReceiveDamage(1);
            TriggerBulletExplosion(collision);
        }

        // Player controlled bullet
        if (collision.tag == "Enemy" && bulletType == "friendly")
        {
            collision.GetComponent<EnemyController>().ReceiveDamage(1);
            TriggerBulletExplosion(collision);
        }

        if (collision.tag == "Wall")
            TriggerBulletExplosion(collision);

 
        if (collision.tag == "Obstacle")
        {
            ObstacleManager.instance.ObstacleRemoved(collision.gameObject);
            TriggerBulletExplosion(collision);
        }
    }

    void TriggerBulletExplosion(Collider2D collision)
    {
        Instantiate(bulletExplosionPrefab, transform.position, Quaternion.identity);
        Debug.Log("Bullet collided with: " + collision.name);
        AudioManager.instance.PlayHitSplat();

        Destroy(this.gameObject);
    }
}
