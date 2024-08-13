using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public string Name;
    public int Health { get; set; }
    public int CurrentHealth { get; set; }
    public int AttackDamage { get; set; }
    public int CurrentAttackDamage { get; set; }

    public List<IAbility> Abilities;


    // Start is called before the first frame update
    void Start()
    {
        Health = CurrentHealth = 5000;
        AttackDamage = CurrentAttackDamage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}