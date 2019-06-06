using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class VFX : MonoBehaviour
{
    public VisualEffect vfx;
    public Renderer earthRenderer;
    
    public Gradient gradient_good = null;
    public Gradient gradient_sick = null;

    public float transition_frames = 250;

    private bool fucked;
    private AudioSource asrc;

    private GradientColorKey[] tempck;
    private Gradient temp;
    private Gradient g1;
    private Gradient g2;
    private Gradient g3;
    private Gradient g4;

    private void Start()
    {
        asrc = GetComponent<AudioSource>();
        GradientSetup();
    }

    public void PlanetTransition(float val)
    {
        vfx.SetFloat("Radius", Mathf.Lerp(1.05f, 1.2f, val));
        vfx.SetFloat("Intensity", Mathf.Lerp(1f, 2f, val));
        vfx.SetFloat("PartSize", Mathf.Lerp(.2f, .4f, val));
        vfx.SetVector3("Size", Vector3.Lerp(Vector3.one * .2f, Vector3.one * .4f, val));

        earthRenderer.material.SetFloat("Vector1_3B5B2C67", Mathf.Lerp(0f, 1f, val));
        asrc.volume = Mathf.Lerp(0f, 1f, val);

        GradientTransition(val);
    }

    private void GradientSetup()
    {
        temp = new Gradient();
        temp.colorKeys = new GradientColorKey[gradient_good.colorKeys.Length];
        temp.alphaKeys = new GradientAlphaKey[gradient_good.colorKeys.Length];

        tempck = new GradientColorKey[gradient_good.colorKeys.Length];

        temp.alphaKeys = gradient_sick.alphaKeys;

        // Create a gradient for each of the color keys to blend the two gradients
        GradientColorKey[] colorKey1 = new GradientColorKey[2];
        colorKey1[0].color = gradient_good.colorKeys[0].color;
        colorKey1[0].time = 0.0f;
        colorKey1[1].color = gradient_sick.colorKeys[0].color;
        colorKey1[1].time = 1f;

        GradientColorKey[] colorKey2 = new GradientColorKey[2];
        colorKey2[0].color = gradient_good.colorKeys[1].color;
        colorKey2[0].time = 0.0f;
        colorKey2[1].color = gradient_sick.colorKeys[1].color;
        colorKey2[1].time = 1f;

        GradientColorKey[] colorKey3 = new GradientColorKey[2];
        colorKey3[0].color = gradient_good.colorKeys[2].color;
        colorKey3[0].time = 0.0f;
        colorKey3[1].color = gradient_sick.colorKeys[2].color;
        colorKey3[1].time = 1f;

        GradientColorKey[] colorKey4 = new GradientColorKey[2];
        colorKey4[0].color = gradient_good.colorKeys[3].color;
        colorKey4[0].time = 0.0f;
        colorKey4[1].color = gradient_sick.colorKeys[3].color;
        colorKey4[1].time = 1f;

        g1 = new Gradient();
        g2 = new Gradient();
        g3 = new Gradient();
        g4 = new Gradient();

        g1.SetKeys(colorKey1, gradient_sick.alphaKeys);
        g2.SetKeys(colorKey2, gradient_sick.alphaKeys);
        g3.SetKeys(colorKey3, gradient_sick.alphaKeys);
        g4.SetKeys(colorKey4, gradient_sick.alphaKeys);
    }

    private void GradientTransition(float val)
    {
        tempck[0].color = g1.Evaluate(val);
        tempck[0].time = gradient_sick.colorKeys[0].time;
        tempck[1].color = g2.Evaluate(val);
        tempck[1].time = gradient_sick.colorKeys[1].time;
        tempck[2].color = g3.Evaluate(val);
        tempck[2].time = gradient_sick.colorKeys[2].time;
        tempck[3].color = g4.Evaluate(val);
        tempck[3].time = gradient_sick.colorKeys[3].time;

        temp.SetKeys(tempck, temp.alphaKeys);

        vfx.SetGradient("Gradient", temp);
    }
}
