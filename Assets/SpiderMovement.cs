using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMovement : MonoBehaviour
{
    public GameObject player;
    Rigidbody spider_rigidbody;
    private Animator spider_animator;
    float speed = 3f;
    bool isDead = false;
    bool isAttacking = false;
    bool obstacleAhead = false;
    bool upsideDown = false;

    private int range = 3;
    private RaycastHit hit1, hit2;
    private float rotationSpeed = 50f;
    private int avoidDirection = 1;
    private float upsideDownTime;
    private Collider sphereCollider;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.LookAt(player.transform, transform.up);
        spider_rigidbody = GetComponent<Rigidbody>();
        spider_animator = GetComponent<Animator>();
        sphereCollider = GetComponent<SphereCollider>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Dot(transform.up, Vector3.down) > 0)
        {
            Debug.Log("Cracii in sus");
            upsideDown = true;
            upsideDownTime = Time.time;
            sphereCollider.enabled = true;
        } else
        {
            upsideDown = false;
            sphereCollider.enabled = false;
        }

        if (!upsideDown)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 4)
            {
                isAttacking = true;
                spider_animator.SetBool("isAttacking", true);
                StartCoroutine(waitToAttack());
                if (PlayersHealth.health >= 0)
                    PlayersHealth.health -= Time.deltaTime * 3;
                else
                    PlayersHealth.health = 0;
            }
            else
            {
                isAttacking = false;
                spider_animator.SetBool("isAttacking", false);
            }

            if (isDead == false && isAttacking == false)
            {
                if (!obstacleAhead)
                {
                    Vector3 lookAtTarget = new Vector3(player.transform.position.x - transform.position.x,
                        transform.position.y,
                        player.transform.position.z - transform.position.z);
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookAtTarget), Time.deltaTime);
                    transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                }
                else
                {
                    Debug.Log("evrika");
                    transform.Rotate(-Vector3.up * Time.deltaTime * rotationSpeed * avoidDirection);
                    transform.Translate(Vector3.forward * Time.deltaTime * speed);
                }

                if (Physics.Raycast(transform.position + transform.right + (transform.up / 2), transform.forward, out hit1, 2 * range) ||
                    Physics.Raycast(transform.position - transform.right + (transform.up / 2), transform.forward, out hit2, 2 * range) &&
                    obstacleAhead == false)
                {
                    if (Physics.Raycast(transform.position + transform.right + (transform.up / 2), transform.forward, out hit1, 2 * range))
                    {
                        if (hit1.collider.gameObject.CompareTag("Obstacles"))
                        {
                            avoidDirection = 1;
                            obstacleAhead = true;
                        }
                    }

                    if (Physics.Raycast(transform.position - transform.right + (transform.up / 2), transform.forward, out hit2, 2 * range))
                    {
                        if (hit2.collider.gameObject.CompareTag("Obstacles"))
                        {
                            avoidDirection = 1;
                            obstacleAhead = true;
                            //speed = 0f;
                        }
                    }

                }

                if (Physics.Raycast(transform.position - transform.forward + (transform.up / 2), transform.right, out hit1, range) ||
     Physics.Raycast(transform.position - transform.forward + (transform.up / 2), -transform.right, out hit2, range))
                {
                    if (hit1.collider.gameObject.CompareTag("Obstacles") ||
                        hit2.collider.gameObject.CompareTag("Obstacles"))
                    {
                        Debug.Log("not evrika");
                        obstacleAhead = false;
                        //speed = 3f;
                    }

                }

                Debug.DrawRay(transform.position + transform.right + (transform.up / 2), transform.forward * range * 2, Color.red);
                Debug.DrawRay(transform.position - transform.right + (transform.up / 2), transform.forward * range * 2, Color.red);
                Debug.DrawRay(transform.position - transform.forward + (transform.up / 2), -transform.right * range, Color.yellow);
                Debug.DrawRay(transform.position - transform.forward + (transform.up / 2), transform.right * range, Color.yellow);
            }
        }

        if (isDead == true)
        {
            spider_animator.SetBool("isDead", isDead);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (String.Equals(collision.transform.tag, "Bullet"))
        {
            Destroy(this.gameObject, 2);
            isDead = true;
            Destroy(collision.gameObject);
            if(!spider_animator.GetBool("isDead"))
                PlayersHealth.score += 1;
        }
    }

    private IEnumerator waitToAttack()
    {
        yield return new WaitForSeconds(1.0f);
    }
}
