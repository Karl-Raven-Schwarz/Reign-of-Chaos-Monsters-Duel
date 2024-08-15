using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Models
{
    public class Hand
    {
        public const int MAX_CARDS = 10;

        List<GameObject> Cards { get; set; }
        List<Card> CardsData { get; set; }

        #region FUNCTIONS

        public int CurrentCardsCount()
        {
            return Cards.Count;
        }

        public int GetMaxLevelCard()
        {
            var index = CardsData.Max(c => c.Level);

            return CardsData[index].Level;
        }

        public void AddCard(GameObject card)
        {
            if(Cards.Count < MAX_CARDS)
            {
                Cards.Add(card);
                CardsData.Add(card.GetComponent<Card>());
            }
        }

        public GameObject InvokeCard(int index)
        {
            if (index < 0 || index >= Cards.Count)
            {
                return null;
            }

            return Cards[index];
        }

        #endregion

        public Hand(List<GameObject> cards)
        {
            Cards = cards;
            Debug.Log("Hand created");

            try
            {
                foreach (var card in Cards)
                {
                    //replace with Slot
                    CardsData.Add(card.GetComponent<Card>());
                }
            }
            catch(Exception ex)
            {
                Debug.LogException(ex);
            }
        }
    }
}