using UnityEngine;

namespace BattlePhase
{
    public class Select : MonoBehaviour
    {
        SceneController SceneController { get; set; }
        SlotController SlotController { get; set; }

        void Start()
        {
            SceneController = FindObjectOfType<SceneController>();
            SlotController = FindObjectOfType<SlotController>();
        }


        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit raycastHit))
                {
                    if (raycastHit.collider.GetComponent<Stats>() != null)
                    {
                        var card = raycastHit.collider.gameObject.GetComponent<Stats>();
                        Debug.Log($"Select: Select Slot: {card.Id} - {card.Name}");
                        SceneController.SelectCard(card.Id, card.Name);
                        SceneController.LoadCurrentCard(card);
                    }
                    else if (raycastHit.collider.GetComponent<Slot>() != null)
                    {
                        var slot = raycastHit.collider.gameObject.GetComponent<Slot>();

                        if (slot.Status == Slot.SlotStatus.Empty && slot.IsPlayer)
                        {
                            SlotController.InvokeCard(slot.SlotID);
                        }
                        else if (slot.Status == Slot.SlotStatus.Invoke)
                        {
                            //SceneController.SelectCard(slot.SlotID, slot.IsPlayer);
                        }
                    }
                }
            }
        }
    }
}