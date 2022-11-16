using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class AsteroidDamage : MonoBehaviour
{
    //SerialPort puerto1 = new SerialPort("COM5", 9600); //display
    //SerialPort puerto2 = new SerialPort("COM8", 9600); //demás

    //SerialPort puertoDisplay;
    SerialPort puertoExtras;

    //public PlayerMov playerM;

    private void Start()
    {
        //puertoDisplay = FindObjectOfType<PlayerMov>().puerto1;
        puertoExtras = FindObjectOfType<PlayerMov>().puerto2;

        //puertoDisplay.ReadTimeout = 40;
        //puerto1.Open();

        //playerM = FindObjectOfType<PlayerMov>();

        puertoExtras.ReadTimeout = 40;
        
        if (puertoExtras.IsOpen)
        { }
        
        else 
        {
            puertoExtras.Open();
        }

        Destroy(gameObject, 4.0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.gameObject.GetComponent<PlayerDamage>().life -= 1;
            Destroy(gameObject);
            GameManager.GM.AsteroidDestroyed(this);

            //playerM.Arduino("pierdeVida");

            if (puertoExtras.IsOpen)
            {
                puertoExtras.WriteLine("pierdeVida");
            }
        }
    }
}