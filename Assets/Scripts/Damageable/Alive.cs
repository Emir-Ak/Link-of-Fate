using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alive : Damageable {

    #region Variables
    [Header("Living Object Variables")]
    [Space(8)]

    [HideInInspector]
    public TextInstantiator textInst;
    public SpriteRenderer sprite;
    public Rigidbody2D rb;

    [Space(10)]
    public float knockbackForce = 10f;
    public float knockbackTime = 0.3f;
    public float speed = 3f;
    public float damage = 20f;


    public bool isInvincible = false;

    protected bool isKnocked = false;
    #endregion

    public virtual void Initialize()
    {
        if(textInst == null)
        {
            textInst = FindObjectOfType<TextInstantiator>();
        }
    }
    #region ReceiveDamage
    /// <summary>
    /// Alive object (referred to as "creature" later on) which the component is on will receive damage and may be knockbacked.
    /// </summary>
    /// <param name="damageTaken">Speaks for itseld (Damage the creature will take)</param>
    /// <param name="relativeTransform">Transform of GameObject that will push the creature (assign "null" if it won't)</param>
    public override void ReceiveDamage(float damageTaken)
    {
        Initialize();
        string instantiatedText;

        if (isInvincible == false)
        {
            base.ReceiveDamage(damageTaken);
            instantiatedText = "-" + damageTaken.ToString();
            StartCoroutine(ApplyRedColor());
            if (transform.CompareTag("Player"))
            {
                textInst.InstantiateText(instantiatedText, transform.position, new Color32(255, 0, 0, 255));
            }
        }
        else if (transform.CompareTag("Player"))
        {

            base.ReceiveDamage(damageTaken);
            instantiatedText = "-" + damageTaken.ToString();
            textInst.InstantiateText(instantiatedText, transform.position, new Color32(193, 40, 29, 255));

        }

    }

    IEnumerator ApplyRedColor()
    {
        sprite.color = new Color32(255, 143, 143, 255);
        yield return new WaitForSeconds(0.25f);
        sprite.color = new Color32(255, 255, 255, 255);
    }
    #endregion

    #region Knockback
    ///
    public void ReceiveKnockBack(Vector3 relativePos, float force)
    {
        StartCoroutine(RechargeKnockBackTime());
        StartCoroutine(ApplyKnockback(relativePos, force));
    }

    IEnumerator ApplyKnockback(Vector3 relatve , float force)
    {
        while (isKnocked)
        {
            Vector3 direction = transform.position - relatve;
            direction.Normalize();
            rb.AddForce(direction * (isInvincible ? force / 3.25f : force / 2.5f), ForceMode2D.Impulse);
            yield return new WaitForEndOfFrame();
        }
        rb.velocity = Vector2.zero;
    }

    IEnumerator RechargeKnockBackTime()
    {
        isKnocked = true;
        yield return new WaitForSeconds(knockbackTime);
        isKnocked = false;
    }
#endregion

    #region Invincibility
    protected virtual void ReceiveNoDamage(float invTime)
    {
        StartCoroutine(ApplyInvincibility(invTime));
        Debug.Log("The Object is now invincible for: " + invTime + " sec");
    }


    IEnumerator ApplyInvincibility(float invTime)
    {
        isInvincible = true;
        yield return new WaitForSeconds(invTime);
        isInvincible = false;
    }
    #endregion
}
