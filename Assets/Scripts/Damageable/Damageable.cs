using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damageable : MonoBehaviour {

    [Header("Living Object Variables")]
    [Space(8)]
    public float health = 100f;
    public float speed = 3f;
    public float damage = 20f;
        
    [Space(10)]

    public float knockbackForce = 10f;
    public SpriteRenderer sprite;
    public Rigidbody2D rb;

    Vector2 randomDir;
    Vector3 oppositeDir;
    private bool isLocked;
    private bool isInvincible = false;

    public virtual void ReceiveDamage(float damageTaken)
    {
        ApplyStates();
        if (isInvincible == true)
        {
            health -= damageTaken;
            StartCoroutine(Damaged());
            Debug.Log("-20hp");
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


    IEnumerator Invincibility(float invTime)
    {
        isInvincible = true;
        yield return new WaitForSeconds(invTime);
        isInvincible = false;
    }

    IEnumerator Damaged()
    {
        sprite.color = new Color32(255,143,143,255);
        yield return new WaitForSeconds(0.25f);
        sprite.color = new Color32(255, 255, 255, 255);
       
    }


    private void ApplyStates()
    {
        if(rb.velocity == Vector2.zero)
        {
            randomDir = Random.onUnitSphere;
            isLocked = false;
        }
        else
        {
            oppositeDir = rb.velocity * -1;
            isLocked = true;
        }
    }


    public void ApplyKnockback()
    {
        if (isLocked == false) {
            rb.AddForce(randomDir * knockbackForce, ForceMode2D.Impulse);
            isLocked = false;
        }
        else
        {

            rb.AddForce(oppositeDir * (knockbackForce/2.5f), ForceMode2D.Impulse);
            isLocked = true;
        }
        
    }
}
