﻿using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Pun;
using Pong.General;
using System;
using UnityEngine;
using Zenject;

namespace Pong.MP
{
    public class PhotonChat : MonoBehaviour, IChatClientListener
    {
        [SerializeField] private string nickName;
        private ChatClient chatClient;
        private EventMaster eventMaster;

        private void OnEnable()
        {
            eventMaster.EventUserLoggedIn += ConnectToPhotonChat;
            eventMaster.EventInviteFriend += InviteFriend;
        }

        private void OnDisable()
        {
            eventMaster.EventUserLoggedIn -= ConnectToPhotonChat;
            eventMaster.EventInviteFriend -= InviteFriend;
        }

        private void Update()
        {
            if (chatClient != null)
                chatClient.Service();
        }

        private void ConnectToPhotonChat(string name)
        {
            nickName = name;
            chatClient = new ChatClient(this);
            Debug.Log("Connecting to photon chat service");

            chatClient.AuthValues = new AuthenticationValues(nickName);

            //Deprecated I guess!
            //ChatAppSettings chatSettings = PhotonNetwork.PhotonServerSettings.AppSettings.GetChatSettings();

            Photon.Realtime.AppSettings appSettings = PhotonNetwork.PhotonServerSettings.AppSettings; //adding reference

            ChatAppSettings chatSettings = new ChatAppSettings
            {
                Server = appSettings.Server,
                Protocol = appSettings.Protocol,
                AppIdChat = appSettings.AppIdChat,
                AppVersion = appSettings.AppVersion,
                FixedRegion = appSettings.FixedRegion,
                NetworkLogging = appSettings.NetworkLogging,
            };

            chatClient.ConnectUsingSettings(chatSettings);
        }

        [Inject]
        private void SetInitialReferences(EventMaster _eventMaster)
        {
            eventMaster = _eventMaster;
        }

        public void DebugReturn(DebugLevel level, string message)
        {

        }

        public void OnDisconnected()
        {
            Debug.Log("Disconnected from the photon chat.");
        }

        public void OnConnected()
        {
            Debug.Log("Connected to the photon chat.");
        }

        public void OnChatStateChange(ChatState state)
        {

        }

        public void OnGetMessages(string channelName, string[] senders, object[] messages)
        {

        }

        public void InviteFriend(string receiver)
        {
            chatClient.SendPrivateMessage(receiver, PhotonNetwork.CurrentRoom.Name);
        }

        public void OnPrivateMessage(string sender, object message, string channelName)
        {
            if (!string.IsNullOrEmpty(message.ToString()))
            {
                string[] splittedNames = channelName.Split(new char[] { ':' }); //the format is [sender:receiver]
                string senderName = splittedNames[0];

                if (!sender.Equals(senderName, StringComparison.OrdinalIgnoreCase))
                {
                    Debug.Log(sender + ": " + message);
                    eventMaster.CallEventInvitedToTheRoom(sender, message.ToString());
                }
            }
        }

        public void OnSubscribed(string[] channels, bool[] results)
        {

        }

        public void OnUnsubscribed(string[] channels)
        {

        }

        public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
        {

        }

        public void OnUserSubscribed(string channel, string user)
        {

        }

        public void OnUserUnsubscribed(string channel, string user)
        {

        }
    }
}