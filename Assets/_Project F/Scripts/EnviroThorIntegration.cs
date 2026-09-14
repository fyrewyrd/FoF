using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using THOR;
//using EnviroSky;
 
public class EnviroThorIntegration : MonoBehaviour {
 
    void Start()
    {
        EnviroSkyMgr.instance.OnWeatherChanged += (EnviroWeatherPreset type) =>
        {
            SwitchTHOR(type);
        };
    }
 
    void SwitchTHOR (EnviroWeatherPreset type)
    {
        if(type.isLightningStorm)
        {
           //Enable Thor. The first is set and the second is at random.
		   //THOR_Thunderstorm.instance.probability = 7f;
		   THOR_Thunderstorm.SetProbability(Random.Range(2f, 7f));
        }
        else
        {
           //Disable Thor
		   THOR_Thunderstorm.instance.probability = 0f;
        }
    }
}