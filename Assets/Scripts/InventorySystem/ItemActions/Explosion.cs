using UnityEngine;
using System.Collections;
public class Explosion : MonoBehaviour
{
    private GameObject Object = null;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 &&  Object != collision.gameObject)
        {
            collision.GetComponent<Alive>().ReceiveDamage(20f, false);
            Object = collision.gameObject;
        }
    }
}
