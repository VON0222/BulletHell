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
        BulletSpawner.OnEnemySpawned += IncrementEnemyCount;
        BulletSpawner.OnEnemyDespawned += DecrementEnemyCount;
        BossBehaviour.OnEnemySpawned += IncrementEnemyCount;
        BossBehaviour.OnEnemyDespawned += DecrementEnemyCount;
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
