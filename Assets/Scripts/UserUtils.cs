using System;
using UnityEngine;

public class UserUtils : MonoBehaviour
{
    private static System.Random s_random = new System.Random();

    public static int GenerateRandomNumber(int min = 1, int max = 10)
    {
        return s_random.Next(min, max + 1);
    }
}
