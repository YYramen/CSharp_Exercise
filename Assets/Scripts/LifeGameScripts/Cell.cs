using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LifeGame
{
    [RequireComponent(typeof(Image))]
    public class Cell : MonoBehaviour
    {
        Image _cellImage;
        [SerializeField] CellType _cellType = CellType.dead;
        public CellType CellType  => _cellType;

        private void Awake()
        {
            _cellImage = GetComponent<Image>();
        }

        public void StateChange()
        {
            if (_cellType == CellType.life)
            {
                _cellType = CellType.dead;
                _cellImage.color = Color.white;
            }
            else if(_cellType == CellType.dead)
            {
                _cellType = CellType.life;
                _cellImage.color = Color.black;
            }
        }
    }
}
public enum CellType
{
    life = 0,
    dead = 1,
}

