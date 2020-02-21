using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    public float lookRadius =  10f;
    public float damage = 10f;
    public float attackWait = 1f;

    private float attackTimer;
    Transform target;
    NavMeshAgent agent;
    Animator animator;
    private GameObject _enemyModel;
    [SerializeField]private GameObject _ragdoll;

    private void Awake()
    {

        _ragdoll = this.gameObject.transform.parent.GetChild(0).gameObject;
        _enemyModel = this.gameObject;
        _ragdoll.gameObject.SetActive(false);
    }

    public void Start()
    {
        target = PlayerTracker.tracker.player.transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {

            agent.SetDestination(target.position);
            faceTarget();
            animator.SetBool("Running", true);


        }
        if(agent.hasPath == false || agent.remainingDistance < 1f)
        { Patrol(); }
        if (distance< 10 && Time.time > attackTimer)
        {
            target.GetComponent<PlayerStatus>().takeDamage(damage);
            attackTimer = Time.time + attackWait;
            animator.SetBool("Attack", true);           
        }
        if (distance > 10)
        {
            animator.SetBool("Attack", false);
            //Debug.Log("Attack is set to " + animator.GetBool("Attack"));
        }
                  

    }

    private void Patrol()
    {
        Vector3 randomPosition = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));
        var goToPosition = transform.position + randomPosition;
        agent.SetDestination(goToPosition);
        animator.SetBool("Running", true);

    }

    void faceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.y));
        transform.rotation = Quaternion.Slerp (transform.rotation,lookRotation, Time.deltaTime*5);
    }

    public void takeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
        
    }

    public void Die()
    {
        CopyTransformData(_enemyModel.transform,_ragdoll.transform,agent.velocity);
        _ragdoll.gameObject.SetActive(true);
        _enemyModel.gameObject.SetActive(false);
        agent.enabled = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,lookRadius);

    }

    private void CopyTransformData(Transform sourceTransform, Transform destinationTransform, Vector3 velocity)
    {
        if (sourceTransform.childCount != destinationTransform.childCount)
        {
            Debug.Log("Invalid Transform Copy : Child Count Mismatch");
            return;
        }

        for (int i = 0; i < sourceTransform.childCount; i++)
        {
            var source = sourceTransform.GetChild(i);
            var destination = destinationTransform.GetChild(i);
            destination.position = source.position;
            destination.rotation = source.rotation;
            var rb = destination.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = velocity;
            }

            CopyTransformData(source, destination, velocity);
        }
    }
}
