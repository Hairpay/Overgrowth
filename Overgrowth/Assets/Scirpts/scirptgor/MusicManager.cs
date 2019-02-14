using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    //public int zones;
    public float FadeDuration = 0.01f;
    public AudioClip[] Tracks;
    public List<AudioSource> srcs;
    public List<bool> tracksOn;

    private int i;

	// Use this for initialization
	void Start () {

        foreach (AudioClip trax in Tracks)
        {
            AudioSource a = gameObject.AddComponent<AudioSource>();
            a.clip = trax;
            a.loop = true;
            a.Play();
            srcs.Add(a);

            if(tracksOn[i] == true)
            {
                a.volume = 1.0f;
            }
            else
            {
                a.volume = 0.0f;
            }
            i++;
        }
    }
	
	// Update is called once per frame
	void Update () {

    }

    void Zone(bool[] zone)
    {
        for (int i = 0; i < zone.Length; i++)
        {

            if (zone[i] == true)
            {
                //srcs[i].volume = 1.0f;
                if (tracksOn[i] == true)
                {

                }
                else
                {
                    tracksOn[i] = true;
                    StopCoroutine(VolumeOff(srcs[i]));
                    StartCoroutine(VolumeOn(srcs[i]));

                }

            }
            else if (zone[i] == false)
            {
                //srcs[i].volume = 0.0f;
                if (tracksOn[i] == false)
                {

                }
                else
                {
                    tracksOn[i] = false;
                 //   StopCoroutine(VolumeOn(srcs[i]));
                    StartCoroutine(VolumeOff(srcs[i]));
                }
                
            }

        }
    }

    IEnumerator VolumeOn(AudioSource sr)
    {
        for (float i = 0; i <= 1; i += FadeDuration)
        {
            //sr.volume = Mathf.Lerp(0.0f, 1.0f, i);
            sr.volume = i;
            yield return null;
        }
    }
    IEnumerator VolumeOff(AudioSource sr)
    {
        for (float i = 1; i >= 0; i -= FadeDuration)
        {
            //sr.volume = Mathf.Lerp(1.0f, 0.0f, i);
            sr.volume = i;
            yield return null;
        }
    }
}
