using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletFiring : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D rigid;

    private BliveruMove bliveru;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        bliveru = FindAnyObjectByType<BliveruMove>();
    }
    private void FixedUpdate()
    {
        rigid.velocity = transform.right * speed;
        StartCoroutine(waitDestroy());
    }

    private IEnumerator waitDestroy()
    {
       yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
        bliveru.bulletPool.Push(gameObject);
    }
}
