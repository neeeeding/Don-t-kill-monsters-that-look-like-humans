using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapAlarm : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            BulletUI.instance.isTrap = true;
            Destroy(gameObject);
        }
    }
}
