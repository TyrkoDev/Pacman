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

namespace Pacman
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Pacman : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        //Attributs
        private Carte map;
        private Joueur pacman;
        private Fantome fRouge;
        private Fantome fVert;
        private Fantome fBleu;
        private Fantome fRose;
        private Fantomes listeFantomes;
        private Collision collision;
        private Score score;
        private Niveau niveau;

        public Pacman()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            map = new Carte(Content);
            collision = new Collision(Content);
            listeFantomes = new Fantomes();
            score = Score.instanceScore(Content);
            niveau = Niveau.instanceNiveau(Content);
            pacman = new Joueur(Content, map, collision);
            fRouge = new Fantome(Content, map, collision, "fantomeRouge");
            fVert = new Fantome(Content, map, collision, "fantomeVert");
            fBleu = new Fantome(Content, map, collision, "fantomeBleu");
            fRose = new Fantome(Content, map, collision, "fantomeRose");
            listeFantomes.addFantome(fRouge);
            listeFantomes.addFantome(fVert);
            listeFantomes.addFantome(fBleu);
            listeFantomes.addFantome(fRose);

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
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 620;
            graphics.ApplyChanges();

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

            // TODO: Add your update logic here
            if (pacman.estVivant())
            {
                pacman.update();
                listeFantomes.update();
                collision.update(map, pacman, listeFantomes);
            }

            if (niveau.addNiveau(map))
                nouveauNiveau();

            if (pacman.fini)
            {
                nouveauNiveau();
                reset();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            map.draw(spriteBatch);

            if(!pacman.fini)
                pacman.draw(spriteBatch);

            if(pacman.alive)
                listeFantomes.draw(spriteBatch);

            score.draw(spriteBatch);
            niveau.draw(spriteBatch);

            base.Draw(gameTime);
        }

        protected void nouveauNiveau()
        {
            map = new Carte(Content);
            collision = new Collision(Content);
            listeFantomes = new Fantomes();
            pacman = new Joueur(Content, map, collision);
            fRouge = new Fantome(Content, map, collision, "fantomeRouge");
            fVert = new Fantome(Content, map, collision, "fantomeVert");
            fBleu = new Fantome(Content, map, collision, "fantomeBleu");
            fRose = new Fantome(Content, map, collision, "fantomeRose");
            listeFantomes.addFantome(fRouge);
            listeFantomes.addFantome(fVert);
            listeFantomes.addFantome(fBleu);
            listeFantomes.addFantome(fRose);
        }

        protected void reset()
        {
            niveau.reset();
            score.reset();
        }
    }
}
