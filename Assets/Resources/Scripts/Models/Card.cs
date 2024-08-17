using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Models
{
    public class Card : MonoBehaviour
    {
        [Header("Data")]
        

        /// <summary>
        /// _name is the name of the card, evading the reserved Object.name
        /// </summary>
        [SerializeField] private string _name;
        public string Name
        {
            get => _name;
            set => _name = value;
        }
        
        [SerializeField] private int health;
        public int Health
        {
            get => health;
            set => health = value;
        }

        private int currentHealth;
        public int CurrentHealth
        {
            get => currentHealth;
            set => currentHealth = value;
        }

        [SerializeField] private int damage;
        public int Damage
        {
            get => damage;
            set => damage = value;
        }

        private int currentDamage;
        public int CurrentDamage
        {
            get => currentDamage;
            set => currentDamage = value;
        }

        [SerializeField] private string type;
        public string Type
        {
            get => type;
            set => type = value;
        }

        [Range(1, 15)] [SerializeField] private int level;
        public int Level
        {
            get => level;
            set => level = value;
        }

        [SerializeField] private string id;
        public string Id
        {
            get => id;
            set => id = value;
        }

        [Header("UI")]
        public Sprite ImageSprite;
        public TextMeshProUGUI NameTMP;
        public TextMeshProUGUI LevelTMP;
        public TextMeshProUGUI AttackTMP;
        public TextMeshProUGUI HealthTMP;

        void Start()
        {
        }

        void Update()
        {
        }
    }
}