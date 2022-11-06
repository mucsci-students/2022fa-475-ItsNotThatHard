using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

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

    public Material OnMaterial;
    public Material OffMaterial;

    private PlayerHUD _playerHUD;

    public event EventHandler OnLaserGridDisabled;
    public event EventHandler OnInteracted;
    public bool HasPower { get; private set; }

    private ComputerUser _authenticatedUser;

    private bool CanDisableLaserGrid()
        => HasPower && LaserGrid != null && IsAuthenticated() && _authenticatedUser.CanDisableLaserGrid;

    private bool IsAuthenticated() => _authenticatedUser != null;

    public string GetAuthenticatedUserFullName() => _authenticatedUser.FullName;
    public string GetAuthenticatedUserFrontPageText() => _authenticatedUser.FontPageText;

    public void Start()
    {

        _playerHUD = FindObjectOfType<PlayerHUD>();

        HasPower = ControllingBreaker.BreakerIsOn;
        GetComponent<Renderer>().material = HasPower ? OnMaterial : OffMaterial;

        ControllingBreaker.OnTurnedOn += (_, _) => 
        { 
            HasPower = true; 
            GetComponent<Renderer>().material = OnMaterial; 
        };
        ControllingBreaker.OnTurnedOff += (_, _) =>
        {
            SignOut();
            HasPower = false;
            GetComponent<Renderer>().material = OffMaterial;
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

    public bool DisableLaserGrid()
    {

        if (CanDisableLaserGrid()) 
        {

            Destroy(LaserGrid);
            OnLaserGridDisabled?.Invoke(_authenticatedUser.Username, EventArgs.Empty);
            return true;

        }

        return false;

    }

    public EInteractableState GetInteractableState() => HasPower ? EInteractableState.INTERACTABLE : EInteractableState.DISABLED;

    public void Interact(GameObject interactor)
    {

        FirstPersonController controller = interactor.GetComponentInChildren<FirstPersonController>();
        if (_playerHUD != null && controller != null && HasPower)
        {
            _playerHUD.OpenComputer(this);

        }

        else if (_playerHUD != null) { _playerHUD.ShowThought("It looks like this computer isn't getting power"); }

    }

    private ComputerUser GetFirstAdmin() => Users.Where(user => user.CanDisableLaserGrid).FirstOrDefault();

    public void ChangeAdminPassword(string newPassword)
    {

        var adminAccount = GetFirstAdmin();
        if (adminAccount != default)
        { adminAccount.Password = newPassword; }

    }

    public void ChangeAdminUsername(string newUsername)
    {

        var adminAccount = GetFirstAdmin();
        if (adminAccount != default)
        { adminAccount.Username = newUsername; }

    }

}
