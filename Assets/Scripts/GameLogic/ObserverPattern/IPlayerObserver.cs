using Abilities;
using PlayerController;

namespace GameLogic.ObserverPattern
{
    public interface IPlayerObserver
    {
        void OnPlayerNotify(Player player, bool isMiniBossKill);
    }
}