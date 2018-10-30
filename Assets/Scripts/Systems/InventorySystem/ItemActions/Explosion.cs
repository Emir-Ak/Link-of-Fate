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
                PlayerShieldComponent shield = player.playerShieldComponent;

                if (shield.IsUsingShield == true)
                    shield.ReceiveDamage(damage);
                else
                    player.ReceiveDamage(damage);

                player.ReceiveKnockBack(transform.position, player.knockbackForce * 1.5f);
            }
            else if ((bool)collision.GetComponent<Alive>()?.hostileTo.Contains(Alive.LivingBeings.Player))
            {
                Alive livingThing = collision.GetComponent<Alive>();
                livingThing.ReceiveDamage(damage);
                livingThing.ReceiveKnockBack(transform.position, livingThing.knockbackForce);

                if (livingThing.health <= 0)
                {
                    PlayerAchievementComponent.OnCreatureKilled(livingThing);
                    Destroy(livingThing.gameObject, livingThing.knockbackTime);
                }
            }
            else{
                Damageable damageable = collision.GetComponent<Damageable>();
                damageable.ReceiveDamage(damage);
            }

        }
    }
}
