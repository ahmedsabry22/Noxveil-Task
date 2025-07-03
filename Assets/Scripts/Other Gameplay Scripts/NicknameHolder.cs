using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class NicknameHolder : MonoBehaviour
{
    public TMP_InputField input_field_nickname;
    public Button button_play;


    private void Start ()
    {
        input_field_nickname.text = PlayerPrefs.GetString ( "Nickname" , "" );
    }


    public void SetNickname ()
    {
        PlayerPrefs.SetString ( "Nickname" , input_field_nickname.text );
    }


    public void OnNicknameInputFieldChanged ( string value )
    {
        button_play.interactable = value.Length > 2;
    }
}