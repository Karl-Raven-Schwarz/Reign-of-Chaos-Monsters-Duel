using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlatformSelect : MonoBehaviour
{
    public List<GameObject> Cards;
    public BattlePhase.SceneController SceneController; 
    private List<GameObject> CardsSaved;

    public TextMeshProUGUI CardName;
    public TextMeshProUGUI CardStats;
    public TextMeshProUGUI DeckPlayer;
    public TextMeshProUGUI TMStars;
    public Canvas PickCanvas;

    private int index;
    private List<int> Indexes;
    private int Stars;
    private int CurrentStars;
    private string CurrentName;
    private Vector3 CardPosition;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;   
        Stars = 25;
        TMStars.text = "25";

        if (SceneController.GetPhase() == BattlePhase.SceneController.GamePhase.Selection) 
        {
            CardsSaved.Add(Instantiate(Cards[index]));
            var stats = CardsSaved[index].GetComponent<Stats>();
            CurrentStars = stats.Stars;
            CardName.text = $"{stats.Name}";
            CardStats.text = $"{stats.AttackDamage}\n{stats.Health}";
            CurrentName = stats.Name;
        }
        
    }

    // Update is called once per frame
    void Update() {
    }
/*
    public void PickedCard(Vector3 position, int id) {
        CardPosition = position;
        for (int i = 0; i < Cards.Count)
    }
*/
    public void SelectCard() {

        PickCanvas.transform.position = CardPosition;

        /*
        if (CurrentStars > Stars) {
            TMStars.color = Color.Lerp(Color.white, Color.red, 1.0f);
            TMStars.color = Color.Lerp(Color.red, Color.white, 1.0f);
        }
        */

        DeckPlayer.text += $"-{CurrentName}\n";
        Stars -= CurrentStars;
        TMStars.text = $"{Stars}";
        Indexes.Add(index);
        index++;
        
        
    }
}