using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ImageSwap : MonoBehaviour
{
    private Animator anim;


    void Start()
    {
        anim = GetComponent<Animator>(); 
    }

    public void TriggerScore()
    {
        anim.SetTrigger("Score"); 
    }
}
