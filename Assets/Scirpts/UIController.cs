using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }


    
    public Image[] heartIcons;

    public Sprite heartFull, heartEmpty;

    public Image FadeScreen;

    public float fadeScreen = 2f;
    private bool fadingToBlack, fadingFromBlack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(fadingToBlack)
        {
            FadeScreen.color = new Color (FadeScreen.color.r, FadeScreen.color.g, FadeScreen.color.b, Mathf.MoveTowards(FadeScreen.color.a, 1f, fadeScreen * Time.deltaTime));

            if(FadeScreen.color.a == 1f)
            {
                fadingToBlack = false;
            }
        }else if (fadingFromBlack)
        {
            FadeScreen.color = new Color(FadeScreen.color.r, FadeScreen.color.g, FadeScreen.color.b, Mathf.MoveTowards(FadeScreen.color.a, 0f, fadeScreen * Time.deltaTime));
            if (FadeScreen.color.a == 0f)
            {
                fadingFromBlack = false;
            }
        }
    }

    public void UpdateHealthDisplay(int health, int maxHealth)
    {
        for (int i = 0; i < heartIcons.Length; i++)
        {
            heartIcons[i].enabled = true;

            /*if(health <= i) 
            
            {
                heartIcons[i].enabled = false;


            }*/

            if(health > i)
            {
                heartIcons[i].sprite = heartFull;
            }
            else
            {
                heartIcons[i].sprite = heartEmpty;

                if(maxHealth <= i)
                {
                    heartIcons[i].enabled = false;
                }


            }

        }
       


    } 
    
        public void StartFadeToBlack()
        {
            fadingToBlack = true;
            fadingFromBlack = false;


        }

        public void StartFadeFromBlack()
        {
            fadingFromBlack = true;
            fadingToBlack = false;
        }
}
