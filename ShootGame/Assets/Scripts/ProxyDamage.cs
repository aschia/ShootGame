/*
 * Created by aschia
 * Date Created: Sept 20
 * 
 * Last Edited: Sept 20
 * 
 * Desc: Ouch
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProxyDamage : MonoBehaviour
{
    // vars
    public float damageRate = 10f;  // damage per sec


    private void OnTriggerStay(Collider other)
    {
        Health H = other.gameObject.GetComponent<Health>();

        if (H == null) return;

        H.hp -= damageRate;//* Time.deltaTime;
    }
}
