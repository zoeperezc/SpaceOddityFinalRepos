using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class Blue : MonoBehaviour
{
    //SerialPort puertoDisplay;
    SerialPort puertoExtras;

    public Sprite[] animationSprites;

    public float animationTime = 5.0f;
    private SpriteRenderer _spriteRenderer;
    private int _animationFrame;

    public System.Action killed;

    //public PlayerMov playerM;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        //puertoDisplay = FindObjectOfType<PlayerMov>().puerto1;
        puertoExtras = FindObjectOfType<PlayerMov>().puerto2;

        InvokeRepeating(nameof(AnimateSprite), this.animationTime, this.animationTime);

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
    }

    private void AnimateSprite()
    {
        _animationFrame++;

        if (_animationFrame >= this.animationSprites.Length)
        {
            _animationFrame = 0;
        }

        _spriteRenderer.sprite = this.animationSprites[_animationFrame];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.gameObject.GetComponent<PlayerDamage>().life = 0;

            //playerM.Arduino("muere");

            if (puertoExtras.IsOpen)
            {
                puertoExtras.WriteLine("muere");
            }
        }
    }
}