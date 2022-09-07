using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Sampleweek4test : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int _rows = 5;
    [SerializeField] private int _colms = 5;
    [Tooltip("手数のカウント")] int _count = 0;
    [Tooltip("クリアしたかのフラグ")] bool _isClear = false;
    public Image _checkCell;

    GameObject[,] _cells;
    

    private void Start()
    {
        SetUp();
    }

    /// <summary>
    /// ゲームを再生した時に使う関数。Cell を配置する
    /// </summary>
    void SetUp()
    {
        _isClear = false;

        //  GridLayoutGroup の処理
        var layout = GetComponent<GridLayoutGroup>();
        layout.constraintCount = _colms;

        //  _cells[] の初期化
        _cells = new GameObject[_rows, _colms];

        //  乱数の生成
        int[] randomRows = new int[_rows];
        int[] randomCols = new int[_colms];
        for (int i = 0; i < _rows; i++)
        {
            randomRows[i] = Random.Range(0, _rows);
        }
        for(int k = 0; k < _colms; k++)
        {
            randomCols[k] = Random.Range(0, _colms);
        }

        //  cell を配置して Image を取ってくる。
        for (var r = 0; r < _cells.GetLength(0); r++)
        {
            for (var c = 0; c < _cells.GetLength(1); c++)
            {
                var cell = new GameObject($"Cell({r}, {c})");
                cell.transform.parent = transform;
                var image = cell.AddComponent<Image>();

                //  _cells[] のなかに cell を入れる。
                _cells[r, c] = cell;

                //image.color = Color.black;  //デバッグ用
            }
        }

        //ToggleColor(_cells[2, 1]);
        //ToggleColor(_cells[2, 2]);
        //ToggleColor(_cells[2, 3]);
        //ToggleColor(_cells[1, 2]);
        //ToggleColor(_cells[3, 2]);

        //生成した乱数の要素番号の色を黒にする。
        for (var i = 0; i < _rows; i++)
        {
            _cells[randomRows[i], randomCols[i]].GetComponent<Image>().color = Color.black;
            for (var h = 0; h < _colms; h++)
            {
                _cells[randomRows[h], randomCols[h]].GetComponent<Image>().color = Color.black;
            }
        }
    }

    private void Update()
    {
        Judge();

        if (_isClear == true)
        {
            Debug.Log($"クリア！クリアまでに{_count}回クリックした");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //  マウスクリックされた GameObject を cell に入れる
        var cell = eventData.pointerCurrentRaycast.gameObject;
        _count++;
        Debug.Log($"現在{_count}回クリックした");

        // クリックしたセルがどのセルなのか確認
        for (var r = 0; r < _rows; r++)
        {
            for (var c = 0; c < _colms; c++)
            {
                if (cell == _cells[r, c])
                {   
                    Debug.Log($"Clicked: {r},{c}"); // とりあえず出力しているだけ。確認用。
                    ToggleColor(cell);
                    if (r - 1 >= 0) { ToggleColor(_cells[r - 1, c]); } ;
                    if (r + 1 < _rows) { ToggleColor(_cells[r + 1, c]); };
                    if (c - 1 >= 0) { ToggleColor(_cells[r, c - 1]); };
                    if (c + 1 < _colms) { ToggleColor(_cells[r, c + 1]); };
                    break;
                    
                }
            }
        }
    }

    public void Judge()
    {
        int num = 0;
        for (var r = 0; r < _rows; r++)
        {
            for (var c = 0; c < _colms; c++)
            {
                _checkCell = _cells[r,c].GetComponent<Image>();

                if (_checkCell.color == Color.black)
                {
                    num++;
                }
                else if (_checkCell.color == Color.white)
                {
                    num--;
                }
                else
                {
                    Debug.Log("なんかおかしい");
                }
            }
        }

        if(num == _rows * _colms)
        {
            _isClear = true;
        }
        if(num == - 1 * (_rows * _colms))
        {
            _isClear = true;
        }

        //if(_checkCell.color == Color.black)
        //{
        //    _isClear = true;
        //}
        //else if(_checkCell.color == Color.white)
        //{
        //    _isClear = true;
        //}
        //else
        //{
        //    _isClear = false;
        //}
    }

    private static void ToggleColor(GameObject cell)
    {
        var image = cell.GetComponent<Image>();
        image.color = image.color == Color.white ? Color.black : Color.white;
    }
}
