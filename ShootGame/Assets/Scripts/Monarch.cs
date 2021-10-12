using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monarch : MonoBehaviour
{
    public float shotDelay = 2f;
    public int shotsPerBurst = 3;
    bool canFire = false;
    float cooldown = 0;
    Mover mv = null;

    public int[] attacks = new int[] { 0, 1, 0, 1, 2, 3 };
    public int attackInd = 0;

    public Transform[] turretTransforms;
    public GameObject[] spawnables;
    public Spawner sp;
    bool sp_active = false;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("EnableFire", shotDelay);
        mv = GetComponent<Mover>();
        sp.interval = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!sp_active && GetComponent<Health>().hp <= GetComponent<Health>().hp * 0.66)
        {
            sp_active = true;
            sp.interval = 2;
        }

        /*GameManager.GM.bossDefeated = true;
        if (GetComponent<Health>().hp <= 0)
        {
            GameManager.GameOver();
        }*/

        if (canFire)
        {
            Transform bullet = null;
            switch (attacks[attackInd]) {
                case 0:
                    if (cooldown > 0)
                    {
                        cooldown -= Time.deltaTime;
                        return;
                    }

                    foreach (Transform T in turretTransforms)
                    {
                        bullet = AmmoManager.SpawnAmmo(T.position, T.rotation, 0);
                        bullet.GetComponent<Bullet>().affil = 0;
                        bullet.GetComponent<Bullet>().lifetime = 1f;
                        bullet.GetComponent<Bullet>().enabled = false;
                        bullet.GetComponent<Bullet>().enabled = true;
                        bullet.eulerAngles = new Vector3(0, bullet.eulerAngles.y, 0);
                    }
                    cooldown = (shotDelay / shotsPerBurst)/10;
                    break;

                case 1:
                    if (cooldown > 0)
                    {
                        cooldown -= Time.deltaTime;
                        return;
                    }

                    foreach (Transform T in turretTransforms)
                    {
                        bullet = AmmoManager.SpawnAmmo(T.position, T.rotation, 0);
                        bullet.GetComponent<Bullet>().affil = 0;
                        bullet.eulerAngles = new Vector3(0, bullet.eulerAngles.y, 0);
                        bullet.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                        bullet.GetComponent<Bullet>().enabled = false;
                        bullet.GetComponent<Bullet>().enabled = true;
                    }
                    cooldown = shotDelay / shotsPerBurst;
                break;

                case 2:
                    if (cooldown > 0)
                    {
                        cooldown -= Time.deltaTime;
                        return;
                    }

                    bullet = AmmoManager.SpawnAmmo(turretTransforms[0].position, turretTransforms[0].rotation, 0);
                    bullet.GetComponent<Bullet>().affil = 0;
                    bullet.eulerAngles = new Vector3(0, bullet.eulerAngles.y, 0);
                    bullet.transform.localScale = new Vector3(2f, 2f, 2f);
                    bullet.GetComponent<Bullet>().enabled = false;
                    bullet.GetComponent<Bullet>().enabled = true;

                    cooldown = shotDelay*2 / shotsPerBurst;
                break;

                /*case 3:
                    int spawnChoice = Random.Range(0, spawnables.Length);
                    Vector3 startpos = (transform.position - Vector3.one / shotsPerBurst / 2);
                    for (int i = 0; i < shotsPerBurst; i++)
                    {
                        Vector3 pos = startpos + (Vector3.one * i / shotsPerBurst);
                        GameObject tr = Instantiate(spawnables[spawnChoice], pos, Quaternion.identity);
                        tr.transform.position = new Vector3(tr.transform.position.x, 0, tr.transform.position.z);
                    }
                    canFire = false;
                break;*/
            }
        }
    }

    /*private void Update()
    {
        if (!sp_active && GetComponent<Health>().hp <= GetComponent<Health>().hp*0.66)
        {
            sp_active = true;
            sp.interval = 2;
        }

        GameManager.GM.bossDefeated = true;
        if (GetComponent<Health>().hp <= 0)
        {
            GameManager.GameOver();
        }
    }*/
    private void OnDestroy()
    {
        GameManager.GM.bossDefeated = true;
        if (GetComponent<Health>().hp <= 0)
        {
            GameManager.GameOver();
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

        attackInd++;
        if (attackInd >= attacks.Length) attackInd = 0;

        Invoke("EnableFire", shotDelay);
    }
}
