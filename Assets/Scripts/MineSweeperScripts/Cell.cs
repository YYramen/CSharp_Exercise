using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CellState
{
    None = 0,

    One = 1,
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,

    Mine = -1,
}

public class Cell : MonoBehaviour
{
    [SerializeField] Text _view = default;

    [SerializeField] CellState _cellState = CellState.None;
    public CellState CellState
    {
        get => _cellState;
        set
        {
            _cellState = value;
            CellStateChanged();
        }
    }

    //private void Start()
    //{
    //    CellStateChanged();
    //}

    private void OnValidate()
    {
        CellStateChanged();
    }

    private void CellStateChanged()
    {
        if (_view == null) { return; }

        if (_cellState == CellState.None)
        {
            _view.text = "";
        }
        else if (_cellState == CellState.Mine)
        {
            _view.color = Color.red;
            _view.text = "Åú";
        }
        else
        {
            _view.text = ((int)_cellState).ToString();
            _view.color = Color.green;
        }
    }
}
