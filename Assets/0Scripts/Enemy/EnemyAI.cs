//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Vehicles.Ball;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    float agentSpeed;
    public LayerMask whatIsGround, whatIsPlayer;
    
    bool killerBall;
    Animator animator;
    [SerializeField]
    float timeBetweenAttacks=1;
    bool alreadyAttacked;
    public float sightRange=30, attackRange;
    public bool playerInAttackRange;
    public bool playerInSightRange;
    [SerializeField]
    float attackSpeed=0.5f;
    [SerializeField]
    float enemyDistanceRun = 4f;
    [SerializeField]
    
    bool walkPointSet=false;
    Vector3 walkpoint;
    [SerializeField]
    float walkPointRange;
   
    bool isDead = false;

    [SerializeField]
    bool isDPS = false;
    [SerializeField]
    int DPS = 2;
    float savedTime;
    [SerializeField]
    int damageAmount = 50;
    [SerializeField] int deathScore=20;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        //targetLen = targets.Count;
        agentSpeed = agent.speed;
        savedTime = 0;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (GameObject.FindGameObjectsWithTag("Player").Length != 0)
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
                killerBall = player.GetComponent<Ball>().isKillerBall;
                if (killerBall)
                {
                    float distance = Vector3.Distance(transform.position, player.transform.position);
                    if (distance < enemyDistanceRun)
                    {
                        Vector3 dirToPlayer = transform.position - player.transform.position;
                        Vector3 newpos = transform.position + dirToPlayer;
                        agent.SetDestination(newpos);
                        walkPointSet = false;
                        animator.SetBool("isWalking", true);
                    }
                    else
                    {
                        Walk();
                    }
                }
                else
                {
                    playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
                    playerInSightRange= Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
                    if (!playerInSightRange && !playerInAttackRange)
                    {
                        Walk();
                    }
                    else if (playerInSightRange && !playerInAttackRange)
                    {
                        ChasePlayer();
                    }
                    else if (playerInAttackRange)
                    {
                        AttackPlayer();
                    }
                }
            }
        }
        

        
    }

    private void AttackPlayer()
    {
        agent.SetDestination(player.position);
        if (!alreadyAttacked)
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isAttacking", true);
            agent.speed += attackSpeed;

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
        else
        {
            ChasePlayer();
            agent.speed = agentSpeed;
        }
       
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        animator.SetBool("isWalking", true);
        animator.SetBool("isAttacking", false);
    }

    private void Walk()
    {
        if (!walkPointSet)
        {

            SearchTargetPoint();
            
        }
        if (walkPointSet)
        {
            
            agent.SetDestination(walkpoint);
            animator.SetBool("isWalking", true);
            animator.SetBool("isAttacking", false);

            Vector3 distanceToWalkPoint = transform.position - walkpoint;
            if (distanceToWalkPoint.magnitude < 1f)
            {
                walkPointSet = false;
            }
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    void SearchTargetPoint()
    {
        //randomIndex = Random.Range(0, targetLen);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkpoint = new Vector3(transform.position.x + randomX, transform.position.y , transform.position.z + randomZ);
        if (Physics.Raycast(walkpoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }


    }

    void OnCollisionEnter(Collision coll)
    {
        if (killerBall && coll.gameObject.tag=="Player")
        {
            animator.SetBool("isDying", true);
            animator.SetBool("isWalking", false);
            animator.SetBool("isAttacking", false);
            isDead = true;
            audioSource.Play();
            player.GetComponent<Ball>().IncreaseScore(deathScore);
        }
        else if(!killerBall && coll.gameObject.tag == "Player")
        {
            if (!isDPS)
            {
                player.GetComponent<Health>().ApplyDamage(damageAmount);
            }
        }
    }

    void OnCollisionStay(Collision coll)
    {
        if (!killerBall && coll.gameObject.tag == "Player" && isDPS)
        {
            if (Time.time - savedTime >= 1)
            {
                savedTime = Time.time;
                player.GetComponent<Health>().ApplyDamage(DPS);
            }
        }

    }
}
