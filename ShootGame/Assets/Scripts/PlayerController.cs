/*
 * Created by aschia
 * Date Created: Sept 13
 * 
 * Last Edited: Sept 27
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
    public string FireAxis = "Jump";
    public float MaxSpeed = 5f;

    public float shotDelay = 0.2f;
    public bool canFire = true;
    public Transform[] turretTransforms;

    [SerializeField] private bool doTankControls = false;

    Rigidbody rgbdy = null;
    Health hlth = null;

    // Rise and shine
    void Awake()
    {
        rgbdy = GetComponent<Rigidbody>();
        hlth = GetComponent<Health>();
        hlth.affil = 1;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        if (Input.GetButton(FireAxis) && canFire)
        {
            foreach(Transform T in turretTransforms)
            {
                AmmoManager.SpawnAmmo(T.position,T.rotation,1);
            }
            canFire = false;
            Invoke("EnableFire", shotDelay);
        }
    }

    void EnableFire()
    {
        canFire = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // var setup
        float hsp = 0;
        float vsp = 0;

        if (doTankControls)
        {
            hsp = Input.GetAxis(HorzAxis);
            vsp = Input.GetAxis(VertAxis);

            Vector3 dir = new Vector3(hsp, 0.0f, vsp);

            //move
            rgbdy.AddForce(dir.normalized * MaxSpeed);
            rgbdy.velocity = new Vector3(Mathf.Clamp(rgbdy.velocity.x, -MaxSpeed, MaxSpeed),
                Mathf.Clamp(rgbdy.velocity.y, -MaxSpeed, MaxSpeed), Mathf.Clamp(rgbdy.velocity.z, -MaxSpeed, MaxSpeed));
        }
        else
        {
            hsp = Input.GetAxisRaw(HorzAxis);
            vsp = Input.GetAxisRaw(VertAxis);

            rgbdy.velocity = new Vector3(hsp * MaxSpeed, 0.0f, vsp * MaxSpeed);
        }

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

        /*if (Input.GetButtonDown(FireAxis) && canFire)
        {
            foreach (Transform T in turretTransforms)
            {
                AmmoManager.SpawnAmmo(T.position, T.rotation, 1);
            }
            canFire = false;
            Invoke("EnableFire", shotDelay);
        }*/
    }

    /*private void OnDrawGizmos()
    {
        Vector3 mouseposworld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,-Camera.main.transform.position.z) );
        mouseposworld = new Vector3(mouseposworld.x, 0.0f, mouseposworld.z);
        Gizmos.DrawCube(mouseposworld, Vector3.one);
    }*/

    private void OnDestroy()
    {
        GameManager.GameOver();
        GameManager.isPlayerDead = true;
    }
}
