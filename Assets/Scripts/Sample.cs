using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 1ŽŸŒ³”z—ñ
/// </summary>
public class Sample : MonoBehaviour
{
    void Start()
    {
        //int[] array = new int[3] {10, 20, 30};
        var array = new[] { 10, 20, 30 };

        for (int i = 0; i < array.Length; i++)
        {
            Debug.Log($"array[{i}] = {array[i]}", this);
        }

        foreach (int e in array)
        {
            Debug.Log($"e = {e}");
        }
    }
}
