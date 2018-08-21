using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Alive {

    [SerializeField]
    Transform target;
    
    public float followRange = 5f;
    private Vector3 spawnPoint;
    private bool wasTouched = false;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.gameObject && wasTouched == false && collision.gameObject != null)
        {
            var playerController = collision.GetComponent<PlayerController>();
            wasTouched = true;
            if (playerController.playerShieldComponent.IsUsingShield)
            {
                playerController.playerShieldComponent.ReceiveShieldDamage(damage, true);
            }
            else
            {
                playerController.ReceiveDamage(damage, true);
            }
            StartCoroutine(PushPlayer(collision.gameObject));

        }

    }

    protected virtual void FollowTarget()
    {

       if (Vector3.Distance(transform.position, target.position) < followRange && wasTouched == false)
        {
            transform.LookAt(target.position);
            transform.Rotate(new Vector3(0, -90, 0), Space.Self);
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        else
        {
            transform.LookAt(spawnPoint);
            transform.Rotate(new Vector3(0, -90, 0), Space.Self);
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
            
        }

    }
    IEnumerator PushPlayer(GameObject player)
    {
        StartCoroutine(KnockBackTime());
        while (isHit)
        {
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, Mathf.Cos(Vector2.Angle(target.position, transform.position))) * 4f, ForceMode2D.Impulse);
            yield return new WaitForEndOfFrame();
        }
        wasTouched = false;
    }

}
