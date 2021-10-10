using Pong.General;
using Zenject;

namespace Pong.MP
{
    public class MP_Installer : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EventMaster>().AsSingle();
            Container.Bind<IScoreHandler_MP>().To<ScoreHandlerModel>().AsSingle();
            Container.Bind<IMoveBall>().To<BallMovementModel>().AsSingle();
        }
    }
}