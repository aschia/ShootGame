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
    public float flashtime = 0.25f;

    public SpriteRenderer sprend = null;

    public Sprite[] spranim = null;

    enum affils
    {
        enemy = 0,
        ally = 1,
    }

    private void OnEnable()
    {
        CancelInvoke();
        Invoke("Die", lifetime);

        if (sprend == null) sprend = transform.GetChild(0).GetComponent<SpriteRenderer>();

        if (spranim == null) spranim = new Sprite[] { sprend.sprite };
        InvokeRepeating("Flash", 0f, flashtime);
    }

    private void Flash()
    {
        Debug.Log("flash");
        if (affil == 1)
        {
            sprend.sprite = spranim[0];
            CancelInvoke("Flash");
            return;
        }

        if (sprend.sprite == spranim[1]) sprend.sprite = spranim[2];
        else sprend.sprite = spranim[1];
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
