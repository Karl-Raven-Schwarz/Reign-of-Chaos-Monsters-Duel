using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot: MonoBehaviour
{
    #region PROPERTIES

    [Header("Properties")]
   
    public Vector3 Position;
    public bool IsPlayer { get; set; }
    public int SlotID { get; set; } = -1;
    public SlotStatus Status { get; set; }

    [Header("GameObjects")]
    public GameObject HitEffect;
    public GameObject HealthBarObject;
    public HealthBar HealthBar;

    /// <summary>
    /// Card image template
    /// </summary>
    public GameObject CardTemplate;

    /// <summary>
    /// 0: Red
    /// 1: Green
    /// 2: Blue
    /// 3: Orange
    /// </summary>
    public List<GameObject> SelectionEffect;
    public GameObject Card { get; set; }

    #endregion

    #region FUNCTIONS
    
    public bool HasCard()
    {
        return Card != null;
    }

    #endregion

    public enum SlotStatus
    {
        Enabled = 0,
        Disabled = 1,
        Selected = 2,
    }

    private void Start()
    {
        HealthBarObject = Instantiate(HealthBarObject);
        HealthBarObject.SetActive(false);
    }

    /// <summary>
    /// option: 0 (Red), 1 (Green), 2 (Blue), 3 (Orange)
    /// state: true (Enable), false (Disable)
    /// </summary>
    /// <param name="option"></param>
    /// <param name="state"></param>
    public void ChangeSelectionEffect(int option, bool state)
    {
        SelectionEffect[option].SetActive(state);
    }
}