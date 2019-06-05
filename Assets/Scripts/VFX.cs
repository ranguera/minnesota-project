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

    public Transform hand;

    private Vector3 handVector;
    private bool handOn;
    private bool fucked;
    private AudioSource asrc;

    private void Start()
    {
        asrc = GetComponent<AudioSource>();
    }

    void Update()
    {
        handVector = hand.position;
        vfx.SetVector3("handPos", hand.position);
        vfx.SetVector3("VFXPos", this.transform.position);

        if (Input.GetKeyDown(KeyCode.Alpha1)) // sick to good
        {
            //UnFuckPlanet();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) // good to sick
        {
            //FuckPlanet();
        }

        if( Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (fucked)
            {
                handOn = !handOn;

                if (handOn)
                    vfx.SetFloat("handInfluence", 1f);
                else
                    vfx.SetFloat("handInfluence", 0f);
            }
        }
    }

    //public void FuckPlanet()
    //{
    //    fucked = true;

    //    StartCoroutine(Vector3Transition(Vector3.one * .2f, Vector3.one * .4f, "Size"));
    //    StartCoroutine(FloatTransition(1.05f, 1.2f, "Radius"));
    //    StartCoroutine(FloatTransition(1f, 2f, "Intensity"));
    //    StartCoroutine(FloatTransition(.2f, .4f, "PartSize"));

    //    StartCoroutine(ShaderTransition(0f, 1f));

    //    StartCoroutine(GradientTransition(true));
    //}

    //public void UnFuckPlanet()
    //{
    //    fucked = false;
    //    vfx.SetFloat("handInfluence", 0f);

    //    StartCoroutine(Vector3Transition(Vector3.one * .4f, Vector3.one * .2f, "Size"));
    //    StartCoroutine(FloatTransition(1.2f, 1.05f, "Radius"));
    //    StartCoroutine(FloatTransition(2f, 1f, "Intensity"));
    //    StartCoroutine(FloatTransition(.4f, .2f, "PartSize"));

    //    StartCoroutine(ShaderTransition(1f, 0f));

    //    StartCoroutine(GradientTransition(false));
    //}

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

    private IEnumerator FloatTransition(float from, float to, string f)
    {
        for (int i = 0; i < transition_frames; i++)
        {
            vfx.SetFloat(f, Mathf.Lerp(from, to, i/ transition_frames));
            yield return null;
        }
    }

    private IEnumerator Vector3Transition(Vector3 from, Vector3 to, string v)
    {
        for (int i = 0; i < transition_frames; i++)
        {
            vfx.SetVector3(v, Vector3.Lerp(from, to, i/ transition_frames));
            yield return null;
        }
    }

    private IEnumerator ShaderTransition(float from, float to)
    {
        for (int i = 0; i < transition_frames; i++)
        {
            earthRenderer.material.SetFloat("Vector1_3B5B2C67", Mathf.Lerp(from, to, i / transition_frames));
            asrc.volume = Mathf.Lerp(from, to, i / transition_frames);
            yield return null;
        }
    }


    private void GradientTransition(float val)
    {
        Gradient temp = new Gradient();
        temp.colorKeys = new GradientColorKey[4];
        temp.alphaKeys = new GradientAlphaKey[4];

        GradientColorKey[] tempck = new GradientColorKey[4];

        Gradient from = null;
        Gradient to = null;

        from = gradient_good;
        to = gradient_sick;

        temp.alphaKeys = gradient_sick.alphaKeys;

        // Create a gradient for each of the color keys to blend the two gradients
        GradientColorKey[] colorKey1 = new GradientColorKey[2];
        colorKey1[0].color = from.colorKeys[0].color;
        colorKey1[0].time = 0.0f;
        colorKey1[1].color = to.colorKeys[0].color;
        colorKey1[1].time = 1f;

        GradientColorKey[] colorKey2 = new GradientColorKey[2];
        colorKey2[0].color = from.colorKeys[1].color;
        colorKey2[0].time = 0.0f;
        colorKey2[1].color = to.colorKeys[1].color;
        colorKey2[1].time = 1f;

        GradientColorKey[] colorKey3 = new GradientColorKey[2];
        colorKey3[0].color = from.colorKeys[2].color;
        colorKey3[0].time = 0.0f;
        colorKey3[1].color = to.colorKeys[2].color;
        colorKey3[1].time = 1f;

        GradientColorKey[] colorKey4 = new GradientColorKey[2];
        colorKey4[0].color = from.colorKeys[3].color;
        colorKey4[0].time = 0.0f;
        colorKey4[1].color = to.colorKeys[3].color;
        colorKey4[1].time = 1f;

        Gradient g1 = new Gradient();
        Gradient g2 = new Gradient();
        Gradient g3 = new Gradient();
        Gradient g4 = new Gradient();

        g1.SetKeys(colorKey1, to.alphaKeys);
        g2.SetKeys(colorKey2, to.alphaKeys);
        g3.SetKeys(colorKey3, to.alphaKeys);
        g4.SetKeys(colorKey4, to.alphaKeys);

        tempck[0].color = g1.Evaluate(val);
        tempck[0].time = to.colorKeys[0].time;
        tempck[1].color = g2.Evaluate(val);
        tempck[1].time = to.colorKeys[1].time;
        tempck[2].color = g3.Evaluate(val);
        tempck[2].time = to.colorKeys[2].time;
        tempck[3].color = g4.Evaluate(val);
        tempck[3].time = to.colorKeys[3].time;

        temp.SetKeys(tempck, temp.alphaKeys);

        vfx.SetGradient("Gradient", temp);

        // Evaluate each of the colorkey gradients, create a new temp gradient and assign to vfx
        //for (int i = 0; i < transition_frames; i++)
        //{
            

        //    yield return null;
        //}
    }
}
