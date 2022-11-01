using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CSRandom = System.Random;

public class TextScript : MonoBehaviour
{
    public int tileID = 0;
    public TMP_Text letterText;
    private char[] letterList = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
