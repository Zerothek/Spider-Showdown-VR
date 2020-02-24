using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootBullets : MonoBehaviour
{
    public GameObject bullet_prefab;
    float speed = 1600f;

    float bullet_elapsed_time;
    bool cooldown = false;
    public Image cooldwonImage;

    // Start is called before the first frame update
    void Start()
    {
        bullet_elapsed_time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown)
        {
            cooldwonImage.fillAmount = 1 - (Time.time - bullet_elapsed_time) / 2;
            if (cooldwonImage.fillAmount <= 0)
            {
                cooldown = false;
                cooldwonImage.fillAmount = 1;
            }
        }

        //if (Input.GetMouseButtonDown(0) && !cooldown && PlayersHealth.health > 0)
        //if (!cooldown && PlayersHealth.health > 0)
        if (Input.GetButtonDown("Fire1") && !cooldown && PlayersHealth.health > 0 && SpiderSpawner.gameOn)
        {
            GameObject new_bullet = Instantiate(bullet_prefab,
                Camera.main.transform.position + Camera.main.transform.forward, Quaternion.identity);
            Rigidbody new_bullet_rigidbody = new_bullet.GetComponent<Rigidbody>();
            new_bullet_rigidbody.AddForce(Camera.main.transform.forward * speed * Time.deltaTime);
            Destroy(new_bullet, 2);
            bullet_elapsed_time = Time.time;
            cooldown = true;
        }
        
    }
}
