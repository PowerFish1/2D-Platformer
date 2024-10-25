using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollection : MonoBehaviour
{
    private int bananas = 0;

    [SerializeField] private Text bananaText;
    [SerializeField] private AudioSource itemCollectSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Banana"))
        {
            itemCollectSound.Play();
            Destroy(collision.gameObject);
            bananas++;
            bananaText.text = "Bananas:" + bananas;
        }
    }



}
