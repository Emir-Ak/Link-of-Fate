using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAchievementComponent : MonoBehaviour {

    public static void OnCreatureKilled(Alive alive)
    {
        var aliveType = alive.LivingBeingType;
        switch (aliveType)
        {
            case Alive.LivingBeings.Player:
                break;
            case Alive.LivingBeings.Goblin:
                break;
            case Alive.LivingBeings.Ogre:
                break;
            default:
                break;
        }
    }
}
