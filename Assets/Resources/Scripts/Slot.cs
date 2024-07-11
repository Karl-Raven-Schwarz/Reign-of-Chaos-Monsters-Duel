using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot: MonoBehaviour
{
    #region Properties

    [Header("Properties")]
   
    public Vector3 Position;
    public bool IsPlayer;
    public int SlotID { get; set; } = -1;

    #endregion

    #region GameObjects

    [Header("GameObjects")]
    public GameObject HitEffect;
    public GameObject HealthBarObject;
    public HealthBar HealthBar;

    public GameObject CardTemplate;
    public List<GameObject> SelectionEffect;
    public GameObject Card { get; set; }

    /// <summary>
    /// 2 = Invoke
    /// </summary>
    public int Status { get; set; }

    #endregion

    private void Start()
    {
        HealthBarObject = Instantiate(HealthBarObject);
        HealthBarObject.SetActive(false);
    }

    public void ActiveSelectionCard()
    {
        SelectionEffect[0].SetActive(true);
    }

    public bool HaveCard()
    {
        return Card != null;
    }
}