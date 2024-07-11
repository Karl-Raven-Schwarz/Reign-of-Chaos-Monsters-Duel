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

    int CurrentCardSelected { get; set; } = -1;

    /// <summary>
    /// -1 = None, 0 = Player and 1 = PC
    /// </summary>
    int IsPlayerDeckSelected { get; set; } = -1;

    #endregion

    #region Properties

    const int MaxSlots = 7;

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
            PlayerSlots.Add(playerSlot);

            GameObject pcSlot = Instantiate(SlotPrefab);
            pcSlot.SetActive(true);
            pcSlot.transform.position = new Vector3(-xInitialPosition, yPosition, zPosition);
            pcSlot.GetComponent<Slot>().SlotID = i;
            PCSlots.Add(pcSlot);

            xInitialPosition += 0.2f;
        }
    }

    public void ShowFreeSlots()
    {
        foreach(var slot in PlayerSlots)
        {
            if(!slot.GetComponent<Slot>().Card)
            {
                slot.GetComponent<Slot>().SelectionEffect[2].SetActive(true);
                slot.GetComponent<Slot>().CardTemplate.SetActive(true);
                slot.GetComponent<Slot>().Status = 2;
            }
        }
    }

    public void HideFreeSlots()
    {
        foreach (var slot in PlayerSlots)
        {
            if (!slot.GetComponent<Slot>().Card)
            {
                slot.GetComponent<Slot>().SelectionEffect[2].SetActive(false);
                slot.GetComponent<Slot>().CardTemplate.SetActive(false);
                slot.GetComponent<Slot>().Status = 0;
            }
        }
    }

    public void InvokeCard(int slotId)
    {
        var card = PlayerDeck.GetComponent<Deck>().InvokeCard();

        var getStats = card.GetComponent<Stats>();

        Debug.Log(getStats.Stars + "---------");
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
        Debug.Log("Start SetColor");
        PlayerSlots[slotId].GetComponent<Slot>().HealthBarObject.GetComponentInChildren<HealthBar>().SetColor(true);
        Debug.Log("End SetColor");
        PlayerSlots[slotId].GetComponent<Slot>().Status = 0;

        HideFreeSlots();
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
        if (isPlayer)
        {
            PlayerSlots[slotId].GetComponent<Slot>().SelectionEffect[1].SetActive(true);
            PlayerSlots[slotId].GetComponent<Slot>().CardTemplate.SetActive(true);

            if(CurrentCardSelected != -1)
            {
                PlayerSlots[CurrentCardSelected].GetComponent<Slot>().SelectionEffect[1].SetActive(false);
                PlayerSlots[CurrentCardSelected].GetComponent<Slot>().CardTemplate.SetActive(false);
            }

            CurrentCardSelected = slotId;
        }
        else
        {
            PCSlots[slotId].GetComponent<Slot>().SelectionEffect[1].SetActive(true);
            PCSlots[slotId].GetComponent<Slot>().CardTemplate.SetActive(true);

            if (CurrentCardSelected != -1)
            {
                PCSlots[CurrentCardSelected].GetComponent<Slot>().SelectionEffect[1].SetActive(false);
                PCSlots[CurrentCardSelected].GetComponent<Slot>().CardTemplate.SetActive(false);
            }

            CurrentCardSelected = slotId;
        }
    }

    #endregion
}