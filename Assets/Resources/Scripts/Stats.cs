using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Stats : MonoBehaviour
{
    public int Health;
    public int CurrentHealth;
    public int AttackDamage;
    public int CurrentAttackDamage;
    public string Name;
    public string Type;
    [Range(1, 15)]
    public int Stars;
    public int Id;

    // Id in game, not in database
    public Guid UID = Guid.NewGuid();
    public int Player = 0;
    //public SpriteRenderer ImageCard;

    /*
    HeroCombat heroCombatScript;
    GameObject player;
    */

    // Start is called before the first frame update
    void Start()
    {
        //Sprite mySprite = Resources.Load<Sprite>("Images/Cards/Bronze Dragon");

        //ImageCard.sprite = mySprite;

        /*
        heroCombatScript = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroCombat>();
        player = GameObject.FindGameObjectWithTag("Player");
        */
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(health <= 0) {
            Destroy(gameObject);
            heroCombatScript.targetedEnemy = null;
            heroCombatScript.performMeleeAttack = false;
            //Give Exp
            //player.GetComponent<LevelUpStats>().SetExperience(expValue);
        }
        */
    }
}