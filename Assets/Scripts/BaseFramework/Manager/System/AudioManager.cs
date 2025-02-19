using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace BF
{
    public class AudioManager : Single<AudioManager>
    {
        [SerializeField] GameObject audioPrefab;        

        private void Start()
        {
#if UNITY_EDITOR
            if (!PoolManager.Instance().IsContain(audioPrefab.name))
            {
                Debug.Log("Dont have audioPrefab");
            }
#endif
        }   
        //
        public void Play(AudioBuilder builder)
        {
            var go = PoolManager.Instance().Release(audioPrefab.name);
            builder.SetAudioSource(go.GetComponent<AudioSource>());
            builder.Play();
            if (!builder.isLoop)
            {
                StartCoroutine("RecyclingAudioSource", builder);
            }
        }
        IEnumerator RecyclingAudioSource(AudioBuilder builder)
        {
            var source = builder.LastAudioSource;
            yield return new WaitForSeconds(builder.clip.length);
            PoolManager.Instance().Recycle(source.gameObject);
        }
        public void Pause(AudioBuilder builder)
        {
            if (builder.LastAudioSource && builder.LastAudioSource.isPlaying)
                builder.LastAudioSource.Pause();
        }
        public void UnPause(AudioBuilder builder)
        {
            if (builder.LastAudioSource && builder.LastAudioSource.isPlaying)
                builder.LastAudioSource.UnPause();
        }
        public void Stop(AudioBuilder builder)
        {
            if (builder.LastAudioSource && builder.LastAudioSource.isPlaying)
            {
                if (!builder.isLoop)
                {
                    StopCoroutine("RecyclingAudioSource");
                }
                PoolManager.Instance().Recycle(builder.LastAudioSource.gameObject);
            }
        }
    }

    public class AudioBuilder
    {
        //Necessary
        public AudioClip clip;
        public Vector3 pos;
        public AudioMixerGroup mixer;
        //Optional
        public int priority = 128;

        public Transform parent;
        public bool isLoop = false;
        public bool isPlayOnAwake = false;

        public bool isRandomPitch = false;
        public Vector2 randomPitchRange;

        public bool isDelay = false;
        public float delayTime;
        //
        AudioSource lastAudioSource;
        public AudioSource LastAudioSource => lastAudioSource;

        public AudioBuilder Initialize(AudioClip clip, Vector3 pos, AudioMixerGroup mixer)
        {
            this.clip = clip;
            this.pos = pos;
            this.mixer = mixer;
            return this;
        }
        #region Set Optional Paras
        /// <param name="priority">0: Max priority, 256: Min priority, 128: Default</param>
        /// <returns></returns>
        public AudioBuilder SetPriority(int priority)
        {
            this.priority = priority;
            return this;
        }
        public AudioBuilder SetParent(Transform parent)
        {
            this.parent = parent;
            return this;
        }
        public AudioBuilder SetLoop(bool isLoop)
        {
            this.isLoop = isLoop;
            return this;
        }
        public AudioBuilder SetIsPlayOnAwake(bool isPlayOnAwake)
        {
            this.isPlayOnAwake = isPlayOnAwake;
            return this;
        }
        public AudioBuilder SetRandomPitch(bool isRandomPitch, Vector2 randomPitchRange)
        {
            this.isRandomPitch = isRandomPitch;
            this.randomPitchRange = randomPitchRange;
            return this;
        }
        public AudioBuilder SetDelay(bool isDelay, float delayTime)
        {
            this.isDelay = isDelay;
            this.delayTime = delayTime;
            return this;
        }
        public void SetAudioSource(AudioSource audioSource)
        {
            lastAudioSource = audioSource;
        }
        #endregion

        public void Play()
        {
            lastAudioSource.clip = clip;
            lastAudioSource.transform.position = pos;
            lastAudioSource.outputAudioMixerGroup = mixer;

            lastAudioSource.transform.parent = parent;
            lastAudioSource.loop = isLoop;
            lastAudioSource.playOnAwake = isPlayOnAwake;
            if (isRandomPitch)
                lastAudioSource.pitch = Random.Range(randomPitchRange.x, randomPitchRange.y);
            else
                lastAudioSource.pitch = 1f;

            if (isDelay)
                lastAudioSource.PlayDelayed(delayTime);
            else
                lastAudioSource.Play();
        }
    }
}

