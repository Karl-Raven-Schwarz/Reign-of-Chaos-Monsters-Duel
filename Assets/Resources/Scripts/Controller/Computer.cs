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
        List<GameObject> Slots;

        #endregion

        #region FUNCTIONS

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
        }
    }
}