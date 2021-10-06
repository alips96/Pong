using Pong.General;
using Pong.MP;
using Pong.MP.PlayFab;
using UnityEngine;
using Zenject;

public class PongInstaller : MonoInstaller
{
    [SerializeField] private GameObject friendsUIPrefab;
    [SerializeField] private GameObject invitesUIPrefab;
    [SerializeField] private GameObject buttonStatsUI;

    public override void InstallBindings()
    {
        Container.Bind<EventMaster>().AsSingle();
        Container.BindFactory<Transform, FriendsUI, FriendsUI.Factory>().FromComponentInNewPrefab(friendsUIPrefab);
        Container.BindFactory<InviteUI, InviteUI.Factory>().FromComponentInNewPrefab(invitesUIPrefab);

        Container.Bind<PlayerStatsModel>().AsSingle();
        Container.Bind<PlayerStatsView>().FromComponentOn(buttonStatsUI).AsSingle();
    }
}