using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script will enable parallaxing, which is a technique that creates an illusion 
/// of depth in a 2D scene by moving background images more slowly past the camera than foreground images. 
/// This script is bound to the backgrounds.
/// </summary>
public class Parallax : MonoBehaviour
{
    [SerializeField]
    private Vector2 parallaxEffect;
    private Vector3 prevCameraPos;

    private Transform parallaxCamera;

    // Start is called before the first frame update
    void Start()
    {
        parallaxCamera = Camera.main.transform; // Setting the parallax effect to the main camera
        prevCameraPos = parallaxCamera.position; // Referencing the position of the parallax effect.
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ParallaxFunction();
    }

    public void ParallaxFunction()
    {
        // Calculating the parallaxmovement for different backgrounds. Accessible through inspector.
        Vector3 parallaxMovement = parallaxCamera.position - prevCameraPos;
        transform.position += new Vector3(parallaxMovement.x * parallaxEffect.x, parallaxMovement.y * parallaxEffect.y);
        prevCameraPos = parallaxCamera.position;
    }
}
