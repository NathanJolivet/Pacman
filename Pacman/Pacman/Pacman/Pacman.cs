using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;
using System.Timers;
using System.Media;

namespace Pacman
{
    class Pacman
    {
        public static int score = 0;
        public static int vies = 3;
        public ObjetAnime pacman;
        byte[,] map;
        string direction;
        SoundEffect sonBean;

        public Pacman ( ObjetAnime pacman, byte[,] map) {
            this.pacman = pacman;
            this.map = map;
          //  this.direction = direction;
        }

        public  void Afficher(ObjetAnime pacman, SpriteBatch spriteBatch)
        {
            this.pacman = pacman;
            spriteBatch.Begin();
            for (int x = 0; x < Affichage.VX; x++)
            {
                for (int y = 0; y < Affichage.VY; y++)
                {
                    if (x == pacman.coord.X && y == pacman.coord.Y)
                    {
                        int xpos, ypos;
                        xpos = pacman.coord.X * 20;
                        ypos = pacman.coord.Y * 20;
                        Vector2 pos = new Vector2(ypos, xpos);
                        spriteBatch.Draw(pacman.Texture, pos, Color.White);
                    }
                }
            }
            spriteBatch.End();
        }

        //Pas très optimisé comme méthode....
        public void Deplacement(GameTime gameTime, ContentManager Content, SpriteBatch spriteBatch)
        {
            KeyboardState keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.Right))
            {
                if (map[pacman.coord.X, pacman.coord.Y + 1] == 0)   //Si le pacman veut se diriger vers un mur,
                {                                                   // la méthode ContrainteDeplacement() ne traite pas ce cas, donc le pacman ne change pas de direction
                    ContrainteDeplacement(direction, spriteBatch);  //La direction ne change pas et le pacman continuer d'avancer tout droit
                }
                else
                {
                    direction = "Right";
                    ContrainteDeplacement(direction, spriteBatch);
                    pacman.Texture = Content.Load<Texture2D>("pacman");
                }
            }
            else if (keyboard.IsKeyDown(Keys.Left))
            {
                if (map[pacman.coord.X, pacman.coord.Y - 1] == 0)
                {
                    ContrainteDeplacement(direction, spriteBatch);
                }
                else
                {
                    direction = "Left";
                    ContrainteDeplacement(direction, spriteBatch);
                    pacman.Texture = Content.Load<Texture2D>("pacmanGauche");
                }
            }
            else if (keyboard.IsKeyDown(Keys.Up))
            {
                if (map[pacman.coord.X - 1, pacman.coord.Y] == 0)
                {
                    ContrainteDeplacement(direction, spriteBatch);
                }
                else
                {
                    direction = "Up";
                    ContrainteDeplacement(direction, spriteBatch);
                    pacman.Texture = Content.Load<Texture2D>("PacmanHaut");
                }
            }
            else if (keyboard.IsKeyDown(Keys.Down))
            {
                if (map[pacman.coord.X + 1, pacman.coord.Y] == 0)
                {
                    ContrainteDeplacement(direction, spriteBatch);
                }
                else
                {
                    direction = "Down";
                    ContrainteDeplacement(direction, spriteBatch);
                    pacman.Texture = Content.Load<Texture2D>("PacmanBas");
                }
            }
            else
            {
                ContrainteDeplacement(direction, spriteBatch);
            }
        }

        public void ContrainteDeplacement(string key, SpriteBatch spriteBatch)
        {
            //Permet de sortir des 2 cotés de la map

            if (key == "Left" && pacman.coord.X == 14 && pacman.coord.Y == 1) //Si le joueur sort par la gauche
            {
                pacman.coord.X = 14; pacman.coord.Y = Affichage.VY - 1; //Il rentre par la droite
                ContrainteDeplacement("Left", spriteBatch);
            }
            //Problème: Quand le pacman sort à droit, il ne rentre pas à gauche sur la bonne case, il entre une case après
            //surrement du à VY-2 qui aurait du être VY-1. Je ne sais pas d'où vient cet erreur
            else if (pacman.coord.X == 14 && pacman.coord.Y == Affichage.VY - 2 && key == "Right")
            {
                pacman.coord.X = 14; pacman.coord.Y = 1;
                ContrainteDeplacement("Right", spriteBatch);
            }


            //Modification des coordonnées du Pacman
            else if (map[pacman.coord.X, pacman.coord.Y - 1] != 0 && key == "Left") //Si le pacman veut tourner à fauche et qu'il n'y a pas de mur...
            {
                pacman.coord.Y = pacman.coord.Y - 1;                                //On modifie ses coordonnées
                Mangerbean(pacman.coord.X, pacman.coord.Y, spriteBatch);            //Et il mange les beans
            }
            else if (map[pacman.coord.X, pacman.coord.Y + 1] != 0 && key == "Right")
            {
                pacman.coord.Y = pacman.coord.Y + 1;
                Mangerbean(pacman.coord.X, pacman.coord.Y, spriteBatch);
            }
            else if (map[pacman.coord.X - 1, pacman.coord.Y] != 0 && key == "Up")
            {
                pacman.coord.X = pacman.coord.X - 1;
                Mangerbean(pacman.coord.X, pacman.coord.Y, spriteBatch);
            }
            else if (map[pacman.coord.X + 1, pacman.coord.Y] != 0 && key == "Down")
            {
                pacman.coord.X = pacman.coord.X + 1;
                Mangerbean(pacman.coord.X, pacman.coord.Y, spriteBatch);
            }
        }

        public void Mangerbean(int a, int b, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            for (int x = 0; x < Affichage.VX; x++)
            {
                for (int y = 0; y < Affichage.VY; y++)
                {
                    if (map[x, y] == 1 && (x == a && y == b))
                    {
                        map[x, y] = 2; //Dans le main, sur le map, les 2 sont des case vides (comme celle de la "maison" des pacman)
                        score += 10;   //Donc on remplace le bean par un fond bleu 
                    }
                }
            }
            spriteBatch.End();

        }


    }
}
