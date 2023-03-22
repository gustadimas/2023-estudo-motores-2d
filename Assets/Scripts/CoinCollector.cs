using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCollector : MonoBehaviour
{
    int ring = 0;
    [SerializeField] Text ringText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Rings"))
        {
            Destroy(collision.gameObject);
            ring++;
            ringText.text = "Rings: " + ring;
            AudioManager.instance.Play("Itens");
        }
    }
}
