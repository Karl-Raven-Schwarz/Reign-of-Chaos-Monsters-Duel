using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider Slider;
    public GameObject HealthSlider;
    public Stats Card;
    public Image Fill;

    public Color FillColor { get; set; } = Color.gray;
    bool IsOn = false;

    void Start()
    {
        if(Card)
        {
            Slider.maxValue = Card.Health;
        }
        
        Slider.minValue = 0;

        if(Fill)
        {
            Fill.color = FillColor;
        }
    }

    void Update()
    {
        if(Card != null) Slider.value = Card.CurrentHealth;

        if( Card && (Card.CurrentHealth < 1 || Card == null) && HealthSlider) 
        {
            HealthSlider.SetActive(false);
        }
    }

    public void SetColor(bool isPlayer)
    {
        if(isPlayer)
        {
            FillColor = Color.green;
        }
        else
        {
            FillColor = Color.red;
        }
    }

    public void TurnOn()
    {
        IsOn = true;
    }

    public void TurnOff()
    {
        IsOn = false;
    }
}