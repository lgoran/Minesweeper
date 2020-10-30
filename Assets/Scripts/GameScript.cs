using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    public MineLand minsko_polje;
    int x, y, broj_mina;
    static GameObject[,] kucice;
    static GameObject kucica;
    public static GameScript CreateComponent(GameObject where, int sirina, int visina, int br_mina, GameObject kuca)
    {
        GameScript myC = where.AddComponent<GameScript>();
        myC.x=sirina;
        myC.y = visina;
        myC.broj_mina = br_mina;
        kucica = kuca;
        return myC;
    }
    // Start is called before the first frame update
    void Start()
    {
        kucice = new GameObject[y,x];
        minsko_polje = new MineLand(x, y, broj_mina);
        for(int i=0;i<x;i++)
        {
            for(int j=0;j<y;j++)
            {
                kucice[j,i] = Instantiate(kucica, new Vector3((-x/2 + i) / 4F,(-y/2 + j) / 4F, 0), Quaternion.identity);
                var skripta_onebox = kucice[j, i].GetComponent<OneBox>();
                skripta_onebox.mine_number = minsko_polje.polje[j,i];
                skripta_onebox.xpos = i;
                skripta_onebox.ypos = j;
                if (minsko_polje.polje[j, i]>=0) skripta_onebox.mine_texture = skripta_onebox.slicice_texture[minsko_polje.polje[j, i]];
                else skripta_onebox.mine_texture = skripta_onebox.slicice_texture[9];
                skripta_onebox.default_texture = kucice[j, i].GetComponent<SpriteRenderer>().sprite;
            }
        }
    }
    public void ShowAll()
    {
        
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                Destroy(kucice[j, i].GetComponent<MouseAction>());
                kucice[j, i].GetComponent<SpriteRenderer>().sprite = kucice[j,i].GetComponent<OneBox>().mine_texture;
            }
        }
    }
    public void DisableAll()
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                Destroy(kucice[j, i].GetComponent<MouseAction>());
                Destroy(kucice[j, i].GetComponent<OneBox>());
            }
        }
    }
    public void DistroyAll()
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                Destroy(kucice[j, i]);
            }
        }
    }


    public void OpenAdjacentWhenEmpty(int i, int j)
    {
        if (minsko_polje.polje[j,i]==0)
        {
            NewGameScript.UvecajOtvorenaPolja();
            Destroy(kucice[j, i].GetComponent<BoxCollider2D>());
            kucice[j,i].GetComponent<SpriteRenderer>().sprite = kucice[j, i].GetComponent<OneBox>().slicice_texture[0];
            minsko_polje.polje[j, i] = 10;
            if (i < x - 1)
            {
                if(j < y - 1)OpenAdjacentWhenEmpty(i + 1, j + 1);
                OpenAdjacentWhenEmpty(i + 1, j);
                if(j > 0)OpenAdjacentWhenEmpty(i + 1, j - 1);
            }
            if (j < y - 1) OpenAdjacentWhenEmpty(i , j + 1);
            if (j > 0) OpenAdjacentWhenEmpty(i , j - 1);
            if (i > 0)
            {
                if (j < y - 1) OpenAdjacentWhenEmpty(i - 1, j + 1);
                OpenAdjacentWhenEmpty(i - 1, j);
                if (j > 0) OpenAdjacentWhenEmpty(i - 1, j - 1);
            }
        }
        if(minsko_polje.polje[j, i] > 0 && minsko_polje.polje[j, i] < 9)
        {
            NewGameScript.UvecajOtvorenaPolja();
            Destroy(kucice[j, i].GetComponent<BoxCollider2D>());
            kucice[j, i].GetComponent<SpriteRenderer>().sprite = kucice[j, i].GetComponent<OneBox>().mine_texture;
            minsko_polje.polje[j, i] = 10;
        }
    }

}
