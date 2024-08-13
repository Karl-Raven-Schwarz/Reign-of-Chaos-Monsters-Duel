using BattlePhase;
using Models;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Controller
{
    public class ComputerController : MonoBehaviour
    {
        #region PROPERTIES

        public Computer Model { get; set; }
        public List<GameObject> Slots;

        #endregion

        #region FUNCTIONS

        void CreateSlots()
        {
            //Load Slot prefab
            var slotPrefab = Resources.Load<GameObject>("Prefabs/BattleArenaObjects/Slot");

            float xInitialPosition = -0.6f;
            const float yPosition = 0f;
            const float zPosition = 0.15f;

            for (int i = 0; i < SceneController.SLOTS; i++)
            {
                GameObject pcSlot = Instantiate(slotPrefab);
                pcSlot.SetActive(true);
                pcSlot.transform.position = new Vector3(-xInitialPosition, yPosition, zPosition);
                pcSlot.GetComponent<Slot>().SlotID = i;
                pcSlot.GetComponent<Slot>().IsPlayer = false;
                Slots.Add(pcSlot);

                xInitialPosition += 0.2f;
            }
        }

        void BattleLogic()
        {
            if(Model.CurrentCardsInHand() > 0)
            {

            }
        }

        public void InvokeCard()
        {
            /*
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
            */
        }

        #endregion

        #region HANDLES

        void HandlePhaseChanged(SceneController.GamePhase gamePhase)
        {
            switch(gamePhase)
            {
                case SceneController.GamePhase.Battle:
                    BattleLogic();
                    break;
                default:
                    break;
            }
        }  

        void HandleTurnChanged(bool isPlayerTurn)
        {
            if(!isPlayerTurn)
            {
            }
            else
            {
                BattleLogic();
            }
        }

        #endregion

        void Start()
        {
            SceneController.OnPhaseChange += HandlePhaseChanged;
            SceneController.OnTurnChanged += HandleTurnChanged;

            Model = new Computer();

            CreateSlots();
        }

        void Update()
        {
        }
    }
}