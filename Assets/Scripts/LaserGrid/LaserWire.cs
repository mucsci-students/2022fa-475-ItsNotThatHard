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

    void Start()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
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

    void Update(){
        ParticleSystem ps = GetComponent<ParticleSystem>();
        count = 0;
        for(int i = 0; 8 > i; i++) {
            if( ( wires[i].transform.localRotation.y == 0) ) {
                count++;
            }
        }
        if(count == 8 && HasPower == false){
            GetComponent<BoxCollider>().enabled = true;
            ps.Play();
            count = 0;
        } else if (count != 8 && HasPower == false) {
            GetComponent<BoxCollider>().enabled = true;
            ps.Play();
            count = 0;
        } else if(count == 8 && HasPower == true){
            GetComponent<BoxCollider>().enabled = false;
            ps.Stop();
            count = 0;
        } else if (count != 8 && HasPower == true) {
            GetComponent<BoxCollider>().enabled = true;
            ps.Play();
            count = 0;
        }
    }

}
