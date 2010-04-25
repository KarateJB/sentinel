using Sentinel.Logs.Interfaces;

namespace Sentinel.Providers.Interfaces
{
    public interface ILogProvider
    {
        IProviderInfo Information { get; }

        ILogger Logger { get; set; }

        void Start();

        void Pause();

        void Close();

        string Name { get; set; }

        bool IsActive { get; }
    }
}