using UnityEngine;
using TMPro;

public class FloatingDamageText : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    public CanvasGroup group;

    public float riseSpeed = 1.5f;
    public float lifetime = 0.8f;
    public Vector3 worldOffset = new Vector3(0, 1.0f, 0);

    float t;
    Transform cam;

    void Awake()
    {
        if (!cam) cam = Camera.main.transform;
    }

    public void Show(int amount, Vector3 worldPos)
    {
        t = 0f;
        transform.position = worldPos + worldOffset;
        if (tmp) tmp.text = amount.ToString();
        if (group) group.alpha = 1f;
    }

    void LateUpdate()
    {
        // face camera
        if (cam) transform.rotation = Quaternion.LookRotation(cam.forward, Vector3.up);

        // rise and fade
        transform.position += Vector3.up * riseSpeed * Time.deltaTime;

        t += Time.deltaTime;
        if (group) group.alpha = 1f - Mathf.Clamp01(t / lifetime);

        if (t >= lifetime) Destroy(gameObject);
    }
}
