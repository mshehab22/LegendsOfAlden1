using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public List<Collider2D> detectedColiders = new List<Collider2D>();
    Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        detectedColiders.Add(collision);
        
    }
        private void OnTriggerExit2D(Collider2D collision)
    {
        detectedColiders.Remove(collision);
    }
}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
