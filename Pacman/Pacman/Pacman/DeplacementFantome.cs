using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Pacman
{
    class DeplacementFantome
    {
       // byte[,] map;
       // ObjetAnime Fantome;
       // int VY = 28;

        public DeplacementFantome(GameTime gameTime) { }

        List<string> liberte;
        Stopwatch timer = new Stopwatch();
        public void DeplacerFantome(ObjetAnime Fantome, byte[,] map, int compteur)
        {
          //  this.Fantome = Fantome;
            List<string> liberte = degreDeLiberte(Fantome,map);
            this.liberte = liberte;

            Random rand = new Random();
            int proba;
            string mvt;

            proba = rand.Next(0, liberte.Count);
            mvt = liberte[proba];
          //  Tourner(mvt, Fantome, map);
          //  if (!Tourner(mvt, Fantome, map))
          //  {

                ContrainteDeplacement(Fantome, mvt, map, compteur);
          //  }

        }

        public List<string> degreDeLiberte(ObjetAnime Fantome, byte[,] map)
        {
            List<string> degLiberte = new List<string>();           //Liste string qui permet de connaitre les dégré de liberté du fantome
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

        public void ContrainteDeplacement(ObjetAnime objet, string key, byte[,] map, int compteur)
        {
           //Modification des coordonnées du Fantome
            if (map[objet.coord.X, objet.coord.Y - 1] != 0 && key == "Left")
            {
                do
                {
                    if (compteur > 6)
                        objet.coord.Y = objet.coord.Y - 1;
                } while (map[objet.coord.X, objet.coord.Y - 1] != 0);
            }
            else if (map[objet.coord.X, objet.coord.Y + 1] != 0 && key == "Right")
            {
                do
                {
                    if (compteur > 6)
                        objet.coord.Y = objet.coord.Y + 1;
                } while (map[objet.coord.X, objet.coord.Y + 1] != 0);
            }
            else if (map[objet.coord.X - 1, objet.coord.Y] != 0 && key == "Up")
            {
                do
                {
                    if (compteur > 6)
                        objet.coord.X = objet.coord.X - 1;
                } while (map[objet.coord.X - 1, objet.coord.Y] != 0);
            }
            else if (map[objet.coord.X + 1, objet.coord.Y] != 0 && key == "Down")
            {
               do
                {
                    if (compteur > 6)
                        objet.coord.X = objet.coord.X + 1;
                } while (map[objet.coord.X + 1, objet.coord.Y] != 0);
            }
        }

      /*  public Boolean Tourner(string key, ObjetAnime objet, byte [,] map)
        {
            Boolean tourne = false;                                                  //Va permettre de savoir si le Pacman tourne ou pas
            if (key == "Left")
            {
                if (map[objet.coord.X, objet.coord.Y - 1] != 0)
                {
                    objet.coord.Y--;

                }
                else
                {
                    //ContrainteDeplacement(objet, key, map);
                    //Tourner(key, objet, map);
                     tourne = true;
                }
            }
            else if (key == "Right")
            {
                if (map[objet.coord.X, objet.coord.Y + 1] != 0)
                {
                    objet.coord.Y++;
                    Tourner(key, objet, map);
                    //  tourne = false;
                }
                else
                {
                    //ContrainteDeplacement(objet, key, map);
                     tourne = true;
                }

            }
            else if (key == "Up")
            {
                if (map[objet.coord.X - 1, objet.coord.Y] != 0)
                {
                    objet.coord.X--;
                    Tourner(key, objet, map);
                      tourne = false;
                }
                else
                {
                    //ContrainteDeplacement(objet, key, map);
                       tourne = true;
                }
            }
            else if ( key == "Down")
            {
                if (map[objet.coord.X + 1, objet.coord.Y] != 0)
                {
                    objet.coord.X++;
                    Tourner(key, objet, map);
                     tourne = false;
                }
                else
                {
                    //ContrainteDeplacement(objet, key, map);
                      tourne = true;
                }
            }
            return tourne;
        }*/
    }
}
