using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NodePiece : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    // WHY IS THE IMAGE CHANGING!?!?! Initialize is called in Math3, but the sprite is only val-1. How is val random?

    public int value;
    public Point index;

    [HideInInspector]
    public Vector2 pos;
    [HideInInspector]
    public NodePiece flipped;
    [HideInInspector]
    public RectTransform rect;

    bool updating;
    Image img;

    public void Initialize(int v, Point p, Sprite piece)
    {
        flipped = null;
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

    public void MovePosition(Vector2 move)
    {
        rect.anchoredPosition = move * Time.deltaTime * 16f; // Direct movement
    }

    public void MovePositionTo(Vector2 move)
    {
        rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, move, Time.deltaTime * 16f); // Lerp does a gradual movement from point A - B
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

    public bool UpdatePiece()
    {
        if(Vector3.Distance(rect.anchoredPosition, pos) > 1)
        {
            MovePositionTo(pos);
            updating = true;
            return true;
        }
        else
        {
            rect.anchoredPosition = pos;
            updating = false;
            return false;
        }
        // return false if not moving
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (updating) return;
        MovePieces.instance.MovePiece(this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        MovePieces.instance.DropPiece();
    }
}
