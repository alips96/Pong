# Pong
Online Multiplayer Pong Game

I have developed this game using Unity, Microsoft Azure PlayFab, Photon Engine (photon cloud services), and Discord.

The code is divided into four master folders. The general folder is for handling the ball movement and player movement. Why General?! Because these scripts are used both in single-player and multiplayer, no matter which game mode it is. The login menu contains all the code related to the login process. Multiplayer Folder has all the logic about photon, playfab, and discord. And then we have single player folder handling the single-player logic. No other external package is installed except the buttons set :)

The observer design pattern is mainly used for the modules interacting together.

The game starts by asking the player to log in. Each player has their account on the game server. For authentication, we make an API call to the playfab sever. 
After logging in, we will have to connect to the photon master server with our playfab username. The playfab username would be our photon nickname and we connect to the photon master server using our nickname. Then based on the server settings from the photon dashboard, we connect to the master server.

Now that we are connected to the master server using our nickname, we need to join a lobby to be able to find our friends in the lobby. After that, we make another API call to the playfab server to find our friends in the game, and after we got the list of our friends, we gotta have to synchronize our playfab friends' list to the photon friends list. This part is quite complicated and I’m not gonna go through this in detail. Just bear in mind that we should have the same friends on photon as playfab. The friends' list must get updated every 15 seconds to know how many of our friends are online. We could also add any other friend if we want, by typing in their playfab username. Remember the player we are adding as our friend must have an account on playfab!

The single-player mode doesn’t have anything to do with the photon engine. It simply runs the game locally and stores the result on the playfab database. The playfab leaderboard is dedicated to handling the high score logic and data.

For the multiplayer mode, we should first create a new room. Then a room is created with our username. Remember when we are in a room, the friends' list won’t get updated because we cannot do this action while we are in a room. We can now send an invitation to our friends to kick off the match. This step is done by photon chat services. When we send a request to another player, we send a private message to the other player with the room information we just created. If the player accepts the invitation, he/she would join the room and the room would turn into a closed and invisible state, which means that no one else can join it anymore. Then both players join the multiplayer scene and play. After one player loses the match, their stats would get updated by an API call to the playfab server. They can also replay or get back to the main menu. But if one of them leaves the room (by getting back to the menu scene), the other player cannot replay anymore and should only get back to the menu as well. When both players leave the room, the room will be automatically destroyed. 

All the game notifications would be handled by discord. For this purpose, I have created a discord webhook.  
Then, I copied and pasted the webhook URL to our script. Every time we wanna send a notification, we make an HTML call to this webhook with the desired parameters. These scripts are written in javascript and get executed entirely on the playfab cloud servers. I’ve also added some embeds for chatting purposes.
