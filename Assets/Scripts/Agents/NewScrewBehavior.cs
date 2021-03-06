using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewScrewBehavior : MonoBehaviour
{
    public float moveSpeed;
    public int points;

    private GameObject magnet;
    private Rigidbody2D _rb;
    private CircleCollider2D _col;
    private CircleCollider2D _col2;

    public FMODUnity.EventReference addObjectToBall;
    public FMODUnity.EventReference addObjectToMagnet;

    private ParticleSystem _particles;


    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _col = gameObject.GetComponent<CircleCollider2D>();
        _col2 = gameObject.GetComponentInChildren<CircleCollider2D>();
        _particles = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MagneticObject"))
        {
            _particles.Play();
            DesactivatePhysics();
            SetChildOfTheMagnet(collision.gameObject);
            ScoreManager.Instance.AddScore(points);
            FMODUnity.RuntimeManager.PlayOneShot(addObjectToBall);
            //_isAtracted = true;
            magnet = collision.gameObject;
        }
        else if (collision.gameObject.CompareTag("Edge"))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DesactivatePhysics();
            FMODUnity.RuntimeManager.PlayOneShot(addObjectToMagnet);
            SetChildOfTheMagnet(collision.gameObject);
            magnet = collision.gameObject;
            this.enabled = false;
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
