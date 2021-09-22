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

    private void OnEnable()
    {
        CancelInvoke();
        Invoke("Die", lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Health H = other.gameObject.GetComponent<Health>();
        H.hp -= dmg;
        Die();
    }

    void Die()
    {
        this.gameObject.SetActive(false);
    }
}
