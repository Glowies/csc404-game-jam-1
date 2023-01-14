using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    public static EnergyManager instance { get; private set; }
    public float maxEnergy = 100f;
    private float currentEnergy;

    public bool IsSleeping;

    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
        
        if (instance != null && instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            instance = this; 
        } 
        currentEnergy = 40f;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetEnergy() {
        return currentEnergy;
    }

    public void LoseEnergy() {
        if (currentEnergy <= 0.0f) {
            currentEnergy = 0.0f;
        }
        else {
            currentEnergy -= 0.1f;
        }
    }

    public void AddEnergy() {
        if (currentEnergy >=maxEnergy) {
            currentEnergy = maxEnergy;
        }
        else {
            currentEnergy += 0.1f;
        }
    }
    
}
