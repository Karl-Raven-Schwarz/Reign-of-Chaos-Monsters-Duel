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

    bool IsOn = false;

    void Start()
    {
        //if(Death) Death.SetActive(false);
        if(Card)
        {
            Slider.maxValue = Card.Health;
        }
        
        Slider.minValue = 0;

        if(Fill) Fill.color = Color.red;
    }

    void Update()
    {
        if(Card != null) Slider.value = Card.CurrentHealth;
        //Slider3D.value = Slider2D.value;

        if( Card && (Card.CurrentHealth < 1 || Card == null) && HealthSlider) 
        {
            //if(Death) Death.SetActive(true);
            HealthSlider.SetActive(false);
        }
    }

    public void SetColor(bool isPlayer)
    {
        Debug.Log("HealthBar.SetColot: "+ isPlayer);
        if(isPlayer)
        {
            Fill.color = Color.green;
        }
        else
        {
            Fill.color = Color.red;
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