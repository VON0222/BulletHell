using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyCounter : MonoBehaviour
{
    public TextMeshProUGUI enemyCounterText;
    
    private int enemyCount = 0;

    public void OnEnable()
    {
        BulletSpawner.OnSpawned += IncrementEnemyCount;
        BossBehaviour.OnBossSpawned += IncrementEnemyCount;
        BulletSpawner.OnDespawned += DecrementEnemyCount;
        BossBehaviour.OnBossDespawned += DecrementEnemyCount;
    }

    public void IncrementEnemyCount()
    {
        enemyCount++;
        UpdateEnemyCounterText();
    }

    public void DecrementEnemyCount()
    {
        enemyCount--;
        UpdateEnemyCounterText();
    }

    private void UpdateEnemyCounterText()
    {
        if (enemyCounterText != null)
        {
            enemyCounterText.text = "Enemies: " + enemyCount.ToString();
        }
    }
}