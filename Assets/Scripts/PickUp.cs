using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(MeshCollider))]

public class PickUp : MonoBehaviour 
{
    [SerializeField] private int _points = 100;
    private bool _isCollected = false; // make sure the pickup is only collected once (protect from multiple hits)

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (!_isCollected)
            {
                Collect();
            }
        }
    }

    public void Collect()
    {
        if (!_isCollected)
        {
            _isCollected = true;
            GameManager.IncrementScore(_points);
            GameManager.RespawnPickup();
            Destroy(gameObject);
        }
    }
}
