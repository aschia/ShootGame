using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float maxRadius = 1f;
    public float interval = 5f;
    public GameObject spawnObj = null;
    private Transform origin = null;

    private void Awake()
    {
        origin = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.Log("lol");
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 0f, interval);
    }

    void Spawn()
    {
        if (origin == null) return;

        Vector3 spawnPos = origin.position + Random.onUnitSphere * maxRadius;
        spawnPos = new Vector3(spawnPos.x, 0f, spawnPos.z);
        Instantiate(spawnObj, spawnPos, Quaternion.identity);
    }
}
