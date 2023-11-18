using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Result : MonoBehaviour
{
    public TextMeshProUGUI ResultText;
    public GameObject player;

    public void OnEnable()
    {
        BossBehaviour.OnEnd += Win;
    }

    void Update()
    {
        UpdateBulletCounterText();
    }

    void Win()
    {
        ResultText.text = "YOU WIN";
    }

    private void UpdateBulletCounterText()
    {
        if (player.GetComponent<PlayerController>().health <= 0)
        {
            ResultText.text = "GAME OVER";
            Destroy(gameObject);
        }
    }
}
