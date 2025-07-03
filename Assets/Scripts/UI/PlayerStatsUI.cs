using TMPro;
using UnityEngine;


public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text_nickname;
    [SerializeField] private TextMeshProUGUI text_score;


    public void UpdateUI ( string nickname , int score )
    {
        if ( !gameObject.activeSelf )
        {
            gameObject.SetActive ( true );
        }

        text_nickname.text = nickname;
        text_score.text = score.ToString ();
    }
}