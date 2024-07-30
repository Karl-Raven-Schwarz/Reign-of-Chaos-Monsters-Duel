using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot: MonoBehaviour
{
    #region Properties

    [Header("Properties")]
   
    public Vector3 Position;
    public bool IsPlayer { get; set; }
    public int SlotID { get; set; } = -1;

    #endregion

    #region GameObjects

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

    /// <summary>
    /// </summary>
    public SlotStatus Status {
        get
        {
            return Card == null ? SlotStatus.Empty : SlotStatus.Invoke;
        }
    }

    public enum SlotStatus
    {
        Empty = 0,
        Invoke = 1, // have card
    }

    #endregion

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