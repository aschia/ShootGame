/*
 * Created by aschia
 * Date Created: Sept 22
 * 
 * Last Edited: Sept 22
 * 
 * Desc: It costs four hundred thousand dollars
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // vars
    public float dmg = 100f;
    public float lifetime = 2f;
    public int affil = -1;
    enum affils
    {
        enemy = 0,
        ally = 1,
    }

    private void OnEnable()
    {
        CancelInvoke();
        Invoke("Die", lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (affil == -1)
        {
            Health Hpref = other.gameObject.GetComponent<Health>();
            affil = Hpref.affil;
            return;
        }

        Health H = other.gameObject.GetComponent<Health>();
        if (H == null) return;

        if (affil != H.affil)
        {
            Debug.Log("bullet affil: " + affil + ", target affil: " + H.affil);
            H.hp -= dmg;
            Die();
        }
    }

    void Die()
    {
        this.gameObject.SetActive(false);
    }
}
