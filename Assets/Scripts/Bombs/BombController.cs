using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    private Rigidbody bombBody;
    private BoxCollider bombCollider;
    private AudioSource bombAudio;
    public ParticleSystem smallBombExplosion;

    void Start()
    {
        bombBody = GetComponent<Rigidbody>();
        bombCollider = GetComponent<BoxCollider>();
        bombAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 18f * Time.deltaTime, 0f, Space.Self);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")){
            // Play explosion sound effects
            bombAudio.PlayOneShot(bombAudio.clip);
            // Play debris effect
            smallBombExplosion.Play();
            // Throw the tank back
            other.GetComponent<TankImpact>().OnBombImpact();
            // Decrease tank health
            other.GetComponent<TankHealth>().Explode();
        } 
    }
}
