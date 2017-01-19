using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Pacman
{
    class Fantomes : Affichage
    {
        ObjetAnime fantome;

        Texture2D fantomeMangeable;
        Texture2D textureOriginale;

        public Fantomes(ObjetAnime fantome) : base()
        {
            this.fantome = fantome;
        }
        
        
        public void Afficher(ObjetAnime fantome, SpriteBatch spriteBatch, ContentManager Content)
        {
            spriteBatch.Begin();
            for (int x = 0; x < Affichage.VX; x++)
            {
                for (int y = 0; y < Affichage.VY; y++)
                {
                    if (x == fantome.coord.X && y == fantome.coord.Y)
                    {
                        int xpos, ypos;
                        xpos = fantome.coord.X * 20;
                        ypos = fantome.coord.Y * 20;
                        Vector2 pos = new Vector2(ypos, xpos);
                        spriteBatch.Draw(fantome.Texture, pos, Color.White);
                    }
                }
            }
            if(Pacman.fantomeMangeable == true)
            {
                fantomeMangeable = Content.Load<Texture2D>("fan_mangeable");
                fantome.Texture = fantomeMangeable;
            }
            else
            {
                textureOriginale = fantome.Texture;
            }

            if (Pacman.tpsPouvoir.ElapsedMilliseconds > 2000)
            {
                fantome.Texture = textureOriginale;
            }
            spriteBatch.End();
        }

        //Déplacement aléatoire est pas terrible du tout, teste la dans le main, dans la fonction Update
        public void DeplacementAleatoire(ObjetAnime fantome, ObjetAnime pacman, byte [,] map)
        {

            List<string> liberte = degreDeLiberte(fantome, map); //Liste des mouvement possibles pour le fantomes

            Random rand = new Random();
            int proba;
            string mvt;

            proba = rand.Next(0, liberte.Count); //Valeur aleatoire entre 0 et le nbre d'élement dans la liste
            mvt = liberte[proba]; //L'élément choisit est stocké dans mvt
            ContrainteDeplacement(fantome, mvt, map);

    
        }

        public void ContrainteDeplacement(ObjetAnime objet, string key, byte[,] map)
        {
            //Modification des coordonnées du Fantome
            if (key == "Left")
            {
                        objet.coord.Y = objet.coord.Y - 1;
            }
            else if (key == "Right")
            {    
                        objet.coord.Y = objet.coord.Y + 1;
            }
            else if (key == "Up")
            {
                        objet.coord.X = objet.coord.X - 1;
            }
            else if (key == "Down")
            {
                        objet.coord.X = objet.coord.X + 1;
            }
        }

        public List<string> degreDeLiberte(ObjetAnime Fantome, byte[,] map)
        {
            List<string> degLiberte = new List<string>();           //Liste string qui permet de connaitre les dégrés de liberté du fantome
            if (map[Fantome.coord.X + 1, Fantome.coord.Y] != 0)     //C'est à dire les directions où il n'y a pas de mur       
                degLiberte.Add("Down");
            if (map[Fantome.coord.X - 1, Fantome.coord.Y] != 0)
                degLiberte.Add("Up");
            if (map[Fantome.coord.X, Fantome.coord.Y + 1] != 0)
                degLiberte.Add("Right");
            if (map[Fantome.coord.X, Fantome.coord.Y - 1] != 0)
                degLiberte.Add("Left");

            return degLiberte;
        }
    }
}
