using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTether : MonoBehaviour
{
    public Transform newTether;
    public Swing swing;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            swing.pendulum.SwitchTether(newTether.transform.position);
        }
    }
}
