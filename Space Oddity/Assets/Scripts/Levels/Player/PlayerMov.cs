using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using Leap.Unity;

public class PlayerMov : MonoBehaviour
{
    //public SerialPort puerto1 = new SerialPort("COM5", 9600); //display
    public SerialPort puerto2 = new SerialPort("COM7", 9600); //demás

    public Camera mainCamera;

    public CapsuleHand L;

    private float offsetY = 20.0f;
    private float offsetX = 40.0f;


    private void Start()
    {
        //puerto1.ReadTimeout = 40;

        //Arduino("YesStart");
    }

    private void FixedUpdate()
    {
        if (L.GetLeapHand() != null)
        {
            Vector3 newpos = new Vector3(L.GetLeapHand().PalmPosition.x * offsetX, L.GetLeapHand().PalmPosition.z * offsetY, 0);
            transform.position = newpos;
        }
    }
    
    //public void Arduino(string comando)
    //{
    //    if (puerto1.IsOpen)
    //    {
    //        puerto1.Close();
    //    }

    //    else
    //    {
    //        puerto2.Close();

    //        puerto1.Open();

    //        if (puerto1.IsOpen)
    //        {
    //            puerto1.WriteLine(comando);
    //        }

    //        puerto1.Close();

    //        puerto2.Open();
    //    }
    //}
}