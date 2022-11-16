using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;
using Leap.Unity;

public class CursorMenu : MonoBehaviour
{

    SerialPort puerto = new SerialPort("COM4", 9600);

    public Camera mainCamera;

    public CapsuleHand L;

    private float offsetY = 20.0f;
    private float offsetX = 40.0f;
    RectTransform myPos;
    private void Awake()
    {
        if (puerto.IsOpen)
        {
            puerto.WriteLine("MenuStart");
        }
        myPos = GetComponent<RectTransform>();
        offsetX = mainCamera.pixelWidth * 2;
        offsetY = mainCamera.pixelHeight * 2;
    }

    private void Start()
    {
        puerto.ReadTimeout = 40;
        puerto.Open();
    }

    private void FixedUpdate()
    {
        if (L.GetLeapHand() != null)
        {
            Vector3 newpos = new Vector3(L.GetLeapHand().PalmPosition.x * offsetX, L.GetLeapHand().PalmPosition.z * offsetY, 0);
            //transform.position = newpos;
            myPos.anchoredPosition = newpos;
        }
    }
}
