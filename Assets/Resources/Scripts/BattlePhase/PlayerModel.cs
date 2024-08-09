using UnityEngine;

namespace BattlePhase
{
    public class PlayerModel : MonoBehaviour
    {
        #region PROPERTIES

        public string Name { get; set; }
        public int Health { get; set; }
        public int CurrentHealth { get; set; }
        public int Damage { get; set; }
        public int CurrentDamage { get; set; }
        public int Manna { get; set; }
        public int CurrentManna { get; set; }

        #endregion

        // Start is called before the first frame update
        void Start()
        {
            Health = CurrentHealth = 50;
            Damage = CurrentDamage = 0;
            Manna = CurrentManna = 2;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}