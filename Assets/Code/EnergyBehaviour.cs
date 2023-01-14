using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBehaviour : MonoBehaviour
{
    private Slider energy;

    // Start is called before the first frame update
    void Start()
    {
        energy = gameObject.GetComponent<Slider>();
        energy.value = EnergyManager.instance.GetEnergy() / 100f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up") || Input.GetKey("down") || Input.GetKey("right") || Input.GetKey("left"))
            {
                EnergyManager.instance.LoseEnergy();
                energy.value = EnergyManager.instance.GetEnergy() / 100f;
            }
    }
}