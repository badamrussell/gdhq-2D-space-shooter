using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;

    private Player _player;
    private int _points = 10;

    private Animator _animator;

    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {

        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();

        if (_player == null)
        {

        } 
        _animator = this.GetComponent<Animator>();
        if (_animator == null)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0f;
            _audioSource.Play();
            Destroy(this.gameObject, 2.8f);
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(_points);
            }
            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0f;
            _audioSource.Play();

            Destroy(GetComponent<Collider2D>());

            Destroy(this.gameObject, 2.8f);
        }
    }
}
