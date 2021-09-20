/*
 * Created by aschia
 * Date Created: Sept 20
 * 
 * Last Edited: Sept 20
 * 
 * Desc: Adam Sandler's Face Obj (defines facing object target)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceObj : MonoBehaviour
{
    // vars
    public Transform target = null;
    public bool facePlayer = false;

    public void Awake()
    {
        if (!facePlayer) { return; }
        GameObject playObj = GameObject.FindGameObjectWithTag("Player");

        if (playObj != null)
        {
            target = playObj.transform;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;

        Vector3 dirToObj = target.position - transform.position;

        if (dirToObj != Vector3.zero)
        {
            transform.localRotation = Quaternion.LookRotation(dirToObj);
        }
    }
}
