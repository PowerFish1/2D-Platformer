using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rigidbody;
    [SerializeField] private AudioSource dieSoundFX;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();   
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }
    }


    private void Die()
    {
        dieSoundFX.Play();
        rigidbody.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("Death");
    }

    private void RestartLevel()
    {
        animator.SetTrigger("Born");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
