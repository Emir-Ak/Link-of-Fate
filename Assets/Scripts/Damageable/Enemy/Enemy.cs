using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : Alive
{
    [Tooltip("Target to follow |:-/")]
    [SerializeField]
    private Transform target;
    [SerializeField]
    private LayerMask identifierMask;
    #region Range_Values

    [Space(10)]
    [Header("Range values")]
    [Tooltip("Range in which AI follows player")]
    public float followRange = 5f;

    [Tooltip("Range of the bounds that the AI will never pass")]
    public float maxRange = 20f;

    [Tooltip("Range after which AI will return if in idle state")]
    public float idleRange = 4f;

    #endregion Range_Values

    #region Time_Values

    [Space(10)]
    [Header("Time values")]
    [Tooltip("Time AI will return to spawn after maxRange is reached")]
    public float returnToSpawnTime = 4f;

    [Tooltip("Time AI will maneuver after hit")]
    public float maneuverTime = 0.4f;

    [Tooltip("Normal speed during maneuvers is multiplied by this value")]
    public float speedMultiplier = 1.5f;

    #endregion Time_Values

    #region Spawnpoint

    Vector3 spawnPoint;

    #endregion Spawnpoint

    #region Behaviour_Values

    private LayerMask ShieldMask; //used for checking if attack touches the shield or the player.

    private float playerDistance;
    private float spawnDistance;

    private bool isManeuvring = false;
    private bool returnToSpawn = false;
    private bool isReturning = false;
    private bool isIdle = false;

    #endregion Behaviour_Values

    private void Start()
    {

        spawnPoint = transform.position;

    }

    private void Update()
    {
        FollowTarget();

        #region Object_Destruction

        if (health <= 0)
        {
            Destroy(gameObject, knockbackTime);
        }

        #endregion Object_Destruction

        if (isKnocked == false && rb.velocity != Vector2.zero)
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject?.transform == target && collision.gameObject && isManeuvring == false && collision.gameObject != null)
        {
            {
                Alive livingBeing = target.GetComponent<Alive>();

                if (collision.gameObject.CompareTag("Player")) target.GetComponent<PlayerController>().ReceiveDamage(damage, transform.position);
                else livingBeing.ReceiveDamage(damage);

                livingBeing.ReceiveKnockBack(transform.position, livingBeing.knockbackForce);
                StartCoroutine(Maneuver());
               
            }
        }
        rb.velocity = Vector3.zero;
    }

    protected virtual void FollowTarget()
    {
        if (!isManeuvring)
        {
            spawnDistance = Vector3.Distance(transform.position, spawnPoint);


            if (maxRange <= spawnDistance)
            {
                Move(spawnPoint);
                isReturning = true;
            }
            else if (isReturning == true)
            {
                isReturning = false;
                StartCoroutine(BackToSpawn());
            }
            else if (CheckForEnemies() && returnToSpawn == false)
            {
                Move(target.position);
            }


            else if (spawnDistance > 0.5f && isIdle == false)
            {
                Move(spawnPoint);
            }
            else if (spawnDistance <= 0.5f && isIdle == false)
            {
                StartCoroutine(IdleMove());
            }
        }
    }

    private void Move(Vector3 aimPos)
    {
        transform.position = Vector3.MoveTowards(transform.position, aimPos, speed * Time.deltaTime);
    }

    private IEnumerator IdleMove()
    {
        isIdle = true;

        while (!CheckForEnemies())
        {
           
            if (spawnDistance > idleRange)
            {
                while (spawnDistance > 0.5f)
                {
                    if (CheckForEnemies() || isManeuvring)
                    {
                        isIdle = false;
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

            while (randDir != (Vector2)transform.position)
            {
                if (CheckForEnemies() || isManeuvring)
                {
                    break;
                }
                Move(randDir);
                yield return new WaitForEndOfFrame();
            }

            if (CheckForEnemies())
            {
                break;
            }
            yield return new WaitForSeconds(Random.Range(0.5f, 1.2f));
        }
        isIdle = false;
    }

    /// <summary>
    /// This checks for any of the living creatures that this creature is hostile to
    /// </summary>
    /// <returns>
    /// returns a bool if in range
    /// </returns>
    private bool CheckForEnemies()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, followRange,identifierMask);
        List<GameObject> enemies = new List<GameObject>();

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject?.transform != transform  && hostileTo.Contains(collider.gameObject.GetComponentInParent<Alive>().LivingBeingType))
            {
                enemies.Add(collider.gameObject);
            }
        }

        Transform temptarget = transform;
        if (enemies.Count > 0)
        {
            List<float> distanceArrays = new List<float>();
            foreach (GameObject enemy in enemies)
            {
                distanceArrays.Add(Vector3.Distance(transform.position, enemy.transform.position));
            }


            target = enemies?[distanceArrays.IndexOf(distanceArrays.Min())].GetComponentInParent<Alive>().transform;
            return true;
        }
        else return false;
            


    }

    private IEnumerator BackToSpawn()
    {
        returnToSpawn = true;
        yield return new WaitForSeconds(returnToSpawnTime);
        returnToSpawn = false;
    }

    private IEnumerator Maneuver()
    {
        isManeuvring = true;
        speed *= speedMultiplier;

        float time = 0f;

        Vector3 manueverDir = transform.position - target.position;
        manueverDir.Normalize();
        while (time <= maneuverTime)
        {
            transform.position += manueverDir * speed * Time.deltaTime;
            time += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    
        speed /= speedMultiplier;
        isManeuvring = false;
    }
}