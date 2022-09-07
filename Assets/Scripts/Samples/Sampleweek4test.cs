using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Sampleweek4test : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int _rows = 5;
    [SerializeField] private int _colms = 5;
    [Tooltip("�萔�̃J�E���g")] int _count = 0;
    [Tooltip("�N���A�������̃t���O")] bool _isClear = false;
    public Image _checkCell;

    GameObject[,] _cells;
    

    private void Start()
    {
        SetUp();
    }

    /// <summary>
    /// �Q�[�����Đ��������Ɏg���֐��BCell ��z�u����
    /// </summary>
    void SetUp()
    {
        _isClear = false;

        //  GridLayoutGroup �̏���
        var layout = GetComponent<GridLayoutGroup>();
        layout.constraintCount = _colms;

        //  _cells[] �̏�����
        _cells = new GameObject[_rows, _colms];

        //  �����̐���
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

        //  cell ��z�u���� Image ������Ă���B
        for (var r = 0; r < _cells.GetLength(0); r++)
        {
            for (var c = 0; c < _cells.GetLength(1); c++)
            {
                var cell = new GameObject($"Cell({r}, {c})");
                cell.transform.parent = transform;
                var image = cell.AddComponent<Image>();

                //  _cells[] �̂Ȃ��� cell ������B
                _cells[r, c] = cell;

                //image.color = Color.black;  //�f�o�b�O�p
            }
        }

        //ToggleColor(_cells[2, 1]);
        //ToggleColor(_cells[2, 2]);
        //ToggleColor(_cells[2, 3]);
        //ToggleColor(_cells[1, 2]);
        //ToggleColor(_cells[3, 2]);

        //�������������̗v�f�ԍ��̐F�����ɂ���B
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
            Debug.Log($"�N���A�I�N���A�܂ł�{_count}��N���b�N����");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //  �}�E�X�N���b�N���ꂽ GameObject �� cell �ɓ����
        var cell = eventData.pointerCurrentRaycast.gameObject;
        _count++;
        Debug.Log($"����{_count}��N���b�N����");

        // �N���b�N�����Z�����ǂ̃Z���Ȃ̂��m�F
        for (var r = 0; r < _rows; r++)
        {
            for (var c = 0; c < _colms; c++)
            {
                if (cell == _cells[r, c])
                {   
                    Debug.Log($"Clicked: {r},{c}"); // �Ƃ肠�����o�͂��Ă��邾���B�m�F�p�B
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
                    Debug.Log("�Ȃ񂩂�������");
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
