using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Alive {

    [Tooltip("Target to follow |:-/")]
    [SerializeField]
    Transform target;

    [Space(10)]
    [Header("Range values")]

    [Tooltip("Range in which AI follows player")]
    public float followRange = 5f;

    [Tooltip("Range of the bounds that the AI will never pass")]
    public float maxRange = 20f;

    [Tooltip("Range after which AI will return if in idle state")]
    public float idleRange = 4f;

    [Space(10)]
    [Header("Time values")]

    [Tooltip("Time AI will return to spawn after maxRange is reached")]
    public float returnToSpawnTime = 4f;

    [Tooltip("Time AI will maneuver after hit")]
    public float maneuverTime = 0.4f;
    [Tooltip("Normal speed during maneuvers is multiplied by this value")]
    public float speedMultiplier = 1.5f;

    private Vector3 spawnPoint;
    private Vector3 spawnOffset;

    private float playerDistance;
    private float spawnDistance;

    private bool wasTouched = false;
    private bool returnToSpawn = false;
    private bool isReturning = false;
    private bool isIdle = false;

    private void Start()
    {
        spawnPoint = transform.position;
    }
    private void Update()
    {
        FollowTarget();
        if(health <= 0)
        {
            Destroy(gameObject, knockbackTime);
        }
        if(isKnocked == false && rb.velocity != Vector2.zero)
        {
            rb.velocity = Vector2.zero;
        }
        
    }

    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject && wasTouched == false && collision.gameObject != null)
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            wasTouched = true;
            if (player.playerShieldComponent.IsUsingShield)
            {

                player.playerShieldComponent.ReceiveDamage(damage);
            }
            else
            {
                player.ReceiveDamage(damage);         
            }

            player.ReceiveKnockBack(transform.position, player.knockbackForce);
            StartCoroutine(Maneuver());
        }

        rb.velocity = Vector3.zero;
    }

    protected virtual void FollowTarget()
    {
        if (target != null)
        {
            playerDistance = Vector3.Distance(transform.position, target.position);
            spawnDistance = Vector3.Distance(transform.position, spawnPoint);

            if (spawnDistance >= maxRange)
            {

                Move(spawnPoint);
                isReturning = true;

            }
            else if (isReturning == true)
            {
                isReturning = false;
                StartCoroutine(BackToSpawn());
            }
            
            else if (wasTouched == true)
            {

                Vector3 direction = transform.position - target.position;
                direction.Normalize();
                Move(direction);
            }
            else if (playerDistance < followRange && wasTouched == false && returnToSpawn == false)
            {
                Move(target.position);
            }
            else if (spawnDistance <= 0.5f && isIdle == false)
            {
               
                StartCoroutine(IdleMove());
            }
            else if(spawnDistance > 0.5f && isIdle == false)
            {;
                Move(spawnPoint);
            }
        }
    }

    void Move(Vector3 aimPos)
    {
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, aimPos, step);
    }

    IEnumerator IdleMove()
    {
        isIdle = true;


        while (playerDistance > followRange)
        {
            if (spawnDistance > idleRange)
            {
                while (spawnDistance > 0.5f)
                {
                    if (playerDistance <= followRange)
                    {
                        break;
                    }
                    Move(spawnPoint);
                    yield return new WaitForEndOfFrame();
                }
                yield return new WaitForSeconds(0.6f);
            }

            Vector2 randDir = Random.onUnitSphere;
            
            randDir *= Random.Range(2f, idleRange - 1f);
            randDir += (Vector2)transform.position;

           
            while(randDir != (Vector2)transform.position)
            {
                if(playerDistance <= followRange)
                {
                    break;
                }
                Move(randDir);
                yield return new WaitForEndOfFrame();
            }

            if (playerDistance <= followRange)
            {
                break;
            }
            yield return new WaitForSeconds(Random.Range(0.5f, 1.2f));
        }
        isIdle = false;
    }

    IEnumerator BackToSpawn()
    {

        returnToSpawn = true;
        yield return new WaitForSeconds(returnToSpawnTime);
        returnToSpawn = false;
    }


    IEnumerator Maneuver()
    {
        speed *= speedMultiplier;
        yield return new WaitForSeconds(maneuverTime);
        wasTouched = false;
        speed /= speedMultiplier;
    }
    
}
