using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [Header("Data")]
    public Sprite ImageSprite;
    public string Name;
    public int Level;
    public int Attack;
    public int Health;

    [Header("UI")]
    public Image Image;
    public TextMeshProUGUI NameTMP;
    public TextMeshProUGUI LevelTMP;
    public TextMeshProUGUI AttackTMP;
    public TextMeshProUGUI HealthTMP;

    // Start is called before the first frame update
    void Start()
    {
        Image.sprite = ImageSprite;
        NameTMP.text = Name;
        LevelTMP.text = Level.ToString();
        AttackTMP.text = Attack.ToString();
        HealthTMP.text = Health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Image.sprite = ImageSprite;
        NameTMP.text = Name;
        LevelTMP.text = Level.ToString();
        AttackTMP.text = Attack.ToString();
        HealthTMP.text = Health.ToString();
    }
}
