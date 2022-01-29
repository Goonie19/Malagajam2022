using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewScrewBehaviour : MonoBehaviour
{
    public float moveSpeed;

    private bool _isAtracted = false;
    private GameObject magnet;
    private Rigidbody2D _rb;
    private CircleCollider2D _col;
    private CircleCollider2D _col2;

    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _col = gameObject.GetComponent<CircleCollider2D>();
        _col2 = gameObject.GetComponentInChildren<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MagneticObject"))
        {
            DesactivatePhysics();
            SetChildOfTheMagnet(collision.gameObject);
            //_isAtracted = true;
            magnet = collision.gameObject;
        }
        else if (collision.gameObject.CompareTag("Edge"))
        {
            Destroy(gameObject);
        }
    }

    private void MoveToTheMagnet(Transform target)
    {
        Vector3.MoveTowards(transform.position, target.position, moveSpeed * 100 * Time.deltaTime);
    }

    private void SetChildOfTheMagnet(GameObject magnet)
    {
        gameObject.transform.SetParent(magnet.transform);
    }

    private void DesactivatePhysics()
    {
        Destroy(_rb);
        _col.enabled = false;
        _col2.enabled = false;
    }
}
