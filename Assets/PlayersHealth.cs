using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayersHealth : MonoBehaviour
{
    public static float health;
    public static float score;

    //public Texture maxHealth;
    public Image health2;
    public Image health3;
    public Image minHealth;
    public Image dead;
    private Image currentImage;

    public TextMesh data;
    float auxHealth = 0;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        score = 0;
        currentImage = health2;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(health);
        if (auxHealth <= health && health < 100 && health > 0)
            health += Time.deltaTime * 3;
        auxHealth = health;
        data.text = "Health " + ((int)health).ToString() + "\nScore " + score.ToString();
    }

    private void OnGUI()
    {
        if (health >= 96 && health <= 100)
        {
            //GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), maxHealth);
            currentImage.gameObject.SetActive(false);
        }

        if (health < 95 && health > 80)
        {
            //GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), health2);
            currentImage.gameObject.SetActive(false);
            health2.gameObject.SetActive(true);
            currentImage = health2;
        }

        if (health < 80 && health > 50)
        {
            //GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), health3);
            currentImage.gameObject.SetActive(false);
            health3.gameObject.SetActive(true);
            currentImage = health3;
        }

        if (health < 50 && health > 0)
        {
            // GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), minHealth);
            currentImage.gameObject.SetActive(false);
            minHealth.gameObject.SetActive(true);
            currentImage = minHealth;
        }

        if (health <= 0)
        {
            //GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), dead);
            currentImage.gameObject.SetActive(false);
            dead.gameObject.SetActive(true);
            if(Input.GetButtonDown("Jump"))
            {
                // reset game;
                Application.LoadLevel(Application.loadedLevel);
                SpiderSpawner.gameOn = false;
            }
        }
    }
}
