using ComputerInterface.Interfaces;
using Zenject;

namespace MonkeTunes.ComputerInterface
{
    internal class MainInstaller : Installer
    {
        public override void InstallBindings()
        {
            base.Container.Bind<IComputerModEntry>().To<MonkeTunesEntry>().AsSingle();
        }
    }
}
