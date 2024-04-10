using Assets.Scripts.ItemFactory;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Items
{
    /// <summary>
    /// Subclass of the Baseclass Boost
    /// Overrides Methods such as BoostPlayer() (when needed) and
    /// InstantiateBoostPrefab()
    /// </summary>
    public class SpeedBoost : Boost
    { 
        public SpeedBoost() : base()
        {
            BoostType = BoostType.SPEEDBOOST;
            Name = "Speed boost";
        }

        /// <summary>
        /// Method for increasing the speed of the player by 5 seconds and resetting it after 3 seconds
        /// </summary>
        /// <param name="movement">of the player</param>
        public override IEnumerator BoostPlayer(PlayerMovement movement)
        {
            movement.Speed += 5; 
            yield return new WaitForSeconds(3); // is what actually tells Unity to pause the script and continue on the next frame.
            movement.Speed = 10;
        }

        /// <summary>
        /// Method for instantiating the associated prefab
        /// </summary>
        public override void InstantiateBoostPrefab()
        {
            var prefab = GameObject.Instantiate(Resources.Load("Prefab/Boosts/BranchPrefab"));
            BoostObj = (GameObject)prefab;

        }
    }
}