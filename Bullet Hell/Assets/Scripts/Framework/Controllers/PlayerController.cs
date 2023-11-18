using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bulletPrefab;

    public float speed = 50.0f;
    public float horizontalInput;
    public float forwardInput;
    public int Mode = 0;
    public int health = 3;

    public KeyCode switchKey;
    public KeyCode shootKey;

    void Start()
    {
        
    }
    
    void Update()
    {
        if(health <= 0){
            Destroy(gameObject);
        }

        if(Mode == 0){
            speed = 50.0f;

            horizontalInput = Input.GetAxis("Horizontal");
            forwardInput = Input.GetAxis("Vertical");

            if(GetComponent<Rigidbody>().position.x > -115){
                if(forwardInput < 0){
                    transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
                }
            }

            if(GetComponent<Rigidbody>().position.x < -9){
                if(forwardInput > 0){
                    transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
                }
            }

            if(GetComponent<Rigidbody>().position.z < 37){
                if(horizontalInput < 0){
                    transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
                }
            }

            if(GetComponent<Rigidbody>().position.z > -16){
                if(horizontalInput > 0){
                    transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
                }
            }
        }

        if(Mode == 1){
            speed = 25.0f;

            horizontalInput = Input.GetAxis("Horizontal");
            forwardInput = Input.GetAxis("Vertical");

            if(GetComponent<Rigidbody>().position.x > -115){
                if(forwardInput < 0){
                    transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
                }
            }

            if(GetComponent<Rigidbody>().position.x < -9){
                if(forwardInput > 0){
                    transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
                }
            }

            if(GetComponent<Rigidbody>().position.z < 37){
                if(horizontalInput < 0){
                    transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
                }
            }

            if(GetComponent<Rigidbody>().position.z > -16){
                if(horizontalInput > 0){
                    transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
                }
            }
        }

        if(Input.GetKeyDown(switchKey))
        {
            if(Mode == 0){
                Mode = 1;
            }
            else{
                Mode = 0;
            }
        }

        if(Input.GetKeyDown(shootKey))
        {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        float angle = 0f;
        Vector3 spawnPosition = transform.position + Quaternion.Euler(0, angle, 0) * (Vector3.right * 5f);

        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.Euler(0, angle, 0));

        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.velocity = bullet.transform.right * 40f;

        yield return null;
    }
}
