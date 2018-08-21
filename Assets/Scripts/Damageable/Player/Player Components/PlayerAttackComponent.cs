using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackComponent : MonoBehaviour {

    #region Variables

    #region Components
    
    [SerializeField]
    private PlayerAnimatorController animatorController;
    [SerializeField]
    public PlayerController playerController;
    #endregion




    #region For_Attack

    [SerializeField]
    private GameObject SwordAttackPrefab; // prefab for sword collider

    private GameObject attackCollder; //needed so that the collider is allocated to this variable

    private bool isPlayerAttacking;//Is the player currently attacking?

    private bool _isAttackButtonPressed = false; //Is the button for attack pressed


    public bool IsAttackButtonPressed { get { return this._isAttackButtonPressed; } set { this._isAttackButtonPressed = value; } }

    #endregion For_Attack

    #endregion

    private void Start()
    {
        
        animatorController = GetComponent<PlayerAnimatorController>();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        
        


        if (!isPlayerAttacking && IsAttackButtonPressed && !animatorController.IsPlayerMoving)
        {
            isPlayerAttacking = true;
            StartAttack();
        }

        //animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f
        if ((isPlayerAttacking && animatorController.animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack")) && animatorController.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f && !animatorController.animator.IsInTransition(0))
        {
            EndAttack();
            isPlayerAttacking = false;
        }
        else if (!animatorController.animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack") && !playerController.IsPlayerMoving)
        {
            EndAttack();
        }



        #region Animator_Controller_Values_Refresh
        animatorController.IsPlayerAttacking = isPlayerAttacking;
        //animatorController.IsShieldBroken = _isShieldBroken;
        #endregion

    }

    private void StartAttack()
    {

        playerController.speed = 0;

        animatorController.StartAttackAnimation();


        attackCollder = Instantiate(SwordAttackPrefab, transform);
        attackCollder.GetComponent<SwordCollider>().ChangeOffset(animatorController.LastMove);
    }

    private void EndAttack()
    {
        Destroy(attackCollder);
        playerController.speed = playerController.standardMoveSpeed;

        animatorController.EndAttackAnimation();
        animatorController.IsPlayerMoving = false;
    }
}
