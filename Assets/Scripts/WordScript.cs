using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CSRandom = System.Random;

public class WordScript : MonoBehaviour
{
    public float[] oddVals = { 0.36f, 0.3f, 0.24f, 0.18f, 0.12f, 0.06f, 0f, -0.06f, -0.12f, -0.18f, -0.24f, -0.3f, -0.36f };
    public float[] evenVals = { 0.33f, 0.27f, 0.21f, 0.15f, 0.09f, 0.03f, -0.03f, -0.09f, -0.15f, -0.21f, -0.27f, -0.33f };
    public string[] words = { "ZOPPETTI", "GAMEDEV", "INTEGER", "BYTES", "BREAKOUT", "COMPUTER", "SCIENCE", "CONTROL" };
    private char[] chars;
    private GameObject tilePrefab;
    private int wordLength;
    private CSRandom randVal = new CSRandom();
    // Start is called before the first frame update
    void Start()
    {
        float[] coordinates;
        string pickedWord = words[randVal.Next(words.Length)];
        int wordLength = pickedWord.Length;
        if (wordLength % 2 == 0)
        {
            if (wordLength == 6)
            {
                coordinates = new float[6];
                coordinates[0] = oddVals[6];
                coordinates[1] = oddVals[5];
                coordinates[2] = oddVals[7];
                coordinates[3] = oddVals[4];
                coordinates[4] = oddVals[8];
                coordinates[5] = oddVals[3];
            }
            else if (wordLength == 8)
            {
                coordinates = new float[8];
                coordinates[0] = oddVals[6];
                coordinates[1] = oddVals[5];
                coordinates[2] = oddVals[7];
                coordinates[3] = oddVals[4];
                coordinates[4] = oddVals[8];
                coordinates[5] = oddVals[3];
                coordinates[6] = oddVals[9];
                coordinates[7] = oddVals[2];
            }
            else
            {
                coordinates = new float[10];
                coordinates[0] = oddVals[6];
                coordinates[1] = oddVals[5];
                coordinates[2] = oddVals[7];
                coordinates[3] = oddVals[4];
                coordinates[4] = oddVals[8];
                coordinates[5] = oddVals[3];
                coordinates[6] = oddVals[9];
                coordinates[7] = oddVals[2];
                coordinates[6] = oddVals[10];
                coordinates[7] = oddVals[1];
            }
        }
        else
        {
            if (wordLength == 5)
            {
                coordinates = new float[5];
                coordinates[0] = oddVals[6];
                coordinates[1] = oddVals[7];
                coordinates[2] = oddVals[5];
                coordinates[3] = oddVals[8];
                coordinates[4] = oddVals[4];
            }
            else if (wordLength == 7)
            {
                coordinates = new float[7];
                coordinates[0] = oddVals[6];
                coordinates[1] = oddVals[7];
                coordinates[2] = oddVals[5];
                coordinates[3] = oddVals[8];
                coordinates[4] = oddVals[4];
                coordinates[5] = oddVals[9];
                coordinates[6] = oddVals[3];
            }
            else
            {
                coordinates = new float[9];
                coordinates[0] = oddVals[6];
                coordinates[1] = oddVals[7];
                coordinates[2] = oddVals[5];
                coordinates[3] = oddVals[8];
                coordinates[4] = oddVals[4];
                coordinates[5] = oddVals[9];
                coordinates[6] = oddVals[3];
                coordinates[7] = oddVals[10];
                coordinates[8] = oddVals[2];
            }
        }
        chars = new char[wordLength];
        chars = pickedWord.ToCharArray();
        SpawnTiles(coordinates);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public char GetLetter (int iD)
    {
        if (iD < wordLength)
        {
            return chars[iD];
        }
        return '-';
    }

    public void SpawnTiles(float[] coordinates)
    {
        int[] idArray = new int[10];
        GameObject temp;
        if (wordLength % 2 == 0)
        {
            for (int i = 0; i < wordLength; i++)
            {
                idArray[0] = i;
                Vector3 spawnPos = new Vector3(coordinates[i], 1f, 0f);
                temp = Instantiate(tilePrefab) as GameObject;
                temp.transform.position = spawnPos;
            }
        }
    }
}
