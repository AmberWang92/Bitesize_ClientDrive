using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkSpawner : NetworkBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject spawnPrefab;
    [SerializeField] List<GameObject> poolPrefabs;
    [SerializeField] float spawnDelay;
    bool spawnerEnabled;
    int counter;

    private void FixedUpdate()
    {
        if (!spawnerEnabled && IsServer && counter <= 8)
        {
            InvokeRepeating(nameof(SpawnObject), 0, spawnDelay);
            spawnerEnabled = true;
            counter++;

        }
    }

    void SpawnObject()
    {
        if (counter >= 8)
        {
            CancelInvoke(nameof(SpawnObject));
            return;
        }

        var instance = Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation);
        instance.GetComponent<NetworkObject>().Spawn();
        poolPrefabs.Add(instance);
        counter++;
        // instance.SetActive(false)
        StartCoroutine(DeactivateAfterDelay(instance, 5f));
    }

    IEnumerator DeactivateAfterDelay(GameObject instance, float delay)
    {
        yield return new WaitForSeconds(delay);
        instance.SetActive(false);
    }

}