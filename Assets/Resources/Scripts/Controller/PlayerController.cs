using Models;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

namespace BattlePhase
{
    public class PlayerController : MonoBehaviour
    {

        #region PROPERTIES

        public Player Model { get; set; }

        #endregion

        #region FUNCTIONS

        public int GetCurrentPlayerHealth()
        {
            return Model.CurrentHealth;
        }

        public void OnHandCardSelected(int index)
        {
            Model.OnHandCardSelected(index);
        }

        /// <summary>
        /// Functions for detect user input
        /// </summary>
        void DetectInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit raycastHit))
                {
                    if (raycastHit.collider.GetComponent<Slot>() != null)
                    {
                        var slot = raycastHit.collider.gameObject.GetComponent<Slot>();

                        if (!slot.HasCard() && slot.IsPlayer)
                        {
                            Model.InvokeCard(slot.SlotID);
                        }
                        else if (slot.HasCard())
                        {
                            //SlotController.SelectCard(slot.SlotID, slot.IsPlayer);
                        }
                    }
                }
            }
        }

        #endregion

        private void Awake()
        {
            Model = new Player(SceneController.GameCards);
        }

        void Start()
        {
        }

        void Update()
        {
            DetectInput();
        }
    }
}