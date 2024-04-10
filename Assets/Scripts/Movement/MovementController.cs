using UnityEngine;

namespace Assets.Scripts.Movement
{
    public class MovementController 
    {
        public MovementController()
        {
        }

        /// <summary>
        /// Method for moving the player
        /// </summary>
        /// <param name="body"> of the player </param> 
        /// <param name="horizontalMovement"> number between -1 and 1 for the player moving on x-axis</param> 
        /// <param name="speed"> of the player</param> 
        public void Move(Rigidbody2D body, float horizontalMovement, float speed)
        {
            //moveInput = Input.GetAxisRaw("Horizontal");

            body.velocity = new Vector2(horizontalMovement * speed, body.velocity.y);
        }

        /// <summary>
        /// Method for the player to jump in the game
        /// </summary>
        /// <param name="body"> of the player </param> 
        /// <param name="feet"> of the player contact with ground </param> 
        /// <param name="layerMask"> for this example the ground, so the player knows when it can jump </param> 
        /// <param name="jumpForce"> determines jump height of the player </param> 
        /// <param name="animator"></param>
        public void Jump(Rigidbody2D body, Transform feet, LayerMask layerMask, float jumpForce, Animator animator)
        {
            if (IsGrounded(feet, layerMask, animator) && Input.GetKeyDown(KeyCode.W))
            {
                body.velocity = Vector2.up * jumpForce;
                animator.SetBool("IsJumping", true);
            }
        }

        /// <summary>
        /// Method for the player controling the jump e.g. jumps higher if w is pressed longer, jumps shorer if w is presse shorter
        /// </summary>
        /// <param name="body"> of the player </param> 
        /// <param name="fallMultiplier"> how fast the player falls </param> 
        /// <param name="lowJumpMultiplier"> lowest point the player jumps </param> 
        public void ControlJumping(Rigidbody2D body, float fallMultiplier, float lowJumpMultiplier)
        {
            if (body.velocity.y < 0)
            {
                body.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
            }
            else if (body.velocity.y > 0 && !Input.GetKey(KeyCode.W))
            {
                body.velocity += Vector2.up * Physics2D.gravity.y * lowJumpMultiplier * Time.deltaTime;
            }
        }

        /// <summary>
        /// Method for checking if the player is allowed to jump
        /// </summary>
        /// <param name="feet"> of the player </param> 
        /// <param name="layerMask"> for this example the ground, so the player knows when it can jump </param> 
        /// <returns></returns>
        public bool IsGrounded(Transform feet, LayerMask layerMask, Animator animator)
        {
            Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 0.1f, layerMask);
            if (groundCheck != null)
            {
                OnLanding(animator);
                return true;
            } else
            {
                return false;
            }
        }

        /// <summary>
        /// When the charakter is Landing the 
        /// </summary>
        /// <param name="animator"></param>
        public void OnLanding(Animator animator)
        {
            animator.SetBool("IsJumping", false);
        }

        /// <summary>
        /// CharacterLayers don´t collide anymore
        /// </summary>
        public void IgnoreCharacterCollision()
        {
            Physics2D.IgnoreLayerCollision(7, 8, true);
        }

        /// <summary>
        /// Method for changing the direction the player is facing or wants to walk to
        /// Instead of an if - else statement, there are two if statements used to catch up the direction = 0 case
        /// If 0 is reached the direction or the facing of the player should stay instead of change
        /// </summary>
        /// <param name="body"> of the player </param>
        /// <param name="direction"> if the player is facing right direction = 1 if player is facing left direction = -1 </param>
        public void ChangeDirection(Rigidbody2D body, float direction)
        {   
            bool facingRight;
            if (direction > 0)
            {
                facingRight = true;
                Flip(body, facingRight);
            }
            if (direction < 0)
            {
                facingRight = false;
                Flip(body, facingRight);
            }
        }

        /// <summary>
        /// Method for flipping the image of the player
        /// If the player is looking left or right the image of the player has to be scaled the right way
        /// </summary>
        /// <param name="body"> of the player</param>
        /// <param name="facingRight">when the player looks to the right side it is true</param>
        private void Flip(Rigidbody2D body, bool facingRight)
        {
            Vector3 scale = body.transform.localScale;
            if (((facingRight) && (scale.x < 0)) || ((!facingRight) && (scale.x > 0)))
            {
                scale.x *= -1;
            }
            body.transform.localScale = scale;
        }
    }
}