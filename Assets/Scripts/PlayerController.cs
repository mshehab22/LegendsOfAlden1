using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;

    private Vector3 startingPosition;
    private float startingZ;

    // How far the camera has moved since the start (in 2D space)
    private Vector2 CamMoveSinceStart => (Vector2)cam.transform.position - (Vector2)startingPosition;

    // Distance between this layer and the follow target (for parallax)
    private float ZDistanceFromTarget => transform.position.z - followTarget.position.z;

    // Clipping plane calculation (used to normalize parallax)
    private float ClippingPlane => Mathf.Abs(cam.transform.position.z) + (ZDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane);

    // Parallax multiplier based on depth
    private float ParallaxFactor => Mathf.Abs(ZDistanceFromTarget) / ClippingPlane;

    void Start()
    {
        if (cam == null) cam = Camera.main;
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }

    void LateUpdate()
    {
        if (cam == null || followTarget == null) return;

        Vector2 newPos2D = (Vector2)startingPosition + CamMoveSinceStart * ParallaxFactor;
        transform.position = new Vector3(newPos2D.x, newPos2D.y, startingZ);
    }
}







