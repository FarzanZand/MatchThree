using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodePiece : MonoBehaviour
{

    // WHY IS THE IMAGE CHANGING!?!?! Initialize is called in Math3, but the sprite is only val-1. How is val random?

    public int value;
    public Point index;

    [HideInInspector]
    public Vector2 pos;
    [HideInInspector]
    public RectTransform rect;

    // bool updating;
    Image img;

    public void Initialize(int v, Point p, Sprite piece)
    {
        img = GetComponent<Image>();
        rect = GetComponent<RectTransform>();

        value = v;
        SetIndex(p);
        img.sprite = piece; // The randomness is a ??
    }

    public void ResetPosition()
    {
        pos = new Vector2(32 + (64 * index.x), -32 - (64 * index.y));
    }

    public void SetIndex(Point p)
    {
        index = p;
        ResetPosition();
        UpdateName();
    }
    
    void UpdateName()
    {
        transform.name = "Node [" + index.x + ", " + index.y + "]";
    }
}
