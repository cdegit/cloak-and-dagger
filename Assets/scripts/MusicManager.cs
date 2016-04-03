using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {
	public AudioSource titleScreen;
	public AudioSource desert;
	public AudioSource city;
	public AudioSource garden;

	public float crossFadeModifier = 0.5f;

	private AudioSource currentAudio;
	private AudioSource newAudio;

	private bool doCrossfade;


	void Start() {
		currentAudio = titleScreen;
	}

	void Update() {
		if (doCrossfade) {
			Crossfade();
		}
	}

	void Crossfade() {
		if (!currentAudio || !newAudio) {
			return;
		}

		if (!newAudio.isPlaying) {
			newAudio.Play();
		}

		if (currentAudio.volume > 0.01) {
			currentAudio.volume -= crossFadeModifier * Time.deltaTime;
		} else if (currentAudio.isPlaying) {
			currentAudio.Stop();
		}

		if (newAudio.volume < 1) {
			newAudio.volume +=  crossFadeModifier * Time.deltaTime;
		} else {
			currentAudio = newAudio;
			newAudio = null;
			doCrossfade = false;
		}
	}

	public void PlayDesertMusic() {
		doCrossfade = true;
		newAudio = desert;
	}

	public void PlayCityMusic() {
		doCrossfade = true;
		newAudio = city;
	}

	public void PlayGardenMusic() {
		doCrossfade = true;
		newAudio = garden;
	}
}
