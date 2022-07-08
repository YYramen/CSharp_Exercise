using UnityEngine;
using UnityEngine.UI;

public class MineSweeper : MonoBehaviour
{
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

    private void Start()
    {
        _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _gridLayoutGroup.constraintCount = _columns;

        var cells = new Cell[_rows, _columns];
        var parent = _gridLayoutGroup.transform;
        for (var r = 0; r < _rows; r++)
        {
            for (var c = 0; c < _columns; c++)
            {
                var cell = Instantiate(_cellPrefab);
                cell.transform.SetParent(parent);
                cells[r, c] = cell;
            }
        }

        // �Z�������������Ēn���������_���ɐݒu����B
        ClearCells(cells, _mineCount);

        // ���ׂẴZ����T������2�d���[�v
        for (var r = 0; r < _rows; r++)
        {
            for (var c = 0; c < _columns; c++)
            {
                // ���̃Z���̎���8�}�X�̒n���𐔂���
                var cell = cells[r, c];
                cell.CellState = GetMineCount(cells, r, c);
            }
        }
    }

    /// <summary>
    /// �w�肵��2�����z��̃Z�������ׂď��������āA�w�萔�̒n���������_���ɔz�u����B
    /// �n�������Z������葽���ꍇ�A���ׂẴZ����n���Ŗ��߂�B
    /// </summary>
    /// <param name="cells">�Z����2�����z��B</param>
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
    /// <param name="cells">�Z����2�����z��B</param>
    /// <param name="r">�s�ԍ��B</param>
    /// <param name="c">��ԍ��B</param>
    /// <returns>�Z���̎���8�ߖT�̒n�����B</returns>
    private CellState GetMineCount(Cell[,] cells, int r, int c)
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
}