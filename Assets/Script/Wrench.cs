using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrench : MonoBehaviour
{
    private BliveruMove bliveru;

    private void Awake()
    {
        bliveru = FindAnyObjectByType<BliveruMove>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bliveru.drillCan = true;
            StopScreen.instance.itemPage = 4;
            StopScreen.instance.enemyPage = 2;
            BulletUI.instance.isDrill = true;
            StopScreen.instance.explanationText[0] = "드릴 기능을 추가된 블리베루. 이제 원격 조종이 가능하다. 적을 밀어내는 것도 가능해 좋다!";
            Destroy(gameObject);
        }

    }
}
