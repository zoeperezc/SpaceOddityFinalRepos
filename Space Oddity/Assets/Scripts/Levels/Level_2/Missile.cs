using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class Missile : MonoBehaviour
{
    //SerialPort puertoDisplay;
    SerialPort puertoExtras;

    //public PlayerMov playerM;

    public Vector3 direction;

    public float speed;

    private void Start()
    {
        //puertoDisplay = FindObjectOfType<PlayerMov>().puerto1;
        puertoExtras = FindObjectOfType<PlayerMov>().puerto2;

        //puertoDisplay.ReadTimeout = 40;
        //puerto1.Open();

        //playerM = GetComponent<PlayerMov>();

        puertoExtras.ReadTimeout = 40;
        if (puertoExtras.IsOpen)
        {

        }

        else
        {
            puertoExtras.Open();
        }

        Destroy(this.gameObject, 4.0f);
    }

    private void Update()
    {
        this.transform.position += this.direction * this.speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //playerM.Arduino("pierdeVida");
            other.gameObject.GetComponent<PlayerDamage>().life -= 1;
            if (puertoExtras.IsOpen)
            {
                puertoExtras.WriteLine("pierdeVida");
            }
            Destroy(this.gameObject);
            GameManager.GM.Missile(this);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            Destroy(this.gameObject);
            GameManager.GM.Missile(this);
        }
    }
}