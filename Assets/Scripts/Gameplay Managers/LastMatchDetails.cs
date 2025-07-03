using UnityEngine;
using System.Collections.Generic;
using System.IO;


public class LastMatchDetails : MonoBehaviour
{
    private MatchInfo last_match_info;


    public void WriteMatchInfoToFile ( string match_id , ushort players_number , string winner_name , string match_time , List<PlayerInfo> players )
    {
        last_match_info = new MatchInfo ()
        {
            MatchID = match_id ,
            PlayersNumber = players_number ,
            WinnerName = winner_name ,
            MatchTime = match_time ,
            PlayersRanking = players
        };

        string json = JsonUtility.ToJson ( last_match_info , true );
        File.WriteAllText ( Path.Combine ( Application.streamingAssetsPath , "last_match.json" ) , json );
    }



    //-----------------------------------------------------



    [System.Serializable]
    public class MatchInfo
    {
        public string MatchID;
        public ushort PlayersNumber;
        public string WinnerName;
        public string MatchTime;
        public List<PlayerInfo> PlayersRanking;
    }


    [System.Serializable]
    public class PlayerInfo
    {
        public string Nickname;
        public ushort Rank;
        public ushort Score;
    }
}