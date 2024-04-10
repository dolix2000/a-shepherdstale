using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Lever
{

    /// <summary>
    /// Method that shows a platform if its invisble else it makes it invisible
    /// </summary>
    /// <param name="ground"> The ground which is affected by the method</param>

    public void MakePlatformVisibleorInvisbile(GameObject ground)
    {
        if (ground.GetComponent<Renderer>().isVisible)
        {
            ground.GetComponent<Renderer>().enabled = false;
            ground.GetComponent<TilemapCollider2D>().enabled = false;


        }
        else
        {
            ground.GetComponent<Renderer>().enabled = true;
            ground.GetComponent<TilemapCollider2D>().enabled = true;


        }


    }





    /// <summary>
    /// Method that sets an Ground or background inactive when its active so the player can see hidden Objects or Levers else 
    /// the ground/background gets active and the other Object disapears behind the wall again
    /// </summary>
    /// <param name="ground">The ground which is affected by the method</param>

    public void SetGroundActiveorInactive(GameObject ground)
    {
        if (ground.activeSelf)
        {
            //ground.GetComponent<Renderer>().enabled = false;
            // ground.GetComponent<TilemapCollider2D>().enabled = false;
            ground.SetActive(false);

        }
        else
        {
            ground.GetComponent<Renderer>().enabled = true;
            //ground.GetComponent<TilemapCollider2D>().enabled = true;
            ground.SetActive(true);

        }
    }


    /// <summary>
    /// Method which activates the platform moving script and make them visible 
    /// </summary>
    /// <param name="ground">The ground which is affected by the method</param>

    public void Move(GameObject platform)
    {

        if (platform.activeSelf)
        {

            Debug.Log("Ich bin aktiv");
            platform.SetActive(false);
            platform.GetComponent<Platform>().enabled = true;

        }
        else
        {
            Debug.Log("Ich bin aus");
            platform.SetActive(true);
            platform.GetComponent<Platform>().enabled = true;

        }


    }










}