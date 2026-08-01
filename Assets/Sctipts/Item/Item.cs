using UnityEngine;

public class Item : MonoBehaviour
{

    private Animator animator;
    private AudioSource audioSource;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void SelfDestroy()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider collision)
    {
        animator.SetTrigger("Get");
        
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
