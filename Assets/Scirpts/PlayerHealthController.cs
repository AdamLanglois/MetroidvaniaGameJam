using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    
    public static PlayerHealthController instance;


        private void Awake()
    {
        instance = this;
    }


    public int currentHealth, maxHealth;

    public float invincibilityLength = 1f;
    private float invincibilityCounter;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        UIController.instance.UpdateHealthDisplay(currentHealth, maxHealth);

    }

    // Update is called once per frame
    void Update()
    {
        if(invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;
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

                gameObject.SetActive(false);

            }

            UIController.instance.UpdateHealthDisplay(currentHealth, maxHealth);
        }


    }
}
