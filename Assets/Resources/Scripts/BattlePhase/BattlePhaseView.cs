using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BattlePhase 
{
    public class BattlePhaseView : MonoBehaviour
    {
        #region ATRIBUTES

        [Header("Canvas Start Game")]
        public GameObject PlayButton;
        public GameObject FirstTurnCoin;
        public GameObject SecondTurnCoin;
        public GameObject BlackBackground;

        [Header("Canvas End Game")]
        public GameObject RestartButton;
        public GameObject WinImage;
        public GameObject LoseImage;

        [Header("Canvas Player Stats Cards")]
        public TextMeshProUGUI PlayerCardName;
        public TextMeshProUGUI PlayerCardStats;
        public GameObject PlayerDamageCardImage;
        public GameObject PlayerHealthCardImage;

        [Header("Canvas PC Stats Cards")]
        public TextMeshProUGUI ComputerCardName;
        public TextMeshProUGUI ComputerCardStats;
        public GameObject ComputerDamageCardImage;
        public GameObject ComputerHealthCardImage;

        [Header("Selection and Target Area")]
        public List<GameObject> SelectionAreas;
        public List<GameObject> TargetAreas;
        public List<GameObject> EnemyAreas;

        [Header("Battle Canvas")]
        public GameObject AttackButton;

        #endregion

        #region PROPERTIES



        #endregion

        #region FUNCTIONS

        public void LoadPlayerCardStats(string cardName, int attackDamage, int currentHealth)
        {
            PlayerCardName.text = cardName;
            PlayerCardStats.text = $"{ attackDamage }\n{ currentHealth }";
        }

        public void ChangePlayerStatsImageState(bool state)
        {
            PlayerDamageCardImage.SetActive(state);
            PlayerHealthCardImage.SetActive(state);
        }

        public void LoadComputerCardStats(string cardName, int attackDamage, int currentHealth)
        {
            ComputerCardName.text = cardName;
            ComputerCardStats.text = $"{ attackDamage }\n{ currentHealth }";
        }

        public void ChangeComputerStatsImageState(bool state)
        {
            ComputerDamageCardImage.SetActive(state);
            ComputerHealthCardImage.SetActive(state);
        }


        #endregion


        // Start is called before the first frame update
        void Start()
        {
            AttackButton!.SetActive(false);

            BlackBackground!.SetActive(true);
        }



        // Update is called once per frame
        void Update()
        {
        }
    }
}