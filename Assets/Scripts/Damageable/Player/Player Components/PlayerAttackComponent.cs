using System;
using UnityEngine;

public class PlayerAttackComponent : MonoBehaviour
{
    #region Variables

    #region Components
    [SerializeField] private PlayerAnimatorController animatorController;
    [SerializeField] public PlayerController playerController;
    [SerializeField] public PlayerAchievementComponent AchievementsComponent;
    #endregion Components

    #region For_Attack

    [SerializeField]
    private GameObject SwordAttackPrefab; // prefab for sword collider

    private GameObject attackCollider;

    private bool isPlayerAttacking;//Is the player currently attacking?

    private bool _isAttackButtonPressed = false; //Is the button for attack pressed

    public bool IsAttackButtonPressed { get { return this._isAttackButtonPressed; } set { this._isAttackButtonPressed = value; } }

    #endregion For_Attack

    #endregion Variables

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

        if (isPlayerAttacking && animatorController.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && !animatorController.animator.IsInTransition(0) && animatorController.animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack"))
        {
            EndAttack();
            isPlayerAttacking = false;
        }

        #region Animator_Controller_Values_Refresh

        animatorController.IsPlayerAttacking = isPlayerAttacking;
        //animatorController.IsShieldBroken = _isShieldBroken;

        #endregion Animator_Controller_Values_Refresh
    }

    private void StartAttack()
    {
        playerController.speed = 0;

        animatorController.StartAttackAnimation();

        attackCollider = Instantiate(SwordAttackPrefab, transform);
        var colliderScript = attackCollider.GetComponent<WeaponCollider>();
        colliderScript.ChangeOffset(animatorController.LastMove,WeaponCollider.TypeOfWeapon.Sword);

        /*
         * in case of another weapon type a different prefab would be created
         * however that prefab would have the same script but with different offsets for each type of weapon
         * in which case different TypeOfWeapon will be used as such:
         * (enum)number
         * in fact if the weapon system will be created the enum will beoutside and not inside the WeaponCollider (however regardless right now so ignore this)
         * we're not going to add any other weapons most likely but extendabity is always good ;')
         */
    }

    private void EndAttack()
    {
        Destroy(attackCollider);
        playerController.speed = playerController.standardMoveSpeed;

        animatorController.EndAttackAnimation();
        animatorController.IsPlayerMoving = false;
    }
}