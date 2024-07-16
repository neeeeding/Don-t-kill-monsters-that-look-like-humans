using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHumanConfinement : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            Rigidbody2D rigid = collision.gameObject.GetComponent<Rigidbody2D>();
            rigid.constraints = RigidbodyConstraints2D.None;
            rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
