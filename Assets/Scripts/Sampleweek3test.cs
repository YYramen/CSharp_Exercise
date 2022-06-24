using UnityEngine;
using UnityEngine.UI;

public class Sampleweek3test : MonoBehaviour
{
    [SerializeField] private int _rows = 5;
    [SerializeField] private int _columns = 5;

    private GameObject[,] _cells;
    private int _selectedRow;
    private int _selectedCol;
    GridLayoutGroup _group;

    private void Start()
    {
        _group = GetComponent<GridLayoutGroup>();
        _group.constraintCount = _columns;

        _cells = new GameObject[_rows, _columns];

        for (var r = 0; r < _rows; r++) //または cells.GetLength(0)
        {
            for (var c = 0; c < _columns; c++)  //または cells.GetLength(1)
            {
                var obj = new GameObject($"Cell({r}, {c})");
                obj.transform.parent = transform;
                obj.AddComponent<Image>();
                _cells[r, c] = obj;
            }
        }
    }

    private void Update()
    {
        if(_cells[_selectedCol,_selectedRow] != null)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) // 左キーを押した
            {
                if (_selectedCol - 1 >= 0)
                    _selectedCol--;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow)) // 右キーを押した
            {
                if (_selectedCol + 1 < _cells.GetLength(1))
                    _selectedCol++;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow)) // 上キーを押した
            {
                if (_selectedRow - 1 >= 0)
                    _selectedRow--;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow)) // 下キーを押した
            {
                if (_selectedRow + 1 < _cells.GetLength(0))
                    _selectedRow++;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var cell = _cells[_selectedRow, _selectedCol];
                if (cell != null)
                {
                    cell.GetComponent<Image>().enabled = false;
                }
            }
        }



        for (var r = 0; r < _rows; r++) //または cells.GetLength(0)
        {
            for (var c = 0; c < _columns; c++)  //または cells.GetLength(1)
            {
                var cell = _cells[r, c];

                var image = cell.GetComponent<Image>();
                if (r == _selectedRow && c == _selectedCol)
                { 
                    image.color = Color.red; 
                }
                else { image.color = Color.white; }
            }
        }
    }
}