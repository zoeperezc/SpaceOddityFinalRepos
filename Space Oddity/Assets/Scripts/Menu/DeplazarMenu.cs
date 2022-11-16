using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplazarMenu : MonoBehaviour
{
    public RectTransform menutransform;
    public Vector3 Desplazo;
    public int pos;
    public int maxpos = 3;

    public Animator[] anims;

    void Start()
    {
        pos = 1;
        ActivarAnimator(0);

    }

    void Update()
    {
    
    }

    public void DesplazarMenuDer()
    {
        if (pos > 1)
        {
            menutransform.position += Desplazo;
            pos--;
            ActivarAnimator(pos -1);
        }
    }

    public void DesplazarMenuIzq()
    {
        if (pos < maxpos)
        {
            menutransform.position -= Desplazo;

            pos++;
            ActivarAnimator(pos -1);
        }
    }

    void DesactivarAnimators()
    {
        int count = 0;
        while (count < anims.Length)
        {
            anims[count].enabled = false;
            count++;
        }
    }

    void ActivarAnimator(int animpos)
    {
        anims[animpos].SetTrigger ("play");

    }
}
