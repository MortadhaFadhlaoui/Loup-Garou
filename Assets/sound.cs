using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound : MonoBehaviour {

    
        public AudioSource myAudio;
        // Use this for initialization
        void Start()
        {

            StartCoroutine(PlaySoundAfterDelay(myAudio, 300.0f));
        }

        // Update is called once per frame
        void Update()
        {

        }

        IEnumerator PlaySoundAfterDelay(AudioSource audioSource, float delay)
        {
            if (audioSource == null)
                yield break;
            yield return new WaitForSeconds(delay);
            audioSource.Play();
        }

    }