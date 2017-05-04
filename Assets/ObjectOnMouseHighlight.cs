using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOnMouseHighlight : MonoBehaviour {

    void OnMouseOver()
    {
        GetComponent<Renderer>().material.shader = Shader.Find("Self-Illumin/Outlined Diffuse");
    }

    void OnMouseExit()
    {
        GetComponent<Renderer>().material.shader = Shader.Find("Diffuse");
    }
}
