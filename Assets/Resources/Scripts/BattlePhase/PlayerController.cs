using BattlePhase;
using UnityEngine;

namespace BattlePhase
{
    public class PlayerController : MonoBehaviour
    {

        #region PROPERTIES

        public PlayerModel Player { get; set; }

        #endregion

        #region FUNCTIONS

        public int GetCurrentPlayerHealth()
        {
            return Player.CurrentHealth;
        }

        #endregion

        // Start is called before the first frame update
        void Start()
        {
            Player = FindObjectOfType<PlayerModel>();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}