using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectReturner : MonoBehaviour
{
    public static Vector2Int GetSimplifiedAspectRatio(int width, int height, bool logValue = false)
    {
        int gcd = GCD(width, height);
        int simpleWidth = width / gcd;
        int simpleHeight = height / gcd;


        Vector2Int aspectRatio = new(simpleWidth, simpleHeight);
        if(logValue) Debug.Log(aspectRatio);
        return aspectRatio;
    }

    private static int GCD(int a, int b)
    {
        // Can't be negative
        a = Mathf.Abs(a);
        b = Mathf.Abs(b);

        while (b != 0) // Loop until B==0
        {
            int temp = b; // Save value of B (remainder)

            /* Remainder calculation */

                // B is the remainder of GCD whilst A is the whole number
                b = a % b;

                // Set A to previous B value (remainder)
                a = temp; 
        }
        return a; // GDC
    }

}
