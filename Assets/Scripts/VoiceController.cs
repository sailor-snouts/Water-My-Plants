using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceController : MonoBehaviour {

    private AudioSource[] voices;
    [SerializeField]
    private float volumeChangeRate = 0.25f;
    [SerializeField]
    private float fullVolumePadding = 0.25f;

    // Use this for initialization
    void Start () {
        voices = this.gameObject.GetComponentsInChildren<AudioSource>();
    }

    public void HandleVoices(float completion) {
        /**
         * Ensure a minimum volume level and set our max volume ceiling by padding the completion amount.
         * With 0 padding, the voices will only be at full volume right as you complete the level.
         */
        float totalDesiredVolume = Mathf.Clamp(completion + fullVolumePadding , 0.1f, 1f);
        
        float voice_count = voices.Length;

        /**
         * Returns the whole part of the float, which represents the number of voices to move towards max volume.
         */
        int voicesAtMaxVolume = (int) Mathf.Lerp(0f, voice_count, totalDesiredVolume);

        /**
         * Returns decimal part of the float, which represents the volume level that the last
         * audible voice should move towards.
         */
        float volumeRemainder = Mathf.Lerp(0f, voice_count, totalDesiredVolume) % 1;
        
        int i = 0;
        foreach(AudioSource voice in voices ) {
            /**
             * Max out all voices until we hit the threshold for voices that should be at max volume.
             */
            if (i < voicesAtMaxVolume) {
                maxOutVoice(voice);
            }

            /**
             * We've hit the tipping point, so this voice won't be at max, but just gets the remainder volume level.
             */
            if(i == voicesAtMaxVolume) {
                setVolume(voice, volumeRemainder);
            }

            /**
             * All the voices past the threshold are muted.
             */ 

            if(i > voicesAtMaxVolume) {
                muteVoice(voice);
            }
            i++;
        }
    }

    /**
     * Smoothly push the volume of the given voice in the direction indicated by targetVolume.
     */
    private void setVolume(AudioSource voice, float targetVolume) {
        float adjustedVolumeForThisFrame = Mathf.SmoothStep(voice.volume, targetVolume, volumeChangeRate);
        voice.volume = adjustedVolumeForThisFrame;
    }

    private void maxOutVoice(AudioSource voice) {
        setVolume(voice, 1f);
    }

    private void muteVoice(AudioSource voice) {
        setVolume(voice, 0f);
    }   
	
	// Update is called once per frame
	void Update () {
		
	}
}
