using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockHealth : MonoBehaviour
{
    private int hitCount = 0;
    private int maxHits = 10;

    public void TakeDamage()
    {
        hitCount++;
        Debug.Log($"shield hit {hitCount}/{maxHits}");
        if (hitCount >= maxHits)
        {
            Debug.Log("shield destroyed!");
            Destroy(gameObject);
        }
    }
}
