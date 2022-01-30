using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMenuSound : MonoBehaviour
{
    AudioSource source;
    void Start()
    {
        TryGetComponent(out source);
        source.Play();
    }
}
