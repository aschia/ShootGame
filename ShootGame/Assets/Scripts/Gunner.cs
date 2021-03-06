using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunner : MonoBehaviour
{
    public float shotDelay = 2f;
    public int shotsPerBurst = 3;
    bool canFire = false;
    float cooldown = 0;
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
            if (cooldown > 0)
            {
                cooldown -= Time.deltaTime;
                return;
            }

            foreach (Transform T in turretTransforms)
            {
                Transform bullet = AmmoManager.SpawnAmmo(T.position, T.rotation, 0);
                bullet.GetComponent<Bullet>().affil = 0;
                bullet.eulerAngles = new Vector3(0, bullet.eulerAngles.y, 0);
                bullet.transform.localScale = new Vector3(1.5f,1.5f,1.5f);
                bullet.GetComponent<Bullet>().enabled = false;
                bullet.GetComponent<Bullet>().enabled = true;
            }
            cooldown = shotDelay / shotsPerBurst;
        }
    }

    void EnableFire()
    {
        canFire = true;
        mv.enabled = false;
        Invoke("EnableMove", shotDelay);
    }

    void EnableMove()
    {
        canFire = false;
        mv.enabled = true;
        Invoke("EnableFire",shotDelay);
    }
}
