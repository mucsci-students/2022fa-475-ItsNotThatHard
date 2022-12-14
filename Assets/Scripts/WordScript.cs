using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CSRandom = System.Random;

public class WordScript : MonoBehaviour
{
    public string[] words = { "ZOPPETTI", "GAMEDEV", "INTEGER", "BYTES", "BREAKOUT", "COMPUTER", "SCIENCE", "CONTROL" };
    private float tileWidth = 1.25f;
    private char[] chars;
    [SerializeField] private TextScript tilePrefab;
    private TextScript[] scriptList;
    private int wordLength;
    private CSRandom randVal = new CSRandom();
    private string pickedWord;

    public ComputerScript TargetComputer;

    // Start is called before the first frame update
    void Start()
    {
        pickedWord = words[randVal.Next(words.Length)];
        wordLength = pickedWord.Length;
        scriptList = new TextScript[wordLength];
        
        chars = pickedWord.ToCharArray().Shuffle();
        SpawnTiles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnTiles()
    {

        // NOTE: Parent's scale is assumed to be uniform
        float parentScaleModifier = transform.localScale.x;

        for (int i = 0; i < chars.Length; i++)
        {
            var spawnTile = Instantiate(tilePrefab);
            scriptList[i] = spawnTile;
            spawnTile.transform.parent = transform;
            spawnTile.transform.localPosition = new Vector3(-tileWidth * i, 0.0f, 0.0f);
            spawnTile.transform.localRotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
            spawnTile.transform.localScale = new Vector3(1.0f, 1.0f, 0.3f);
            spawnTile.AssignChar(chars[i]);
            spawnTile.OnInteracted += OnTileInteracted;
        }

        var firstTileLocation = scriptList[0].transform.localPosition.x;
        var lastTileLocation = scriptList[scriptList.Length - 1].transform.localPosition.x;

        float offSet = (firstTileLocation - lastTileLocation) / 2 * parentScaleModifier;
        Vector3 currentPos = transform.position;
        currentPos.z -= offSet;
        transform.position = currentPos;

    }

    private string getUserWord ()
    {
        var currentChar = scriptList.Select((currentTile) => currentTile.letterText.text.ToCharArray()[0]).ToArray();
        return new string(currentChar);
    }

    private void OnTileInteracted(object sender, EventArgs args)
    {
        if (getUserWord() == pickedWord)
        {
            for (int i = 0; i < chars.Length; i++)
            {
                scriptList[i].IsSolved = true;
            }

            // Set the computer password so the user HAS to unscramble it first
            TargetComputer.ChangeAdminPassword(pickedWord);

        }
        else
        {
            print(getUserWord());
        }
    }
}
