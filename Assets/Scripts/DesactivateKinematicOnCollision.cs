using UnityEngine;
using System.Collections;

public class DesactivateKinematicOnCollision : MonoBehaviour {

    public AudioSource CrashSound;

    void OnCollisionEnter(Collision collision)
    {

        if (collision.relativeVelocity.magnitude >= 10)
        {
            
            // Sonar crash de roto
            if (CrashSound != null && !CrashSound.isPlaying)
            {
                CrashSound.Play();
            }

            this.GetComponent<Rigidbody>().isKinematic = false;

        }

    }

}
