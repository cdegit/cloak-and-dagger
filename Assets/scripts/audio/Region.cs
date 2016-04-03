using UnityEngine;
using System.Collections;

public class Region : MonoBehaviour {
	public enum RegionEnum{Desert, City, Garden};
	public RegionEnum region;

	private MusicManager music;

	void Start() {
		music = GameObject.Find("Music").GetComponent<MusicManager>();
	}

	void OnTriggerEnter(Collider other) {
		if (!music) {
			return;
		}

		if (region == RegionEnum.Desert) {
			music.PlayDesertMusic();
		}

		if (region == RegionEnum.City) {
			music.PlayCityMusic();
		}

		if (region == RegionEnum.Garden) {
			music.PlayGardenMusic();
		}
	}
}
