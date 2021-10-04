using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunner : MonoBehaviour
{
    public float shotDelay = 2f;
    bool canFire = false;

    public Transform[] turretTransforms;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("EnableFire", shotDelay);
    }

    // Update is called once per frame
    void Update()
    {
        if (canFire)
        {
            foreach (Transform T in turretTransforms)
            {
                Transform bullet = AmmoManager.SpawnAmmo(T.position, T.rotation, 0);
                bullet.GetComponent<Bullet>().affil = 0;
                bullet.eulerAngles = new Vector3(0, bullet.eulerAngles.y, 0);
            }
            canFire = false;
            Invoke("EnableFire", shotDelay);
        }
    }

    void EnableFire()
    {
        canFire = true;
    }
}
