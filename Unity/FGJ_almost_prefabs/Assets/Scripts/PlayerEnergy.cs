using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEnergy : MonoBehaviour {

    
    public Image energyBar;
    public float energyLoss = 5.0f;
    public int energyCount = 5;

    private int max = 5;

    public void EnergyLost()
    {
        energyBar.fillAmount -= 1.0f / energyLoss ;
        energyCount--;
    }
    public void EnergyRecover()
    {
        if (energyCount <= max)
        {
            energyBar.fillAmount += 2.0f / energyLoss;
            energyCount += 2;
            if (energyCount >= max)
            {
                energyCount = max;
            }
        }
    }
   
}
