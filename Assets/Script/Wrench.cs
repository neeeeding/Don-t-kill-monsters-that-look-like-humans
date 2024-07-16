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
            StopScreen.instance.explanationText[0] = "�帱 ����� �߰��� ������. ���� ���� ������ �����ϴ�. ���� �о�� �͵� ������ ����!";
            Destroy(gameObject);
        }

    }
}
