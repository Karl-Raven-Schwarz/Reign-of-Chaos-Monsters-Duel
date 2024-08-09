using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BattlePhase 
{
    public class BattlePhaseView : MonoBehaviour
    {
        #region ATRIBUTES

        [Header("Start Game Panel")]
        [SerializeField] private GameObject StartGamePanel;
        [SerializeField] private GameObject FirstTurnCoin;
        [SerializeField] private GameObject SecondTurnCoin;

        [Header("Battle Canvas")]
        //public GameObject AttackButton;
        public GameObject BattleCanvas;

        [Header("Canvas End Game")]
        public GameObject EndGameCanvas;
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

        [Header("Sounds")]
        public AudioSource PlaySound;
        public AudioSource RestartSound;
        public AudioSource AttackSound;
        public AudioSource HitSound;

        /// <summary>
        /// Sound for user turn
        /// </summary>
        public AudioSource YouTurnSound;
        public AudioSource WinSound;
        public AudioSource LoseSound;
        public AudioSource MonsterDyingSound;

        #endregion

        #region PROPERTIES



        #endregion

        #region FUNCTIONS

        public void StartGame(bool showCoins, bool playerIsFirst)
        {
            if (showCoins)
            {

                StartGamePanel.SetActive(true);

                if (playerIsFirst)
                {
                    FirstTurnCoin.SetActive(true);
                    SecondTurnCoin.SetActive(false);
                }
                else
                {
                    FirstTurnCoin.SetActive(false);
                    SecondTurnCoin.SetActive(true);
                }
            }
            else
            {
                StartGamePanel.SetActive(false);
            }
        }

        public void EndGame(bool playerIsWinner)
        {
            if(playerIsWinner)
            {
                WinImage.SetActive(true);
                LoseImage.SetActive(false);
            }
            else
            {
                WinImage.SetActive(false);
                LoseImage.SetActive(true);
            }
        }

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
        }



        // Update is called once per frame
        void Update()
        {
        }
    }
}