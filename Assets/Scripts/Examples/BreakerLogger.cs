using UnityEngine;

/// <summary>
/// An example class that demonstrates reacting to changes in the breaker switch
/// For each event, this script prints out an appropriate message
/// </summary>
public class BreakerLogger : MonoBehaviour
{

    /// <summary>
    /// The breaker being controlled
    /// </summary>
    public BreakerScript Breaker;
    
    void Start()
    {

        // Bind to the breaker's interaction events to print an appropriate message
        Breaker.OnInteracted += (_, _) => { print("Breaker Interacted With"); };
        Breaker.OnTurnedOn += (_, _) => { print("Power Turned On"); };
        Breaker.OnTurnedOff += (_, _) => { print("Power Turned Off"); };

    }

}
