using UnityEngine;
using System;
using Assets.Scripts.Items;
using Assets.Scripts.ItemFactory;
using Assets.Scripts.Movement;

namespace Handler
{
    /// <summary>
    /// Boosts gets created in BoostHandler.
    /// It handles how much, and what kind of items get created and set them in the scene.
    /// Implements the logic of the BoostController
    /// Creating the boosts (through BoostController) is based on their type and sets their position based on the values in the inspector.
    /// </summary>
    [System.Serializable]
    public class BoostHandler : MonoBehaviour, IBoostHandler
    {
        private BoostController boostController;
        private BoostFactory boostFactory;

        [SerializeField]
        private GameObject player;


        [SerializeField]
        private BoostEntry[] boostEntry;
        public BoostEntry[] BoostEntry
        {
            get { return boostEntry; }
            set { boostEntry = value; }
        }

        [SerializeField]
        private int quantity;
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        [SerializeField]
        private BoostType boostType;
        public BoostType BoostType
        {
            get { return boostType; }
            set { boostType = value; }
        }

        private Transform parent;
        public Transform Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        private void Awake()
        {
            parent = this.transform;
            boostFactory = new BoostFactory();
            boostController = new BoostController(this, boostFactory);
        }

        // Use this for initialization
        private void Start()
        {
            boostController.CreateBoosts();
        }

        /// <summary>
        /// this method sets the fixed size of an array based on the amount of items that get created
        /// </summary>
        private void OnValidate()
        {
            //boostEntry.Length = quantity;
            if (boostEntry.Length != quantity)
            {
                Debug.LogWarning("Wrong array size!");
                Array.Resize(ref boostEntry, quantity); // change reference from array with keyword ref
            }
        }

        /// <summary>
        /// Method for boosting the player
        /// </summary>
        /// <param name="other"> object/player that gets boosted</param>
        private void OnTriggerEnter2D(Collider2D other)
        {
            PlayerMovement shepherd = player.transform.GetChild(0).GetComponent<PlayerMovement>();
            PlayerMovement dog = player.transform.GetChild(1).GetComponent<PlayerMovement>();

            if (other.gameObject.CompareTag("Player"))
            {
                if (other.gameObject.name == "Shepherd")
                {

                    boostController.BoostPlayer(this, shepherd);
                }
                else
                {
                    boostController.BoostPlayer(this, dog);
                }
            }
        }

    }
}

/// <summary>
/// Helperclass to expose the variables in the inspector, by creating an array in BoostHandler
/// Convient for setting the values of the boosts
/// </summary>
[Serializable]
public class BoostEntry
{
    [SerializeField]
    private Vector3 position;

    private Boost boost;

    public Boost Boost
    {
        get { return boost; }
        set { boost = value; }
    }

    public Vector3 Position
    {
        get { return position; }
        set { position = value; }
    }
}


