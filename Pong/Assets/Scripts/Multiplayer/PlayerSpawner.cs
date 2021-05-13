using Photon.Pun;
using UnityEngine;

public class PlayerSpawner : MonoBehaviourPun
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private float offset = 7f;

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(offset, 0f), Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(-offset, 0f), Quaternion.identity);
        }
    }
}
