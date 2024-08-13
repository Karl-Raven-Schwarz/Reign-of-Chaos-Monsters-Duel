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
        }
    }
}