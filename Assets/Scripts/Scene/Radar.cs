using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour {


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            LevelDemo.Instance.OnRadarFound();
        }
    }

}
