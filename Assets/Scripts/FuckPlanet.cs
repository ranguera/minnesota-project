using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class FuckPlanet : MonoBehaviour
{
    // a reference to the action
    public SteamVR_Action_Boolean fuckPlanet;

    // a reference to the hand
    public SteamVR_Input_Sources handType;

    public VFX vfx;

    private bool fucked;

    void Update()
    {
        if (SteamVR_Input.__actions_default_in_GrabPinch.GetStateDown(handType))
        {
            fucked = !fucked;

            if (fucked)
                vfx.UnFuckPlanet();
            else
                vfx.FuckPlanet();
        }
    }
}
