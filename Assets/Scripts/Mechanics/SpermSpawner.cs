using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpermSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject spermPrefab;

    [SerializeField]
    private float spermInterval = 3.5f;

    void Start()
    {
        StartCoroutine(spawnEnemy(spermInterval, spermPrefab));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(interval);
            GameObject newEnemy = Instantiate(enemy, new Vector2(this.transform.position.x, Random.Range(-2f, 2f)), Quaternion.identity);
        }

        StartCoroutine(spawnEnemy(interval, enemy));
    }
}
