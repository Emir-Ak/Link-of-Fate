using UnityEngine;
using System.Collections;
public class Explosion : MonoBehaviour
{
    public float damage = 50f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Damageable"))
        {

            if (collision.CompareTag("Player"))
            {
                PlayerController player = collision.GetComponent<PlayerController>();
                player.ReceiveDamage(damage, transform.position);

                player.ReceiveKnockBack(transform.position, player.knockbackForce * 1.5f);
            }
            else if (collision.CompareTag("Enemy"))
            {
                Enemy enemy = collision.GetComponent<Enemy>();
                enemy.ReceiveDamage(damage);
                enemy.ReceiveKnockBack(transform.position, enemy.knockbackForce);
            }
            else
            {
                Damageable damageable = collision.GetComponent<Damageable>();
                damageable.ReceiveDamage(damage);
            }

        }
    }
}
