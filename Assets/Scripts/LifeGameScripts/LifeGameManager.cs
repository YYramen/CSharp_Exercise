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

    bool _isStart = false;

    private void Start()
    {
        SetUp();
    }

    private void FixedUpdate()
    {
        if (_isStart)
        {
            for (var r = 0; r < _rows; r++)
            {
                for (var c = 0; c < _columns; c++)
                {
                    if (_cells[r, c].CellType == CellType.dead)
                    {
                        if (CheckCell(r, c) == 3)
                        {
                            _cells[r, c].CellType = CellType.life;
                        }
                    }
                }
            }

            for (var r = 0; r < _rows; r++)
            {
                for (var c = 0; c < _columns; c++)
                {
                    if (_cells[r, c].CellType == CellType.life)
                    {

                        if (CheckCell(r, c) == 2 || CheckCell(r, c) == 3)
                        {
                            _cells[r, c].CellType = CellType.life;
                        }
                        else if (CheckCell(r, c) <= 1 || CheckCell(r, c) >= 4)
                        {
                            _cells[r, c].CellType |= CellType.dead;
                        }

                    }
                }
            }
        }
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
            for (int c = 0; c < _columns; c++)
            {
                var cell = Instantiate(_cellPrefab);
                cell.transform.SetParent(parent);
                _cells[r, c] = cell;
            }
        }
    }

    /// <summary>
    /// ボタンに使うゲームをリセットするための関数
    /// </summary>
    public void ResetGame()
    {
        _isStart = false;
        foreach (var cell in _cells) { cell.CellType = CellType.dead; }
    }

    /// <summary>
    /// ボタンに使うゲームを止めるための関数
    /// </summary>
    public void StopGame()
    {
        _isStart = false;
    }

    /// <summary>
    /// ボタンに使うゲーム開始用の関数
    /// </summary>
    public void EnableGame()
    {
        _isStart = true;
    }

    /// <summary>
    /// 周りのセルを調べる
    /// </summary>
    private int CheckCell(int r, int c)
    {
        int countLife = 0;

        if (r - 1 >= 0)
        {
            if (c - 1 >= 0 && _cells[r - 1, c - 1].CellType == CellType.life)
            {
                countLife++;

            }
            if (_cells[r - 1, c].CellType == CellType.life)
            {
                countLife++;
            }
            if (c + 1 < _columns && _cells[r - 1, c + 1].CellType == CellType.life)
            {
                countLife++;
            }
        }
        if (c - 1 >= 0 && _cells[r, c - 1].CellType == CellType.life)
        {
            countLife++;
        }
        if (c + 1 < _columns && _cells[r, c + 1].CellType == CellType.life)
        {
            countLife++;
        }
        if (r + 1 < _rows)
        {
            if (c - 1 >= 0 && _cells[r + 1, c - 1].CellType == CellType.life)
            {
                countLife++;
            }
            if (_cells[r + 1, c].CellType == CellType.life)
            {
                countLife++;
            }
            if (c + 1 < _columns && _cells[r + 1, c + 1].CellType == CellType.life)
            {
                countLife++;
            }
        }
        
        return countLife;
    }

    /// <summary>
    /// クリックした時の処理
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        var obj = eventData.pointerCurrentRaycast.gameObject;

        if (obj.TryGetComponent<Cell>(out Cell cell))
            cell.CellType = CellType.life;
    }
}
