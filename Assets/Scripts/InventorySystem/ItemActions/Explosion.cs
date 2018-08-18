using UnityEngine;
using System.Collections;
public class Explosion : MonoBehaviour
{
    private bool wasTouched = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == 8 && wasTouched == false)
        {
            collision.GetComponent<Damageable>().ReceiveDamage(20f);

            StartCoroutine(KnockBack(collision));

            wasTouched = true;
        }
    }
    IEnumerator KnockBack(Collider2D other)
    {
        while (true && other != null)
        {
            other.gameObject.GetComponent<Damageable>().ApplyKnockback();
            yield return new WaitForEndOfFrame();
        }
    }
}
