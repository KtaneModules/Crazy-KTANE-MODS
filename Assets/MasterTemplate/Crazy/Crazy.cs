using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class Crazy : MonoBehaviour {

   private KMBombInfo Bomb;
   private KMAudio Audio;

    [SerializeField]
    private Material[] ratMaterials;

    [SerializeField]
    private Material[] solveMaterials;

    [SerializeField]
    private MeshRenderer surface;

    [SerializeField]
    private AudioClip crazyAudio;

    [SerializeField]
    private AudioClip testAudio;

    bool debug = false;

    static int ModuleIdCounter = 1;
    int ModuleId;
    private bool ModuleSolved;

    private bool focused = false;
    private int startingTime;
    private int goalTime;

    private float currentTime;
    private bool playingAudio;
    private KMAudio.KMAudioRef audioReference;

   void Awake () 
   {
        Bomb = GetComponent<KMBombInfo>();
        Audio = GetComponent<KMAudio>();
        ModuleId = ModuleIdCounter++;
        GetComponent<KMSelectable>().OnFocus += delegate () { if (ModuleSolved) { return; } Logging("You are with the rats :D"); focused = true; };
        GetComponent<KMSelectable>().OnDefocus += delegate () { if (ModuleSolved) { return; } Logging("You have left the rats :("); Logging($"Your total amount of time with the rats has been {string.Format("{0:0}:{1:00}", (int)currentTime / 60, (int)currentTime % 60)}"); focused = false; };
   }

   void Start () 
   {
        surface.material = ratMaterials[Rnd.Range(0, ratMaterials.Length)];
        startingTime = (int)Bomb.GetTime();
        goalTime = startingTime / 5;
        currentTime = 0;
        Logging($"Be with the rats for {goalTime/60} minutes and {goalTime%60} seconds");
   }

   void Update () 
   {
        float bombTime = Bomb.GetTime();
        bool timeStarted = startingTime > bombTime;

        if (!playingAudio && !ModuleSolved && timeStarted)
        {
            Debug.Log("Audio started playing");
            AudioClip clip = debug ? testAudio : crazyAudio;
            playingAudio = true;
            audioReference = Audio.PlaySoundAtTransformWithRef(clip.name, transform);
        }

        if (!timeStarted || !focused || ModuleSolved)
        {
            return;
        }

        currentTime += Time.deltaTime;

        if (currentTime >= goalTime)
        {
            Logging("The rats are appeased and will let you go");
            surface.material = solveMaterials[Rnd.Range(0, solveMaterials.Length)];
            playingAudio = false;
            ModuleSolved = true;
            audioReference.StopSound();
            GetComponent<KMBombModule>().HandlePass();
        }
   }

    void OnDestroy()
    {
        if (playingAudio)
        {
            audioReference.StopSound();
        }
    }

    void Logging(string log)
    {
        if (log == "")
        {
            return;
        }

        Debug.LogFormat($"[Crazy? #{ModuleId}] {log}");
    }

#pragma warning disable 414
    private readonly string TwitchHelpMessage = @"Use !{0} to do something.";
#pragma warning restore 414

   IEnumerator ProcessTwitchCommand (string Command) {
      yield return null;
   }

   IEnumerator TwitchHandleForcedSolve () {
      yield return null;
   }
}
