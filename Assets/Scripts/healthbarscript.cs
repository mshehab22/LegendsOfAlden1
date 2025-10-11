using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("Refs")]
    public Slider slider;            // assign the UI Slider
    public Damageable damageable;    // drag your player here

    [Header("Visuals")]
    public bool smooth = true;
    public float speed = 8f;         // higher is snappier

    float target01 = 1f;

    void Awake()
    {
        if (slider != null)
        {
            slider.minValue = 0f;
            slider.maxValue = 1f;
            slider.value = 1f;
        }
    }

    void Update()
    {
        if (damageable == null || slider == null) return;

        int hp = Mathf.Max(0, damageable.Health);
        int max = Mathf.Max(1, damageable.MaxHealth);

        target01 = (float)hp / max;

        if (smooth)
            slider.value = Mathf.MoveTowards(slider.value, target01, speed * Time.deltaTime);
        else
            slider.value = target01;
    }
}
