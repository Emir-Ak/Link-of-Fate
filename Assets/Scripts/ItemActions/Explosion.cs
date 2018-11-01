using UnityEngine;
using System.Collections;
public class Explosion : MonoBehaviour
{
    public float damage = 50f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.CompareTag("Player"))
            {
                PlayerController player = collision.GetComponent<PlayerController>();
                player.ReceiveDamage(damage, transform.position);
                player.ReceiveKnockBack(transform.position, player.knockbackForce * 1.5f);
            }
            //else if ((bool)collision.GetComponent<Alive>()?.hostileTo.Contains(Alive.LivingBeings.Player))
            //{
            //    Alive livingThing = collision.GetComponent<Alive>();
            //    livingThing.ReceiveDamage(damage);
            //    livingThing.ReceiveKnockBack(transform.position, livingThing.knockbackForce);
            //}
            else
            {
                Damageable damageable = collision.GetComponent<Damageable>();
                damageable.ReceiveDamage(damage);
            }      
    }
}
