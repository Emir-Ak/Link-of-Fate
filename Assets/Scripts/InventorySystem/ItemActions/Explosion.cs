using UnityEngine;
using System.Collections;
public class Explosion : MonoBehaviour
{
    private GameObject Object = null;
    public float damage = 100f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 &&  Object != collision.gameObject)
        {
            collision.GetComponent<Alive>().ReceiveDamage(damage);
            Object = collision.gameObject;
        }
    }
}
