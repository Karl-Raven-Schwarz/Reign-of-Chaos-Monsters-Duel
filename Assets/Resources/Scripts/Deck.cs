using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Deck : MonoBehaviour
{
    public List<GameObject> Cards = new();
    SlotController SlotController;
    public int CurrentIndexForInvoke { get; set; } = -1;

    void Start()
    {
        SlotController = FindObjectOfType<SlotController>();
    }

    void Update()
    {
        
    }

    public void InvokeCard(int index)
    {
        SlotController.ToggleFreeSlots(CurrentIndexForInvoke == index);
        CurrentIndexForInvoke = index;
    }

    public GameObject InvokeCard()
    {
        if(CurrentIndexForInvoke == -1)
        {
            return null;
        }

        return Cards[CurrentIndexForInvoke];
    }
}
