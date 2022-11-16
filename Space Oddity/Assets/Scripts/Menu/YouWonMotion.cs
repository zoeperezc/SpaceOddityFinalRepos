using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouWonMotion : MonoBehaviour
{
    public float speed = 1f;

    void SetTransformX(float n)
    {
        transform.position = new Vector3(n, -3, 0);
    }
    void Start()
    {
        SetTransformX(-12.0f);
    }
    void Update()
    { 
        transform.Translate(Vector3.forward * Input.GetAxis("Horizontal") * Time.deltaTime * speed);
    }
}
