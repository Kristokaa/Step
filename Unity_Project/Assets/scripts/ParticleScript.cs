using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{

    public CameraMovement cameraScript;
    public float displayDistance;
    private ParticleSystem particles;
    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Mathf.Abs(Vector3.Distance(cameraScript.transform.position, transform.position));
        if (distance - displayDistance > cameraScript.currentStamina/6 ){
            particles.Stop();
        }
        else {
            particles.Play();
        }
    }
}
