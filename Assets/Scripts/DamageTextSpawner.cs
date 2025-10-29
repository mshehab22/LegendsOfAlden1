using UnityEngine;

public class DamageTextSpawner : MonoBehaviour
{
    public Damageable damageable;
    public FloatingDamageText damageTextPrefab;
    public Transform anchor;

    void Awake()
    {
        if (!damageable) damageable = GetComponent<Damageable>();
        if (!anchor) anchor = transform;
        damageable.damageableHit.AddListener(OnHit);
    }

    void OnDestroy()
    {
        if (damageable != null)
            damageable.damageableHit.RemoveListener(OnHit);
    }

    void OnHit(int amount, Vector2 knockback)
    {
        var inst = Instantiate(damageTextPrefab, anchor.position, Quaternion.identity);
        inst.Show(amount, anchor.position);
    }
}
