using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletCounter : MonoBehaviour
{
    public TextMeshProUGUI bulletCounterText;
    
    private int bulletCount = 0;

    public void OnEnable()
    {
        Bullet.OnBulletSpawned += IncrementBulletCount;
        Bullet.OnBulletDespawned += DecrementBulletCount;
    }

    public void IncrementBulletCount()
    {
        bulletCount++;
        UpdateBulletCounterText();
    }

    public void DecrementBulletCount()
    {
        bulletCount--;
        UpdateBulletCounterText();
    }

    private void UpdateBulletCounterText()
    {
        if (bulletCounterText != null)
        {
            bulletCounterText.text = "Bullets: " + bulletCount.ToString();
        }
    }
}

