using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SlotController : MonoBehaviour
{
    int Mana, Mana2;

    #region Public

    public GameObject SlotPrefab;
    List<GameObject> PlayerSlots;
    List<GameObject> PCSlots;

    GameObject PlayerDeck;
    Deck PCDeck;

    /// <summary>
    /// Current Player Card Selected
    /// </summary>
    int CurrentPlayerCard { get; set; } = -1;

    /// <summary>
    /// Current PC Card Selected
    /// </summary>
    int CurrentPCCard { get; set; } = -1;

    #endregion

    #region ATRIBUTES

    const int MaxSlots = 7;

    #endregion

    #region Properties

    bool FreeSlotIsEnabled { get; set; } = false;
    Card CurrentCard { get; set; }

    #endregion

    void Start()
    {
        CreateSlots();
        PlayerDeck = GameObject.FindWithTag("PlayerDeck");
        PCDeck = GameObject.FindWithTag("PCDeck").GetComponent<Deck>();
        Mana = 4;
    }

    void Update()
    {
    }

    #region Functions

    public void CreateSlots()
    {
        PlayerSlots = new List<GameObject>(MaxSlots);
        PCSlots = new List<GameObject>(MaxSlots);

        float xInitialPosition = -0.6f;
        const float yPosition = 0f;
        const float zPosition = 0.15f;

        for (int i = 0; i < MaxSlots; i++)
        {
            GameObject playerSlot = Instantiate(SlotPrefab);
            playerSlot.SetActive(true);
            playerSlot.transform.position = new Vector3(xInitialPosition, yPosition, -zPosition);
            playerSlot.GetComponent<Slot>().SlotID = i;
            playerSlot.GetComponent<Slot>().IsPlayer = true;
            PlayerSlots.Add(playerSlot);

            GameObject pcSlot = Instantiate(SlotPrefab);
            pcSlot.SetActive(true);
            pcSlot.transform.position = new Vector3(-xInitialPosition, yPosition, zPosition);
            pcSlot.GetComponent<Slot>().SlotID = i;
            pcSlot.GetComponent<Slot>().IsPlayer = false;
            PCSlots.Add(pcSlot);

            xInitialPosition += 0.2f;
        }
    }

    public void ToggleFreeSlots(bool isEqualIndex)
    {
        if (FreeSlotIsEnabled && isEqualIndex)
        {
            foreach (var slot in PlayerSlots)
            {
                if (!slot.GetComponent<Slot>().Card)
                {
                    slot.GetComponent<Slot>().SelectionEffect[2].SetActive(false);
                    slot.GetComponent<Slot>().CardTemplate.SetActive(false);
                }
            }

            FreeSlotIsEnabled = false;
        }
        else
        {
            foreach(var slot in PlayerSlots)
            {
                if(!slot.GetComponent<Slot>().Card)
                {
                    slot.GetComponent<Slot>().SelectionEffect[2].SetActive(true);
                    slot.GetComponent<Slot>().CardTemplate.SetActive(true);
                }
            }

            FreeSlotIsEnabled = true;
        }
    }

    public void InvokeCard(int slotId)
    {
        var card = PlayerDeck.GetComponent<Deck>().InvokeCard();

        if (card == null)
        {
            return;
        }

        var getStats = card.GetComponent<Stats>();

        if (getStats.Stars > Mana) return;

        PlayerSlots[slotId].GetComponent<Slot>().Card = Instantiate(card);
        PlayerSlots[slotId].GetComponent<Slot>().Card.SetActive(true);

        var slotPosition = PlayerSlots[slotId].GetComponent<Slot>().transform.position;
        var cardPrefabPosition = PlayerSlots[slotId].GetComponent<Slot>().Card.transform.position;

        PlayerSlots[slotId].GetComponent<Slot>().Card.transform.position = new Vector3
        (
            slotPosition.x, 
            cardPrefabPosition.y, 
            slotPosition.z
        );
        
        PlayerSlots[slotId].GetComponent<Slot>().SelectionEffect[2].SetActive(false);
        PlayerSlots[slotId].GetComponent<Slot>().CardTemplate.SetActive(false);
        PlayerSlots[slotId].GetComponent<Slot>().HealthBarObject.SetActive(true);
        PlayerSlots[slotId].GetComponent<Slot>().HealthBarObject.transform.position = new Vector3
        (
            slotPosition.x,
            0.2f,
            slotPosition.z
        );

        PlayerSlots[slotId].GetComponent<Slot>().HealthBarObject.GetComponentInChildren<HealthBar>().SetColor(true);

        ToggleFreeSlots(true);
        Mana++;
    }

    IEnumerator GameDelay(float time)
    {
        //Debug.Log("Start delay");
        yield return new WaitForSeconds(10f);
        //Debug.Log("End delay");
    }

    public void PCInvokeCard()
    {
        
        StartCoroutine(GameDelay(12));
        Debug.Log("End delay");
        var slot = PCSlots.Where(s => s.GetComponent<Slot>().Card == null).FirstOrDefault().GetComponent<Slot>();
        
        //Random invoke (hard data)
        int cardIndex = UnityEngine.Random.Range(0, 10);

        var card = PCDeck.Cards[cardIndex];

        PCSlots[slot.SlotID].GetComponent<Slot>().Card = Instantiate(card);
        PCSlots[slot.SlotID].GetComponent<Slot>().Card.SetActive(true);

        PCSlots[slot.SlotID].GetComponent<Slot>().IsPlayer = false;

        var slotPosition = PCSlots[slot.SlotID].GetComponent<Slot>().transform.position;
        var cardPrefabPosition = PCSlots[slot.SlotID].GetComponent<Slot>().Card.transform.position;

        PCSlots[slot.SlotID].GetComponent<Slot>().Card.transform.position = new Vector3
        (
            slotPosition.x,
            cardPrefabPosition.y,
            slotPosition.z
        );

        PCSlots[slot.SlotID].GetComponent<Slot>().Card.transform.Rotate(0.0f, 180.0f, 0.0f, Space.Self);

        //Health Bar
        PCSlots[slot.SlotID].GetComponent<Slot>().HealthBarObject.SetActive(true);
        PCSlots[slot.SlotID].GetComponent<Slot>().HealthBarObject.transform.position = new Vector3
        (
            slotPosition.x,
            0.2f,
            slotPosition.z
        );

        //PCSlots[slot.SlotID].GetComponent<Slot>().HealthBar.GetComponent<HealthBar>().SetColor(false);
        PCSlots[slot.SlotID].GetComponent<Slot>().HealthBarObject.GetComponentInChildren<HealthBar>().SetColor(false);
    }

    #endregion

    #region Select Card

    public Stats GetCard(int slotId, bool isPlayer)
    {
        if(isPlayer)
        {
            return PlayerSlots[slotId].GetComponent<Slot>().Card.GetComponent<Stats>();
        }
        else
        {
            return PCSlots[slotId].GetComponent<Slot>().Card.GetComponent<Stats>();
        }
    }

    public void ShowSelectionEffect(int slotId, bool isPlayer)
    {
        Debug.Log($"ShowSelectionEffect: {slotId} - {isPlayer}");
        if (isPlayer)
        {
            PlayerSlots[slotId].GetComponent<Slot>().ChangeSelectionEffect(1, true);
            PlayerSlots[slotId].GetComponent<Slot>().CardTemplate.SetActive(true);
            
            if(CurrentPlayerCard != slotId && CurrentPlayerCard != -1)
            {
                PlayerSlots[CurrentPlayerCard].GetComponent<Slot>().ChangeSelectionEffect(1, false);
                PlayerSlots[CurrentPlayerCard].GetComponent<Slot>().CardTemplate.SetActive(false);
            }

            CurrentPlayerCard = slotId;
        }
        else
        {
            PCSlots[slotId].GetComponent<Slot>().ChangeSelectionEffect(0, true);
            PCSlots[slotId].GetComponent<Slot>().CardTemplate.SetActive(true);

            if (CurrentPCCard != slotId && CurrentPCCard != -1)
            {
                PCSlots[CurrentPCCard].GetComponent<Slot>().ChangeSelectionEffect(0, false);
                PCSlots[CurrentPCCard].GetComponent<Slot>().CardTemplate.SetActive(false);
            }

            CurrentPCCard = slotId;
        }
    }

    public void SelectCard(int slotID, bool isPlayer)
    {
        /*
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
            ShowSelectionEffect(slotID, isPlayer);

            //Move Portrait Camera
            var postion = Potrait1.transform.position;
            Potrait1.transform.position = new Vector3(-0.4f + ((slotID - 1) * 0.2f), postion.y, postion.z);

            CurrentCard = slotID;
        }
        else
        {
            var card = SlotController.GetCard(slotID, isPlayer);
            //Load Card Stats in UI
            DamagePCCardImage.SetActive(true);
            HealthPCCardImage.SetActive(true);
            PCCardName.text = $"{card.Name}";
            PCCardStats.text = $"{card.AttackDamage}\n{card.CurrentHealth}";

            //Show Selection Effect
            SlotController.ShowSelectionEffect(slotID, isPlayer);

            // Move Portrait Camera
            var postion = Potrait2.transform.position;
            Potrait2.transform.position = new Vector3(0.4f - ((slotID - 1) * 0.2f), postion.y, postion.z);

            CurrentCard = slotID;
        }
        */
    }

    #endregion
}