using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alive : Damageable {

    [Header("Living Object Variables")]
    [Space(8)]
    public SpriteRenderer sprite;
    public Rigidbody2D rb;

    [Space(10)]
    Vector2 randomDir;
    Vector3 velocityDir;
    private bool isLocked;
    protected bool isInvincible = false;
    public float knockbackForce = 10f;
    public float knockbackTime = 0.3f;
    public float speed = 3f;
    public float damage = 20f;

    
    public bool isHit = false;
    public override void ReceiveDamage(float damageTaken, bool isEnemy)
    {
        if (isEnemy == false)
        {
            ApplyStates();
        }
        if (isInvincible == false)
        {
            health -= damageTaken;
            StartCoroutine(Damaged());
            Debug.Log("-" +  damageTaken + "hp");
        }
        else
        {
            Debug.Log("Immortal object...");
        }   
    }

    protected virtual void ReceiveNoDamage(float invTime)
    {
        StartCoroutine(Invincibility(invTime));
        Debug.Log("The Object is now invincible for: " + invTime + " sec");
    }


    protected IEnumerator Invincibility(float invTime)
    {
        isInvincible = true;
        yield return new WaitForSeconds(invTime);
        isInvincible = false;
    }

    protected IEnumerator Damaged()
    {
        sprite.color = new Color32(255, 143, 143, 255);
        yield return new WaitForSeconds(0.25f);
        sprite.color = new Color32(255, 255, 255, 255);

    }


    protected void ApplyStates()
    {

        if (rb.velocity == Vector2.zero)
        {
            randomDir = Random.onUnitSphere;
            isLocked = false;
        }
        else
        {

            velocityDir = rb.velocity * -1;
            isLocked = true;
        }
        StartCoroutine(KnockBackTime());
        StartCoroutine(KnockBack());       

    }

    public IEnumerator KnockBackTime()
    {
        isHit = true;
        if (!rb.gameObject.CompareTag("Player"))
        {
            yield return new WaitForSeconds(knockbackTime/2);
        }
        else
        {
            yield return new WaitForSeconds(knockbackTime);
        }
            isHit = false;
    }
     IEnumerator KnockBack()
    {
        while (isHit)
        {
            ApplyKnockback();
            yield return new WaitForEndOfFrame();
        }
        if (!rb.gameObject.CompareTag("Player"))
        {
            rb.velocity = Vector2.zero;
        }

    }

    private void ApplyKnockback()
    {
        if (isLocked == false)
        {
            rb.AddForce(randomDir * (isInvincible ? knockbackForce / 2f : knockbackForce), ForceMode2D.Impulse);
            isLocked = false;
        }
        else
        {

            rb.AddForce(velocityDir * (isInvincible ? knockbackForce / 5f : knockbackForce / 2.5f), ForceMode2D.Impulse);
            isLocked = true;
        }
        

    }
}
