using System.Collections;
using System.Collections.Generic;
using System;

public class MineLand
{
    int x, y, broj_mina;
    public int[,] polje;    
    public MineLand(int _x,int _y,int br_mina)
    {
        x = _x;
        y = _y;
        broj_mina = br_mina;
        polje = new int[y,x];
        Random rnd = new Random();

        for(int i=0;i<broj_mina;i++)
        {
            int a, b;
            do
            {
                a = rnd.Next(0, y - 1);
                b = rnd.Next(0, x - 1);
            } while (polje[a, b] == -1);
            polje[a, b] = -1;
        }
        for(int i=0;i<y;i++)
        {
            for (int j = 0; j < x; j++)
            {
                if(polje[i,j]!=-1)
                {
                    int br = 0;
                    if (i != 0)
                    {
                        if (polje[i - 1, j] == -1) br++;
                        if (j > 0)
                        {
                            if (polje[i - 1, j - 1] == -1) br++;
                        }
                        if (j < x - 1)
                        {
                            if (polje[i - 1, j + 1] == -1) br++;
                        }
                    }
                    if (i < y - 1)
                    {
                        if (polje[i + 1, j] == -1) br++;
                        if (j > 0)
                        {
                            if (polje[i + 1, j - 1] == -1) br++;
                        }
                        if (j < x - 1)
                        {
                            if (polje[i + 1, j + 1] == -1) br++;
                        }
                    }
                    if (j > 0)
                    {
                        if (polje[i, j - 1] == -1) br++;
                    }
                    if (j < x - 1)
                    {
                        if (polje[i, j + 1] == -1) br++;
                    }
                    polje[i, j] = br;
                }
            }
        }

    }

}
