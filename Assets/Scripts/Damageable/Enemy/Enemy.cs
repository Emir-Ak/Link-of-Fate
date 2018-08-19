using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Alive {

    [SerializeField]
    Transform target;

    public float followRange = 5f;
    public float maxRange = 20f;
    [Header("Time AI will return to spawn after maxRange is reached")]
    public float returnoSpawnTime = 4f;

    private Vector3 spawnPoint;
    private bool wasTouched = false;
    private bool returnToSpawn = false;
    private bool isReturning = false;
    private void Start()
    {
        spawnPoint = transform.position;

    }

    //usually it will be put into update of the Orc etc class.
    private void Update()
    {
        FollowTarget();
        if(health <= 0)
        {
            Destroy(gameObject,0.1f);
        }
        
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player") && collision.gameObject && wasTouched == false && collision.gameObject != null)
    //    {
    //        wasTouched = true;
    //        collision.GetComponent<PlayerController>().ReceiveDamage(damage, true);
    //        StartCoroutine(PushPlayer(collision.gameObject));
    //    }

    //}
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject && wasTouched == false && collision.gameObject != null)
        {
            Debug.Log("hi");
            wasTouched = true;
            collision.gameObject.GetComponent<PlayerController>().ReceiveDamage(damage, true);
            StartCoroutine(PushPlayer(collision.gameObject));
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {

        }
        rb.velocity = Vector3.zero;
    }

    protected virtual void FollowTarget()
    {
        float playerDistance = Vector3.Distance(transform.position, target.position);
        float spawnDistance = Vector3.Distance(transform.position, spawnPoint);

        if (spawnDistance >= maxRange)
        {
            
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, spawnPoint, step);
            isReturning = true;
          
        }
        else if (isReturning == true){
            isReturning = false;
            StartCoroutine(BackToSpawn());
        }
        else if (playerDistance < followRange && wasTouched == false && returnToSpawn == false)
        {           
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }

        else if (Vector3.Distance(transform.position, spawnPoint) > 0.5f)
        {

            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, spawnPoint, step);
        }

    }
    IEnumerator BackToSpawn()
    {
        returnToSpawn = true;
        yield return new WaitForSeconds(4f);
        returnToSpawn = false;
    }


    IEnumerator PushPlayer(GameObject player)
    {
        StartCoroutine(KnockBackTime());
        PlayerController playerController = player.GetComponent<PlayerController>();
        bool isShielded = playerController.isInvincible;
        while (isHit)
        {
            

            Vector3 direction = transform.position - player.transform.position;
            direction.Normalize();
            player.GetComponent<Rigidbody2D>().AddForce(-1 * direction * ( isShielded ? 5f : 10f), ForceMode2D.Impulse);

            yield return new WaitForEndOfFrame();
        }
        wasTouched = false;
    }
    
}
