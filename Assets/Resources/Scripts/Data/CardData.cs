using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "NewCard", menuName = "Cards/Card")]
    public class CardData : ScriptableObject
    {
        public string cardName;
        public int attackPoints;
        public int defensePoints;
        public Sprite cardImage;
        public GameObject cardModel; // 3D Model
        //public CardType cardType;
    }
}