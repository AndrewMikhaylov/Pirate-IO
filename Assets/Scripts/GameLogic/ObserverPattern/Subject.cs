using System.Collections.Generic;
using Abilities;
using PlayerController;
using UnityEngine;

namespace GameLogic.ObserverPattern
{
    public abstract class Subject: MonoBehaviour
    {
        private List<IPlayerObserver> _observers = new List<IPlayerObserver>();

        public void AddObserver(IPlayerObserver playerObserver)
        {
            _observers.Add(playerObserver);
        }

        public void RemoveObserver(IPlayerObserver playerObserver)
        {
            _observers.Remove(playerObserver);
        }

        protected void NotifyAll(Player player, bool isMiniBossKill)
        {
            foreach (var observer in _observers)
            {
                observer.OnPlayerNotify(player, isMiniBossKill);
            }
        }
    }
}