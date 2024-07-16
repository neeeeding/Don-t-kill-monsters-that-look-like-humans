using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBlockTheRoad : MonoBehaviour
{
    public GameObject block;
    public GameObject bonusBoss;

    private void Start()
    {
        block.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            block.SetActive(true);
            bonusBoss.SetActive(true);
            BulletUI.instance.isBonus = true;
            Destroy(gameObject);
        }
    }
}
