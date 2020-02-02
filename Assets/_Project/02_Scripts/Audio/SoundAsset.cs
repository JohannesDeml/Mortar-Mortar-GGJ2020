using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Supyrb;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundAsset", menuName = "GGJ2020/SoundAsset")]
public class SoundAsset : ScriptableObject
{
	[SerializeField]
	private AudioClip[] audioClips = null;

	private GameObject instance;
	private AudioSource audioSource;
	
	[Button()]
	public void Play()
	{
		Play(0);
	}

	[Button()]
	public void PlayRandom()
	{
		Play(Random.Range(0, audioClips.Length));
	}
	
	public void Play(int index)
	{
		if (!Application.isPlaying)
		{
			Debug.Log("Only available in play mode");
			return;
		}
		LazyInitialize();
		audioSource.clip = audioClips[index];
		audioSource.Play();
	}

	private void LazyInitialize()
	{
		if (instance == null)
		{
			instance = new GameObject(this.name);
			audioSource = instance.AddComponent<AudioSource>();
			audioSource.playOnAwake = false;
		}
	}
}
