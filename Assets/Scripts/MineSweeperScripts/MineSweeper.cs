using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MineSweeper : MonoBehaviour, IPointerClickHandler
{
    [Header("�Z���̌�")]
    [SerializeField, Tooltip("�s")] int _rows = 0;
    [SerializeField, Tooltip("��")] int _columns = 0;

    [Header("�Z���̐ݒ�")]
    [SerializeField, Tooltip("�Z���̃v���n�u")] Cell _cellPrefab;
    [SerializeField, Tooltip("�n���̐�")] int _mineCount = 1;


    [SerializeField, Tooltip(" GridLayoutGroup �Q�Ɨp")] GridLayoutGroup _layout;
    [Tooltip("�Z���̓�d�z��")] Cell[,] _cells;

    private void Start()
    {
        SetUp();

        GenerateMines();
    }

    /// <summary>
    /// �Z����z�u�A��d�z��̒��ɃZ�����i�[�B
    /// </summary>
    private void SetUp()
    {
        _layout.constraintCount = _columns;
        _cells = new Cell[_rows, _columns];

        for (var r = 0; r < _rows; r++)
        {
            for (var c = 0; c < _columns; c++)
            {
                var cell = Instantiate(_cellPrefab, _layout.transform);
                _cells[r, c] = cell;
            }
        }
    }

    /// <summary>
    /// _mineCount���n����u���B
    /// </summary>
    private void GenerateMines()
    {
        for (var i = 0; i <= _mineCount - 1; i++)
        {
            var randomRows = Random.Range(0, _rows);
            var randomColumns = Random.Range(0, _columns);
            var cell = _cells[randomRows, randomColumns];
            cell.CellState = CellState.Mine;

            _cells[randomRows + 1, randomColumns] = 
        }
    }

    /// <summary>
    /// �N���b�N���ꂽ�Z���̎���𒲂ׂ�
    /// </summary>
    /// <param name="data"></param>
    public void OnPointerClick(PointerEventData data)
    {
        var cell = data.pointerCurrentRaycast.gameObject.GetComponent<Cell>();

        for (var r = 0; r < _rows; r++)
        {
            for (var c = 0; c < _columns; c++)
            {
                if(cell == _cells[r, c])
                {
                    Debug.Log($"Clicked : {r}, {c}");
                }
            }
        }
    }
}
