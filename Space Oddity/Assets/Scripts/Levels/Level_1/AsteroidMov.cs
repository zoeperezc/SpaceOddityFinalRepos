using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMov : MonoBehaviour
{
    public float speed = 3;

    void Update()
    {
        transform.position += -transform.up * Time.deltaTime * speed;
    }
}