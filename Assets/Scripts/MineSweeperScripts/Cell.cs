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
    [SerializeField]
    private Text _view = default;

    [SerializeField]
    private CellState _cellState = CellState.None;

    [SerializeField]
    private Image _coverImg = null;

    private bool _isOpen = false;
    public bool IsOpen
    {
        get => _isOpen;
        set
        {
            _isOpen = value;
            OnIsOpenChanged();
        }
    }

    public CellState CellState
    {
        get => _cellState;
        set
        {
            _cellState = value;
            CellStateChanged();
        }
    }
    
    private void OnIsOpenChanged()
    {
        if(_coverImg == null) { return; }
        _coverImg.gameObject.SetActive(!_isOpen);
    }

    private void OnValidate()
    {
        CellStateChanged();
        OnIsOpenChanged();
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
