/*
 * Created by aschia
 * Date Created: Sept 20
 * 
 * Last Edited: Sept 20
 * 
 * Desc: Halthy
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // vars
    public bool destroyOnDeath = true;
    public bool canBeHit = true;
    public GameObject deathPartiPrefab = null;
    [SerializeField] private float _hp = 0;        // i have no idea why we were instructed to make it a float what the
    public int affil = 0;               // affiliation
    public float invulnTime = 0;
    enum affils
    {
        enemy = 0,
        ally = 1,
    }
    SpriteRenderer sr;

    private void Start()
    {
        sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    public float hp {
        get { return _hp; }
        set {
            // hit invuln
            if (value < hp)
            {
                if (!canBeHit) return;
                else if (invulnTime > 0)
                {
                    canBeHit = false;
                    Invoke("CanHit", invulnTime);
                    InvokeRepeating("FlashHit",0,invulnTime / 5f);
                }
            }
            _hp = value;
            if (_hp <= 0) {
                SendMessage("Die", SendMessageOptions.DontRequireReceiver);

                if (deathPartiPrefab != null) {
                    GameObject parti = Instantiate(deathPartiPrefab);
                    parti.transform.position = transform.position;
                    parti.transform.rotation = Quaternion.LookRotation(new Vector3(0,90,0), transform.up);
                }
                if (destroyOnDeath) {
                    Destroy(gameObject);
                }
            }
        }
    }

    void CanHit()
    {
        canBeHit = true;
        sr.color = Color.white;
        CancelInvoke("FlashHit");
    }

    void FlashHit()
    {
        if (sr.color == Color.white) sr.color = Color.red;
        else sr.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug healthtest
        /*if (Input.GetKeyDown(KeyCode.F)) {
            hp = 0;
        }*/
    }
}
