using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject playerCharacter; // Reference to the player character
    public Transform targetPoint;
    public Slider healthSlider; // Reference to the health slider
    public float moveSpeed = 2f; // Speed of the enemy movement
    private int health = 10; // Enemy health
    [SerializeField] float stoppingDistance = 0.5f;
    NavMeshAgent agent;

    private void Awake()
    {
        playerCharacter = GameObject.Find("PlayerCharacter");
        targetPoint = playerCharacter.transform;
        
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.stoppingDistance = stoppingDistance;

        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    void Update()
    {
        MoveTowardsPlayer();
    }

    // Function to move the enemy towards the player
    void MoveTowardsPlayer()
    {
        agent.SetDestination(targetPoint.position);

        //if (playerCharacter != null)
        //{
        //    // Calculate direction to the player
        //    Vector3 direction = (playerCharacter.transform.position - transform.position).normalized;

        //    float distanceToPlayer = Vector3.Distance(playerCharacter.transform.position, transform.position);

        //    // Move enemy towards the player only if the distance is greater than 0.5f
        //    if (distanceToPlayer > 0.5f)
        //    {
        //        transform.position += direction * moveSpeed * Time.deltaTime;
        //    }
        //}
    }

    // Function to receive damage
    public void ReceiveDamage(int amount)
    {
        health -= amount;
        healthSlider.value = health;

        if (health <= 0)
        {
            EnemySpawner.instance.EnemyDied(this.gameObject);
            Destroy(gameObject);
        }
    }
}