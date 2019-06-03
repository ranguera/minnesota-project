using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.T))
            this.transform.Rotate(Vector3.up, Time.deltaTime * .1f);

        if (Input.GetKey(KeyCode.G))
            this.transform.Rotate(Vector3.down, Time.deltaTime * .1f);

        if (Input.GetKey(KeyCode.Y))
            this.transform.Rotate(Vector3.right, Time.deltaTime * .1f);

        if (Input.GetKey(KeyCode.H))
            this.transform.Rotate(Vector3.left, Time.deltaTime * .1f);

        if (Input.GetKey(KeyCode.U))
            this.transform.Rotate(Vector3.forward, Time.deltaTime * .1f);

        if (Input.GetKey(KeyCode.J))
            this.transform.Rotate(Vector3.back, Time.deltaTime * .1f);
    }
}
