using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Confine : MonoBehaviour
{
    private TilemapCollider2D confineCollider;
    private TilemapRenderer confine;
    private bool isF =false;

    private void Awake()
    {
        confine = GetComponent<TilemapRenderer>();
        confineCollider = GetComponent<TilemapCollider2D>();
    }

    private void Update()
    {
        if (isF)
        {
            confineCollider.enabled = false;
            confine.enabled = false;
        }
        else
        {
            confineCollider.enabled = true;
            confine.enabled = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isF = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isF = false;
        }
    }
}
