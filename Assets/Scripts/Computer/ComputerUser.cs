using System;
using UnityEngine;

[Serializable]
public class ComputerUser
{
    
    [Header("Account Configuration")]
    public string FullName;
    public string Username;
    public string Password;
    public string FontPageText;

    [Header("Permissions")]
    public bool CanDisableLaserGrid;

}
