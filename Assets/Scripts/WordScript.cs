using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CSRandom = System.Random;

public class WordScript : MonoBehaviour
{
    public float[] oddVals = { 0.36f, 0.3f, 0.24f, 0.18f, 0.12f, 0.06f, 0f, -0.06f, -0.12f, -0.18f, -0.24f, -0.3f, -0.36f };
    public float[] evenVals = { 0.33f, 0.27f, 0.21f, 0.15f, 0.09f, 0.03f, -0.03f, -0.09f, -0.15f, -0.21f, -0.27f, -0.33f };
    public string[] words = { "ZOPPETTI", "GAMEDEV", "INTEGER", "BYTES", "BREAKOUT", "COMPUTER", "SCIENCE", "CONTROL" };
    private float tileWidth = 1.75f;
    private char[] chars;
    [SerializeField] private TextScript tilePrefab;
    private TextScript[] scriptList;
    private int wordLength;
    private CSRandom randVal = new CSRandom();
    private string pickedWord;
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
        for (int i = 0; i < chars.Length; i++)
        {
            var spawnTile = Instantiate(tilePrefab);
            scriptList[i] = spawnTile;
            spawnTile.transform.parent = transform;
            spawnTile.transform.localPosition = new Vector3 (-tileWidth * i, 0, 0);
            spawnTile.AssignChar (chars[i]);
            spawnTile.OnInteracted += OnTileInteracted;
        }
        float offSet = (-wordLength * tileWidth) / 2;
        Vector3 currentPos = transform.position;
        currentPos.x -= offSet;
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
            print("Puzzle solved!");
        }
        else
        {
            print(getUserWord());
        }
    }
}
