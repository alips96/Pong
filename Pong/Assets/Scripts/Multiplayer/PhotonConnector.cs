using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Pong.General;
using Zenject;

namespace Pong.MP
{
    public class PhotonConnector : MonoBehaviourPunCallbacks
    {
        [SerializeField] private byte maxPlayersInRoom = 2;

        [SerializeField] private GameObject joinRoomButton;
        [SerializeField] private GameObject leaveRoomButton;
        [SerializeField] private GameObject singlePlayerButton;

        private EventMaster eventMaster;

        private void Start()
        {
            PhotonNetwork.AutomaticallySyncScene = true; //To scync scenes again. it was turned off previously.

            eventMaster.EventUserLoggedIn += ConnectToPhoton;
            eventMaster.EventRoomInvitationAccepted += RoomInvitationAccepted;
        }

        private void OnDestroy()
        {
            eventMaster.EventUserLoggedIn -= ConnectToPhoton;
            eventMaster.EventRoomInvitationAccepted -= RoomInvitationAccepted;
        }

        private void RoomInvitationAccepted(string roomName)
        {
            PlayerPrefs.SetString("PhotonRoom", roomName);

            if (PhotonNetwork.InRoom)
            {
                PhotonNetwork.LeaveRoom();
            }
            else
            {
                if (PhotonNetwork.InLobby)
                {
                    JoinPlayerRoom();
                }
            }
        }

        private void JoinPlayerRoom()
        {
            string roomName = PlayerPrefs.GetString("PhotonRoom");
            PlayerPrefs.SetString("PhotonRoom", "");
            PhotonNetwork.JoinRoom(roomName);
        }

        public void OnCreateRoomButtonClicked() //Called by join room button
        {
            if (PhotonNetwork.InLobby)
            {
                CreatePhotonRoom(PhotonNetwork.LocalPlayer.UserId);
                joinRoomButton.SetActive(false);
            }
        }

        public void OnLeaveRoomButtonClicked() //Called by Leave room button
        {
            if (PhotonNetwork.InRoom)
            {
                PhotonNetwork.LeaveRoom();
            }
        }

        [Inject]
        private void SetInitialReferences(EventMaster _eventMaster)
        {
            eventMaster = _eventMaster;
        }

        private void ConnectToPhoton(string nickName)
        {
            Debug.Log("Connect to photon as " + nickName);
            PhotonNetwork.AuthValues = new AuthenticationValues(nickName);
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.NickName = nickName;
            PhotonNetwork.ConnectUsingSettings();
        }

        public void DisconnectFromPhoton() //Called by logout button.
        {
            PlayerPrefs.DeleteAll();
            PhotonNetwork.Disconnect();
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.Log("Disconnected from photon: " + cause);
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected to the photon master server");

            if (!PhotonNetwork.InLobby)
            {
                PhotonNetwork.JoinLobby();
            }
        }

        public override void OnJoinedLobby()
        {
            Debug.Log("Connected to a photon lobby");

            eventMaster.CallEventGetPhotonFriends();

            string roomName = PlayerPrefs.GetString("PhotonRoom");

            if (!string.IsNullOrEmpty(roomName))
            {
                JoinPlayerRoom();
            }
            else
            {
                joinRoomButton.SetActive(true);
            }
        }

        private void CreatePhotonRoom(string roomName)
        {
            RoomOptions ro = new RoomOptions();
            ro.IsOpen = true;
            ro.IsVisible = true;
            ro.MaxPlayers = maxPlayersInRoom;

            PhotonNetwork.JoinOrCreateRoom(roomName, ro, TypedLobby.Default);
        }

        public override void OnCreatedRoom()
        {
            Debug.Log("Created a photon room named " + PhotonNetwork.CurrentRoom.Name);

            eventMaster.CallEventToggleInvitationUI(); //To enable the players to invite their friends.
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Joined the photon room named " + PhotonNetwork.CurrentRoom.Name);

            leaveRoomButton.SetActive(true);
            singlePlayerButton.SetActive(true);
        }

        public override void OnLeftRoom()
        {
            Debug.Log("You have left a photon room");

            leaveRoomButton.SetActive(false);
            singlePlayerButton.SetActive(false);
            joinRoomButton.SetActive(true);
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.Log("Failed to join a photon room: " + message);
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Debug.Log("Another player joined the room: " + newPlayer.UserId);

            if (PhotonNetwork.CurrentRoom.PlayerCount == maxPlayersInRoom)
            {
                string myName = PhotonNetwork.LocalPlayer.NickName;
                string opponentName = newPlayer.NickName;

                Debug.Log(myName + " Vs " + opponentName);

                PhotonNetwork.CurrentRoom.IsOpen = false;
                eventMaster.CallEventDiscordJoinMessage(myName, opponentName);

                PhotonNetwork.LoadLevel(2); //Multiplayer Scence
            }
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            Debug.Log("Player left the room: " + otherPlayer.UserId);
        }

        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            Debug.Log("New master client is: " + newMasterClient.UserId);
        }
    }
}