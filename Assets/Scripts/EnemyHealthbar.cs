using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider slider;
    public Damageable damageable;
    public bool hideWhenFull = true;

    void Awake()
    {
        if (!slider) slider = GetComponent<Slider>();
        if (!damageable) damageable = GetComponentInParent<Damageable>();

        slider.minValue = 0f;
        slider.maxValue = 1f;
        slider.value = 1f;
    }

    void LateUpdate()
    {
        if (!damageable) return;

        int hp = Mathf.Max(0, damageable.Health);
        int max = Mathf.Max(1, damageable.MaxHealth);
        float t = (float)hp / max;

        slider.value = t;

        if (hideWhenFull)
            slider.gameObject.SetActive(t < 1f && damageable.IsAlive);
    }
}
