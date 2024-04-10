using System.Collections;
using UnityEngine;
using Assets.Scripts.ItemFactory;

namespace Assets.Scripts.Items
{
    /// <summary>
    /// Base class of all the boosts
    /// </summary>
    public abstract class Boost
    {
        // basic constructor   
        public Boost()
        {
            Name = "Boost";
            InstantiateBoostPrefab();
        }

        public string Name
        {
            get;
            set;
        }

        public BoostType BoostType
        {
            get;
            set;
        }

        public GameObject BoostObj
        {
            get;
            set;
        }

        public Vector3 Position
        {
            get;
            set;
        }

        /// <summary>
        /// method for boosting the stats of the player
        /// </summary>
        /// <param name="movement">to access stats of the player such as jumpForce, speed, etc.</param>
        /// <returns>changes the stats based on the boost</returns>
        public virtual IEnumerator BoostPlayer(PlayerMovement movement) 
        {
            yield return new WaitForSeconds(0f);
        }


        /// <summary>
        /// method for instantiating the items into the scene
        /// </summary>
        public abstract void InstantiateBoostPrefab();
    }
}