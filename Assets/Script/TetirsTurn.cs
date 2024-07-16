using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetirsTurn : MonoBehaviour
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
        transform.position = Vector3.Lerp(transform.position, player.transform.position, 0.05f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == player)
        {
            Hp playerHp = player.GetComponent<Hp>();
            playerHp.hp -= StartUI.instance.isHard ? 2 : 1;
            Destroy(this);
        }
    }
}
