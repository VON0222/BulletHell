using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject player;

    public static Action OnEnemySpawned;
    public static Action OnEnemyDespawned;
    
    public float bulletSpeed = 40f;
    public int numberOfBullets = 5;
    public float circleRadius = 5f;
    public float fireRate = 3f;
    private float nextFireTime;

    public void OnEnable()
    {
        TimeManager.OnSecondChanged += TimeCheck;
        OnEnemySpawned?.Invoke();
    }

    private IEnumerator SpawnAtack()
    {
        float startAngle = UnityEngine.Random.Range(0f, 360f);

        for (int i = 0; i < numberOfBullets; i++)
        {
            float angle = startAngle + i * 360f / numberOfBullets;
            Vector3 spawnPosition = transform.position + Quaternion.Euler(0, angle, 0) * (Vector3.right * circleRadius);

            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.Euler(0, angle, 0));

            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.velocity = bullet.transform.right * bulletSpeed;

            yield return null;
        }
    }

    void Update()
    {
        if(player)
        {
            if (TimeManager.Second <= 30)
            {
                if (Time.time > nextFireTime)
                {
                    StartCoroutine(SpawnAtack());

                    nextFireTime = Time.time + 1f / fireRate;
                }
            }
        }
    }

    private void TimeCheck()
    {
        if(player)
        {
            if(TimeManager.Minute == 0)
            {
                if(TimeManager.Second == 1 || TimeManager.Second == 11 || TimeManager.Second == 21)
                {
                    StartCoroutine(Move1());
                }

                if(TimeManager.Second == 6 || TimeManager.Second == 16 || TimeManager.Second == 26)
                {
                    StartCoroutine(Move2());
                }
                
                if(TimeManager.Minute == 0 && TimeManager.Second == 31)
                {
                    StartCoroutine(Move3());
                }

                if(TimeManager.Minute == 0 && TimeManager.Second == 33)
                {
                    Destroy(gameObject);
                    OnEnemyDespawned?.Invoke();
                }
            }
        }
    }

    private IEnumerator Move1()
    {
        transform.position = new Vector3(-43.9f,2f,10f);
        Vector3 targetPos = new Vector3(-94.6f,2f,10f);

        Vector3 currentPos = transform.position;

        float timeElapsed = 0;
        float timeToMove = 4;

        while(timeElapsed < timeToMove){
            transform.position = Vector3.Lerp(currentPos,targetPos,timeElapsed/timeToMove);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator Move2()
    {
        transform.position = new Vector3(-94.6f,2f,10f);
        Vector3 targetPos = new Vector3(-43.9f,2f,10f);

        Vector3 currentPos = transform.position;

        float timeElapsed = 0;
        float timeToMove = 4;

        while(timeElapsed < timeToMove){
            transform.position = Vector3.Lerp(currentPos,targetPos,timeElapsed/timeToMove);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator Move3()
    {
        transform.position = new Vector3(-43.9f,2f,10f);
        Vector3 targetPos = new Vector3(-43.9f,86f,10f);

        Vector3 currentPos = transform.position;

        float timeElapsed = 0;
        float timeToMove = 1;

        while(timeElapsed < timeToMove){
            transform.position = Vector3.Lerp(currentPos,targetPos,timeElapsed/timeToMove);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
}
