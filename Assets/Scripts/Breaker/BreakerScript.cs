using System;
using UnityEngine;

/// <summary>
/// A script representing an on/off switch
/// </summary>
public class BreakerScript : MonoBehaviour, IInteractable
{

    /// <summary>
    /// The current state of the interactable
    /// </summary>
    public EInteractableState CurrentInteractableState;
    public EInteractableState GetInteractableState() => CurrentInteractableState;

    public event EventHandler OnInteracted;

    /// <summary>
    /// Event fired when the breaker has been turned on
    /// </summary>
    public event EventHandler OnTurnedOn;
    
    /// <summary>
    /// Event fired when the breader has been turned off
    /// </summary>
    public event EventHandler OnTurnedOff;

    /// <summary>
    /// Indicates whether or not the breaker is currently turned on
    /// </summary>
    public bool BreakerIsOn;

    // Switch on/ off sounds
    public AudioClip SwitchOnSound;
    public AudioClip SwitchOffSound;

    private Component _mainSwitch;
    private AudioSource _switchSound;

    public void Interact(GameObject interactor)
    {

        if (GetInteractableState() == EInteractableState.INTERACTABLE)
        {

            // Toggle the breaker's switch
            BreakerIsOn = !BreakerIsOn;
            OnInteracted?.Invoke(interactor, EventArgs.Empty);
            
            if (BreakerIsOn) { OnTurnedOn?.Invoke(interactor, EventArgs.Empty); }
            else { OnTurnedOff?.Invoke(interactor, EventArgs.Empty); }

            FlipSwitch(BreakerIsOn);

        }

    }

    /// <summary>
    /// Mirrors the main switch of the breaker to give the visual effect of being flipped
    /// </summary>
    private void MirrorSwitch()
    {

        if (_mainSwitch != null)
        {

            Vector3 currentScale = _mainSwitch.transform.localScale;
            currentScale.z *= -1;
            _mainSwitch.transform.localScale = currentScale;

        }

    }

    /// <summary>
    /// Flips the breaker switch to the provided position
    /// </summary>
    /// <param name="isOn">Indicates whether or not the breaker should be in the on position</param>
    /// <param name="flipQuietly">If true, no sound will be played when the switch is flipped</param>
    private void FlipSwitch(bool isOn, bool flipQuietly = false)
    {
        
        Vector3 currentScale = _mainSwitch.transform.localScale;
        if (isOn)
        {

            if (currentScale.z < 0) { MirrorSwitch(); }

            _switchSound.clip = SwitchOnSound;

        }

        else 
        {

            if (currentScale.z >= 0) { MirrorSwitch(); }

            _switchSound.clip = SwitchOffSound;

        }

        if (!flipQuietly) { _switchSound.Play(); }

    }

    public void Start()
    {

        gameObject.GetComponentsInChildren(typeof(MeshRenderer));

        _mainSwitch = gameObject.GetChildComponentByName("MainSwitch", typeof(Component));
        _switchSound = gameObject.GetComponentInChildren<AudioSource>();

        FlipSwitch(BreakerIsOn, true);

    }

}
