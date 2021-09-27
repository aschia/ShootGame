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
    public GameObject deathPartiPrefab = null;
    [SerializeField] private float _hp = 0;        // i have no idea why we were instructed to make it a float what the
    public int affil = 0;               // affiliation
    enum affils
    {
        enemy = 0,
        ally = 1,
    }

    public float hp {
        get { return _hp; }
        set {
            _hp = value;
            if (_hp <= 0) {
                SendMessage("Die", SendMessageOptions.DontRequireReceiver);

                if (deathPartiPrefab != null) {
                    Instantiate(deathPartiPrefab, transform.position, transform.rotation);
                }
                if (destroyOnDeath) {
                    Destroy(gameObject);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Debug healthtest
        if (Input.GetKeyDown(KeyCode.F)) {
            hp = 0;
        }
    }
}
