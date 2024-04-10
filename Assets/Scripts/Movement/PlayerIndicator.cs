using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Movement
{
    /// <summary>
    /// Script that shows, where the other playable character is to have an overview
    /// </summary>
    public class PlayerIndicator : MonoBehaviour
    {

        [SerializeField]
        private GameObject indicator;

        [SerializeField]
        private GameObject target;

        private Renderer _renderer;

        private readonly float smoothTime = 0.1f; // time to reach the target - e.g. camera collider zone
        private Vector3 velocity = Vector3.zero; // internal variable for SmoothDamp function, changes during usage.

        // bitshift, ignore all layers except for the tenth layer
        private LayerMask layerMask = 1 << 10;

        // Use this for initialization
        private void Start()
        {          
            _renderer = GetComponent<Renderer>();
        }

        private void FixedUpdate()
        {
            IndicateOtherPlayer();
        }

        /// <summary>
        /// Method for indicating the other player, to have an overview
        /// </summary>
        public void IndicateOtherPlayer()
        {
            // if the other player is visible
            // and the pointer is active --> deactivate it
            // else activate to see where the other playable character is
            if (!_renderer.isVisible)
            {
                if (!indicator.activeSelf)
                {
                    indicator.SetActive(true);
                }
                // calculate the direction of of the target and the current player
                Vector2 direction = target.transform.position - transform.position;

                // start point | endpoint
                // create a raycast between the other player and the current player, 
                // position of current player | the the direction to the other player | distance from the ray's origin to the impact point | mask thet it should collide with
                RaycastHit2D ray = Physics2D.Raycast(transform.position, direction, 1000, layerMask);

                // if the collider hits the collider of the main camera, set the position of the pointer element there to see the other player
                if (ray.collider != null)
                {
                    
                    Vector3 currentPosition = indicator.transform.position;
                    indicator.transform.position = Vector3.SmoothDamp(currentPosition, ray.point, ref velocity, smoothTime);
                   
                    Debug.DrawRay(transform.position, direction);
                }
            }
            else
            {
                if (indicator.activeSelf)
                {
                    indicator.SetActive(false);
                }
            }

        }
    }
}