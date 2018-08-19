using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollider : MonoBehaviour {


    public Vector3 colliderOffset;
    #region Collider_Offsets
    private static Vector2 colliderOffsetRight = new Vector2(0.35f, -0.15f);
    private static Vector2 colliderOffsetLeft = new Vector2(-0.35f, -0.15f);
    private static Vector2 colliderOffsetUp = new Vector2(-0.05f, 0.25f);
    private static Vector2 colliderOffsetDown = new Vector2(-0.05f, -0.4f);
    #endregion

    public void ChangeOffset(Vector2 lastDirection)
    {

        Collider2D collider = GetComponent<Collider2D>();

        if (lastDirection == new Vector2(1,0))
        {

            

            collider.offset = colliderOffsetRight;

        }
        else if (lastDirection == new Vector2(-1, 0))
        {

            collider.offset = colliderOffsetLeft;


        }
        else if (lastDirection == new Vector2(0, 1))
        {

            collider.offset = colliderOffsetUp;;

        }
        else if (lastDirection == new Vector2(0, -1))
        {

            collider.offset = colliderOffsetDown;

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player") && collision.gameObject.layer == 8)
        {
            Alive livingThing = collision.GetComponent<Alive>();
            float damage = FindObjectOfType<PlayerController>().damage;
            livingThing.ReceiveDamage(damage, true);
        }
    }




}   
