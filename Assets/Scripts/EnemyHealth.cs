using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private int hitCount = 0;
    private int maxHits = 1;

    public void TakeDamage()
    {
        hitCount++;
        Debug.Log($"enemy hit {hitCount}/{maxHits}");
        if (hitCount >= maxHits)
        {
            Debug.Log("enemy destroyed!");
            Destroy(gameObject);
        }
    }
}
