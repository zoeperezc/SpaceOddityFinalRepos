using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueDamage : MonoBehaviour
{
    public int life = 1;
    
    void Update()
    {
        if (life <= 0)
        {
            Destroy(this.gameObject);
            GameManager.GM.Invader1Destroyed(this);
        }
    }
}
