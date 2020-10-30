using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseAction : MonoBehaviour
{

    void OnMouseDown()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<OneBox>().slicice_texture[0];
    }
    void OnMouseExit()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<OneBox>().default_texture;
    }
}