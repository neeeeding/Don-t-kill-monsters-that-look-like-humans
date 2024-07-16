using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaedRoll : MonoBehaviour
{
    private float z;
    private float rollSpeed = 150f;
    private float speed = 1f;
    private float moveNum;
    private Vector3 moveDir;
    private bool isReturn = true;

    private ChimeraBattle body;

    private void Awake()
    {
        body = FindAnyObjectByType<ChimeraBattle>();
    }

    private void Update()
    {
        if (isReturn)
        {
            StartCoroutine(TimeD());
        }
        Roll();
        if (transform.position.x >= -398.7f && transform.position.x <= -381.6f)
        {
            move();
        }
        else
        {
            Retrun();
        }
    }

    private void Roll()
    {
        z += Time.deltaTime * rollSpeed;
        transform.rotation = Quaternion.Euler(0, 0, z);
    }

    private void move()
    {
        if (moveDir == Vector3.zero)
        {
            moveNum = Random.Range(1, 3);
            switch (moveNum)
            {
                case 1:
                    moveDir += new Vector3(1f, 0f, 0f);
                    break;
                case 2:
                    moveDir += new Vector3(-1f, 0f, 0f);
                    break;
            }
        }
        transform.position += moveDir * speed * Time.deltaTime;
    }

    private void Retrun()
    {
        switch (moveNum)
        {
            case 1:
                moveDir += new Vector3(-1f, 0f, 0f);
                break;
            case 2:
                moveDir += new Vector3(1f, 0f, 0f);
                break;
        }
        transform.position += moveDir * speed * Time.deltaTime;
    }

    private IEnumerator TimeD()
    {
        isReturn = true;
        yield return new WaitForSeconds(1f);
        isReturn = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("chimera") && !isReturn)
        {
            body.HeadPool.Push(gameObject);
            gameObject.SetActive(false);
            body.isRoll = false;
            isReturn = true;
            moveDir = Vector3.zero;
            if (moveDir == Vector3.zero)
            {
                moveNum = Random.Range(1, 3);
            }
        }
    }
}
