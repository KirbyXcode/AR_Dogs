using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DogProject
{
	public class AudioManager : BaseManager 
	{
		private AudioClip clip;
		private AudioSource audio;

		public AudioManager (Facade facade): base(facade)
		{
			audio = Camera.main.GetComponent<AudioSource> ();
			LoadAudio ();
			audio.clip = clip;
		}

		private void LoadAudio()
		{
			clip = Resources.Load<AudioClip> ("Sound");
		}

		public void PlaySound(float volume = 0.5f, bool isLoop = false)
		{
			audio.volume = volume;
			audio.loop = isLoop;
			audio.Play ();
		}
	}
}
