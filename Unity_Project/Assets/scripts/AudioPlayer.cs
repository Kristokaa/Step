using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioClip[] steps;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayRandom(){
        int chosen = Random.Range(0, steps.Length);
        var source = GetComponent<AudioSource>();
        source.clip = steps[chosen];
        source.Play();
    }
}
