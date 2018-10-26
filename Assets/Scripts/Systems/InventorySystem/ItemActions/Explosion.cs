using UnityEngine;
using System.Collections;
public class Explosion : MonoBehaviour
{
    private GameObject Object = null;
    public float damage = 50f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 &&  Object != collision.gameObject)
        {

            if (collision.CompareTag("Player"))
            {
                PlayerController player = collision.GetComponent<PlayerController>();
                PlayerShieldComponent shield = player.playerShieldComponent;

                if (shield.IsUsingShield == true)
                    shield.ReceiveDamage(damage);
                else
                    player.ReceiveDamage(damage);

                player.ReceiveKnockBack(transform.position, player.knockbackForce * 1.5f);
            }
            else if (collision.CompareTag("Enemy"))
            {
                Enemy enemy = collision.GetComponent<Enemy>();
                enemy.ReceiveDamage(damage);
                enemy.ReceiveKnockBack(transform.position, enemy.knockbackForce);
            }
            else{
                Damageable damageable = collision.GetComponent<Damageable>();
                damageable.ReceiveDamage(damage);
            }

        }
    }
}
