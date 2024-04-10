using UnityEngine;
using Handler;

namespace Assets.Scripts.ItemFactory
{
    public class BoostController
    {
        private IBoostHandler boostHandler;
        private BoostFactory boostFactory;

        public BoostController(IBoostHandler boostHandler, BoostFactory boostFactory)
        {
            this.boostHandler = boostHandler;
            this.boostFactory = boostFactory;
        }

        /// <summary>
        /// Method for creating boosts and instantiating them into the scene
        /// </summary>
        /// <param name="parent">of the created boosts</param> 
        public void CreateBoosts()//Transform parent)
        {
             // creating branches
            for (int i = 0; i < boostHandler.Quantity; i++)
            {
                // setting ItemEntry if not intialized by the inspector e.g. tests
                if (boostHandler.BoostEntry[i] == null)
                {
                    boostHandler.BoostEntry[i] = new BoostEntry();
                }
                // creating the items & instantiating them into the scene
                boostHandler.BoostEntry[i].Boost = boostFactory.CreateBoost(boostHandler.BoostType);

                // setting the position
                boostHandler.BoostEntry[i].Boost.BoostObj.transform.position = boostHandler.BoostEntry[i].Position;

                // setting the parent of the object
                boostHandler.BoostEntry[i].Boost.BoostObj.transform.SetParent(boostHandler.Parent);
            }
        }

        /// <summary>
        /// Method to boost the player stats such as speed, jumpforce..
        /// </summary>
        /// <param name="mono">MonoBehaviour class for starting the coroutine</param>
        /// <param name="playerMovement">of the player</param>
        public void BoostPlayer(MonoBehaviour mono, PlayerMovement playerMovement)
        {
            mono.StartCoroutine(boostHandler.BoostEntry[0].Boost.BoostPlayer(playerMovement));
        }
    }
}