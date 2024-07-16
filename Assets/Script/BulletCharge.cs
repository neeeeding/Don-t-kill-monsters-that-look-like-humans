using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCharge : MonoBehaviour
{
    private BliveruMove gun;

    private void Awake()
    {
        gun = FindAnyObjectByType<BliveruMove>();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gun.canFire += StartUI.instance.isHard ?  4: 3;
            Destroy(gameObject);
            StopScreen.instance.itemPage = StopScreen.instance.itemPage < 3 ? 3 : StopScreen.instance.itemPage;
        }
    }
}
