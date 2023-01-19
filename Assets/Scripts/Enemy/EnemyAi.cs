using UnityEngine;
using UnityEngine.AI;
using DVSN.GameManagment;
using System.Collections.Generic;
using DVSN.Plot;

namespace DVSN.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyAi : MonoBehaviour
    {
        [SerializeField] internal EnemyType enemyType;
        [SerializeField] EnemyElement enemyElement;
        [SerializeField] List<GameObject> dropItems = new List<GameObject>();

        NavMeshAgent agent;

        [SerializeField] Transform player;

        [SerializeField] LayerMask whatIsGround;
        [SerializeField] LayerMask whatIsPlayer;

        [SerializeField] float health;

        //Patroling
        [SerializeField] float walkPointRange;
        [SerializeField] Vector3 walkPoint;
        bool walkPointSet;

        //Attacking
        [SerializeField] GameObject projectile;
        [SerializeField] float timeBetweenAttacks;
        bool alreadyAttacked;

        //States
        [SerializeField] float sightRange;
        [SerializeField] float attackRange;
        [SerializeField] bool playerInSightRange;
        [SerializeField] bool playerInAttackRange;

        private void Awake()
        {
            //player = Managers.Player.playerObject.transform;
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            //Check for sight and attack range
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange)
            {
                Patroling();
            }
            else if (playerInSightRange && !playerInAttackRange)
            {
                ChasePlayer();
            }
            else if (playerInAttackRange && playerInSightRange)
            {
                AttackPlayer();
            }
        }

        private void Patroling()
        {
            if (!walkPointSet) SearchWalkPoint();

            if (walkPointSet)
                agent.SetDestination(walkPoint);

            Vector3 distanceToWalkPoint = transform.position - walkPoint;

            //Walkpoint reached
            if (distanceToWalkPoint.magnitude < 1f)
                walkPointSet = false;
        }

        private void SearchWalkPoint()
        {
            //Calculate random point in range
            float randomZ = Random.Range(-walkPointRange, walkPointRange);
            float randomX = Random.Range(-walkPointRange, walkPointRange);

            walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

            if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
                walkPointSet = true;
        }

        private void ChasePlayer()
        {
            agent.SetDestination(player.position);
        }

        private void AttackPlayer()
        {
            GetComponent<CombatEnemy>().StartCombat();
        }

        private void ResetAttack()
        {
            alreadyAttacked = false;
        }

        public void TakeDamage(int damage)
        {
            health -= damage;

            if (health <= 0)
            {
                foreach(GameObject dropItem in dropItems)
                {
                    Instantiate(dropItem, transform.position, Quaternion.identity);
                }

                Managers.Quest.CheckForAllQuests(enemyElement);

                Invoke(nameof(DestroyEnemy), 0.5f);
            }
        }

        public void Die()
        {
            foreach (GameObject dropItem in dropItems)
            {
                Instantiate(dropItem, transform.position, Quaternion.identity);
            }

            Managers.Quest.CheckForAllQuests(enemyElement);

            Invoke(nameof(DestroyEnemy), 0.5f);
        }

        private void DestroyEnemy()
        {
            Destroy(gameObject);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, sightRange);
        }
    }

    enum EnemyType
    {
        FROZEN = 0,
        ANGRY = 1
    }
}
