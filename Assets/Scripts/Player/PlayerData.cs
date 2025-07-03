using Fusion;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class PlayerData : NetworkBehaviour
{
    [SerializeField] private AudioSource audio_source;
    [SerializeField] private AudioClip coin_sound;
    [SerializeField] private TextMeshProUGUI text_nickname;


    [Networked] public NetworkString<_16> NickName { get; set; }


    [Networked] public int Score { get; set; }


    [Networked] public bool DataReady { get; private set; }


    public static List<PlayerData> AllPlayersData = new ();
    public static PlayerData LocalPlayer;


    public override void Spawned ()
    {
        AllPlayersData.Add ( this );

        if ( HasInputAuthority )
        {
            LocalPlayer = this;
            Rpc_SetNickname ( NicknameHolder.nickname );
        }

        Runner.SetPlayerObject ( NetworkManager.Instance.runner.LocalPlayer , Object );
    }


    public void IncreaseScore ()
    {
        Score++;

        Rpc_PlayCoinSound ();

        if ( Score >= GameSceneReferences.Instance.Coins_Manager.target_coins_to_win )
        {
            Rpc_OnPlayerWon ( NickName.ToString () );
        }
    }


    [Rpc ( RpcSources.InputAuthority , RpcTargets.All )]
    public void Rpc_SetNickname ( string nickname )
    {
        NickName = nickname;
        text_nickname.text = NickName.ToString ();

        DataReady = true;
    }


    [Rpc ( RpcSources.StateAuthority , RpcTargets.All )]
    public void Rpc_OnPlayerWon ( string player_nickname )
    {
        GameSceneReferences.Instance.Final_Score_Board.ShowBoard ();
        NetworkManager.Instance.EndMatch ( player_nickname );
    }


    [Rpc ( RpcSources.StateAuthority , RpcTargets.All )]
    public void Rpc_PlayCoinSound ()
    {
        audio_source.PlayOneShot ( coin_sound );
    }
}