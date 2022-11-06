using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using TMPro;
using CSRandom = System.Random;

public class TextScript : MonoBehaviour, IInteractable
{
    public int tileID = 0;
    public TMP_Text letterText;
    private char[] letterList = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};
    public event EventHandler OnInteracted;
    public Material selectedMaterial;
    public Material normalMaterial;
    public Material solvedMaterial;
    public bool IsSolved = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsSolved)
        {
            GetComponent<Renderer>().material = solvedMaterial;
        }
    }

    // Assign this tile's letter
    public void AssignChar (char Char)
    {
        foreach (var letter in letterList)
        {
            if (letter == Char)
            {
                letterText.text = letter.ToString();
            }
        }
    }

    public EInteractableState GetInteractableState()
    {
        throw new NotImplementedException();
    }

    public void Interact (GameObject interactor)
    {
        if (IsSolved || interactor == null) return;
        var controller = interactor.GetComponentInChildren<FirstPersonController>();
        if (controller != null)
        {
            if (controller.lastClickedTile != null)
            {
                var tempLetter = controller.lastClickedTile.letterText.text;
                controller.lastClickedTile.letterText.text = letterText.text;
                letterText.text = tempLetter;
                controller.lastClickedTile.setSelected(false);
                setSelected(false);
                controller.lastClickedTile = null;
            }
            else
            {
                controller.lastClickedTile = this;
                setSelected(true);
            }
        }
        OnInteracted?.Invoke(interactor, EventArgs.Empty);
    }

    private void setSelected(bool isSelected) => GetComponent<Renderer>().material = isSelected ? selectedMaterial : normalMaterial;
}
