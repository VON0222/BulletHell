using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossBehaviour : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject player;

    public static Action OnEnd;
    public static Action OnBossSpawned;
    public static Action OnBossDespawned;

    public float bulletSpeed = 40f;
    public int numberOfBullets = 12;
    public float circleRadius = 5f;
    public float fireRate = 3f;

    private float nextFireTime;
    private float supportFireRate = 10f;
    private float supportNextFireTime;

    float startAngle = 0f;
    float startAngle2 = 160f;
    float[] angles = {180f, 190f, 200f};
    float[] supportAngles = {90f, 270f};
    
    int iter = 0;
    int acum = 0;
    int count = 0;

    public void OnEnable()
    {
        OnBossSpawned?.Invoke();
        TimeManager.OnSecondChanged += TimeCheck;
    }

    private IEnumerator Routine1()
    {
        for (int i = 0; i < 2; i++)
        {
            float angle = startAngle;
            if(i == 1)
            {
                angle += 180f;
            }
            Vector3 spawnPosition = transform.position + Quaternion.Euler(0, angle, 0) * (Vector3.right * circleRadius);

            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.Euler(0, angle, 0));

            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.velocity = bullet.transform.right * bulletSpeed;

            yield return null;
        }
    }

    private IEnumerator Routine2()
    {
        for (int i = 0; i < 3; i++)
        {
            float angle = angles[i];
            Vector3 spawnPosition = transform.position + Quaternion.Euler(0, angle, 0) * (Vector3.right * circleRadius);

            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.Euler(0, angle, 0));

            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.velocity = bullet.transform.right * bulletSpeed;

            yield return null;
        }
    }

    private IEnumerator Routine3()
    {
        float angle = startAngle2;
        Vector3 spawnPosition = transform.position + Quaternion.Euler(0, angle, 0) * (Vector3.right * circleRadius);

        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.Euler(0, angle, 0));

        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.velocity = bullet.transform.right * bulletSpeed;

        yield return null;
    }

    private IEnumerator SupportRoutine()
    {
        for (int i = 0; i < 2; i++)
        {
            float angle = supportAngles[i];
            Vector3 spawnPosition = transform.position + Quaternion.Euler(0, angle, 0) * (Vector3.right * circleRadius);

            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.Euler(0, angle, 0));

            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.velocity = bullet.transform.right * bulletSpeed * 2f;

            yield return null;
        }
    }

    void Update()
    {
        if (player)
        {
            if(TimeManager.Minute == 0)
            {
                if (TimeManager.Second >= 37 && TimeManager.Second <= 47){
                    if (Time.time > nextFireTime)
                    {
                        StartCoroutine(Routine1());

                        nextFireTime = Time.time + 1f / (fireRate + 7);

                        startAngle += 10f;
                    }
                }

                if (TimeManager.Second >= 48 && TimeManager.Second <= 58){

                    bulletSpeed = 80f;
                    if (Time.time > supportNextFireTime)
                    {
                        StartCoroutine(SupportRoutine());

                        supportNextFireTime = Time.time + 1f / supportFireRate;
                    }

                    if (Time.time > nextFireTime)
                    {
                        if(iter == 0)
                        {
                            iter++;
                        }
                        else if(iter <= 2)
                        {
                            angles[0] -= 10f;
                            angles[1] -= 10f;
                            angles[2] -= 10f;
                            iter++;
                        }
                        else if(iter == 3)
                        {
                            angles[0] += 10f;
                            angles[1] += 10f;
                            angles[2] += 10f;
                            iter++;
                        }
                        else
                        {
                            angles[0] += 10f;
                            angles[1] += 10f;
                            angles[2] += 10f;
                            iter = 1;
                        }

                        StartCoroutine(Routine2());

                        nextFireTime = Time.time + 1f / fireRate;
                    }
                }
            }

            if (TimeManager.Minute == 1 && TimeManager.Second <= 10){

                if (Time.time > supportNextFireTime)
                {
                    StartCoroutine(SupportRoutine());

                    supportNextFireTime = Time.time + 1f / supportFireRate;
                }

                if (Time.time > nextFireTime)
                {
                    StartCoroutine(Routine3());

                    nextFireTime = Time.time + 1f / (fireRate + 7);
                    if(acum < 10)
                    {
                        startAngle2 += 10f;
                        acum++;
                    }
                    else
                    {
                        startAngle2 -= 10f;
                        count++;
                    }
                    if(count > 10)
                    {
                        acum = 0;
                        count = 0;
                    }
                }
            }
        }
    }

    private void TimeCheck()
    {
        if (player)
        {
            if(TimeManager.Minute == 0)
            {
                if(TimeManager.Second == 32)
                {
                    StartCoroutine(StartingMove());
                }

                if(TimeManager.Second == 49)
                {
                    StartCoroutine(SecondMove());
                }
            }
            
            if(TimeManager.Minute == 1)
            { 
                if(TimeManager.Second == 11)
                {
                    OnEnd?.Invoke();
                    StartCoroutine(Escape());
                }

                if(TimeManager.Second == 18)
                {
                    OnBossDespawned?.Invoke();
                    Destroy(gameObject);
                }
            }
        }
    }

    private IEnumerator StartingMove()
    {
        transform.position = new Vector3(9f,12f,10f);
        Vector3 targetPos = new Vector3(-50f,3f,10f);

        Vector3 currentPos = transform.position;

        float timeElapsed = 0;
        float timeToMove = 5;

        while(timeElapsed < timeToMove){
            transform.position = Vector3.Lerp(currentPos,targetPos,timeElapsed/timeToMove);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator SecondMove()
    {
        transform.position = new Vector3(-50f,3f,10f);
        Vector3 targetPos = new Vector3(-25f,3f,10f);

        Vector3 currentPos = transform.position;

        float timeElapsed = 0;
        float timeToMove = 6;

        while(timeElapsed < timeToMove){
            transform.position = Vector3.Lerp(currentPos,targetPos,timeElapsed/timeToMove);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator Escape()
    {
        transform.position = new Vector3(-25f,3f,10f);
        Vector3 targetPos = new Vector3(-25f,86f,10f);

        Vector3 currentPos = transform.position;

        float timeElapsed = 0;
        float timeToMove = 3;

        while(timeElapsed < timeToMove){
            transform.position = Vector3.Lerp(currentPos,targetPos,timeElapsed/timeToMove);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
}
