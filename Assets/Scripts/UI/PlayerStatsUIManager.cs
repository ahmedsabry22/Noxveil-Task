using System.Collections.Generic;
using System.Collections;
using UnityEngine;


public class PlayerStatsUIManager : MonoBehaviour
{
    [SerializeField] private List<PlayerStatsUI> players_stats_elements;


    private Dictionary<PlayerData , PlayerStatsUI> player_stats_dictionary = new ();


    private IEnumerator Start ()
    {
        yield return new WaitUntil ( () => NetworkManager.Instance.IsGameReady && GameSceneReferences.Instance.Players_Spawner.ArePlayersReady );
        yield return new WaitForSeconds ( 3 );

        StartCoroutine ( UpdateUICoroutine () );
    }


    private IEnumerator UpdateUICoroutine ()
    {
        while ( true )
        {
            int index = 0;

            foreach ( var player in PlayerData.AllPlayersData )
            {
                if ( !player_stats_dictionary.ContainsKey ( player ) && index < players_stats_elements.Count )
                {
                    player_stats_dictionary [ player ] = players_stats_elements [ index ];
                    index++;
                }

                if ( player_stats_dictionary.TryGetValue ( player , out var target_stats_ui ) )
                {
                    target_stats_ui.UpdateUI ( player.NickName.ToString () , player.Score );
                }
            }


            yield return new WaitForSeconds ( 1 );
        }
    }
}