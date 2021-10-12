using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float maxRadius = 1f;
    public float interval = 5f;
    public GameObject[] spawnObj = null;
    public float[] intervalMultis = null;
    private Transform origin = null;

    bool hasSpawnedQueen = false;

    private void Awake()
    {
        origin = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Spawn",interval);
    }

    void Spawn()
    {
        if (origin == null || interval == -1) return;

        int spawnChoice = Random.Range(0, spawnObj.Length);

        Vector3 spawnPos = origin.position + Random.onUnitSphere * maxRadius;
        spawnPos = new Vector3(spawnPos.x, 0f, spawnPos.z);

        if (Mathf.Abs(Vector3.Distance(spawnPos, origin.position)) < 10) {
            Spawn();
            return;
        }

        Instantiate(spawnObj[spawnChoice], spawnPos, Quaternion.identity);

        if (intervalMultis != null) Invoke("Spawn", interval * intervalMultis[spawnChoice]);
        else Invoke("Spawn", interval);
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 centerp = new Vector3(transform.position.x, 0.0f, transform.position.y);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(centerp, new Vector3(maxRadius, 0.0f, maxRadius));
    }
}
