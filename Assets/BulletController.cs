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

        Instantiate(bulletExplosionPrefab, transform.position, Quaternion.identity);
        Debug.Log("Bullet collided with: " + collision.name);
        Destroy(this.gameObject);
    }
}
