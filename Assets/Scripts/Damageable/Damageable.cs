using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damageable : MonoBehaviour {

    public float health = 100f;
        
    public virtual void ReceiveDamage(float damageTaken, bool isEnemy)
    {
        health -= damageTaken;
    }
}
