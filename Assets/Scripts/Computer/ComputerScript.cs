using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UObject = UnityEngine.Object;

public class ComputerScript : MonoBehaviour, IInteractable
{

    /// <summary>
    /// The laser grid to destroy 
    /// </summary>
    public GameObject LaserGrid;

    /// <summary>
    /// The user accounts stored on this computer
    /// </summary>
    public List<ComputerUser> Users;

    /// <summary>
    /// The breaker that controls power to this computer
    /// </summary>
    public BreakerScript ControllingBreaker;

    public event EventHandler OnLaserGridDisabled;
    public event EventHandler OnInteracted;
    public bool HasPower { get; private set; }

    private ComputerUser _authenticatedUser;

    private bool CanDisableLaserGrid()
        => HasPower && LaserGrid != null && IsAuthenticated() && _authenticatedUser.CanDisableLaserGrid;

    private bool IsAuthenticated() => _authenticatedUser != null;

    public void Start()
    {

        HasPower = ControllingBreaker.BreakerIsOn;

        ControllingBreaker.OnTurnedOn += (_, _) => { HasPower = true; };
        ControllingBreaker.OnTurnedOff += (_, _) =>
        {
            SignOut();
            HasPower = false;
        };

    }

    public bool TrySignIn(string userName, string password, out string errorMessage)
    {

        if (!HasPower) 
        {
            
            errorMessage = "Computer is not powered on";
            return false; 

        }

        errorMessage = "Unexpected authentication error";
        SignOut();

        ComputerUser matchingUser = Users.FirstOrDefault((user) => user.Username == userName);
        if (matchingUser != default(ComputerUser) && password == matchingUser.Password)
        {

            _authenticatedUser = matchingUser;
            errorMessage = $"Welcome {_authenticatedUser.FullName}!";
            return true;

        }

        // Authentication failed
        else if (matchingUser != default(ComputerUser) && password != matchingUser.Password)
        { errorMessage = "The password is not correct"; }

        else if (matchingUser == default(ComputerUser))
        { errorMessage = $"Invalid username: \"{userName}\""; }

        return false;

    }

    public void SignOut() => _authenticatedUser = null;

    public void DisableLaserGrid()
    {

        if (CanDisableLaserGrid()) 
        { 
            
            UObject.Destroy(LaserGrid);
            OnLaserGridDisabled?.Invoke(_authenticatedUser.Username, EventArgs.Empty);

        }

    }

    public EInteractableState GetInteractableState() => HasPower ? EInteractableState.INTERACTABLE : EInteractableState.DISABLED;

    public void Interact(GameObject interactor)
    {
        throw new NotImplementedException();
    }

}
