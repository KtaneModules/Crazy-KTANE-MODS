using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class Dummy : MonoBehaviour
{
    public KMBombInfo Bomb;
    public KMAudio Audio;

    [SerializeField]
    private KMSelectable solveButton;

    [SerializeField]
    private KMSelectable strikeButton;

    static int ModuleIdCounter = 1;
    int ModuleId;
    private bool ModuleSolved = false;

    void Awake()
    {
        ModuleId = ModuleIdCounter++;
        strikeButton.OnInteract += delegate () { if (!ModuleSolved) { GetComponent<KMBombModule>().HandleStrike(); } return false; };
        solveButton.OnInteract += delegate () { if (!ModuleSolved) { GetComponent<KMBombModule>().HandlePass();  ModuleSolved = true;  } return false; };
    }

    void Start()
    {

    }

    void Update()
    {

    }


#pragma warning disable 414
    private readonly string TwitchHelpMessage = @"Use !{0} to do something.";
#pragma warning restore 414

    IEnumerator ProcessTwitchCommand(string Command)
    {
        yield return null;
    }

    IEnumerator TwitchHandleForcedSolve()
    {
        yield return null;
    }
}
