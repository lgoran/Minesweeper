using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGameScript : MonoBehaviour
{    
    public GameScript game;
    private readonly int x_max = 40, y_max = 20;
    public static int x, y, broj_mina;
    public static bool uspjeh = false;
    public GameObject sirina_text, visina_text, mine_text;
    public Text preostalo_polja;
    public GameObject kucica, time_text;
    public static int otvorena_polja = 0;
    private double start_time;
    public GameObject end_text;
    private double[] easy,medium,hard;
    private enum Tezina {custom_level=0,easy_level=1,medium_level=2,hard_level=3};
    private Tezina tezina;
    public GameObject highscore_text;

    // Start is called before the first frame update
    void Start()
    {
        x = y = broj_mina = 0;
        start_time = 0;
        time_text.SetActive(false);
        easy = new double[5] { 0, 0, 0, 0, 0 };
        medium = new double[5] { 0, 0, 0, 0, 0 };
        hard = new double[5] { 0, 0, 0, 0, 0 };
        LoadHighScores();
        WriteHighscoresToCanvas();
    }

    // Update is called once per frame
    void Update()
    {
        if(otvorena_polja>=0)
        preostalo_polja.text = "Preostalo dobrih polja: " + (x * y - otvorena_polja - broj_mina).ToString();
        if (uspjeh == true )
        {
            end_text.GetComponent<Text>().text = "Victory";
            end_text.SetActive(true);
            CheckAndAddHighscore(Time.realtimeSinceStartup - start_time);//fix
            SaveHighScores();
            WriteHighscoresToCanvas();
            uspjeh = false;
            this.enabled = false;
        }
        else if(uspjeh==false && x!=0 && y!=0 && otvorena_polja == x * y - broj_mina)
        {
            end_text.GetComponent<Text>().text = "Game Over";
            end_text.SetActive(true);
            game.ShowAll();
            game.DisableAll();
            this.enabled = false;
        }
        if (time_text.activeSelf==true && x*y-broj_mina-otvorena_polja!=0)
        {            
            time_text.GetComponent<Text>().text = (Time.realtimeSinceStartup - start_time).ToString("0.###");
        }
        
    }
    public void OnNewGameButtonPressed()
    {
        if(game!=null)
        {
            game.DistroyAll();
            Destroy(game);
        }
        
        SetCustomStartGameStuff();
        game =GameScript.CreateComponent(gameObject ,x, y, broj_mina, kucica);
        this.enabled = true;
        
    }
    public void OnEasyLevelButtonPressed()
    {
        if (game != null)
        {
            game.DistroyAll();
            Destroy(game);
        }
        x = y = 9;
        broj_mina = 10;
        SetLevelStartGameStuff();
    }
    public void OnMediumLevelButtonPressed()
    {
        if (game != null)
        {
            game.DistroyAll();
            Destroy(game);
        }
        x = y = 16;
        broj_mina = 40;
        SetLevelStartGameStuff();
    }
    public void OnHardLevelButtonPressed()
    {
        if (game != null)
        {
            game.DistroyAll();
            Destroy(game);
        }        
        x = 30;
        y = 16;
        broj_mina = 99;
        SetLevelStartGameStuff();
    }
    public static void UvecajOtvorenaPolja()
    {
        otvorena_polja++;
    }
    public static void LoseEndGame()
    {
        otvorena_polja = x * y - broj_mina;
        uspjeh = false;
    }
    private void SetLevelStartGameStuff()
    {
        start_time = Time.realtimeSinceStartup;
        time_text.SetActive(true);
        end_text.SetActive(false);
        otvorena_polja = 0;

        SetTezina();
        game = GameScript.CreateComponent(gameObject, x, y, broj_mina, kucica);
        this.enabled = true;
    }
    private void SetCustomStartGameStuff()
    {
        if (sirina_text.GetComponent<Text>().text.Length == 0 || visina_text.GetComponent<Text>().text.Length == 0 || mine_text.GetComponent<Text>().text.Length == 0) return;
        for (int i = 0; i < sirina_text.GetComponent<Text>().text.Length; i++)
        {
            if (sirina_text.GetComponent<Text>().text[i] < '0' || sirina_text.GetComponent<Text>().text[i] > '9') return;
        }
        for (int i = 0; i < visina_text.GetComponent<Text>().text.Length; i++)
        {
            if (visina_text.GetComponent<Text>().text[i] < '0' || visina_text.GetComponent<Text>().text[i] > '9') return;
        }
        for (int i = 0; i < mine_text.GetComponent<Text>().text.Length; i++)
        {
            if (mine_text.GetComponent<Text>().text[i] < '0' || mine_text.GetComponent<Text>().text[i] > '9') return;
        }

        x = int.Parse(sirina_text.GetComponent<Text>().text);
        y = int.Parse(visina_text.GetComponent<Text>().text);
        broj_mina = int.Parse(mine_text.GetComponent<Text>().text);

        if (x > x_max) x = x_max;
        if (x < 1) x = 1;
        if (y > y_max) y = y_max;
        if (y < 1) y = 1;
        if (broj_mina < 1) broj_mina = 1;
        if (broj_mina > x * y / 2) broj_mina = x * y / 2;
        sirina_text.GetComponent<Text>().text = x.ToString();
        visina_text.GetComponent<Text>().text = y.ToString();
        mine_text.GetComponent<Text>().text = broj_mina.ToString();

        start_time = Time.realtimeSinceStartup;
        time_text.SetActive(true);
        end_text.SetActive(false);
        otvorena_polja = 0;

        SetTezina();
    }
    private void CheckAndAddHighscore(double time)
    {
        //dovrsi fju i jos napravit ispisivanje texta na ekran
        if (tezina == Tezina.custom_level) return;
        else if(tezina == Tezina.easy_level)
        {
            for(int i=0;i<easy.Length;i++)
            {
                if(easy[i]==0)
                {
                    easy[i] = time;
                    break;
                }
                if(time<easy[i])
                {
                    for(int j=easy.Length-1;j>i;j--)
                    {
                        easy[j] = easy[j - 1];
                    }
                    easy[i] = time;
                    break;
                }
            }
        }
        else if (tezina == Tezina.medium_level)
        {
            for (int i = 0; i < medium.Length; i++)
            {
                if (medium[i] == 0)
                {
                    medium[i] = time;
                    break;
                }
                if (time < easy[i])
                {
                    for (int j = medium.Length - 1; j > i; j--)
                    {
                        medium[j] = medium[j - 1];
                    }
                    medium[i] = time;
                    break;
                }
            }
        }
        else if (tezina == Tezina.hard_level)
        {
            for (int i = 0; i < hard.Length; i++)
            {
                if (hard[i] == 0)
                {
                    hard[i] = time;
                    break;
                }
                if (time < hard[i])
                {
                    for (int j = hard.Length - 1; j > i; j--)
                    {
                        hard[j] = hard[j - 1];
                    }
                    hard[i] = time;
                    break;
                }
            }
        }
    }
    public void SaveHighScores()
    {
        SaveSystem.SaveHighScores(easy, medium, hard);
    }
    public void LoadHighScores()
    {
        HighScoresData data = SaveSystem.LoadHighScores();
        if (data == null)
        {
            SaveHighScores();
            data = SaveSystem.LoadHighScores();
        }
        easy = data.easy;
        medium = data.medium;
        hard = data.hard;
    }
    private void WriteHighscoresToCanvas()
    {
        highscore_text.GetComponent<Text>().text = "Easy Level:\n";
        for(int i=0;i<easy.Length;i++)
        {
            highscore_text.GetComponent<Text>().text += easy[i].ToString() + "\n";
        }
        highscore_text.GetComponent<Text>().text += "\nMedium Level:\n";
        for (int i = 0; i < medium.Length; i++)
        {
            highscore_text.GetComponent<Text>().text += medium[i].ToString() + "\n";
        }
        highscore_text.GetComponent<Text>().text += "\nHard Level:\n";
        for (int i = 0; i < hard.Length; i++)
        {
            highscore_text.GetComponent<Text>().text += hard[i].ToString() + "\n";
        }
    }
    private void SetTezina()
    {
        if (x == 9 && y == 9 && broj_mina == 10) tezina = Tezina.easy_level;
        else if (x == 16 && y == 16 && broj_mina == 40) tezina = Tezina.medium_level;
        else if (x == 30 && y == 16 && broj_mina == 99) tezina = Tezina.hard_level;
        else tezina = Tezina.custom_level;
    }
}
