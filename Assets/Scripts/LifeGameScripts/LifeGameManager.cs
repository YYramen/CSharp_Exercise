using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using LifeGame;

public class LifeGameManager : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] int _rows = 0;
    [SerializeField] int _columns = 0;
    Cell[,] _cells;

    [SerializeField] Cell _cellPrefab;
    [SerializeField] GridLayoutGroup _grid;

    private void Start()
    {
        SetUp();
    }

    /// <summary>
    /// ゲームの初期化
    /// </summary>
    private void SetUp()
    {
        _cells = new Cell[_rows, _columns];

        _grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _grid.constraintCount = _columns;
        var parent = _grid.transform;

        for (int r = 0; r < _rows; r++)
        {
            for(int c = 0; c < _columns; c++)
            {
                var cell = Instantiate(_cellPrefab);
                cell.transform.SetParent(parent);
                _cells[r, c] = cell;
            }
        }
    }

    /// <summary>
    /// ゲーム実行時の処理
    /// </summary>
    public void EnableGame()
    {
        for(var r = 0; r < _rows; r++)
        {
            for(var c = 0; c < _columns; c++)
            {
                if (_cells[r, c].CellType == CellType.life) //生存しているセルを探す
                {
                    //CheckCell();
                }
            }
        }
    }

    /// <summary>
    /// 生存している周りのセルを調べる
    /// </summary>
    private void CheckCell(int r,int c)
    {
        //int count = 0;
        //if (r - 1 >= 0)
        //{
        //    if (c - 1 >= 0 && _cells[r - 1, c - 1].CellType != CellType.)
        //    {
        //        _cells[r - 1, c - 1].IsOpen = true;
        //        if (_cells[r - 1, c - 1].CellType == CellType.None) Check(r - 1, c - 1);
        //    }
        //    if (_cells[r - 1, c].CellType != CellType.Mine && !_cells[r - 1, c].IsOpen)
        //    {
        //        _cells[r - 1, c].IsOpen = true;
        //        if (_cells[r - 1, c].CellType == CellType.None) Check(r - 1, c);
        //    }
        //    if (c + 1 < _columns && _cells[r - 1, c + 1].CellType != CellType.Mine && !_cells[r - 1, c + 1].IsOpen)
        //    {
        //        _cells[r - 1, c + 1].IsOpen = true;
        //        if (_cells[r - 1, c + 1].CellType == CellType.None) Check(r - 1, c + 1);
        //    }
        //}
        //if (c - 1 >= 0 && _cells[r, c - 1].CellType != CellType.Mine && !_cells[r, c - 1].IsOpen)
        //{
        //    _cells[r, c - 1].IsOpen = true;
        //    if (_cells[r, c - 1].CellType == CellType.None) Check(r, c - 1);
        //}
        //if (c + 1 < _columns && _cells[r, c + 1].CellType != CellType.Mine && !_cells[r, c + 1].IsOpen)
        //{
        //    _cells[r, c + 1].IsOpen = true;
        //    if (_cells[r, c + 1].CellType == CellType.None) Check(r, c + 1);
        //}
        //if (r + 1 < _rows)
        //{
        //    if (c - 1 >= 0 && _cells[r + 1, c - 1].CellType != CellType.Mine && !_cells[r + 1, c - 1].IsOpen)
        //    {
        //        _cells[r + 1, c - 1].IsOpen = true;
        //        if (_cells[r + 1, c - 1].CellType == CellType.None) Check(r + 1, c - 1);
        //    }
        //    if (_cells[r + 1, c].CellType != CellType.Mine && !_cells[r + 1, c].IsOpen)
        //    {
        //        _cells[r + 1, c].IsOpen = true;
        //        if (_cells[r + 1, c].CellType == CellType.None) Check(r + 1, c);
        //    }
        //    if (c + 1 < _columns && _cells[r + 1, c + 1].CellType != CellType.Mine && !_cells[r + 1, c + 1].IsOpen)
        //    {
        //        _cells[r + 1, c + 1].IsOpen = true;
        //        if (_cells[r + 1, c + 1].CellType == CellType.None) Check(r + 1, c + 1);
        //    }
        //}
    }

    /// <summary>
    /// クリックした時の処理
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        var obj = eventData.pointerCurrentRaycast.gameObject;

        if(obj.TryGetComponent<Cell>(out Cell cell))
        cell.StateChange();
    }
}
