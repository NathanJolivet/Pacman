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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Variables Globales
        private Pacman _pacman;
        private FantomeRouge _fantomeRouge;
        private Fantomes _fantomeRose;
        private Fantomes _fantomeOrange;
        private Fantomes _fantomeCyan;
        private Mur _mur;
        private Bean _bean;
        private BeanMagique _beanMagique;
        private SpriteFont _police;

        ObjetAnime mur;
        ObjetAnime bean;
        ObjetAnime pacman;
        ObjetAnime beanMagique;
        ObjetAnime fantomeCyan;
        ObjetAnime fantomeRouge;
        ObjetAnime fantomeOrange;
        ObjetAnime fantomeRose;
        ObjetAnime fantomeMangeable;
        DeplacementFantome mvtAleatoire;


       // Stopwatch tpsVitesseFantome = new Stopwatch();

        public static byte[,] map;
        int VX, VY;

        int compteurVitessePacman = 0;
        int compteurVitesseFantome = 0;
        int compteurVitesseAleatoire = 0;



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            map = new byte[,]{
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 2, 2, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 2, 2, 2, 2, 2, 2, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 2, 2, 2, 2, 2, 2, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 2, 2, 2, 2, 2, 2, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 3, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 3, 0},
            {0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0},
            {0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0},
            {0, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 0},
            {0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
            {0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
            {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        };
            VX = map.GetLength(0); //Donne les lignes
            VY = map.GetLength(1); //Donne les colonnes

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //Initialisation des objets du jeu
            _pacman = new Pacman(pacman, map);
            _fantomeRouge = new FantomeRouge(fantomeRouge);

            _fantomeRose = new Fantomes(fantomeRose);
            _fantomeOrange = new Fantomes(fantomeOrange);
            _fantomeCyan = new Fantomes(fantomeCyan);
            _mur = new Mur(mur);
            _bean = new Bean(bean);
            _beanMagique = new BeanMagique(beanMagique);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //  changing the back buffer size changes the window size (when in windowed mode)
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 660;
            graphics.ApplyChanges();

            //Chargement des objets: Textures dans le dossier 'Content' du projet et coordonnées de départ     
            _police = Content.Load<SpriteFont>("PoliceScore");

            mur = new ObjetAnime(Content.Load<Texture2D>("mur"), new Vector2(0f, 0f), new Vector2(20f, 20f));
            bean = new ObjetAnime(Content.Load<Texture2D>("bean"), new Vector2(0f, 0f), new Vector2(20f, 20f));
            pacman = new ObjetAnime(Content.Load<Texture2D>("pacman"), new Vector2(0f, 0f), new Vector2(20f, 20f), new Coord(23,13));
            beanMagique = new ObjetAnime(Content.Load<Texture2D>("beanMagique"), new Vector2(0f, 0f), new Vector2(20f, 20f));
            fantomeRose = new ObjetAnime(Content.Load<Texture2D>("fantome_Rose"), new Vector2(0f, 0f), new Vector2(20f, 20f),new Coord(14,15));
            fantomeCyan = new ObjetAnime(Content.Load<Texture2D>("fantome_Cyan"), new Vector2(0f, 0f), new Vector2(20f, 20f), new Coord(14,14));
            fantomeRouge = new ObjetAnime(Content.Load<Texture2D>("fantome_Rouge"), new Vector2(0f, 0f), new Vector2(20f, 20f), new Coord(14,13));
            fantomeOrange = new ObjetAnime(Content.Load<Texture2D>("fantome_Orange"), new Vector2(0f, 0f), new Vector2(20f, 20f), new Coord(14,12));
            fantomeMangeable = new ObjetAnime(Content.Load<Texture2D>("fan_mangeable"), new Vector2(0f, 0f), new Vector2(20f, 20f));
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            //La fonction Update() met à jour régulièrement le jeu, donc à partir d'un compteur on gérer les vitesses de déplacement
            //Vitesse et déplacement du Pacman
            if (compteurVitessePacman > 5)
            {
                compteurVitessePacman = 0;
                _pacman.Deplacement(gameTime, Content, spriteBatch);             
            }
            compteurVitessePacman++;

            //Vitesse et déplacement des fantomes
            if (compteurVitesseFantome > 5)
            {
                compteurVitesseFantome = 0;
             //   _fantomeCyan.DeplacementAleatoire(fantomeCyan, pacman, map);// A tester pour comprendre les déplacement aleatoires des fantomes
                fantomeRouge.coord = _fantomeRouge.Dijkstra(pacman, fantomeRouge, map, new Coord (14,13));
            }
            compteurVitesseFantome++;

            //Vitesse et déplacement des fantomes aleatoire


            //Haricot magique
            _pacman.MangerPouvoir(pacman);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {   //Draw permet de "dessiner" les objets qu'on utilisera
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Importe la police d'écriture que j'ai crée (Dossier Content)
            spriteBatch.Begin();
            spriteBatch.DrawString(_police, "SCORE: ", new Vector2(600, 100), Color.White);
            spriteBatch.DrawString(_police, "VIES: ", new Vector2(600, 150), Color.White);
            spriteBatch.DrawString(_police, Pacman.score.ToString(), new Vector2(800, 100), Color.White);
            spriteBatch.End();

            //Affichage de la map
            _bean.AfficherMap(bean, spriteBatch, 1, map);
            _mur.AfficherMap(mur, spriteBatch,0, map);
            _beanMagique.AfficherMap(beanMagique, spriteBatch, 3, map);
            _pacman.Afficher(pacman, spriteBatch);
            _fantomeCyan.Afficher(fantomeCyan, spriteBatch, Content);
            _fantomeOrange.Afficher(fantomeOrange, spriteBatch, Content);
            _fantomeRose.Afficher(fantomeRose, spriteBatch, Content);
            _fantomeRouge.Afficher(fantomeRouge, spriteBatch, Content);

            base.Draw(gameTime);

        }       

     /*   protected void MangerPouvoir(int a, int b) //Faudra essayer de mettre cette fonction dans la classe Pacman
        {
            for (int x = 0; x < VX; x++)
            {
                for (int y = 0; y < VY; y++)
                {
                    if (map[x, y] == 3 && (x == a && y == b))
                    {
                        map[x, y] = 2;

                        fantomeCyan.Texture = fantomeOrange.Texture = fantomeRose.Texture = fantomeRouge.Texture = fantomeMangeable.Texture;

                        _fantomeCyan.Afficher(fantomeCyan, spriteBatch);
                        _fantomeOrange.Afficher(fantomeOrange, spriteBatch);
                        _fantomeRose.Afficher(fantomeRose, spriteBatch);
                        _fantomeRouge.Afficher(fantomeRouge, spriteBatch);
                        tpsPouvoir.Start();
                        Dijkstra.mangeable = true;
                    }
                }
            }
        }*/

        protected void RegenFantome() //Les fantomes retrouve leurs textures d'origine
        {
            fantomeCyan.Texture = Content.Load<Texture2D>("fantome_cyan");
            fantomeOrange.Texture = Content.Load<Texture2D>("fantome_orange");
            fantomeRose.Texture = Content.Load<Texture2D>("fantome_rose");
            fantomeRouge.Texture = Content.Load<Texture2D>("fantome_rouge");
        }

        protected void MangerFantome(ObjetAnime pacman, ObjetAnime fantome) //Déplacer aussi cette fonction dans une classe
        {
            if (Dijkstra.mangeable)
            {
                if(pacman.coord == fantome.coord)
                {
                    fantome.Texture = Content.Load<Texture2D>("FantomePeur1");
                }
            }
        }
    }

}
