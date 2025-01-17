using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Hp playerHp = collision.gameObject.GetComponent<Hp>();
            playerHp.hp -= StartUI.instance.isHard ? 10 : 3;
        }
    }
}
