using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BliveruMove : MonoBehaviour
{
    public GameObject lie; //플레이어

    public GameObject bulletPrefabs; //총알
    public GameObject Fire; //총알 발사 입구
    public GameObject bulletWarehouse;

    private float colltime = 0.5f;
    public bool isColltime = false;

    private Vector3 bliveruPos; //현재 블리베루(자신의) 위치
    private Vector3 liePos; //블리베루가 위치해야하는 위치(플레이어의 위치보다 살짝 위)

    private bool playermove = false; //플레이어가 움직이나요? 아니요.

    public bool flip; //회전 하는지에 대한 부울 변수

    public int canFire;

    public bool drillCan;

    public Stack<GameObject> bulletPool = new Stack<GameObject>(); //총알을 보관할 스택

    private void Start()
    {
        BulletStorage(); //총알 30개 보관 시킵시다.
        canFire = 15;
        drillCan = false;
    }


    private void Update()
    {
        bliveruPos = transform.position; //블리베루(자신의) 위치
        liePos = new Vector3(lie.transform.position.x + 0.8f, lie.transform.position.y + 0.99f, 0); //블리베루가 위치해야하는 위치(플레이어의 위치보다 살짝 위)
        if (playermove == true)
        {
            transform.position = Vector3.Lerp(bliveruPos, liePos, 0.1f);
        }

        if(isColltime == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                flip = false;
                if(canFire > 0)
                {
                    bulletFire();
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                flip = true;
                if (canFire > 0)
                {
                    bulletFire();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playermove = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playermove = false;
        }
    }

    private void BulletStorage()
    {
        for(int i = 0; i<=5; i++)
        {
            GameObject bullet = Instantiate(bulletPrefabs, bulletWarehouse.transform);
            bulletPool.Push(bullet);
            bullet.SetActive(false);
        }
    }

    private void bulletFire()
    {
        canFire--;

        Quaternion flipAngle;
        GameObject bullet = bulletPool.Pop();
        bullet.SetActive(true);
        bullet.transform.position = Fire.transform.position;

        float angle = flip ? 180 : 0;
        flipAngle = Quaternion.Euler(0, angle, 0);
        bullet.transform.rotation = Fire.transform.rotation * flipAngle;

        StartCoroutine(firebefore());

    }

    private IEnumerator firebefore()
    {
        isColltime = true;
        yield return new WaitForSeconds(colltime);
        isColltime = false;
    }
}
