using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    public class Deck
    {
        #region PROPERTIES

        List<GameObject> Cards { get; set; }
        public int CurrentIndexForInvoke { get; set; } = -1;

        #endregion

        #region FUNCTIONS

        public void InvokeCard(int index)
        {
            //SlotController.ToggleFreeSlots(CurrentIndexForInvoke == index);
            CurrentIndexForInvoke = index;
        }

        public GameObject InvokeCard()
        {
            if (CurrentIndexForInvoke == -1)
            {
                return null;
            }

            return Cards[CurrentIndexForInvoke];
        }

        #endregion

        public Deck(List<GameObject> gameCards)
        {
            Cards = gameCards;
        }
    }
}