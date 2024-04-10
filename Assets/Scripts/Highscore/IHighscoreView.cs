using UnityEngine;
using TMPro;

namespace Assets.Scripts.Highscore
{
    /// <summary>
    /// Interface for test purposes and intialising values if needed.
    /// Refer to HOP (Humble Object Pattern)
    /// </summary>
    public interface IHighscoreView 
    {
        public GameObject PlayerRow
        {
            get;
            set;
        }

        public TMP_FontAsset Font
        {
            get;
            set;
        }

        public Canvas Canvas
        {
            get;
            set;
        }
    }
}