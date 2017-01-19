using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    class Mur : Affichage
    {
        ObjetAnime mur;

        public Mur(ObjetAnime mur)
        {
            this.mur = mur;
        }

       /* public void Afficher (ObjetAnime objet, SpriteBatch spriteBatch, byte[,] map)
        {
            spriteBatch.Begin();
            for (int x = 0; x < VX; x++)
            {
                for (int y = 0; y < VY; y++)
                {
                    if (map[x, y] == 0)
                    {
                        int xpos, ypos;
                        xpos = x * 20;
                        ypos = y * 20;
                        Vector2 pos = new Vector2(ypos, xpos);
                        spriteBatch.Draw(objet.Texture, pos, Color.White);
                    }
                }
            }
            spriteBatch.End();
        }*/
    }
}
