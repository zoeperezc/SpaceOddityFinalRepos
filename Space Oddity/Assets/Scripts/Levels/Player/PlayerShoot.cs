 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class PlayerShoot : MonoBehaviour
{
    SerialPort puertoExtras;

    public Bullet bulletprefab;
    private bool playerShoot = false;
    
    public float speed = 3;

    private float shootRate = 0.5f;
    private float nextShoot = 0.5f;

    void Start()
    {
        puertoExtras = FindObjectOfType<PlayerMov>().puerto2;

        puertoExtras.ReadTimeout = 140;
        if (puertoExtras.IsOpen)
        {

        }

        else
        {
            puertoExtras.Open();
        }

    }

    private void Update()
    {
        string Lectura = "";
        if (puertoExtras.IsOpen )
        {
            Lectura = puertoExtras.ReadLine();
            Debug.Log(Lectura);
            if (Lectura.Equals("PRESS") && Time.time > nextShoot)
            {
                nextShoot = Time.time + shootRate;
                Shoot();
                playerShoot = true;
            }
            else
            {
                playerShoot = false;
            }
        }
        else
        {
            Debug.Log("Puerto cerrado");
        }
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(this.bulletprefab, this.transform.position, Quaternion.identity);
    }

    private void FixedUpdate()
    {
        Debug.Log("Player Shooting " + playerShoot);
    }
}