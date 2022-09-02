using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ジャグ配列
/// </summary>
public class Sample4 : MonoBehaviour
{
    void Start()
    {
        //  int型の1次元配列を要素とする配列を作りたい
        //                      ジャグ配列↓
        var r1 = new int[3];    //■■■
        var r2 = new int[4];    //■■■■
        var r3 = new int[5];    //■■■■■

        //  これがジャグ配列
        //  ジャグ配列は配列の配列
        //  2次元配列とは別の概念
        int[][] iAry = new int[][] {r1, r2, r3};

        //  2次元配列はこう↓
        //  ■■■■■
        //  ■■■■■
        //  ■■■■■
    }
}
