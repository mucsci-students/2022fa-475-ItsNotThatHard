using System;
using System.Collections.Generic;

public static class ArrayExtensions
{

    /// <summary>
    /// Shuffles the provided array and returns the result as a new array
    /// </summary>
    /// <typeparam name="T">The type of the array to shuffle</typeparam>
    /// <param name="toShuffle">The array to shuffle</param>
    /// <returns>The shuffled array as a new array</returns>
    public static T[] Shuffle<T>(this T[] toShuffle)
    {

        Random rng = new Random();

        T[] result = new T[toShuffle.Length];
        int currentIndex = 0;

        List<T> spool = new List<T>(toShuffle);
        while (spool.Count > 0)
        {
            var target = spool[rng.Next(spool.Count)];
            spool.Remove(target);
            result[currentIndex++] = target;
        }

        return result;

    }

}
