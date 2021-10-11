using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBreather : MonoBehaviour
{
    public float shotDelay = 0.2f;
    bool canFire = true;
    Mover mv = null;

    public Transform[] turretTransforms;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("EnableFire", shotDelay);
        mv = GetComponent<Mover>();
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
                bullet.GetComponent<Bullet>().lifetime = 1f;
                bullet.GetComponent<Bullet>().enabled = false;
                bullet.GetComponent<Bullet>().enabled = true;
                bullet.eulerAngles = new Vector3(0, bullet.eulerAngles.y, 0);
                bullet.GetComponent<Bullet>().enabled = false;
                bullet.GetComponent<Bullet>().enabled = true;
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
