using UnityEngine;

namespace BattlePhase
{
    public class Select : MonoBehaviour
    {
        SlotController SlotController { get; set; }

        void Start()
        {
            SlotController = FindObjectOfType<SlotController>();
        }


        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit raycastHit))
                {
                    if (raycastHit.collider.GetComponent<Slot>() != null)
                    {
                        var slot = raycastHit.collider.gameObject.GetComponent<Slot>();

                        if (slot.Status == Slot.SlotStatus.Empty && slot.IsPlayer)
                        {
                            SlotController.InvokeCard(slot.SlotID);
                        }
                        else if (slot.Status == Slot.SlotStatus.Invoke)
                        {
                            SlotController.SelectCard(slot.SlotID, slot.IsPlayer);
                        }
                    }
                }
            }
        }
    }
}