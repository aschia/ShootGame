using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float maxRadius = 1f;
    public float interval = 5f;
    public GameObject[] spawnObj = null;
    private Transform origin = null;

    private void Awake()
    {
        origin = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 0f, interval);
    }

    void Spawn()
    {
        if (origin == null) return;

        int spawnChoice = Random.Range(0, spawnObj.Length);

        Vector3 spawnPos = origin.position + Random.onUnitSphere * maxRadius;
        spawnPos = new Vector3(spawnPos.x, 0f, spawnPos.z);
        Instantiate(spawnObj[spawnChoice], spawnPos, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 centerp = new Vector3(transform.position.x, 0.0f, transform.position.y);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(centerp, new Vector3(maxRadius, 0.0f, maxRadius));
    }
}
