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
    IEnumerator MovePosition() //변신 전의 적의 움직임을 정할 수를 랜덤으로 1초 마다 정하기
    {
        while (true) //이 안의 것을 계속(무한) 반복 하세요.
        {
            decision = Random.Range(0, 3); //0~4까지의 수를 랜덤으로 정하세요.
            yield return new WaitForSeconds(2f); //1초 기다리세요.
        }
    }
    private void Move() //적을 랜덤 수를 기반으로 움직이기
    {
        moveDir = Vector3.zero; //초기화
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
        transform.position += moveDir * speed * Time.deltaTime; //위에 것을 바탕으로 움직입시다.
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
