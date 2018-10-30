using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour {

    public enum TypeOfWeapon
    {
        Sword
    }

    public Vector3 colliderOffset;

    #region Collider_Offsets
    private Vector2[,] weaponOffsetArray = new Vector2[1, 4] 
    {
        {
             //Sword Collider Offsets 
             //order is very important ;/
            new Vector2(0.35f, -0.15f),
            new Vector2(-0.35f, -0.15f),
            new Vector2(-0.05f, 0.25f),
            new Vector2(-0.05f, -0.4f)
        },
    };
    #endregion

    private GameObject enemy = null;
    
    public void ChangeOffset(Vector2 lastDirection,TypeOfWeapon weaponType)
    {

        Collider2D collider = GetComponent<Collider2D>();

        if (lastDirection == new Vector2(1,0))
        {
            collider.offset = weaponOffsetArray[(int)weaponType,0];
        }
        else if (lastDirection == new Vector2(-1, 0))
        {
            collider.offset = weaponOffsetArray[(int)weaponType, 1];
        }
        else if (lastDirection == new Vector2(0, 1))
        {
            collider.offset = weaponOffsetArray[(int)weaponType, 2];
        }
        else if (lastDirection == new Vector2(0, -1))
        {
            collider.offset = weaponOffsetArray[(int)weaponType, 3];
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && enemy != collision.gameObject)
        {
            enemy = collision.gameObject;
            Alive livingThing = collision.GetComponent<Alive>();
            float damage = FindObjectOfType<PlayerController>().damage;
            livingThing.ReceiveDamage(damage);
            livingThing.ReceiveKnockBack(transform.position, livingThing.knockbackForce);

            if (livingThing.health <= 0)
            {
                PlayerAchievementComponent.OnCreatureKilled(livingThing);
                Destroy(livingThing.gameObject, livingThing.knockbackTime);
            }
        }
    }
}   
