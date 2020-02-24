using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public GameObject crosshair;
    public GameObject randomSpider;
    public Text text;
    bool on;

    // Start is called before the first frame update
    void Start()
    {
        crosshair.SetActive(false);
        randomSpider.SetActive(false);
        on = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && !on)
        {
            on = true;
            text.fontSize = 20;
            text.text = "3";
            Invoke("changeText2", 1.0f);
            Invoke("changeText1", 2.0f);
            Invoke("disableText", 3.0f);
        }
    }
    
    void changeText2()
    {
        text.text = "2";
    }

    void changeText1()
    {
        text.text = "1";
    }

    void disableText()
    {
        text.gameObject.SetActive(false);   // text with info disappears
        crosshair.SetActive(true);          // crosshair appears
        SpiderSpawner.gameOn = true;        // spiders appears and it's possible to shoot
        randomSpider.SetActive(true);
    }

    private IEnumerator WaitSec()
    {
        yield return new WaitForSeconds(1.0f);
    }
}
