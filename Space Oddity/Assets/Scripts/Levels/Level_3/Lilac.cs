using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class Lilac : MonoBehaviour
{

    SerialPort puerto = new SerialPort("COM4", 9600);

    public Sprite[] animationSprites;

    public float animationTime = 5.0f;
    private SpriteRenderer _spriteRenderer;
    private int _animationFrame;

    public System.Action killed;

    public int life = 3;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), this.animationTime, this.animationTime);

        puerto.ReadTimeout = 40;
        puerto.Open();
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            life--;

            if (life <= 0)
            {
                GameManager.GM.Invader3Destroyed(this);
                this.killed.Invoke();
                this.gameObject.SetActive(false);
            }
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.gameObject.GetComponent<PlayerDamage>().life = 0;
            if (puerto.IsOpen)
            {
                puerto.WriteLine("muere");
            }
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
}