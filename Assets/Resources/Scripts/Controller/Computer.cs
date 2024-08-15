using BattlePhase;
using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    public class Computer
    {
        #region PROPERTIES

        string Name { get; set; }
        int Health { get; set; }
        int CurrentHealth { get; set; }
        int Damage { get; set; }
        int CurrentDamage { get; set; }
        int Manna { get; set; }
        int CurrentManna { get; set; }

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
                pcSlot.transform.position = new Vector3(-xInitialPosition, yPosition, zPosition);
                pcSlot.GetComponent<Slot>().SlotID = i;
                pcSlot.GetComponent<Slot>().IsPlayer = false;
                Slots.Add(pcSlot);

                xInitialPosition += 0.2f;
            }
        }

        public int CurrentCardsInHand()
        {
            return Hand.CurrentCardsCount();
        }

        #endregion

        public Computer()
        {
            Name = "Computer";
            Health = 100;
            CurrentHealth = Health;
            Damage = 10;
            CurrentDamage = Damage;
            Manna = 100;
            CurrentManna = Manna;

            CreateSlots();
        }
    }
}