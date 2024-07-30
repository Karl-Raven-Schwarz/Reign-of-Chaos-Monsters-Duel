using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCController : MonoBehaviour
{
    SlotController SlotController;
    BattlePhase.SceneController SceneController;

    bool IsMyTurn { get; set; }
    int CardsInvoked = 0;

    void Start()
    {
        SlotController = FindObjectOfType<SlotController>();
        SceneController = FindObjectOfType<BattlePhase.SceneController>();
    }

    void Update()
    {
        if(BattlePhase.SceneController.GamePhase.Battle == SceneController.GetPhase() && IsMyTurn && !SceneController.IsUserTurn && CardsInvoked < 7)
        {
            Debug.Log($"SceneController.GetPhase(): {SceneController.GetPhase()}");
            SlotController.PCInvokeCard();
            IsMyTurn = false;
            CardsInvoked++;
        }
        else if(SceneController.IsUserTurn)
        {
            IsMyTurn = true;
        }
    }

    #region INVOKE



    #endregion
}