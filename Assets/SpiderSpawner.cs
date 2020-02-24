using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderSpawner : MonoBehaviour
{
    public GameObject spider_prefab;
    public GameObject player;
    float spawning_range = 30f;
    float last_spawn_time;
    static public bool gameOn;

    // Start is called before the first frame update
    void Start()
    {
        last_spawn_time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOn && Time.time - last_spawn_time >= 5)
        {
            //GameObject new_spider = Instantiate(spider_prefab, new Vector3(15.32f, -1.12f, -2.81f) , Quaternion.identity);
            last_spawn_time = Time.time;

            Vector3 random_position;
            while (true)
            {
                int behind_x = Random.Range(0, 1), behind_z = Random.Range(0, 1);

                if (behind_x == 0)
                    behind_x = -1;

                if (behind_z == 0)
                    behind_z = -1;

                random_position = new Vector3(behind_x * Random.Range(player.transform.position.x + 2, spawning_range),
                    Random.Range(player.transform.position.y, player.transform.position.y + 5),
                    behind_z * Random.Range(player.transform.position.z + 2, spawning_range));

                if (!somethingNear(random_position))
                {
                    break;
                }
            }

            Instantiate(spider_prefab, random_position, Quaternion.identity);

        }
    }

    bool somethingNear(Vector3 spider_position)
    {
        Collider[] hitColliders = Physics.OverlapSphere(spider_position, 2f);
        if (hitColliders.Length == 0)
        {
            return false;
        }

        return true;
    }
}
