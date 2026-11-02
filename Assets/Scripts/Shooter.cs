using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Rigidbody bulletPrefab;
    public float force = 200f;
    public Transform muzzle;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Rigidbody b = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
            b.AddForce(muzzle.forward * force);
            Destroy(b.gameObject, t: 3f);
        }
    }
}
