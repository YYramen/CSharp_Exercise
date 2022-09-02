using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 2次元配列
/// </summary>
public class Sample3 : MonoBehaviour
{
    void Start()
    {
        //2次元配列の宣言、初期化
        int[,] iAry;
        iAry = new int[3, 5];

        //2次元配列の要素アクセス
        //配列型変数[要素1, 要素2]
        iAry[0, 0] = 0;
        iAry[0, 1] = 1;
        iAry[0, 2] = 2;
        iAry[0, 3] = 3;
        iAry[0, 4] = 4;
        iAry[1, 0] = 10;
        iAry[1, 1] = 11;
        iAry[1, 2] = 12;
        iAry[1, 3] = 13;
        iAry[1, 4] = 14;
        iAry[2, 0] = 20;
        iAry[2, 1] = 21;
        iAry[2, 2] = 22;
        iAry[2, 3] = 23;
        iAry[2, 4] = 24;

        //2次元配列の要素数(総数)
        Debug.Log($"要素数={iAry.Length}");

        // 各次元ごとの要素数をとる
        Debug.Log($"1次元目の要素数={iAry.GetLength(0)}");
        Debug.Log($"二次元目の要素数={iAry.GetLength(1)}");

        // for文を使った2次元配列の処理
        for(int i = 0; i < 3; i++)  //一次元目の処理
        {
            for(int k = 0; k < 5; k++)  //2次元目の処理
            {
                Debug.Log($"{i}, {k} = {iAry[i, k]}");
            }
        }

        // 2次元配列でも foreach での処理は可能
        foreach(var i in iAry)
        {
            Debug.Log(i);
        }
    }
}
