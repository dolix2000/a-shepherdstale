using Handler;
using UnityEngine;

namespace Assets.Scripts.ItemFactory
{
    /// <summary>
    /// Interface for test purposes and intialising values if needed.
    /// Refer to HOP (Humble Object Pattern)
    /// </summary>
    public interface IBoostHandler 
    {
        public BoostEntry[] BoostEntry
        {
            get;
            set;
        }

        public int Quantity
        {
            get;
            set;
        }

        public BoostType BoostType
        {
            get;
            set;
        }

        public Transform Parent
        {
            get;
            set;
        }
    }
}