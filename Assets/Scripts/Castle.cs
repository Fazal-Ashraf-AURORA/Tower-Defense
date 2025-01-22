using UnityEngine;

public class Castle : MonoBehaviour
{

    private void Start()
    {
       // Application.targetFrameRate = 60;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().TakeDamage(9999);
        }
    }
}
