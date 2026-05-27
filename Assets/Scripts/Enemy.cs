using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int health;
    public bool isEnemyDead;
    [Header("Loot")]
    public List<LootItem> lootTable = new List<LootItem>();

    [Header("References")]
    [SerializeField] private NavMeshAgent navAgent;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectilePrefab;

    [Header("Layers")]
    [SerializeField] private LayerMask terrainLayer;
    [SerializeField] private LayerMask playerLayerMask;

    [Header("Patrol Settings")]
    [SerializeField] private float patrolRadius = 10f;
    private Vector3 currentPatrolPoint;
    private bool hasPatrolPoint;

    [Header("Combat Settings")]
    [SerializeField] private float attackCooldown = 1f;
    private bool isOnAttackCooldown;
    [SerializeField] private float forwardShotForce = 10f;
    [SerializeField] private float verticalShotForce = 5f;

    [Header("Detection Ranges")]
    [SerializeField] private float visionRange = 20f;
    [SerializeField] private float engagementRange = 10f;

    private bool isPlayerVisible;
    private bool isPlayerInRange;

    private void Awake()
    {
        isEnemyDead = false;
        Time.timeScale = 1f;

        navAgent = GetComponent<NavMeshAgent>();

        if (playerTransform == null)
        {
            GameObject playerObj = GameObject.Find("Player");

            if (playerObj != null)
                playerTransform = playerObj.transform;
        }
    }

    private void Update()
    {
        DetectPlayer();
        UpdateBehaviourState();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, engagementRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }

    private void DetectPlayer()
    {
        isPlayerVisible = Physics.CheckSphere(transform.position, visionRange, playerLayerMask);
        isPlayerInRange = Physics.CheckSphere(transform.position, engagementRange, playerLayerMask);
    }

    private void FireProjectile()
    {
        if (projectilePrefab == null || firePoint == null) return;

        Rigidbody projectileRb = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        projectileRb.AddForce(transform.forward * forwardShotForce, ForceMode.Impulse);
        projectileRb.AddForce(transform.up * verticalShotForce, ForceMode.Impulse);

        Destroy(projectileRb.gameObject, 3f);
    }

    private void FindPatrolPoint()
    {
        float randomX = Random.Range(-patrolRadius, patrolRadius);
        float randomZ = Random.Range(-patrolRadius, patrolRadius);

        Vector3 potentialPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(potentialPoint, -transform.up, 2f, terrainLayer))
        {
            currentPatrolPoint = potentialPoint;
            hasPatrolPoint = true;
        }
    }

    private IEnumerator AttackCooldownRoutine()
    {
        isOnAttackCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        isOnAttackCooldown = false;
    }

    private void PerformPatrol()
    {
        if (!hasPatrolPoint)
            FindPatrolPoint();

        if (hasPatrolPoint)
            navAgent.SetDestination(currentPatrolPoint);

        if (Vector3.Distance(transform.position, currentPatrolPoint) < 1f)
            hasPatrolPoint = false;
    }

    private void PerformChase()
    {
        if (playerTransform != null)
        {
            navAgent.SetDestination(playerTransform.position);
        }
    }

    private void PerformAttack()
    {
        navAgent.SetDestination(transform.position);

        if (playerTransform != null)
        {
            transform.LookAt(playerTransform);
        }

        if (!isOnAttackCooldown)
        {
            FireProjectile();
            StartCoroutine(AttackCooldownRoutine());
        }
    }

    private void UpdateBehaviourState()
    {
        // FIX: Safety check to prevent errors during scene initialization frames
        if (navAgent == null || !navAgent.isActiveAndEnabled || !navAgent.isOnNavMesh)
        {
            return;
        }

        if (!isPlayerVisible && !isPlayerInRange)
        {
            PerformPatrol();
        }
        else if (isPlayerVisible && !isPlayerInRange)
        {
            PerformChase();
        }
        else if (isPlayerVisible && isPlayerInRange)
        {
            PerformAttack();
        }
    }

    void TakeDamage()
    {
        health -= 50;
        if (health < 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        ShopMenu shop = FindObjectOfType<ShopMenu>();

        if (shop != null)
        {
            shop.AddCoins(1000);
        }

        foreach (LootItem lootItem in lootTable)
        {
            if (Random.Range(0, 100f) <= lootItem.dropChance)
            {
                InstantiateLoot(lootItem.itemPrefab);
            }
            break;
        }

        Destroy(gameObject);
    }


    void InstantiateLoot(GameObject loot)
    {
        if (loot)
        {
            GameObject droppedLoot = Instantiate(loot, transform.position, Quaternion.identity);
        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage();
        }
    }
}