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
    public class JumpBoost: Boost
    {
        public JumpBoost() : base()
        {
            BoostType = BoostType.JUMPBOOST;
            Name = "Strength boost";
        }

        /// <summary>
        /// Method for increasing the jump of the player by 3 seconds and resetting it after 3 seconds
        /// </summary>
        /// <param name="movement">of the player</param>
        public override IEnumerator BoostPlayer(PlayerMovement movement)
        {
            movement.JumpForce = 3;
            //return 2;
            //throw new System.NotImplementedException();
            yield return new WaitForSeconds(3);
            movement.JumpForce = 10;
        }

        /// <summary>
        /// Method for instantiating the associated prefab
        /// </summary>
        public override void InstantiateBoostPrefab()
        {
            var prefab = GameObject.Instantiate(Resources.Load("Prefab/Boosts/MilkPrefab"));
            BoostObj = (GameObject)prefab;
        }

    }
}