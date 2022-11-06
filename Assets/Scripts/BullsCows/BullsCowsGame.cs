using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

[Serializable]
public enum EAffectsComputerAs
{

    DOES_NOT_EFFECT,
    AFFECTS_ADMIN_USERNAME,
    AFFECTS_ADMIN_PASSWORD,

}

public class BullsCowsGame : MonoBehaviour, IInteractable
{

    private string _solution;
    public event EventHandler OnPuzzleSolved;
    public event EventHandler OnInteracted;

    [SerializeField]
    [Range(1, 10)]
    private int _puzzleLength = 5;

    [SerializeField]
    private bool _allowDuplicatesInCode = false;
    
    [SerializeField] private EAffectsComputerAs _affectsComputerAs;
    public ComputerScript AffectedComputer;

    // Start is called before the first frame update
    void Start()
    {
        
        _solution = BullsCowsManager.GenerateCode(_puzzleLength, _allowDuplicatesInCode);

        switch(_affectsComputerAs)
        {

            case EAffectsComputerAs.AFFECTS_ADMIN_USERNAME:
                AffectedComputer.ChangeAdminUsername(_solution);
                break;

            case EAffectsComputerAs.AFFECTS_ADMIN_PASSWORD:
                AffectedComputer.ChangeAdminPassword(_solution);
                break;

        }

    }

    public EInteractableState GetInteractableState()
    {
        throw new NotImplementedException();
    }

    public (int bulls, int cows) Guess(string guess)
    {
        
        var bullsCows = BullsCowsManager.GetBullsCows(guess, _solution);
        return bullsCows;
        
    }

    public void Interact(GameObject interactor)
    {

        var interactingPlayer = interactor.GetComponentInChildren<FirstPersonController>();
        if (interactingPlayer != null)
        { interactingPlayer._playerHUD.StartBullsCows(this); }

    }

    public int GetDigitCount() => _solution.Length;

}
