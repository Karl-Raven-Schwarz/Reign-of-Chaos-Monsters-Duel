using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Threading;
using System.Linq;
using TMPro;

public class SceneController : MonoBehaviour
{
    #region Slots

    [Header("Slots")]
    public List<GameObject> PlayerSlots;
    public List<GameObject> PCSlots;

    SlotController SlotController;

    #endregion

    public List<GameObject> PCCards;
    int PCCardsAliveCount;
    public List<GameObject> PlayerCards;
    int PlayerCardsAliveCount;
    //public List<GameObject> PlayerCards = new List<GameObject>();

    public GameObject Potrait1;
    public GameObject Potrait2;


    public GameObject SelectionCanvas;
    public GameObject EndGameCanvas;
    public GameObject BattleCanvas;
    private List<int> PlayerIdCards;
    private List<int> PCIdCards;
    private GamePhase Phase = GamePhase.Selection;


    #region Hit Particles

    [Header("Hit Particles")]
    public List<ParticleSystem> PlayerHitEffects;
    public List<ParticleSystem> PCHitEffects;

    #endregion

    #region Canvas Start Game

    [Header("Canvas Start Game")]
    public GameObject PlayButton;
    public GameObject Coin1;
    public GameObject Coin2;
    public GameObject BlackBackground;

    #endregion

    #region Canvas End Game

    [Header("Canvas Start Game")]
    public GameObject RestartButton;
    public GameObject WinImage;
    public GameObject LoseImage;

    #endregion

    #region Stats Canvas

    [Header("Canvas Stats Cards")]
    public TextMeshProUGUI PlayerCardName;
    public TextMeshProUGUI PlayerCardStats;
    public GameObject DamagePlayerCardImage;
    public GameObject HealthPlayerCardImage;

    public TextMeshProUGUI PCCardName;
    public TextMeshProUGUI PCCardStats;
    public GameObject DamagePCCardImage;
    public GameObject HealthPCCardImage;

    public TextMeshProUGUI PlayerDeck;
    public TextMeshProUGUI TMLogs;

    #endregion


    #region Canvas for Card Selection in Battle

    [Header("Canvas for Card Selection in Battle")]
    public GameObject SelectionArea;
    public GameObject TargetArea;

    #endregion

    #region Selection and Target Area

    [Header("Selection and Target Area")]
    public List<GameObject> SelectionAreas;
    public List<GameObject> TargetAreas;
    public List<GameObject> EnemyAreas;

    #endregion

    #region Sounds

    [Header("Sounds")]
    public AudioSource PlaySound;
    public AudioSource RestartSound;
    public AudioSource AttackSound;

    public AudioSource HitSound;
    public AudioSource YouTurnSound;
    public AudioSource WinSound;
    public AudioSource LoseSound;
    public AudioSource MonsterDying;

    #endregion

    #region Battle Canvas

    [Header("Battle Canvas")]
    public GameObject AttackButton;

    #endregion

    #region Properties

    [Header("Properties")]
    //public GameObject Background;

    bool initBattle = false;
    bool IsSetPlayerCards = false;

    private int target = 0;

    public int Target {
        get { return target; }
        set { 
            if (value == 0) 
            {
                PCCardName.text = $"";
                PCCardStats.text = $"";
                DamagePCCardImage.SetActive(false);
                HealthPCCardImage.SetActive(false);
            }
            else 
            {
                if(value == target) 
                {
                    for (int i = 0; i < PCCards.Count; i++) 
                    {
                        if (PCCards[i].GetComponent<Stats>().Id == value) 
                        {
                            var card =  PCCards[i].GetComponent<Stats>();
                            PCCardStats.text = $"{card.AttackDamage}\n{card.CurrentHealth}";
                            break;
                        }
                    }   
                }

                if (IsUserTurn) IsUserTurn = true;
            }

            target = value; 
        }
    }

    private int currentCard = 0;

    public int CurrentCard {
        get { return currentCard; }
        set { 
            if (value == 0) {
                PlayerCardName.text = $"";
                PlayerCardStats.text = $"";
                DamagePlayerCardImage.SetActive(false);
                HealthPlayerCardImage.SetActive(false);
            }
            else {
                if(value == currentCard) {
                    for (int i = 0; i < PlayerCards.Count; i++) {
                        if (PlayerCards[i].GetComponent<Stats>().Id == value) {
                            var card =  PlayerCards[i].GetComponent<Stats>();
                            PlayerCardStats.text = $"{card.AttackDamage}\n{card.CurrentHealth}";
                            break;
                        }
                    }   
                }

                if (IsUserTurn) IsUserTurn = true;
            }

            currentCard = value; 
        }
    }

    private bool isUserTurn;

    public bool IsUserTurn {
        get { return isUserTurn; }
        set {
            if (value && Target != 0 && CurrentCard != 0) {
                AttackButton.SetActive(true);
            }
            else AttackButton.SetActive(false);

            isUserTurn = value;
        }
    }

    #endregion

    void Start() {
        SlotController = FindObjectOfType<SlotController>();

        // Scene COntroller
        IsSetPlayerCards = false;
        PlayerIdCards = new List<int> ();
        PCIdCards = new List<int> ();

        for (int i = 0; i < PCCards.Count; i++) {
            PCIdCards.Add(PCCards[i].GetComponent<Stats>().Id);
        }

        PCCardsAliveCount = PCCards.Count;

        for (int i = 0; i < PlayerCards.Count; i++) {
            PlayerIdCards.Add(PlayerCards[i].GetComponent<Stats>().Id);
        }
         PlayerCardsAliveCount = PlayerCards.Count;

        //Phase = GamePhase.Selection;
        Phase = GamePhase.Waiting;
        //TMLogs.text = "SELECTION PHASE";
        LoadPlayerDeck();
        SetPlayerCards();
        //initBattle = false;

        //------
        SelectionArea.SetActive(false);
        TargetArea.SetActive(false);
        EndGameCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if (Phase == GamePhase.Battle) {
            if (!IsSetPlayerCards) SetPlayerCards();

            if ((PlayerCardsAliveCount == 0 || PCCardsAliveCount == 0) && initBattle) {
                Phase = GamePhase.End;
                EndGame();
            }
        }
    }

    #region Functions
    public void TargetFound() {
        if (Phase == GamePhase.Waiting) PlayButton.SetActive(true);
    }

    public void TargetLost() {
        if (Phase == GamePhase.Waiting) PlayButton.SetActive(false);
    }

    public void StartGame() 
    {
        PlaySound.Play(0);
        PlayButton.SetActive(false);
        SelectionCanvas.SetActive(false);
        BlackBackground.SetActive(false);
        BattleCanvas.SetActive(true);
        //Background.SetActive(false);
        StartCoroutine(StartGameDelay());
    }

    IEnumerator StartGameDelay() {
        System.Random rnd = new System.Random();
        var getTurn = rnd.Next(0, 2);
        Phase = GamePhase.Battle;

        if(getTurn == 0) {
            IsUserTurn = true;
            Debug.Log("Player 1");
            Coin1.SetActive(true);
            
            yield return new WaitForSeconds(1f);

            Coin1.SetActive(false);
            Debug.Log("Player 2");
        }
        else {
            IsUserTurn = false;
            Debug.Log("PC 1");
            Coin2.SetActive(true);

            yield return new WaitForSeconds(1f);

            Coin2.SetActive(false);
            StartCoroutine(StartEnemyAttack());
            Debug.Log("PC 2");
        }
        initBattle = true;
    }

    IEnumerator GameDelay(float time) {
        Debug.Log("Start delay");
        yield return new WaitForSeconds(10f);
        Debug.Log("End delay");
    }

    void LoadCardsStats() {
        if (CurrentCard != 0) {
            for (int i = 0; i < PlayerCards.Count; i++) {
                if (PlayerCards[i].GetComponent<Stats>().Id == CurrentCard) {
                    var card = PlayerCards[i].GetComponent<Stats>();
                    PlayerCardName.text = $"{card.Name}";
                    PlayerCardStats.text = $"{card.AttackDamage}\n{card.CurrentHealth}";
                }
            }
        }

        if (Target != 0) {
            for (int i = 0; i < PCCards.Count; i++) {
                if (PCCards[i].GetComponent<Stats>().Id == CurrentCard) {
                    var card = PCCards[i].GetComponent<Stats>();
                    PCCardStats.text = $"{card.Name}";
                    PCCardStats.text = $"{card.AttackDamage}\n{card.CurrentHealth}";
                }
            }
        }
    }

    void SetPlayerCards() {
        for(int i = 0; i < PlayerCards.Count; i++) PlayerCards[i].GetComponent<Stats>().Player = 1;
        IsSetPlayerCards = true;
    }

    void EndGame() {
        //Background.SetActive(true);
        if (PlayerCardsAliveCount > 0) 
        {
            WinSound.Play(0);
            WinImage.SetActive(true);
            foreach(var i in PCCards) i.SetActive(false);
        }
        else 
        {
            LoseSound.Play(0);
            LoseImage.SetActive(true);
            foreach(var i in PlayerCards) i.SetActive(false);
        }

        BattleCanvas.SetActive(false);
        EndGameCanvas.SetActive(true);
    }

    public void Restart() {
        RestartSound.Play(0);
        RestartButton.SetActive(false);
        StartCoroutine(RestartGameDelay());
    }

    IEnumerator RestartGameDelay() {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(0);
        EndGameCanvas.SetActive(false);
    }

    public void SelectCard(int id, string name) 
    {
        if (Phase == GamePhase.Selection) 
        {
            if(PlayerIdCards.Contains(id) == false && PlayerIdCards.Count < 3) 
            { 
                for (int i = 0; i < PCCards.Count; i++) {
                    if (PCCards[i].GetComponent<Stats>().Id == id) {
                        PlayerCards.Add(PCCards[i]);
                        PCCards.RemoveAt(i);
                    }
                }

                PlayerIdCards.Add(id);
                PlayerDeck.text += $"- {name}\n";
                PCIdCards.Remove(id);

                if(PlayerIdCards.Count == 3) {
                    Phase = GamePhase.Battle;
                    TMLogs.text = "-BATTLE PHASE";

                    System.Random rnd = new System.Random();
                    var getTurn = rnd.Next(0, 2);
                    initBattle = true;

                    if(getTurn == 0) {
                        IsUserTurn = true;
                        TMLogs.text = $"-YOU START!\n" + TMLogs.text;
                    }
                    else {
                        IsUserTurn = false;
                        TMLogs.text = $"-PC STARTS!\n" + TMLogs.text;
                    }
                }
            }
        }
    }

    public GamePhase GetPhase() {
        return Phase;
    }

    public bool ContainsPlayer1(int id) {
        return PlayerIdCards.Contains(id) ? true : false;
    }

    public bool ContainsPlayer2(int id) {
        return PCIdCards.Contains(id) ? true : false;
    }

    public void Attack2() {
        AttackButton.SetActive(false);
        if(!IsUserTurn) return;

        AttackSound.Play(0);
        StartCoroutine(AttackGameDelay());
    }

    IEnumerator AttackGameDelay() 
    {
        var target = Target;
        var currentCard = CurrentCard;
        yield return new WaitForSeconds(0.5f);
        var enemyCard = 0;

        for(int i = 0; i < PCCards.Count; i++) 
        {
            if (PCCards[i].GetComponent<Stats>().Id == target) 
            {
                enemyCard = i;
                break;
            }
        }

        var myDamage = 0;
        for(int i = 0; i < PlayerCards.Count; i++) {
            if (PlayerCards[i].GetComponent<Stats>().Id == currentCard) {
                myDamage = PlayerCards[i].GetComponent<Stats>().AttackDamage;
                break;
            }
        }

        PCCards[enemyCard].GetComponent<Stats>().CurrentHealth -= myDamage;

        if(PCCards[enemyCard].GetComponent<Stats>().CurrentHealth < 1) 
        {
            PCHitEffects[enemyCard].Play();
            HitSound.Play(0);
            yield return new WaitForSeconds(0.5f);

            if(target == PCCards[enemyCard].GetComponent<Stats>().Id) 
            {
                Target = 0;
            }

            //PCIdCards.Remove(PCCards[enemyCard].GetComponent<Stats>().Id);
            //PCHitEffects.RemoveAt(enemyCard);
            
            PCCards[enemyCard].SetActive(false);
            //PCCards.RemoveAt(enemyCard);

            TargetAreas[enemyCard].SetActive(false);
            //TargetAreas.RemoveAt(enemyCard);
            //EnemyAreas.RemoveAt(enemyCard);
            PCCardsAliveCount--;
            MonsterDying.Play(0);
        }
        else 
        {
            //Target = Target;
            PCHitEffects[enemyCard].Play();
            HitSound.Play(0);
        }

        IsUserTurn = false;
        yield return new WaitForSeconds(0.25f);
        YouTurnSound.Play(0);
        StartCoroutine(StartEnemyAttack());
    }

    #endregion
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
            EnemyAreas[cartaMayorDamage.Index].SetActive(true);
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

            if(PlayerCards[minHealth.Index].GetComponent<Stats>().CurrentHealth < 1) {
                PlayerHitEffects[minHealth.Index].Play();
                HitSound.Play(0);
                yield return new WaitForSeconds(0.5f);
                
                if(CurrentCard == PlayerCards[minHealth.Index].GetComponent<Stats>().Id) {
                    CurrentCard = 0;
                }

                //PlayerIdCards.Remove(PlayerCards[minHealthIndex].GetComponent<Stats>().Id);
                //PlayerHitEffects.RemoveAt(minHealthIndex);
                
                PlayerCards[minHealth.Index].SetActive(false);
                //PlayerCards.RemoveAt(minHealthIndex);

                SelectionAreas[minHealth.Index].SetActive(false);
                //SelectionAreas.RemoveAt(minHealthIndex);

                MonsterDying.Play(0);
                PlayerCardsAliveCount--;
            }
            else 
            {
                CurrentCard = CurrentCard;
                PlayerHitEffects[minHealth.Index].Play();
                HitSound.Play(0);
            }

            EnemyAreas[maxDamageIndex].SetActive(false);
            IsUserTurn = true;
            yield return new WaitForSeconds(0.25f);
            YouTurnSound.Play(0);
        }
    }

    public void LoadCurrentCard(Stats card) {
        if(!initBattle) return;

        if(PlayerIdCards.Contains(card.Id) && card.Player == 1) 
        {
            
            DamagePlayerCardImage.SetActive(true);
            HealthPlayerCardImage.SetActive(true);
            PlayerCardName.text = $"{card.Name}";
            PlayerCardStats.text = $"{card.AttackDamage}\n{card.CurrentHealth}";

            for (int i = 0; i < PlayerCards.Count; i++) {
                if(PlayerCards[i].GetComponent<Stats>().Id == CurrentCard) SelectionAreas[i].SetActive(false);

                if (PlayerCards[i].GetComponent<Stats>().Id == card.Id) {
                    SelectionAreas[i].SetActive(true);
                    var postion = Potrait1.transform.position;
                    Potrait1.transform.position = new Vector3( -0.4f + i*0.2f, postion.y, postion.z);
                }
            }   
            CurrentCard = card.Id;
            return;
        }

        if(PCIdCards.Contains(card.Id) && card.Player == 0) 
        {
            DamagePCCardImage.SetActive(true);
            HealthPCCardImage.SetActive(true);
            PCCardName.text = $"{card.Name}";
            PCCardStats.text = $"{card.AttackDamage}\n{card.CurrentHealth}";

            for (int i = 0; i < PCCards.Count; i++) 
            {
                if(PCCards[i].GetComponent<Stats>().Id == Target) TargetAreas[i].SetActive(false);

                if (PCCards[i].GetComponent<Stats>().Id == card.Id) 
                { 
                    TargetAreas[i].SetActive(true);
                    var postion = Potrait2.transform.position;
                    Potrait2.transform.position = new Vector3(-0.4f + i * 0.2f, postion.y, postion.z);
                }
            }   
            Target = card.Id;
        }
    }

    private void LoadPlayerDeck () 
    {
        for (int i = 0; i < PlayerCards.Count; i++) {
            var stats = PlayerCards[i].GetComponent<Stats>();
            var position = PlayerCards[i].transform.position;
            //PlayerDeck.text += $"-{stats.Name}\n";
        }
    }

    public enum GamePhase {
        Selection,
        Waiting,
        Battle,
        End
    }

    #region SLOT CONTROLLER

    public void SelectCard(int slotID, bool isPlayer)
    {
        if (!initBattle) return;

        if (isPlayer)
        {
            var card = SlotController.GetCard(slotID, isPlayer);

            //Load Card Stats in UI
            DamagePlayerCardImage.SetActive(true);
            HealthPlayerCardImage.SetActive(true);
            PlayerCardName.text = $"{card.Name}";
            PlayerCardStats.text = $"{card.AttackDamage}\n{card.CurrentHealth}";

            //Show Selection Effect
            SlotController.ShowSelectionEffect(slotID, isPlayer);

            //Move Potrait Camera

            var postion = Potrait1.transform.position;
            Potrait1.transform.position = new Vector3(-0.4f + ((slotID - 1) * 0.2f), postion.y, postion.z);

            CurrentCard = slotID;
            return;
        }
        /*
        if (PCIdCards.Contains(card.Id) && card.Player == 0)
        {
            DamagePCCardImage.SetActive(true);
            HealthPCCardImage.SetActive(true);
            PCCardName.text = $"{card.Name}";
            PCCardStats.text = $"{card.AttackDamage}\n{card.CurrentHealth}";

            for (int i = 0; i < PCCards.Count; i++)
            {
                if (PCCards[i].GetComponent<Stats>().Id == Target) TargetAreas[i].SetActive(false);

                if (PCCards[i].GetComponent<Stats>().Id == card.Id)
                {
                    TargetAreas[i].SetActive(true);
                    var postion = Potrait2.transform.position;
                    Potrait2.transform.position = new Vector3(-0.4f + i * 0.2f, postion.y, postion.z);
                }
            }
            Target = card.Id;
        }
        */
    }

    #endregion

    public void Surrender()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
