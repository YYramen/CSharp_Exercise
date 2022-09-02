using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 2�����z��
/// </summary>
public class Sample3 : MonoBehaviour
{
    void Start()
    {
        //2�����z��̐錾�A������
        int[,] iAry;
        iAry = new int[3, 5];

        //2�����z��̗v�f�A�N�Z�X
        //�z��^�ϐ�[�v�f1, �v�f2]
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

        //2�����z��̗v�f��(����)
        Debug.Log($"�v�f��={iAry.Length}");

        // �e�������Ƃ̗v�f�����Ƃ�
        Debug.Log($"1�����ڂ̗v�f��={iAry.GetLength(0)}");
        Debug.Log($"�񎟌��ڂ̗v�f��={iAry.GetLength(1)}");

        // for�����g����2�����z��̏���
        for(int i = 0; i < 3; i++)  //�ꎟ���ڂ̏���
        {
            for(int k = 0; k < 5; k++)  //2�����ڂ̏���
            {
                Debug.Log($"{i}, {k} = {iAry[i, k]}");
            }
        }

        // 2�����z��ł� foreach �ł̏����͉\
        foreach(var i in iAry)
        {
            Debug.Log(i);
        }
    }
}
