using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLayer : MonoBehaviour
{
    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponentInParent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        sprite.sortingLayerName = "PlayerUp";
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        sprite.sortingLayerName = "Player";
    }
}
