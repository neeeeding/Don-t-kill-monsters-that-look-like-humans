using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffin : MonoBehaviour
{
    public GameObject humanEnemy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Shake());
        }
    }

    private IEnumerator Shake()
    {
        float t = 1f;
        float shakePower = 0.2f;
        Vector3 origin = transform.position;
        while (t > 0f)
        {
            t -= 0.007f;
            transform.position = origin + (Vector3)Random.insideUnitCircle * shakePower * t;
            yield return null;
        }
        transform.position = origin;
        yield return new WaitForSeconds(0.2f);
        GameObject enemy = Instantiate(humanEnemy);
        enemy.transform.position = transform.position;
        Destroy(gameObject);
    }
}
