using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crusher : MonoBehaviour
{
    public float shotDelay = 2f;
    bool canFire = false;
    Mover mv = null;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("ToggleCharge", shotDelay);
        mv = GetComponent<Mover>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canFire && GetComponent<FaceObj>().target != null)
        {
            // cancel charge if far away
            if (Mathf.Abs(Vector3.Distance(transform.position, GetComponent<FaceObj>().target.position)) > 6) return;
            // k i guess charge then
            mv.maxSpeed = 6;
            Invoke("ToggleCharge", shotDelay);
        }
    }

    void ToggleCharge()
    {
        canFire = !canFire;
        mv.maxSpeed = 2;
        Invoke("ToggleCharge", shotDelay*2);
        //Invoke("ToggleCharge", shotDelay / 2);
    }
}
