using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UObject = UnityEngine.Object;

public class BlackLight : MonoBehaviour
{

    /// <summary>
    /// The breaker that controls power to this computer
    /// </summary>
    public BreakerScript ControllingBreaker;

    public Material OnMaterial;
    public Material OffMaterial;

    public bool HasPower { get; private set; }

    public void Start()
    {

        HasPower = ControllingBreaker.BreakerIsOn;
        GetComponent<Renderer>().material = HasPower ? OnMaterial : OffMaterial;

        ControllingBreaker.OnTurnedOn += (_, _) => 
        { 
            HasPower = true; 
            GetComponent<Renderer>().material = OnMaterial; 
        };
        ControllingBreaker.OnTurnedOff += (_, _) =>
        {
            HasPower = false;
            GetComponent<Renderer>().material = OffMaterial;
        };

    }
}

