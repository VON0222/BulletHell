using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageLights : MonoBehaviour
{
    public GameObject player;
    private Light myLight;

    float flashDuration = 0.2f;
    float intensityNum = 10f;
    int num = 0;

    void Start()
    {
        myLight = GetComponent<Light>();
        num = player.GetComponent<PlayerController>().health;
    }

    void Update()
    {
        if (player == null)
        {
            myLight.intensity = 10f;
        }
        else {
            if (player.GetComponent<PlayerController>().health < num)
            {
                num = player.GetComponent<PlayerController>().health;
                StartCoroutine(flash());
            }
        }
    }

    private IEnumerator flash()
    {
        myLight.intensity = intensityNum;

        yield return new WaitForSeconds(flashDuration);

        myLight.intensity = 0f;
    }
}
