/*
 * Created by aschia
 * Date Created: Sept 22
 * 
 * Last Edited: Sept 22
 * 
 * Desc: Manage boolet
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    //vars
    public static AmmoManager AmmoManagerSingleton = null;
    #region AmmoManager Singleton
    void SetAmmoManager()
    {
        if (AmmoManagerSingleton == null)
        {
            AmmoManagerSingleton = this;
        }
        else
        {
            AmmoManagerSingleton = null;
        }
    }
    #endregion

    public GameObject ammoPrefab = null;
    public int poolSize = 100;
    public Queue<Transform> ammoQueue;

    private GameObject[] ammoArray;

    private void Awake()
    {
        SetAmmoManager();

        if (AmmoManagerSingleton == null) return;

        ammoArray = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            ammoArray[i] = Instantiate(ammoPrefab, Vector3.zero, Quaternion.identity, transform) as GameObject;
            Transform objTransform = ammoArray[i].transform;

            ammoQueue.Enqueue(objTransform);
            ammoArray[i].SetActive(false);
        }
    }
    public static Transform SpawnAmmo(Vector3 pos, Quaternion rot)
    {
        Transform spawnedAmmo = AmmoManagerSingleton.ammoQueue.Dequeue();
        spawnedAmmo.gameObject.SetActive(true);
        spawnedAmmo.position = pos;
        spawnedAmmo.localRotation = rot;
        AmmoManagerSingleton.ammoQueue.Enqueue(spawnedAmmo);

        return spawnedAmmo;
    }
}
