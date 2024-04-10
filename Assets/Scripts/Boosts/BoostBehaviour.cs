using Assets.Scripts.Movement;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.ItemFactory
{
    /// <summary>
    /// Regarding perfomance and practical reason, this Script ItemBehaviour was created.
    /// This Script sums up the whole BoostFactory Scripts.
    /// Since setting them in the Unity Scene itself is much easier and faster, than instantiating it through code (position, variety, type).
    /// </summary>
    public class BoostBehaviour: MonoBehaviour
    {
        [SerializeField] 
        private BoostType boostType;
        public BoostType BoostType
        {
            get { return this.boostType; }
            set { this.boostType = value; }

        }

        [SerializeField] 
        private GameObject player;

        /// <summary>
        /// Method for the current player
        /// </summary>
        /// <param name="other">Object/Player that gets boosted</param>
        private void OnTriggerEnter2D(Collider2D other)
        {
            PlayerMovement shepherd = player.transform.GetChild(0).GetComponent<PlayerMovement>();
            PlayerMovement dog = player.transform.GetChild(1).GetComponent<PlayerMovement>();

            if (other.gameObject.CompareTag("Player"))
            {
                if (other.gameObject.name == "Shepherd")
                {
                    
                    StartCoroutine(BoostPlayer(shepherd));
                }
                else
                {
                    StartCoroutine(BoostPlayer(dog));
                }
            }
        }


        /// <summary>
        /// Method for checking which boost gets triggered, so the right response on the stats of the player will be made
        /// </summary>
        /// <param name="playerMovement"> of the player that gets influenced</param>
        /// <returns>changes the stats based on the boost</returns>
        private IEnumerator BoostPlayer(PlayerMovement playerMovement)
        {
            BoostType type = this.boostType;
            switch (type)
            {
                case BoostType.SPEEDBOOST:
                    Debug.Log("Boost of type: <color=brown>" + this.boostType + "</color>");
                    playerMovement.Speed += 5;
                    yield return new WaitForSeconds(3); // is what actually tells Unity to pause the script and continue on the next frame.
                    playerMovement.Speed = 10;
                    break;
                case BoostType.JUMPBOOST:
                    Debug.Log("Boost of type: <color=red>" + this.boostType + "</color>");
                    playerMovement.JumpForce += 23;
                    yield return new WaitForSeconds(2); // is what actually tells Unity to pause the script and continue on the next frame. 
                    playerMovement.JumpForce = 13;
                    break;
                case BoostType.TIMEBOOST:
                    Debug.Log("Boost of type: <color=yellow>" + this.boostType + "</color>");
                    GameManager.Instance.Player.TimeSpent -= 5;
                    break;
            }
        }
    }
}