using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemSelection : MonoBehaviour
{
    public GameObject[] powerUp;
    private GameObject currentPowerUp;
    int index;
    public GameObject IronFeet;
   
   //Randomizes the GameObject Power ups
    public void OnTriggerEnter(Collider other)
    {
        index = Random.Range(0, powerUp.Length - 1);
        currentPowerUp = powerUp[index];
        IronFeet.SetActive(true);
        Destroy(gameObject);

    }
}
