using System;
using UnityEngine;

/// <summary>
/// �ċA�̃T���v���B�v�׋�
/// </summary>
public class Test : MonoBehaviour
{
    private void M(int count)
    {
        if (count <= 0)
        {
            Debug.Log($"Return M: count={count}");
            return;
        }

        Debug.Log($"Begin M: count={count}");
        M(count - 1);
        Debug.Log($"End M: count={count}");
    }

    void Start()
    {
        M(10);
    }
}