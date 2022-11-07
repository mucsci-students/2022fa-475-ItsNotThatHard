using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UObject = UnityEngine.Object;

public class LaserWire : MonoBehaviour
{

    public int count;
    public GameObject[] wires = new GameObject[8];
    public BreakerScript ControllingBreaker;
    public bool HasPower { get; private set; }
     float elapsedTime = 0f;

    private ParticleSystem ps;
    private BoxCollider killerBox;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        killerBox = GetComponent<BoxCollider>();

        HasPower = ControllingBreaker.BreakerIsOn;
        ControllingBreaker.OnTurnedOn += (_, _) => 
        { 
            HasPower = true;
        };
        ControllingBreaker.OnTurnedOff += (_, _) =>
        {
            HasPower = false;
            
        };
    }

    void EnableLaserGrid(bool enabled)
    {

        bool gridEnabled = killerBox.enabled && ps.isPlaying;

        if (enabled != gridEnabled)
        {

            killerBox.enabled = enabled;

            if (enabled) { ps.Play(); }

            else { ps.Stop(); }

        }

    }

    void Update() {
        
        bool wiresPositionedCorrectly = wires.Where(wire => wire.transform.rotation.y == 0).Count() == 8;
        
        elapsedTime = 0f;
        while (elapsedTime < 2f)
        {
            elapsedTime += Time.deltaTime;
        }

        bool laserGridShouldBeOn = !(wiresPositionedCorrectly && HasPower);
        EnableLaserGrid(laserGridShouldBeOn);

    }

}
