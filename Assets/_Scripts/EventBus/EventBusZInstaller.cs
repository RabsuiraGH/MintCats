using Zenject;

namespace Core.GameEventSystem
{
    internal class EventBusZInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EventBus>().AsSingle().NonLazy();
        }
    }
}