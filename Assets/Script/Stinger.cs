using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stinger : MonoBehaviour
{
    private GameObject player;
    private ChimeraBattle body;

    private void Awake()
    {
        body = FindAnyObjectByType<ChimeraBattle>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        Chase();
    }

    private void Chase()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position, 0.02f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Hp playerHp = collision.gameObject.GetComponent<Hp>();
            playerHp.hp -= StartUI.instance.isHard ? 2 : 1;
            body.stingerPool.Push(gameObject);
            gameObject.SetActive(false);
        }
    }
}
