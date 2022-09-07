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

    public void EnableGame()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var obj = eventData.pointerCurrentRaycast.gameObject;

        if(obj.TryGetComponent<Cell>(out Cell cell))
        cell.StateChange();
    }
}
