using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

public class CardController : MonoBehaviour
{
    public RectTransform Parent;
    public GameObject PrefabCardTemplateButton;

    [Serializable]
    public struct CardData
    {
        public Sprite ImageSprite;
        public string Name;
        public int Level;
        public int Attack;
        public int Health;
    }

    [SerializeField] public CardData[] Cards;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gameObject;
        for(int i = 0; i < 16; i++)
        {
            gameObject = Instantiate(PrefabCardTemplateButton, Vector3.zero, Quaternion.identity);
            gameObject.transform.SetParent(Parent.transform);
            gameObject.transform.localScale = Vector3.one;
            gameObject.transform.localPosition = new Vector3();
            gameObject.transform.rotation = Quaternion.Euler(new Vector3());
            gameObject.name = $"Card {i + 1}";
            
            gameObject.GetComponent<Card>().ImageSprite = Cards[i].ImageSprite;
            gameObject.GetComponent<Card>().Name = Cards[i].Name;
            gameObject.GetComponent<Card>().Level = Cards[i].Level;
            gameObject.GetComponent<Card>().Attack = Cards[i].Attack;
            gameObject.GetComponent<Card>().Health = Cards[i].Health;
        }


        double rows = Math.Ceiling( (double)(Cards.Length / 5)) + 1;
        Parent.sizeDelta = new Vector2(Parent.sizeDelta.x, (float)rows * 250);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}