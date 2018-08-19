using UnityEngine;
using System.Collections;
public class Explosion : MonoBehaviour
{
    private bool wasTouched = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == 8 && wasTouched == false)
        {
            collision.GetComponent<Alive>().ReceiveDamage(20f, false);
            wasTouched = true;
        }
    }
}
