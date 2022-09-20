using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour
{
    [SerializeField]
    public Pendulum pendulum;

    void Start()
    {
        pendulum.Initalize();
    }
    void FixedUpdate()
    {
        transform.localPosition = pendulum.MoveBob(transform.localPosition, Time.deltaTime);
    }
}
