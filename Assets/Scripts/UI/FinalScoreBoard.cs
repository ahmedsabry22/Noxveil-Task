
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class FinalScoreBoard : MonoBehaviour
{
    [SerializeField] private GameObject panel_board;
    [SerializeField] private TextMeshProUGUI text_winner_name;
    [SerializeField] private List<FinalScoreItem> score_ui_items;


    private Dictionary<PlayerData , FinalScoreItem> players_scores_dictionary = new ();


    public void ShowBoard ()
    {
        panel_board.SetActive ( true );

        for ( int i = 0 ; i < PlayerData.AllPlayersData.Count ; i++ )
        {
            score_ui_items [ i ].gameObject.SetActive ( true );
            players_scores_dictionary.Add ( PlayerData.AllPlayersData [ i ] , score_ui_items [ i ] );

            score_ui_items [ i ].Fill ( PlayerData.AllPlayersData [ i ].NickName.ToString () , PlayerData.AllPlayersData [ i ].Score );
        }


        var sorted_dic = GetSortedPlayerStats ();
        for ( int i = 0 ; i < sorted_dic.Count ; i++ )
        {
            sorted_dic [ i ].Value.transform.SetSiblingIndex ( i );
        }

        text_winner_name.text = sorted_dic [ 0 ].Key.NickName.ToString ();
    }


    private List<KeyValuePair<PlayerData , FinalScoreItem>> GetSortedPlayerStats ()
    {
        List<KeyValuePair<PlayerData , FinalScoreItem>> sortedList = players_scores_dictionary.OrderByDescending ( pair => pair.Key.Score ).ToList ();

        return sortedList;
    }
}