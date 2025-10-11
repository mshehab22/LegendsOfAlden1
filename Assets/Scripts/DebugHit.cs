using UnityEngine;

public class DebugHit : MonoBehaviour
{
    public Damageable dmg;
    void Awake()
    {
        if (!dmg) dmg = GetComponent<Damageable>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
            dmg.Hit(10, Vector2.zero);
    }
}
