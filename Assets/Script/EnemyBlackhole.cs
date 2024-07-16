using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlackhole : MonoBehaviour
{
    private float decision;
    private Vector3 moveDir;
    private float speed = 1f;

    public float maxSize = 2f;
    public float minSize = 1f;
    private Vector3 minScale = new Vector3(1,1,1);
    private Vector3 maxScale = new Vector3(2, 2, 2);
    private float sizeSpeed = 0.5f;

    private void Start()
    {
        StartCoroutine(MovePosition());
        StartCoroutine(Size());
    }
    private void Update()
    {
        if(Time.timeScale != 0)
        {
            Move();
        }
    }
    IEnumerator MovePosition() //���� ���� ���� �������� ���� ���� �������� 1�� ���� ���ϱ�
    {
        while (true) //�� ���� ���� ���(����) �ݺ� �ϼ���.
        {
            decision = Random.Range(0, 3); //0~4������ ���� �������� ���ϼ���.
            yield return new WaitForSeconds(2f); //1�� ��ٸ�����.
        }
    }
    private void Move() //���� ���� ���� ������� �����̱�
    {
        moveDir = Vector3.zero; //�ʱ�ȭ
        switch (decision)
        {
            case 0:
                break;
            case 1:
                moveDir += new Vector3(0f, 1f, 0f);
                break;
            case 2:
                moveDir += new Vector3(0f, -1f, 0f);
                break;
        }
        transform.position += moveDir * speed * Time.deltaTime; //���� ���� �������� �����Խô�.
    }

    private IEnumerator Size()
    {
        while (true)
        {
            StartCoroutine(SizeUp());
            yield return new WaitForSeconds(1f);
            StartCoroutine(SizDown());
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator SizeUp()
    {
        float time = 0f;

        while (transform.localScale.x < maxSize)
        {
            transform.localScale = minScale * (1f + time * sizeSpeed);
            time += Time.deltaTime;

            if (transform.localScale.x >= maxSize)
            {
                time = 0f;
                break;
            }
            yield return null;
        }
    }

    private IEnumerator SizDown()
    {
        float time = 0f;

        while (transform.localScale.x > minSize)
        {
            transform.localScale = maxScale * (1f - time* sizeSpeed);
            time += Time.deltaTime;

            if (transform.localScale.x <= minSize)
            {
                transform.localScale = minScale;
                time = 0f;
                break;
            }
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Hp playerHp = collision.gameObject.GetComponent<Hp>();
            playerHp.hp = 0;
        }
    }
}
