using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShieldComponent : MonoBehaviour {

    #region Variables

    #region Components
    [SerializeField]
    private PlayerAttackComponent playerAttackComponent;//Component that has the functions for the Attack :3
    [SerializeField]
    private PlayerAnimatorController playerAnimatorController; //Script that controls the animations of the player and it's animaton
    [SerializeField]
    private PlayerController playerController; //Player Controller .-. (I want a cute waifu girlfriend D: (like inori or 02 ;/))
    #endregion

    public float currentShieldHealth; //Current health of the shield
    public float maxShieldHealth = 100; //Used to clamp the current shield health
    public float shieldRechargeDelay = 5;//Time to start regenerating after beign hit
    public float regenAmount = 2; //Amount of health the shield regenarates at one step of regenerating

    private bool isShieldRegenerated = true; //Has shield finished  regenerating?
    private bool isShieldRegenerating = false; //Is shield regenerating right now?\

    [SerializeField]
    private bool _isUsingShield = false; //Is player currently using shield?
    private bool _isShieldBroken = false; //Is the Shield broken?
    [SerializeField]
    private bool _isShieldButtonPressed = false; //Is the button for Shield pressed

    #endregion

    #region Properties
    public bool IsUsingShield { get { return this._isUsingShield; } set { this._isUsingShield = value; } }
    public bool IsShieldBroken { get { return this._isShieldBroken; } set { this._isShieldBroken = value; } }
    //public bool isshieldregenerated { get { return this._isShieldRegenerated; } set { this._isShieldRegenerated = value; } }
    //public bool isshieldregenerating { get { return this._isShieldRegenerating; } set { this._isShieldRegenerating = value; } }
    public bool IsShieldButtonPressed { get { return this._isShieldButtonPressed; } set { this._isShieldButtonPressed = value; } }
    #endregion

    // Use this for initialization
    void Start ()
    {
        #region GetComponent
        playerController = GetComponent<PlayerController>();
        playerAnimatorController = GetComponent<PlayerAnimatorController>();
        playerAttackComponent = GetComponent<PlayerAttackComponent>();

        #endregion
        currentShieldHealth = maxShieldHealth;

    }
	
	// Update is called once per frame
	void Update ()
    {
        currentShieldHealth = Mathf.Clamp(currentShieldHealth, 0, maxShieldHealth);

        if (!isShieldRegenerating && currentShieldHealth <= maxShieldHealth)
        {
            StartCoroutine(RegenerateShield(shieldRechargeDelay, regenAmount));
        }
        else if (isShieldRegenerating)
        {
            StopCoroutine("RegenerateShield");
        }


        if (_isShieldButtonPressed && !_isShieldBroken && isShieldRegenerated)
        {
            _isUsingShield = true;
            StartCoroutine(UseShield());
        }
        else if (!_isShieldButtonPressed)
        {

            _isUsingShield = false;
        }

        


        #region Animator_Controller_Values_Refresh
        playerAnimatorController.IsUsingShield = _isUsingShield;
        playerAnimatorController.IsShieldBroken = _isShieldBroken;
        #endregion

    }


    private IEnumerator UseShield()
    {
        Debug.Log("isShielding" + _isUsingShield);

        playerController.IsInvincible = true;
        playerAnimatorController.IsAnimationLocked = true;

        _isUsingShield = true;
        

        if (currentShieldHealth <= 0)
        {
            _isShieldBroken = true;

        }

        yield return new WaitUntil(() => !_isUsingShield || _isShieldBroken);

        playerController.IsInvincible = false;
        playerAnimatorController.IsAnimationLocked = false;

        _isUsingShield = false;

        Debug.Log("isShielding" + _isUsingShield);
    }

    private IEnumerator RegenerateShield(float rechargeDelay, float regenAmount)
    {
        yield return new WaitForSeconds(rechargeDelay);

        bool hasFinishedRegenerating = false;
        while (!hasFinishedRegenerating)
        {
            yield return new WaitForSeconds(0.2f);
            currentShieldHealth += regenAmount;

            if (currentShieldHealth >= maxShieldHealth)
            {
                hasFinishedRegenerating = true;
                _isShieldBroken = false;
                isShieldRegenerated = true;
                Debug.Log("Finished regenerating shield");

            }
        }

    }


    public  void ReceiveShieldDamage(float damageTaken, bool isEnemy)
    {

        playerController.ApplyStates();

        if (playerController.IsInvincible == false)
        {
            playerController.health -= damageTaken;
            StartCoroutine(playerController.Damaged());
            Debug.Log("-" + damageTaken + "hp");
        }
        else if (_isUsingShield && !_isShieldBroken)
        {
            currentShieldHealth -= damageTaken;

            Debug.Log("Shield is damaged by " + damageTaken + "hp");
        }
    }


}
