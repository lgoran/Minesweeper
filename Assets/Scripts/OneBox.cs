using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneBox : MonoBehaviour
{
    public int mine_number=0, ex_mine_number , xpos, ypos;
    public Sprite[] slicice_texture;
    public Sprite mine_texture, default_texture;
    public GameObject script_object;
    void Start()
    {
        script_object = GameObject.FindGameObjectWithTag("EditorOnly");
    }
    void OnMouseUpAsButton()
    {
        if(mine_number==-1)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = mine_texture;
            Destroy(gameObject.GetComponent<MouseAction>());
            NewGameScript.LoseEndGame();
        }
        else
        {
            script_object.GetComponent<NewGameScript>().game.OpenAdjacentWhenEmpty(xpos,ypos);
            if (NewGameScript.x * NewGameScript.y == NewGameScript.otvorena_polja + NewGameScript.broj_mina) NewGameScript.uspjeh = true;
            Destroy(gameObject.GetComponent<MouseAction>());
        }
        
    } 
   
}
