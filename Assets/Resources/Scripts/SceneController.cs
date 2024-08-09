using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

namespace BattlePhase
{
    public class SceneController : MonoBehaviour
    {
        #region PROPIERTIES

        [Header("Slots")]

        public List<GameObject> PlayerSlots;
        public List<GameObject> PCSlots;

        public List<GameObject> PCCards;
        int PCCardsAliveCount;
        public List<GameObject> PlayerCards;
        int PlayerCardsAliveCount;
        //public List<GameObject> PlayerCards = new List<GameObject>();

        public GameObject Portrait1;
        public GameObject Portrait2;

        private List<int> PlayerIdCards;
        private List<int> PCIdCards;
        
        
        SlotController SlotController { get; set; }
        PlayerController PlayerController { get; set; }

        
        BattlePhaseView BattlePhaseView { get; set; }
        GamePhase Phase { set; get; } = GamePhase.Start;

        private int target;
        public int Target
        {
            get { return target; }
            set
            {
                if (value == 0)
                {
                    BattlePhaseView.LoadComputerCardStats("", 0, 0);
                    BattlePhaseView.ChangeComputerStatsImageState(false);
                }
                else
                {
                    if (value == target)
                    {
                        for (int i = 0; i < PCCards.Count; i++)
                        {
                            if (PCCards[i].GetComponent<Stats>().Id == value)
                            {
                                var card = PCCards[i].GetComponent<Stats>();
                                BattlePhaseView.LoadComputerCardStats(card.Name, card.AttackDamage, card.CurrentHealth);
                                break;
                            }
                        }
                    }

                    if (IsUserTurn) IsUserTurn = true;
                }

                target = value;
            }
        }

        public int CurrentCard
        {
            get { return CurrentCard; }
            set
            {
                if (value == 0)
                {
                    BattlePhaseView.LoadPlayerCardStats("", 0, 0);
                    BattlePhaseView.ChangePlayerStatsImageState(false);
                }
                else
                {
                    if (value == CurrentCard)
                    {
                        for (int i = 0; i < PlayerCards.Count; i++)
                        {
                            if (PlayerCards[i].GetComponent<Stats>().Id == value)
                            {
                                var card = PlayerCards[i].GetComponent<Stats>();
                                BattlePhaseView.LoadComputerCardStats(card.Name, card.AttackDamage, card.CurrentHealth);
                                break;
                            }
                        }
                    }

                    if (IsUserTurn)
                    {
                        IsUserTurn = true;
                    }
                }

                CurrentCard = value;
            }
        }

        bool isUserTurn;
        public bool IsUserTurn
        {
            get { return isUserTurn; }
            set
            {
                if (value && Target != 0 && CurrentCard != 0)
                {
                    //BattlePhaseView.AttackButton.SetActive(true);
                }
                else
                {
                    //BattlePhaseView.AttackButton.SetActive(false);
                }

                isUserTurn = value;
            }
        }

        #endregion

        #region FUNCTIONS

        IEnumerator StartGame()
        {
            var getTurn = new System.Random().Next(0, 2);
            IsUserTurn = getTurn == 0;

            //show Start Game Panel
            BattlePhaseView.StartGame(true, IsUserTurn);

            yield return new WaitForSeconds(1f);

            //Hide Start Game Panel
            BattlePhaseView.StartGame(false, IsUserTurn);
            
            Phase = GamePhase.Battle;
        }

        void EndGame(bool playerIsWinner)
        {
            if (playerIsWinner)
            {
                BattlePhaseView.WinSound.Play(0);
                BattlePhaseView.WinImage.SetActive(true);
                foreach (var i in PCCards) i.SetActive(false);
            }
            else
            {
                BattlePhaseView.LoseSound.Play(0);
                BattlePhaseView.LoseImage.SetActive(true);
                foreach (var i in PlayerCards) i.SetActive(false);
            }

            //BattleCanvas.SetActive(false);
            //EndGameCanvas.SetActive(true);
        }

        #endregion

        public void TargetFound()
        {
            //if (Phase == GamePhase.Waiting) BattlePhaseView.PlayButton.SetActive(true);
        }

        public void TargetLost()
        {
            //if (Phase == GamePhase.Waiting) BattlePhaseView.PlayButton.SetActive(false);
        }

        

        void SetPlayerCards()
        {
            for (int i = 0; i < PlayerCards.Count; i++) PlayerCards[i].GetComponent<Stats>().Player = 1;
            //IsSetPlayerCards = true;
        }


        public void Restart()
        {
            BattlePhaseView.RestartSound.Play(0);
            BattlePhaseView.RestartButton.SetActive(false);
            StartCoroutine(RestartGameDelay());
        }

        IEnumerator RestartGameDelay()
        {
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene(0);
            //EndGameCanvas.SetActive(false);
        }

        public void SelectCard(int id, string name)
        {
            if (Phase == GamePhase.Start)
            {
                if (PlayerIdCards.Contains(id) == false && PlayerIdCards.Count < 3)
                {
                    for (int i = 0; i < PCCards.Count; i++)
                    {
                        if (PCCards[i].GetComponent<Stats>().Id == id)
                        {
                            PlayerCards.Add(PCCards[i]);
                            PCCards.RemoveAt(i);
                        }
                    }

                    PlayerIdCards.Add(id);
                    //BattlePhaseView.PlayerDeck.text += $"- {name}\n";
                    PCIdCards.Remove(id);

                    if (PlayerIdCards.Count == 3)
                    {
                        Phase = GamePhase.Battle;
                        //BattlePhaseView.TMLogs.text = "-BATTLE PHASE";

                        System.Random rnd = new ();
                        var getTurn = rnd.Next(0, 2);
                        //initBattle = true;

                        if (getTurn == 0)
                        {
                            IsUserTurn = true;
                            //BattlePhaseView.TMLogs.text = $"-YOU START!\n" + BattlePhaseView.TMLogs.text;
                        }
                        else
                        {
                            IsUserTurn = false;
                            //BattlePhaseView.TMLogs.text = $"-PC STARTS!\n" + BattlePhaseView.TMLogs.text;
                        }
                    }
                }
            }
        }

        public GamePhase GetPhase()
        {
            return Phase;
        }

        public bool ContainsPlayer1(int id)
        {
            return PlayerIdCards.Contains(id);
        }

        public bool ContainsPlayer2(int id)
        {
            return PCIdCards.Contains(id);
        }

        public void Attack2()
        {
            //BattlePhaseView.AttackButton.SetActive(false);
            if (!IsUserTurn) return;

            BattlePhaseView.AttackSound.Play(0);
            StartCoroutine(AttackGameDelay());
        }

        IEnumerator AttackGameDelay()
        {
            var target = Target;
            var currentCard = CurrentCard;
            yield return new WaitForSeconds(0.5f);
            var enemyCard = 0;

            for (int i = 0; i < PCCards.Count; i++)
            {
                if (PCCards[i].GetComponent<Stats>().Id == target)
                {
                    enemyCard = i;
                    break;
                }
            }

            var myDamage = 0;
            for (int i = 0; i < PlayerCards.Count; i++)
            {
                if (PlayerCards[i].GetComponent<Stats>().Id == currentCard)
                {
                    myDamage = PlayerCards[i].GetComponent<Stats>().AttackDamage;
                    break;
                }
            }

            PCCards[enemyCard].GetComponent<Stats>().CurrentHealth -= myDamage;

            if (PCCards[enemyCard].GetComponent<Stats>().CurrentHealth < 1)
            {
                //PCHitEffects[enemyCard].Play();
                BattlePhaseView.HitSound.Play(0);
                yield return new WaitForSeconds(0.5f);

                if (target == PCCards[enemyCard].GetComponent<Stats>().Id)
                {
                    Target = 0;
                }

                //PCIdCards.Remove(PCCards[enemyCard].GetComponent<Stats>().Id);
                //PCHitEffects.RemoveAt(enemyCard);

                PCCards[enemyCard].SetActive(false);
                //PCCards.RemoveAt(enemyCard);

                //TargetAreas[enemyCard].SetActive(false);
                //TargetAreas.RemoveAt(enemyCard);
                //EnemyAreas.RemoveAt(enemyCard);
                PCCardsAliveCount--;
                BattlePhaseView.MonsterDyingSound.Play(0);
            }
            else
            {
                //Target = Target;
                //PCHitEffects[enemyCard].Play();
                BattlePhaseView.HitSound.Play(0);
            }

            IsUserTurn = false;
            yield return new WaitForSeconds(0.25f);
            BattlePhaseView.YouTurnSound.Play(0);
            StartCoroutine(StartEnemyAttack());
        }

        
        //----------------------------------------------

        IEnumerator StartEnemyAttack()
        {
            yield return new WaitForSeconds(0.5f);

            if (GetPhase() == GamePhase.Battle && !IsUserTurn)
            {

                int maxDamageIndex = 0;
                var getCurrentMaxDamage = PCCards.Where(s => s.GetComponent<Stats>().CurrentHealth > 0).ToList();
                //var maxDamage = getCurrentMaxDamage[0].GetComponent<Stats>();

                var cartaMayorDamage = PCCards
                    .Select((carta, index) => new { Index = index, Carta = carta })
                    .Where(x => x.Carta.GetComponent<Stats>().CurrentHealth > 0)
                    .OrderByDescending(x => x.Carta.GetComponent<Stats>().CurrentAttackDamage)
                    .First();

                /*
                if (PCCards.Count > 1) 
                {
                    for (int i = 1; i < PCCards.Count; i++) 
                    {
                        if (maxDamage.CurrentAttackDamage < PCCards[i].GetComponent<Stats>().CurrentAttackDamage &&
                            PCCards[i].GetComponent<Stats>().CurrentHealth > 0
                            ) 
                        {
                            maxDamageIndex = i;
                            maxDamage = PCCards[i].GetComponent<Stats>();
                        }
                    }
                }
                */
                //BattlePhaseView.EnemyAreas[cartaMayorDamage.Index].SetActive(true);
                yield return new WaitForSeconds(1.5f);

                int minHealthIndex = 0;
                //int minHealth = PlayerCards[0].GetComponent<Stats>().CurrentHealth;

                var minHealth = PlayerCards
                    .Select((carta, index) => new { Index = index, Carta = carta })
                    .Where(x => x.Carta.GetComponent<Stats>().CurrentHealth > 0)
                    .OrderByDescending(x => x.Carta.GetComponent<Stats>().CurrentHealth)
                    .Last();

                /*
                if (PlayerCards.Count > 1) {
                    for (int i = 1; i < PlayerCards.Count; i++) {
                        if (minHealth > PlayerCards[i].GetComponent<Stats>().CurrentHealth &&
                            PlayerCards[i].GetComponent<Stats>().CurrentHealth > 0) {
                            minHealthIndex = i;
                            minHealth = PlayerCards[i].GetComponent<Stats>().CurrentHealth;
                        }
                    }
                }
                */

                PlayerCards[minHealth.Index].GetComponent<Stats>().CurrentHealth -= cartaMayorDamage.Carta.GetComponent<Stats>().CurrentAttackDamage;

                if (PlayerCards[minHealth.Index].GetComponent<Stats>().CurrentHealth < 1)
                {
                    //PlayerHitEffects[minHealth.Index].Play();
                    BattlePhaseView.HitSound.Play(0);
                    yield return new WaitForSeconds(0.5f);

                    if (CurrentCard == PlayerCards[minHealth.Index].GetComponent<Stats>().Id)
                    {
                        CurrentCard = 0;
                    }

                    //PlayerIdCards.Remove(PlayerCards[minHealthIndex].GetComponent<Stats>().Id);
                    //PlayerHitEffects.RemoveAt(minHealthIndex);

                    PlayerCards[minHealth.Index].SetActive(false);
                    //PlayerCards.RemoveAt(minHealthIndex);

                    //SelectionAreas[minHealth.Index].SetActive(false);
                    //SelectionAreas.RemoveAt(minHealthIndex);

                    BattlePhaseView.MonsterDyingSound.Play(0);
                    PlayerCardsAliveCount--;
                }
                else
                {
                    CurrentCard = CurrentCard;
                    //PlayerHitEffects[minHealth.Index].Play();
                    BattlePhaseView.HitSound.Play(0);
                }

                //BattlePhaseView.EnemyAreas[maxDamageIndex].SetActive(false);
                IsUserTurn = true;
                yield return new WaitForSeconds(0.25f);
                BattlePhaseView.YouTurnSound.Play(0);
            }
        }

        public void LoadCurrentCard(Stats card)
        {
            //if (!initBattle) return;

            if (PlayerIdCards.Contains(card.Id) && card.Player == 1)
            {
                BattlePhaseView.ChangePlayerStatsImageState(true);

                BattlePhaseView.LoadPlayerCardStats(card.Name, card.AttackDamage, card.CurrentHealth);

                for (int i = 0; i < PlayerCards.Count; i++)
                {
                    if (PlayerCards[i].GetComponent<Stats>().Id == CurrentCard)
                    {
                        //SelectionAreas[i].SetActive(false);
                    }

                    if (PlayerCards[i].GetComponent<Stats>().Id == card.Id)
                    {
                        //SelectionAreas[i].SetActive(true);
                        var postion = Portrait1.transform.position;
                        Portrait1.transform.position = new Vector3(-0.4f + i * 0.2f, postion.y, postion.z);
                    }
                }
                CurrentCard = card.Id;
                return;
            }

            if (PCIdCards.Contains(card.Id) && card.Player == 0)
            {
                BattlePhaseView.ChangeComputerStatsImageState(true);

                BattlePhaseView.LoadComputerCardStats(card.Name, card.AttackDamage, card.CurrentHealth);

                for (int i = 0; i < PCCards.Count; i++)
                {
                    if (PCCards[i].GetComponent<Stats>().Id == Target)
                    {
                        //TargetAreas[i].SetActive(false);
                    }

                    if (PCCards[i].GetComponent<Stats>().Id == card.Id)
                    {
                        //TargetAreas[i].SetActive(true);
                        var postion = Portrait2.transform.position;
                        Portrait2.transform.position = new Vector3(-0.4f + i * 0.2f, postion.y, postion.z);
                    }
                }
                Target = card.Id;
            }
        }

        private void LoadPlayerDeck()
        {
            for (int i = 0; i < PlayerCards.Count; i++)
            {
                var stats = PlayerCards[i].GetComponent<Stats>();
                var position = PlayerCards[i].transform.position;
                //PlayerDeck.text += $"-{stats.Name}\n";
            }
        }

        

        public void Surrender()
        {
            SceneManager.LoadSceneAsync(1);
        }

        public enum GamePhase
        {
            Start,
            Battle,
            End,
            Paused,
        }

        void Start()
        {
            SlotController = FindObjectOfType<SlotController>();
            BattlePhaseView = FindObjectOfType<BattlePhaseView>();
            PlayerController = FindObjectOfType<PlayerController>();

            Phase = GamePhase.Start;

            StartCoroutine(StartGame());
        }

        void Update()
        {
            if (Phase == GamePhase.Battle)
            {
                if (PlayerController.GetCurrentPlayerHealth() == 0 || PCCardsAliveCount == 0)
                {
                    Phase = GamePhase.End;
                    EndGame(true);
                }
            }
        }
    }
}