using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class LilacMissile : MonoBehaviour
{
    //SerialPort puertoDisplay;
    SerialPort puertoExtras;

    public Vector2 target;
    public int speed;

    public Transform player;

    //public PlayerMov playerM;

    private void Start()
    {
        //puertoDisplay = FindObjectOfType<PlayerMov>().puerto1;
        puertoExtras = FindObjectOfType<PlayerMov>().puerto2;

        //puertoDisplay.ReadTimeout = 40;

        puertoExtras.ReadTimeout = 40;
        puertoExtras.Open();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector2(player.position.x, player.position.y);

        Destroy(gameObject, 4.0f);

        //playerM = GetComponent<PlayerMov>();
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.gameObject.GetComponent<PlayerDamage>().life -= 1;

            //playerM.Arduino("pierdeVida");
            Destroy(gameObject);
            GameManager.GM.LilacMissile(this);

            if (puertoExtras.IsOpen)
            {
                puertoExtras.WriteLine("pierdeVida");
            }
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            Destroy(gameObject);
            GameManager.GM.LilacMissile(this);
        }
    }
}
