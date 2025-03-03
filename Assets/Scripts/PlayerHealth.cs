using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlayerHealth : MonoBehaviour
{
    private int hitCount = 0;
    private int maxHits = 5;

    public void TakeDamage()
    {
        hitCount++;
        Debug.Log($"Player hit {hitCount}/{maxHits}");
        if (hitCount >= maxHits)
        {
            Debug.Log("Player destroyed!");
            Destroy(gameObject);

#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
        }
    }
}
