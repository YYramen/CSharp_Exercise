using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 課題用スクリプト
/// </summary>
public class Sample2test : MonoBehaviour
{
    [SerializeField] int _cellValue = 5;
    private GameObject[] _cells;
    private int _selectedIndex;

    private void Start()
    {
        _cells = new GameObject[_cellValue];
        for (var i = 0; i < _cells.Length; i++)
        {
            var obj = new GameObject($"Cell{i}");
            obj.transform.parent = transform;
            obj.AddComponent<Image>();
            _cells[i] = obj;
        }

        OnSelectedChanged();
    }

    private void Update()
    {
        var v = (Input.GetKeyDown(KeyCode.LeftArrow) ? -1 : 0 +
                 (Input.GetKeyDown(KeyCode.RightArrow) ? 1: 0));

        if(v != 0)
        {
            _selectedIndex += v;
            //OnSelectedChanged();
        }
        if (Input.GetButtonDown("Jump"))
        {
            var cell = _cells[_selectedIndex];
            Destroy(cell);
        }
        OnSelectedChanged();
    }

    private void OnSelectedChanged()
    {
        for (var i = 0; i < _cells.Length; i++)
        {
            var cell = _cells[i];
            if (!cell)
            {
                continue;
            }
            var image = cell.GetComponent<Image>();

            if (i == _selectedIndex)
            {
                image.color = Color.red;
            }
            else
            {
                image.color = Color.white;
            }
        }
    }
}