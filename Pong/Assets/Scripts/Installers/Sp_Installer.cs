using Pong.General;
using Pong.SP;
using UnityEngine;
using Zenject;

public class Sp_Installer : MonoInstaller
{
    [SerializeField] private GameObject playerMovementScript;

    public override void InstallBindings()
    {
        Container.Bind<EventMaster>().AsSingle();
        Container.Bind<IScoreHandler_SP>().To<ScoreHandler_SP>().AsSingle();
        Container.Bind<PlayerMovement>().FromComponentOn(playerMovementScript).AsSingle();
    }
}