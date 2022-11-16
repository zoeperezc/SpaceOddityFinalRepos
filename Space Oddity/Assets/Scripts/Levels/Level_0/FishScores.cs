using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishScores : MonoBehaviour
{
    private int  basicScore = 2;

    private int betterScore = 5;

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.gameObject.GetComponent<PlayerScore>().score += basicScore;
            Destroy(gameObject);
        }
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            other.gameObject.GetComponent<PlayerScore>().score += betterScore;
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        Destroy(gameObject, 4.0f);
    }
}
