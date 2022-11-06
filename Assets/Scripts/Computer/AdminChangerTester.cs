using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

public class AdminChangerTester : MonoBehaviour, IInteractable
{

    private char[] alphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
    public ComputerScript Computer;
    
    public event EventHandler OnInteracted;

    public EInteractableState GetInteractableState()
    {
        throw new NotImplementedException();
    }

    private string GenerateRandomString(int length = 8)
    {

        StringBuilder result = new StringBuilder(length);
        for (int i = 0; i < length; i++) { result.Append(alphabet[Random.Range(0, alphabet.Length)]); }

        return result.ToString();

    }

    public void Interact(GameObject interactor)
    {

        Computer.ChangeAdminUsername(GenerateRandomString());
        Computer.ChangeAdminPassword(GenerateRandomString());

    }

}
