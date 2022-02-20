using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class MonsterSpawner : MonoBehaviour
{
    public List<Monster> availableMonsterPrefabs;
    public MeshFilter meshFilter;
    public AudioSource spawnAudioSource;

    private static Monster spawnedMonster;

    public static void MonsterCaptured()
    {
        spawnedMonster = null;
        FindObjectOfType<ARPlaneManager>().subsystem.Start();
    }

    private void SpawnMonster()
    {
        if (spawnedMonster != null) return;

        var selectedPrefab = availableMonsterPrefabs[Random.Range(0, availableMonsterPrefabs.Count)];
        var position = transform.position + meshFilter.mesh.bounds.center;

        spawnedMonster = Instantiate(selectedPrefab, position, Quaternion.identity);

        FindObjectOfType<ARPlaneManager>().subsystem.Stop();
        spawnAudioSource.Play();
    }

    private void Awake()
    {
        // Delay so ARFoundation can merge some planes
        Invoke(nameof(SpawnMonster), 2);
    }
}
