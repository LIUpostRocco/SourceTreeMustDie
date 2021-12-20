using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class Options : MonoBehaviour {
	[System.Serializable]
	public class Volumes {
		public float master;
		public float music;
		public float sound;
	}

	[System.Serializable]
	public class Sliders {
		public Slider master;
		public Slider music;
		public Slider sound;
	}
	
	public Volumes volumes;
	public Sliders sliders;
	public AudioMixer mixer;

	private void Start() {
		volumes.master = PlayerPrefs.GetFloat("masterV", 0f);
		volumes.music = PlayerPrefs.GetFloat("musicV", 0f);
		volumes.sound = PlayerPrefs.GetFloat("soundV", 0f);

		sliders.master.value = volumes.master;
		sliders.music.value = volumes.music;
		sliders.sound.value = volumes.sound;

		mixer.SetFloat("masterV", volumes.master);
		mixer.SetFloat("musicV", volumes.music);
		mixer.SetFloat("soundV", volumes.sound);
	}

	private void UpdateVolume(string who, float value) {
		mixer.SetFloat(who, value);
		PlayerPrefs.SetFloat(who, value);
	}

	public void UpdateMaster(float value) {
		UpdateVolume("masterV", value);
	}

	public void UpdateMusic(float value) {
		UpdateVolume("musicV", value);
	}

	public void UpdateSound(float value) {
		UpdateVolume("soundV", value);
	}

	public void SetG(int to) {
		QualitySettings.SetQualityLevel(to);
	}

	public void SetF(bool huh) {
		Screen.fullScreen = huh;
	}
}

// ~ Rocco Russo