using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;

namespace Pong.MP
{
    public class MP_GameOver : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverMenu;
        [SerializeField] private Transform ballTransform;

        private void OnEnable()
        {
            PhotonNetwork.NetworkingClient.EventReceived += ApplyGameOver;
        }

        private void OnDisable()
        {
            PhotonNetwork.NetworkingClient.EventReceived -= ApplyGameOver;
        }

        private void ApplyGameOver(EventData obj)
        {
            if (obj.Code == 2)
            {
                Destroy(ballTransform.gameObject);
                gameOverMenu.SetActive(true);
            }
        }
    }
}