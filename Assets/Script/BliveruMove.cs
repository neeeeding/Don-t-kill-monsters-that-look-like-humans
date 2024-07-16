using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BliveruMove : MonoBehaviour
{
    public GameObject lie; //�÷��̾�

    public GameObject bulletPrefabs; //�Ѿ�
    public GameObject Fire; //�Ѿ� �߻� �Ա�
    public GameObject bulletWarehouse;

    private float colltime = 0.5f;
    public bool isColltime = false;

    private Vector3 bliveruPos; //���� ������(�ڽ���) ��ġ
    private Vector3 liePos; //�����簡 ��ġ�ؾ��ϴ� ��ġ(�÷��̾��� ��ġ���� ��¦ ��)

    private bool playermove = false; //�÷��̾ �����̳���? �ƴϿ�.

    public bool flip; //ȸ�� �ϴ����� ���� �ο� ����

    public int canFire;

    public bool drillCan;

    public Stack<GameObject> bulletPool = new Stack<GameObject>(); //�Ѿ��� ������ ����

    private void Start()
    {
        BulletStorage(); //�Ѿ� 30�� ���� ��ŵ�ô�.
        canFire = 15;
        drillCan = false;
    }


    private void Update()
    {
        bliveruPos = transform.position; //������(�ڽ���) ��ġ
        liePos = new Vector3(lie.transform.position.x + 0.8f, lie.transform.position.y + 0.99f, 0); //�����簡 ��ġ�ؾ��ϴ� ��ġ(�÷��̾��� ��ġ���� ��¦ ��)
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
