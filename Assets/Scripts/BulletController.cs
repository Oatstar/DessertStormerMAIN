using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] GameObject bulletExplosionPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "PlayerCharacter")
            return;

        if(collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyController>().ReceiveDamage(1);
        }

        Instantiate(bulletExplosionPrefab, transform.position, Quaternion.identity);
        Debug.Log("Bullet collided with: " + collision.name);
        AudioManager.instance.PlayHitSplat();

        Destroy(this.gameObject);
    }
}
