using UnityEngine;
using UnityEngine.UI;

namespace LifeGame
{
    [RequireComponent(typeof(Image))]
    public class Cell : MonoBehaviour
    {
        Image _cellImage;
        [SerializeField] CellType _cellType = CellType.dead;

        public CellType CellType
        {
            get => _cellType;
            set
            {
                _cellType = value;
                OnStateChange();
            }
        }

        private void Awake()
        {
            _cellImage = GetComponent<Image>();
        }

        public void OnStateChange()
        {
            if (_cellType == CellType.life)
            {
                _cellImage.color = Color.black;
            }
            else if (_cellType == CellType.dead)
            {
                _cellImage.color = Color.white;
            }
        }
    }
}
public enum CellType
{
    life = 0,
    dead = 1,
}

