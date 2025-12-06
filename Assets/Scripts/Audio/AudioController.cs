/*
 Author: Jacob Wiley
 Date: 12/5/2025
 Description: Manages sound effects and allows other scripts to play them
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    static AudioSource[] sounds;

    // Start is called before the first frame update
    void Start()
    {
        sounds = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal static void PlayMissileExplosion()
    {
        sounds[0].Play();
    }

    internal static void PlayMissileLaunch()
    {
        sounds[1].Play();
    }

    internal static void PlayBullet()
    {
        sounds[2].Play();
    }

    internal static void PlayExplosion()
    {
        sounds[3].Play();
    }

    internal static void PlayDamage()
    {
        sounds[4].Play();
    }
}
