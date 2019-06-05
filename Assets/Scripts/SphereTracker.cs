using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereTracker : MonoBehaviour
{

    public VFX vfx;
    private Vector3 initialPos;
    private bool set;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = this.transform.position;    
    }

    // Update is called once per frame
    void Update()
    {
        if(!set)
        {
            this.initialPos = this.transform.position;
            set = true;
        }

        vfx.PlanetTransition((this.transform.position.y - this.initialPos.y)*4f);
    }
}
