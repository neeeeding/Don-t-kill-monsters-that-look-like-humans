using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy4;
    public GameObject spawner;
    public int enemy4Count;
    private Collider2D spawnStart;
    public bool isEnd = false;

    private void Awake()
    {
        spawnStart = GetComponent<Collider2D>();
    }
    
    [SerializeField]private List<Transform> enemy4spawn;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemy4Count = isEnd ? 1 : (StartUI.instance.isHard ? 10 : 4);
            for (int i = 0; i<enemy4Count; i++)
            {
                Vector3 enemy4spawnPoint = enemy4spawn[Random.Range(0,enemy4spawn.Count)].position;
                GameObject enemy = Instantiate(enemy4, spawner.transform);
                enemy.transform.position = enemy4spawnPoint;
            }
            Destroy(spawnStart);
        }
    }
}
