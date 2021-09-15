/*
 * Created by aschia
 * Date Created: Sept 13
 * 
 * Last Edited: Sept 15
 * 
 * Desc: Player movements n stuff
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // vars
    public bool mouseLook = true;
    public string HorzAxis = "Horizontal";
    public string VertAxis = "Vertical";
    public string FireAxis = "Fire1";
    public float MaxSpeed = 5f;

    Rigidbody rgbdy = null;

    // Rise and shine
    void Awake()
    {
        rgbdy = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // var setup
        float hsp = Input.GetAxis(HorzAxis);
        float vsp = Input.GetAxis(VertAxis);

        Vector3 dir = new Vector3(hsp, 0.0f, vsp);

        // move
        rgbdy.AddForce(dir.normalized*MaxSpeed);
        rgbdy.velocity = new Vector3(Mathf.Clamp(rgbdy.velocity.x, -MaxSpeed, MaxSpeed),
            Mathf.Clamp(rgbdy.velocity.y, -MaxSpeed, MaxSpeed),Mathf.Clamp(rgbdy.velocity.z, -MaxSpeed, MaxSpeed));

        // mouse
        if (mouseLook)
        {
            Vector3 mouseposworld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,-Camera.main.transform.position.z) );
            mouseposworld = new Vector3(mouseposworld.x, 0.0f, mouseposworld.z);
            this.transform.localRotation = Quaternion.LookRotation((mouseposworld-transform.position).normalized,Vector3.up);
        }

        // spin
        if (Input.GetKey(KeyCode.R) ) this.transform.Rotate(0, 5, 0);

        // restart
        if (Input.GetKey(KeyCode.F5))
        {
            this.transform.position = new Vector3(0, 0, 0);
            rgbdy.velocity = Vector3.zero;
            this.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }

    /*private void OnDrawGizmos()
    {
        Vector3 mouseposworld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,-Camera.main.transform.position.z) );
        mouseposworld = new Vector3(mouseposworld.x, 0.0f, mouseposworld.z);
        Gizmos.DrawCube(mouseposworld, Vector3.one);
    }*/
}
