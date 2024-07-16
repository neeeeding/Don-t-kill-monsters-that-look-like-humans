using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageAdd : MonoBehaviour
{
    public bool isItem;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isItem)
            {
                StopScreen.instance.itemPage++;
            }
            else
            {
                StopScreen.instance.enemyPage++;
            }
            Destroy(gameObject);
        }
    }
}
