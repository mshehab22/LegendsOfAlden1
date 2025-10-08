using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam;                // Drag your Main Camera here
    public Transform followTarget;    // Drag your player (or camera follow target)

    private Vector2 startingPosition;
    private float startingZ;

    private Vector2 CamMoveSinceStart => (Vector2)cam.transform.position - startingPosition;

    private float ZDistanceFromTarget => transform.position.z - followTarget.transform.position.z;

    private float ClippingPlane => (cam.transform.position.z + (ZDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));

    private float ParallaxFactor => Mathf.Abs(ZDistanceFromTarget) / ClippingPlane;

    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }

    void LateUpdate()
    {
        Vector2 newPosition = startingPosition + CamMoveSinceStart * ParallaxFactor;
        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}

