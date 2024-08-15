using BattlePhase;
using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    public class Player
    {
        #region PROPERTIES

        public string Name { get; set; }
        public int Health { get; set; }
        public int CurrentHealth { get; set; }
        public int Damage { get; set; }
        public int CurrentDamage { get; set; }
        public int Manna { get; set; }
        public int CurrentManna { get; set; }

        /// <summary>
        /// Current index of the hand card selected
        /// </summary>
        int CurrentHandCard { get; set; } = -1;
        bool FreeSlotIsEnabled { get; set; } = false;

        Deck Deck { get; set; }
        Hand Hand { get; set; }
        List<GameObject> Slots { get; set; }

        #endregion

        #region FUNCTIONS

        void CreateSlots()
        {
            //Load Slot prefab
            var slotPrefab = Resources.Load<GameObject>("Prefabs/BattleArenaObjects/Slot");

            float xInitialPosition = -0.6f;
            const float yPosition = 0f;
            const float zPosition = 0.15f;

            Slots = new List<GameObject>(SceneController.SLOTS);

            for (int i = 0; i < SceneController.SLOTS; i++)
            {
                GameObject pcSlot = Object.Instantiate(slotPrefab);
                pcSlot.SetActive(true);
                pcSlot.transform.position = new Vector3(-xInitialPosition, yPosition, -zPosition);
                pcSlot.GetComponent<Slot>().SlotID = i;
                pcSlot.GetComponent<Slot>().IsPlayer = true;
                Slots.Add(pcSlot);

                xInitialPosition += 0.2f;
            }
        }

        public void OnHandCardSelected(int index)
        {
            if (CurrentHandCard == index)
            {
                CurrentHandCard = -1;
                ToggleFreeSlots(false);
            }
            else
            {
                CurrentHandCard = index;
                ToggleFreeSlots(true);
            }
        }

        public void ToggleFreeSlots(bool isEqualIndex)
        {
            foreach (var slot in Slots)
            {
                if (!slot.GetComponent<Slot>().Card)
                {
                    slot.GetComponent<Slot>().SelectionEffect[2].SetActive(isEqualIndex);
                    slot.GetComponent<Slot>().CardTemplate.SetActive(isEqualIndex);
                }
            }
            /*
            if (FreeSlotIsEnabled && isEqualIndex)
            {
                foreach (var slot in Slots)
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
                foreach (var slot in Slots)
                {
                    if (!slot.GetComponent<Slot>().Card)
                    {
                        slot.GetComponent<Slot>().SelectionEffect[2].SetActive(true);
                        slot.GetComponent<Slot>().CardTemplate.SetActive(true);
                    }
                }

                FreeSlotIsEnabled = true;
            }
            */
        }

        public void InvokeCard(int slotId)
        {
            var card = Hand.InvokeCard(0);

            if (card == null)
            {
                return;
            }

            var getStats = card.GetComponent<Stats>();

            if (getStats.Stars > Manna) return;

            Slots[slotId].GetComponent<Slot>().Card = Object.Instantiate(card);
            Slots[slotId].GetComponent<Slot>().Card.SetActive(true);

            var slotPosition = Slots[slotId].GetComponent<Slot>().transform.position;
            var cardPrefabPosition = Slots[slotId].GetComponent<Slot>().Card.transform.position;

            Slots[slotId].GetComponent<Slot>().Card.transform.position = new Vector3
            (
                slotPosition.x,
                cardPrefabPosition.y,
                slotPosition.z
            );

            Slots[slotId].GetComponent<Slot>().SelectionEffect[2].SetActive(false);
            Slots[slotId].GetComponent<Slot>().CardTemplate.SetActive(false);
            Slots[slotId].GetComponent<Slot>().HealthBarObject.SetActive(true);
            Slots[slotId].GetComponent<Slot>().HealthBarObject.transform.position = new Vector3
            (
                slotPosition.x,
                0.2f,
                slotPosition.z
            );

            Slots[slotId].GetComponent<Slot>().HealthBarObject.GetComponentInChildren<HealthBar>().SetColor(true);

            ToggleFreeSlots(true);
            Manna++;

            CurrentHandCard = -1;
        }

        #endregion

        public Player(List<GameObject> gameCards)
        {
            Health = CurrentHealth = 50;
            Damage = CurrentDamage = 0;
            Manna = CurrentManna = 2;

            Hand = new Hand(gameCards);
            CreateSlots();
        }
    }
}