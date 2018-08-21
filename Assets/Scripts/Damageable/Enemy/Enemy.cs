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

    [Space(10)]
    [Header("Time values")]

    [Tooltip("Time AI will return to spawn after maxRange is reached")]
    public float returnToSpawnTime = 4f;

    [Tooltip("Time AI will maneuver after hit")]
    public float maneuverTime = 0.4f;
    [Tooltip("Normal speed during maneuvers is multiplied by this value")]
    public float speedMultiplier = 1.5f;

    private Vector3 spawnPoint;
    private bool wasTouched = false;
    private bool returnToSpawn = false;
    private bool isReturning = false;

    private void Awake()
    {
        spawnPoint = transform.position;
    }
    private void Update()
    {
        FollowTarget();
        if(health <= 0)
        {
            Destroy(gameObject,0.1f);
        }
        if(isKnocked == false && rb.velocity != Vector2.zero)
        {
            rb.velocity = Vector2.zero;
        }
        
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject && wasTouched == false && collision.gameObject != null)
        {
            wasTouched = true;
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.ReceiveDamage(damage);
            player.ReceiveKnockBack(transform.position);
            StartCoroutine(Maneuver());
        }

        rb.velocity = Vector3.zero;
    }

    protected virtual void FollowTarget()
    {
        float playerDistance = Vector3.Distance(transform.position, target.position);
        float spawnDistance = Vector3.Distance(transform.position, spawnPoint);

        if (spawnDistance >= maxRange)
        {

            Move(spawnPoint);
            isReturning = true;
          
        }
        else if (isReturning == true){
            isReturning = false;
            StartCoroutine(BackToSpawn());
        }
        else if (playerDistance < followRange && wasTouched == false && returnToSpawn == false)
        {
            Move(target.position);
        }
        else if (wasTouched == true)
        {

            Vector3 direction = transform.position - target.position;
            direction.Normalize();
            Move(direction);
        }

        else if (Vector3.Distance(transform.position, spawnPoint) > 0.5f)
        {
            Move(spawnPoint);

        }

    }

    void Move(Vector3 aimPos)
    {
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, aimPos, step);
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
