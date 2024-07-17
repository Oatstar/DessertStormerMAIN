using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_GnawerController : MonoBehaviour
{
    GameObject playerCharacter;
    PlayerController playerController;
    [SerializeField] float distanceToPlayer;
    [SerializeField] bool attacking = false;
    float attackTimer;
    float attackInterval = 0.5f;

    private void Start()
    {
        playerCharacter = transform.GetComponentInParent<EnemyController>().playerCharacter;
        playerController = playerCharacter.GetComponent<PlayerController>();
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;
        if(attackTimer >= attackInterval)
        {
            attackTimer = 0;
            TryAttack();
        }

        distanceToPlayer = Vector2.Distance(playerCharacter.transform.position, this.transform.position);

        if (distanceToPlayer < 1.5f)
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
        if(attacking)
        {
            playerController.ReceiveDamage(1);
        }
    }
}
