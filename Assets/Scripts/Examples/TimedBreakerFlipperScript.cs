using UnityEngine;

/// <summary>
/// A script that repeatedly flips a breaker based on a time delay
/// </summary>
public class TimedBreakerFlipperScript : MonoBehaviour
{

    /// <summary>
    /// The time delay between flips
    /// </summary>
    public float LoopDelaySec = 2;    

    /// <summary>
    /// The breaker being controlled
    /// </summary>
    public BreakerScript BreakerToFlip;

    private float _flipDelayTimer = 0;

    public void Update() 
    {

        _flipDelayTimer += Time.deltaTime;
        if (_flipDelayTimer >= LoopDelaySec) 
        {

            FlipSwitch();
            _flipDelayTimer = 0;
        
        }

    }

    private void FlipSwitch()
    {

        if (BreakerToFlip != null) { BreakerToFlip.Interact(null); }

    }

}
