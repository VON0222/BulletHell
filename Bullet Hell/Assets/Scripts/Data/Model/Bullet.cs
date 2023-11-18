using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    public static Action OnBulletSpawned;
    public static Action OnBulletDespawned;

    void Start()
    {
        OnBulletSpawned?.Invoke();
    }

    void Update()
    {
        if(GetComponent<Rigidbody>().position.x < -115 || GetComponent<Rigidbody>().position.x > -9 || GetComponent<Rigidbody>().position.z > 37 || GetComponent<Rigidbody>().position.z < -16)
        {
            OnBulletDespawned?.Invoke();
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "Player"){
            OnBulletDespawned?.Invoke();
            Destroy(gameObject);
            collision.gameObject.GetComponent<PlayerController>().health -= 1;
        }
    }

}
