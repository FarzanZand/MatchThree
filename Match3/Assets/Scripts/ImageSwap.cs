using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

// Triggered when killPiece runs in Match3

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
