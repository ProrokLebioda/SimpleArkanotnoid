using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int points = 100;
    public int hits = 1;
    public Material hitMaterial;

    Material _originalMaterial;
    Renderer _renderer;

    public AudioClip _explosionAudio;
    public AudioClip _hitAudio;

    AudioSource _audioSource;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _originalMaterial = _renderer.sharedMaterial;
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _hitAudio;
    }

    private void OnCollisionEnter(Collision collision)
    {
        hits--;
        //Score points
        if (hits <= 0)
        {
            AudioSource.PlayClipAtPoint(_explosionAudio, new Vector3(0,0,0));//needs work
            GameManager.Instance.Score += points;
            Destroy(gameObject);
        }

        _audioSource.Play();
        _renderer.sharedMaterial = hitMaterial;

        Invoke("RestoreMaterial", 0.2f);
    }

    void RestoreMaterial()
    {
        _renderer.sharedMaterial = _originalMaterial;
    }
}
