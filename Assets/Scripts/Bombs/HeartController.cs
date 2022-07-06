using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartController1 : MonoBehaviour
{
    // private Rigidbody heartBody;
    // private BoxCollider heartCollider;
    // private AudioSource heartAudio;
    // // Start is called before the first frame update
    // void Start()
    // {
    //     heartBody = GetComponent<Rigidbody>();
    //     heartCollider = GetComponent<BoxCollider>();
    //     heartAudio = GetComponent<AudioSource>();
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     transform.Rotate(0f, 18f * Time.deltaTime, 0f, Space.Self);
    // }

    // private void OnTriggerEnter(Collider other) {
    //     if (other.gameObject.CompareTag("Player")){
    //         Debug.Log("Player reached heart");
    //         heartBody.AddForce(Vector3.up * 5, ForceMode.Impulse);
    //         heartBody.useGravity = true;
    //         heartCollider.enabled = false;
    //         heartAudio.PlayOneShot(heartAudio.clip);
    //         Debug.Log("Initial Health");
    //         other.GetComponent<TankHealth>().Heal();
    //         Debug.Log("Healed Health");
    //     } 
    // }
}
