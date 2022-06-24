using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MineSweeper : MonoBehaviour, IPointerClickHandler
{
    [Header("セルの個数")]
    [SerializeField, Tooltip("行")] int _rows = 0;
    [SerializeField, Tooltip("列")] int _columns = 0;

    [Header("セルの設定")]
    [SerializeField, Tooltip("セルのプレハブ")] Cell _cellPrefab;
    [SerializeField, Tooltip("地雷の数")] int _mineCount = 1;


    [SerializeField, Tooltip(" GridLayoutGroup 参照用")] GridLayoutGroup _layout;
    [Tooltip("セルの二重配列")] Cell[,] _cells;

    private void Start()
    {
        SetUp();

        GenerateMines();
    }

    /// <summary>
    /// セルを配置、二重配列の中にセルを格納。
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
    /// _mineCount分地雷を置く。
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
    /// クリックされたセルの周りを調べる
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
