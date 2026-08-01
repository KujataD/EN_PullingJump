using UnityEngine;

public class Item : MonoBehaviour
{

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    } 
    
    void SelfDestroy()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider collision)
    {
        animator.SetTrigger("Get");     
    }
}
