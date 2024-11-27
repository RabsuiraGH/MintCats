using Core.GameInputSystem;
using UnityEngine;
using Zenject;

namespace Core.GameInputSystem
{
    public class InputZInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<InputMaps>().AsSingle().NonLazy();
        }
    }
}