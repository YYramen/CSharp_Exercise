using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class MineSweeper : MonoBehaviour, IPointerClickHandler
{
    private Cell[,] _cells;

    [SerializeField]
    private int _rows = 1;

    [SerializeField]
    private int _columns = 1;

    [SerializeField]
    private int _mineCount = 1;

    [SerializeField]
    private GridLayoutGroup _gridLayoutGroup = null;

    [SerializeField]
    private Cell _cellPrefab = null;

    [SerializeField]
    private Text _infoText;

    private bool _isFirstCell = true;

    [SerializeField]
    private Image _fadeImg = null;
    float _alpha = 0.0f;
    float _fadeSpeed = 0.003f;

    private void Start()
    {
        SetUp();
    }

    /// <summary>
    /// �Z����z�u���Z���ɒn����z�u����
    /// </summary>
    private void SetUp()
    {
        StartCoroutine(FadeIn());
        _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _gridLayoutGroup.constraintCount = _columns;

        _cells = new Cell[_rows, _columns];
        var parent = _gridLayoutGroup.transform;
        for (var r = 0; r < _rows; r++)
        {
            for (var c = 0; c < _columns; c++)
            {
                var cell = Instantiate(_cellPrefab);
                cell.transform.SetParent(parent);
                _cells[r, c] = cell;
            }
        }

        // �Z�������������Ēn���������_���ɐݒu����B
        ClearCells(_cells, _mineCount);

        // ���ׂẴZ����T������2�d���[�v
        for (var r = 0; r < _rows; r++)
        {
            for (var c = 0; c < _columns; c++)
            {
                // ���̃Z���̎���8�}�X�̒n���𐔂���
                var cell = _cells[r, c];
                cell.CellState = GetMineCount(_cells ,r, c);
            }
        }

        _infoText.color = Color.clear;
    }

    /// <summary>
    /// �w�肵��2�����z��̃Z�������ׂď��������āA�w�萔�̒n���������_���ɔz�u����B
    /// �n�������Z������葽���ꍇ�A���ׂẴZ����n���Ŗ��߂�B
    /// </summary>
    /// <param name="_cells">�Z����2�����z��B</param>
    /// <param name="mineCount">�ݒu����n�����B</param>
    private void ClearCells(Cell[,] cells, int mineCount)
    {
        // ���ׂẴZ���̏�Ԃ� None �ŏ���������
        foreach (var cell in cells) { cell.CellState = CellState.None; }

        // �ݒ肳�ꂽ�n�������Z������葽���ꍇ
        if (mineCount > cells.Length)
        {
            // �n�����������I�ɃZ�����ƈ�v������
            mineCount = cells.Length;
        }

        var rows = cells.GetLength(0);
        var columns = cells.GetLength(1);

        // �ݒu�������n�����imineCount�j�����J��Ԃ�
        for (var i = 0; i < mineCount;)
        {
            // �����_���ȍs�ԍ� r�A��ԍ� c �����
            var r = Random.Range(0, rows);
            var c = Random.Range(0, columns);

            // �����_���ɑI�΂ꂽ�Z��
            var cell = cells[r, c];
            if (cell.CellState != CellState.Mine)
            {
                i++; // �J�E���^�[���C���N�������g����
                cell.CellState = CellState.Mine;
            }
            else { Debug.Log("�n���Ē��I"); }
        }
    }

    /// <summary>
    /// �w�肵��2�����z��� r �s c ��ɂ���Z���̎���8�ߖT�̒n�������擾����B
    /// r �s c ��̃Z�����n���Ȃ� <see cref="CellState.Mine"/> ��Ԃ��B
    /// </summary>
    /// <param name="_cells">�Z����2�����z��B</param>
    /// <param name="r">�s�ԍ��B</param>
    /// <param name="c">��ԍ��B</param>
    /// <returns>�Z���̎���8�ߖT�̒n�����B</returns>
    private CellState GetMineCount(Cell[,] cells ,int r, int c)
    {
        var cell = cells[r, c];
        if (cell.CellState == CellState.Mine)
        {
            return CellState.Mine;  // �n���Z���͖�������
        }

        var count = 0;

        // �Z���̎��͂̒n�����𐔂��鏈��
        if (r - 1 >= 0)
        {
            if (c - 1 >= 0 && cells[r - 1, c - 1].CellState == CellState.Mine) { count++; }
            if (cells[r - 1, c].CellState == CellState.Mine) { count++; }
            if (c + 1 < _columns && cells[r - 1, c + 1].CellState == CellState.Mine) { count++; }
        }
        if (c - 1 >= 0 && cells[r, c - 1].CellState == CellState.Mine) { count++; }
        if (c + 1 < _columns && cells[r, c + 1].CellState == CellState.Mine) { count++; }
        if (r + 1 < _rows)
        {
            if (c - 1 >= 0 && cells[r + 1, c - 1].CellState == CellState.Mine) { count++; }
            if (cells[r + 1, c].CellState == CellState.Mine) { count++; }
            if (c + 1 < _columns && cells[r + 1, c + 1].CellState == CellState.Mine) { count++; }
        }

        return (CellState)count; // �n������ CellState �ɕϊ�
    }

    /// <summary>
    /// �N���b�N���ꂽ���̏���
    /// </summary>
    /// <param name="eventData">�N���b�N���ꂽ�Z��</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        var obj = eventData.pointerCurrentRaycast.gameObject;
        Debug.Log($"OnPointerClick: {obj.name}");

        var cell = obj.GetComponent<Cell>();
        if (cell != null)
        {
            // �Z�����N���b�N���ꂽ

            if (_isFirstCell)   //�ŏ��ɊJ�����Z���������ꍇ
            {
                if(cell.CellState == CellState.Mine)    //�ŏ��ɊJ�����Z�����n���������ꍇ
                {
                    Debug.Log("�ŏ��ɊJ�����Z�����n��������");
                    _infoText.text = ("�ŏ��ɊJ�����Z�����n�����������߁A�Q�[�����ăX�^�[�g");
                    StartCoroutine(FadeOut());
                }
                _isFirstCell = false;
            }
            cell.IsOpen = true;
            Debug.Log($"Cell IsOpen: {cell.IsOpen}");
            Debug.Log($"Selected Cell's CellState: {cell.CellState}");

            if (cell.CellState == CellState.Mine)   //�J�����Z�����n���������ꍇ
            {
                _infoText.color = Color.white;
            }

            if(cell.CellState == CellState.None)    //�J�����Z����State��None�������ꍇ
            {
                
            }
        }
    }

    /// <summary>
    /// �J�����Z����State��None�������ꍇ�ɌĂяo���A�����ŃZ����W�J���鏈��
    /// </summary>
    private void AutoOpen(Cell target)
    {
        
    }

    IEnumerator FadeOut()
    {
        Color c = _fadeImg.color;
        c.a = _alpha;
        _fadeImg.color = c;
        while (true)
        {
            yield return null;
            c.a += _fadeSpeed;
            _fadeImg.color = c;
            _fadeImg.raycastTarget = true;

            if (c.a >= 1)
            {
                c.a = 1f;
                _fadeImg.color = c;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            }
        }
    }

    IEnumerator FadeIn()
    {
        Color c = _fadeImg.color;
        c.a = 1;
        _fadeImg.color = c;
        
        while (true)
        {
            yield return null;
            c.a -= _fadeSpeed;
            _fadeImg.color = c;
            _fadeImg.raycastTarget = true;

            if (c.a <= 0)
            {
                c.a = 0f;
                _fadeImg.color = c;
                _fadeImg.raycastTarget = false;
                break;
            }
        }
    }
}