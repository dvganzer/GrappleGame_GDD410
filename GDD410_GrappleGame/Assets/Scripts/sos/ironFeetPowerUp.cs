using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ironFeetPowerUp : MonoBehaviour
{
    public Bob bob;
    private float timer;

    private void Start()
    {
        timer = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= 1 * Time.deltaTime;
        if(timer  > 0)
        {
            this.gameObject.SetActive(true);
            bob.gravity = 40f;
            
        }
        else
        {
            bob.gravity = 20f;
        }
        if(timer <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
