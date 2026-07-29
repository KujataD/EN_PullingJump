using UnityEngine;

public class Item : MonoBehaviour
{
    void SelfDestroy()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider collision)
    {
        SelfDestroy();   
    }
}
