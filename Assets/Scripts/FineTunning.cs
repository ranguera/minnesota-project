﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FineTunning : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
            this.transform.Translate(Vector3.up * Time.deltaTime);
        else if (Input.GetKey(KeyCode.DownArrow))
            this.transform.Translate(Vector3.down * Time.deltaTime);
        else if (Input.GetKey(KeyCode.RightArrow))
            this.transform.Translate(Vector3.right * Time.deltaTime);
        else if (Input.GetKey(KeyCode.LeftArrow))
            this.transform.Translate(Vector3.left * Time.deltaTime);

        if (Input.GetKey(KeyCode.W))
            this.transform.localScale += Vector3.one * Time.deltaTime;
        else if (Input.GetKey(KeyCode.S))
            this.transform.localScale -= Vector3.one * Time.deltaTime;
    }
}
