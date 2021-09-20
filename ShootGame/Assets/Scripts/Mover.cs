/*
 * Created by aschia
 * Date Created: Sept 20
 * 
 * Last Edited: Sept 20
 * 
 * Desc: Move it move it
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    // vars
    public float maxSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * maxSpeed * Time.deltaTime;
    }
}
