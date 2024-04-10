using Assets.Scripts.ItemFactory;
using UnityEngine;

namespace Assets.Scripts.Items
{
    /// <summary>
    /// Subclass of the Baseclass Boost
    /// Overrides Methods such as BoostPlayer() (when needed) and
    /// InstantiateBoostPrefab()
    /// </summary>
    public class TimeBoost : Boost
    {
        public TimeBoost() : base()
        {
            BoostType = BoostType.TIMEBOOST;
            Name = "Time boost";
        }

        /// <summary>
        /// Since the TimeBoost is the only clas which, doesn't change the players stats, it won't override BoostPlayer()
        /// Methods for reduing the time of the player
        /// </summary>
        public void ReduceTime()
        {
            GameManager.Instance.Player.TimeSpent -= 5; // reducing the time by 5 seconds
        }

        /// <summary>
        /// Method for instantiating the associated prefab
        /// </summary>
        public override void InstantiateBoostPrefab()
        {
            var prefab = GameObject.Instantiate(Resources.Load("Prefab/Boosts/ClockPrefab"));
            BoostObj = (GameObject)prefab;
        }
    }
}