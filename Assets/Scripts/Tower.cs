using UnityEngine;

public class Tower : MonoBehaviour
{
    public Transform towerHead;
    public Transform enemy;

    public float attackRange = 3.0f;  // attack range of the tower

    public GameObject bulletPrefab; // bullet prefab reference
    public float bulletSpeed = 3.0f; // speed of the bullet

    private void Update()
    {
        if(Vector3.Distance(enemy.position, towerHead.position) < attackRange)
        {
            towerHead.LookAt(enemy);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject newBullet = Instantiate(bulletPrefab, towerHead.position, Quaternion.identity);
                newBullet.GetComponent<Rigidbody>().velocity = (enemy.position - towerHead.position).normalized * bulletSpeed;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
