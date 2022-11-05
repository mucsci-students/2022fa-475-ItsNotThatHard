using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UObject = UnityEngine.Object;

public class LaserToggle : MonoBehaviour
{

    public BreakerScript ControllingBreaker;

    public bool HasPower { get; private set; }

    void Start()
    {

        HasPower = ControllingBreaker.BreakerIsOn;

        ParticleSystem ps = GetComponent<ParticleSystem>();

        ControllingBreaker.OnTurnedOn += (_, _) => 
        { 
            HasPower = true;
            GetComponent<BoxCollider>().enabled = true;
            ps.Play();
        };
        ControllingBreaker.OnTurnedOff += (_, _) =>
        {
            HasPower = false;
            GetComponent<BoxCollider>().enabled = false;
            ps.Stop();
        };
    }
}
