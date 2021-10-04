using Pong.General;
using Pong.MP;
using UnityEngine;
using Zenject;

public class PongInstaller : MonoInstaller
{
    [SerializeField] private GameObject friendsUIPrefab;
    [SerializeField] private GameObject invitesUIPrefab;

    public override void InstallBindings()
    {
        Container.Bind<EventMaster>().AsSingle();
        Container.BindFactory<Transform, FriendsUI, FriendsUI.Factory>().FromComponentInNewPrefab(friendsUIPrefab);
        Container.BindFactory<InviteUI, InviteUI.Factory>().FromComponentInNewPrefab(invitesUIPrefab);

        Container.Bind<IScoreHandler_SP>().To<ScoreHandler_SP>().AsSingle();
    }
}