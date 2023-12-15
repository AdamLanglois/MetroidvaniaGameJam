using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{

    public static PlayerHealthController instance;


    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        } else
        {
            Destroy(gameObject);
        }
    }


    public int currentHealth, maxHealth;

    public float invincibilityLength = 1f;
    private float invincibilityCounter;

    public SpriteRenderer theSR;
    public Color normalColor, fadeColor;

    private PlayerController thePlayer;


    // Start is called before the first frame update
    void Start()
    {
        thePlayer = GetComponent<PlayerController>();

        currentHealth = maxHealth;
        UIController.instance.UpdateHealthDisplay(currentHealth, maxHealth);


    }

    // Update is called once per frame
    void Update()
    {
        if (invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;

            if (invincibilityCounter <= 0)
            {
                theSR.color = normalColor;
            }

        }



    }

    public void DamagePlayer()

    {
        if (invincibilityCounter <= 0)
        {

            invincibilityCounter = invincibilityLength;



            currentHealth--;

            if (currentHealth <= 0)
            {
                currentHealth = 0;

                // gameObject.SetActive(false);


                RespawnController.instance.Respawn();

            }
            else
            {

                invincibilityCounter = invincibilityLength;

                theSR.color = fadeColor;

                thePlayer.KnockBack();



            }




            UIController.instance.UpdateHealthDisplay(currentHealth, maxHealth);
        }


    }

    public void FillHealth()
    {
        currentHealth = maxHealth;


        UIController.instance.UpdateHealthDisplay(currentHealth, maxHealth);

    }

    public void HealPlayer(int healAmount)
    {

        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {

            currentHealth = maxHealth;
        }

        UIController.instance.UpdateHealthDisplay(currentHealth, maxHealth);
    }
    

}
