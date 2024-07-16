using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazooka : MonoBehaviour
{
    private GameObject player;
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
        transform.position = Vector3.Lerp(transform.position, player.transform.position - new Vector3(0, 1, 0), 0.05f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Hp playerHp = collision.gameObject.GetComponent<Hp>();
            playerHp.hp -= 10;
            Destroy(gameObject);
        }
    }
}
