using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealthManager : MonoBehaviour
{
    public int startingHealth;
    private int currentHealth;

    public float flashLength;
    private float flashCounter;

    private Renderer rend;
    private Color storedColor;

    public Text healthText;

    public Text finalText;

    HeadFlash head;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        rend = GetComponent<Renderer>();
        storedColor = rend.material.GetColor("_Color");
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            finalText.text = "YOU LOST";
            Invoke("LoadMenu", 3);
        }
        
        if (flashCounter > 0)
        {
            flashCounter -= Time.deltaTime;
            if(flashCounter <= 0)
            {
                rend.material.SetColor("_Color", storedColor);
                HeadFlash.instance.SetHeadOriginal();
            }
        }
    }
    void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }


    public void HurtPlayer(int damageAmount)
    {
        currentHealth -= damageAmount;
        flashCounter = flashLength;
        rend.material.SetColor("_Color", Color.red);
        HeadFlash.instance.SetHeadColor();
        setHealthText();
    }

    void setHealthText()
    {
        healthText.text = "Health: " + currentHealth.ToString();
    }
}
