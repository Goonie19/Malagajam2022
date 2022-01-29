using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewBehaviour : MonoBehaviour
{

    public int points;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MagneticObject"))
        {
            ScoreManager.Instance.AddScore(points);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Edge"))
        {
            Destroy(gameObject);
        }
    }
}
