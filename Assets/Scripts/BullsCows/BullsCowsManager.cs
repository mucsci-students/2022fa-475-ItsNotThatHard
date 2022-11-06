using System;
using System.Collections.Generic;
using System.Text;

public static class BullsCowsManager
{

    private static readonly char[] digits = "124567890".ToCharArray();

    public static (int bulls, int cows) GetBullsCows(string guess, string code)
    {

        int bulls = 0;
        int cows = 0;

        if (guess.Length != code.Length) { return (bulls, cows); }

        for (int i = 0; i < code.Length; ++i)
        {

            if (guess[i] == code[i]) { bulls++; }

            else if (code.Contains(guess[i])) { cows++; }

        }

        return (bulls, cows);

    }

    public static string GenerateCode(int codeLength = 4, bool allowDuplicates = false)
    {

        StringBuilder sb = new(codeLength);
        Random rng = new();        
        List<char> digitPool = new(digits);

        // NOTE: Max code length when not allowing duplicates is 10!
        for (int i = 0; i < codeLength; ++i)
        {

            int chosenIndex = rng.Next(digitPool.Count);
            sb.Append(digitPool[chosenIndex]);

            if (!allowDuplicates)
            { digitPool.RemoveAt(chosenIndex); }

        }

        return sb.ToString();

    }

}
