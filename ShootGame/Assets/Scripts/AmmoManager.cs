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
    public Queue<Transform> ammoQueue = new Queue<Transform>();

    public GameObject[] ammoArray;

    private void Awake()
    {
        /*SetAmmoManager();

        if (AmmoManagerSingleton == null) return;*/
        if (AmmoManagerSingleton != null)
        {
            Destroy(GetComponent<AmmoManager>());
            return;
        }

        AmmoManagerSingleton = this;


        ammoArray = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(ammoPrefab, Vector3.zero, Quaternion.identity, this.transform);
            ammoArray[i] = obj;
            Transform objTransform = ammoArray[i].transform;

            if (objTransform != null)
            {
                ammoQueue.Enqueue(objTransform);
                ammoArray[i].SetActive(false);
            }
            else
            {
                Debug.Log("Something went wrong queueing projectile slots.");
            }
        }
    }
    public static Transform SpawnAmmo(Vector3 pos,Quaternion rot,int affil)
    {
        Transform spawnedAmmo = AmmoManagerSingleton.ammoQueue.Dequeue();

        spawnedAmmo.gameObject.SetActive(true);
        spawnedAmmo.position = pos;
        spawnedAmmo.localRotation = rot;
        spawnedAmmo.GetComponent<Bullet>().affil = affil;
        AmmoManagerSingleton.ammoQueue.Enqueue(spawnedAmmo);

        return spawnedAmmo;
    }
}
