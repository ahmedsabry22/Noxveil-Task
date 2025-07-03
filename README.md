# Noxveil-Task

## Technologies User for this project are:
** Unity 6.0.34f1

** Photon Fusion 2.0.3



## To Watch a Demo video, Click this link:
Visit [This Link](https://doc.photonengine.com/fusion/current)





## How To Run this project:
**Open the project in unity editor**

**Make sure you're on Windows or MacOS Platform**

**Build the project**

**Go to the built project's folder and run the .exe file**

**Run the .exe file on four instances in order to test the game**

**Enter your nickname and press PLAY button**


#### I use Fusion (Host) mode, not (Shared Mode) becuase:
**Host mode is ultimate mode of most multiplayer games.**

**Since one player acts as the server, and all network logic must pass through him. That makes the game more secure, reliable, and stable**

**Also, Host mode reduces the ability of cheating**

**Finally, we use Physics in the game to move the players, and Host mode gives us more reliable Physics behaviour and better synchronizing**


## A match you play has some data to be saved
**You can find the match data in your "project's folder -> Noxveil Task_Data -> StreamingAssets -> last_match.json"**

**Open that file and you will see all the match data you need**


## Are there any expected bugs in the current version of the game?
**Yes, because the time was tight, I didn't handle the logic of unexpected behaviours like:**

**If a player leaves the game, I didn't handle that**

**The Master (Host) leaves the game, The Host authority must be switched to another player, I didn't handle that**

**If a player's internet connection drops, I didn't handle that**



#### These issues can all be handled and fixed in the future