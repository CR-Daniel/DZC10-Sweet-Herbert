using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class SoundManager : MonoBehaviour {

    [SerializeField]
    private AudioClip[] clips;
    private AudioSource audioSource;
    private AudioClip[] clipsHurt;
    private AudioClip [] clipsHey;
    private AudioClip [] clipsHipHop;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Step() {
        AudioClip clip = GetRandomClip();
        audioSource.PlayOneShot(clip);
    }

    private AudioClip GetRandomClip() {
        clips = new AudioClip[] {
            (AudioClip)Resources.Load("Footsteps/Footsteps_Tile_Walk_01"),
            (AudioClip)Resources.Load("Footsteps/Footsteps_Tile_Walk_02"),
            (AudioClip)Resources.Load("Footsteps/Footsteps_Tile_Walk_03"),
            (AudioClip)Resources.Load("Footsteps/Footsteps_Tile_Walk_04"),
            (AudioClip)Resources.Load("Footsteps/Footsteps_Tile_Walk_05"),
            (AudioClip)Resources.Load("Footsteps/Footsteps_Tile_Walk_06"),
            (AudioClip)Resources.Load("Footsteps/Footsteps_Tile_Walk_07"),
            (AudioClip)Resources.Load("Footsteps/Footsteps_Tile_Walk_08")
        };
        return clips[UnityEngine.Random.Range(0, clips.Length)];
    }

    private void Hurt() {
        AudioClip hurt = GetRandomClipHurt();
        audioSource.PlayOneShot(hurt);
    }

    private AudioClip GetRandomClipHurt() {
        clipsHurt = new AudioClip[] {
            (AudioClip)Resources.Load("Screams/Mario1"),
            (AudioClip)Resources.Load("Screams/Mario2"),
            (AudioClip)Resources.Load("Screams/Yoda"),
            (AudioClip)Resources.Load("Screams/Oof"),
            (AudioClip)Resources.Load("Screams/Wilhelm"),
            (AudioClip)Resources.Load("Screams/Snake")
        };
        return clipsHurt[UnityEngine.Random.Range(0, clipsHurt.Length)];
    }

    private void Hey() {
        AudioClip hey = GetRandomClipHey();
        audioSource.PlayOneShot(hey);
    }

    private AudioClip GetRandomClipHey() {
        clipsHey = new AudioClip[] {
            (AudioClip)Resources.Load("Hey/Lego"),
            (AudioClip)Resources.Load("Hey/Obi"),
            (AudioClip)Resources.Load("Hey/hello-lionel"),
            (AudioClip)Resources.Load("Hey/hello_Adelle")
        };
        return clipsHey[UnityEngine.Random.Range(0, clipsHey.Length)];
    }

    private void HipHop() {
        AudioClip hiphop = GetRandomClipHipHop();
        audioSource.PlayOneShot(hiphop);
    }

    private AudioClip GetRandomClipHipHop() {
        clipsHipHop = new AudioClip [] {
            (AudioClip)Resources.Load("HipHop/HipHop1")
        };
        return clipsHipHop[UnityEngine.Random.Range(0, clipsHipHop.Length)];
        }
}
