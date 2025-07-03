# Noxveil-Task

<br>
<br>

## Technologies User for this project are:
**Unity 6.0.34f1**

**Photon Fusion 2.0.3**


<br>
<br>


## To Watch a Demo video, Click this link:
Visit [This Link](https://doc.photonengine.com/fusion/current)


<br>
<br>


## How To Run this project:
**- Open the project in unity editor.**

**- Make sure you're on Windows or MacOS Platform.**

**- Build the project**

**- Go to the built project's folder and run the .exe file.**

**- Run the .exe file on four instances in order to test the game.**

**- Enter your nickname and press PLAY button.**


<br>
<br>


### I use Fusion (Host) mode, not (Shared Mode) becuase:
**- Host mode is the ultimate mode of most multiplayer games.**

**- Since one player acts as the server, and all network logic must pass through him. That makes the game more secure, reliable, and stable.**

**- Also, Host mode reduces the ability of cheating.**

**- Finally, we use Physics in the game to move the players, and Host mode gives us more reliable Physics behaviour and better synchronizing.**


<br>
<br>


## A match you play has some data to be saved:
**You can find the match data in:-**


*"your project's folder -> Noxveil Task_Data -> StreamingAssets -> last_match.json"*

**Open that file and you will see all the match data you need.**

**A sample of match saved data is as follows:**

```json
{
    "MatchID": "630a1120-8cf6-49b1-8dda-a75f4e745f44",
    "PlayersNumber": 4,
    "WinnerName": "Axe",
    "MatchTime": "250 seconds",
    "PlayersRanking": [
        {
            "Nickname": "Axe",
            "Rank": 1,
            "Score": 5
        },
        {
            "Nickname": "Sword",
            "Rank": 2,
            "Score": 4
        },
        {
            "Nickname": "Hammer",
            "Rank": 3,
            "Score": 2
        },
        {
            "Nickname": "Saw",
            "Rank": 4,
            "Score": 0
        }
    ]
}
```
<br>
<br>


## Are there any expected bugs in the current version of the game?
**- YES, because the time was tight, I didn't handle the logic of unexpected behaviours like:**

*- If a player leaves the game, I didn't handle that.*

*- The Master (Host) leaves the game, The Host authority must be switched to another player, I didn't handle that.*

*- If a player's internet connection drops, I didn't handle that.*


<br>
<br>


### These issues can all be handled and fixed in the future


<br>
<br>
<br>
<br>


### My Best Regards :)