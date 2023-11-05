using UnityEngine;
using System.Collections;

public class ItemWaveSystem : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5.0f;
    [SerializeField] private GameObject[] collectiblesPrefab;
    [SerializeField] private float spawnInterval = 3.0f;
    [SerializeField] private float minPosition = -3.0f;
    [SerializeField] private float maxPosition = 3.0f;

    private void Start()
    {
        StartCoroutine(SpawnRandomCollectible());
    }

    private IEnumerator SpawnRandomCollectible()
    {
        while (true)
        {
            int randomIndex = Random.Range(0, collectiblesPrefab.Length);
            GameObject randomCollectible = collectiblesPrefab[randomIndex];

            yield return new WaitForSeconds(spawnInterval);

            GameObject newCollectible = Instantiate(randomCollectible,
                new Vector3(this.transform.position.x, Random.Range(minPosition, maxPosition), 0f),
                Quaternion.identity);
            newCollectible.GetComponent<Rigidbody2D>().velocity = new Vector2(movementSpeed, 0f);
        }
    }
}
