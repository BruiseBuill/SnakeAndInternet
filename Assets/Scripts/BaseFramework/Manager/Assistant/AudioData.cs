using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace BF
{
	[CreateAssetMenu(fileName = "AudioData",menuName ="BF/AudioData")]
	public class AudioData : ScriptableObject
	{
		public AudioClip audioClip;
		public AudioMixer audioMixer;
		public float volume = 1;
		public bool isLoop;
	}
}