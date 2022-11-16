using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO.Ports;

public class PlayerDamage : MonoBehaviour
{
    //SerialPort puerto1 = new SerialPort("COM5", 9600); //display
    //SerialPort puerto2 = new SerialPort("COM6", 9600); //demás

    [SerializeField] private GameObject GameOverPanel;

    //SerialPort puertoDisplay;
    SerialPort puertoExtras;

    public int life = 7;

    //public PlayerMov playerM;

    public Animator animator;

    private void Start()
    {
        GameOverPanel.SetActive(false);
        //puertoDisplay = FindObjectOfType<PlayerMov>().puerto1;
        puertoExtras = FindObjectOfType<PlayerMov>().puerto2;

        //playerM = GetComponent<PlayerMov>();

        //puertoDisplay.ReadTimeout = 40;
        //puerto1.Open();

        puertoExtras.ReadTimeout = 40;
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
        if (life <= 0)
        {
            //playerM.Arduino("muere");

            animator.Play("PlayerDeath");
            Time.timeScale = 0;
            GameOverPanel.SetActive(true);

            if (puertoExtras.IsOpen)
            {
                puertoExtras.WriteLine("muere");
            }
        }
    }

    private void FixedUpdate()
    {
        Debug.Log("Player Life = " + life);
    }
}
