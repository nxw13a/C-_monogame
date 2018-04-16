using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game3
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Texture2D rect;
        private int position_X;
        private int position_Y;
        private float timer = 5;
        private bool gameover = false;
        private Texture2D heart;
        private bool change_color = false;
        private Color absolute_color = Color.Yellow;
        private float TIMER = 5;
        private float absolute = 2;
        private Color heart_color = Color.TransparentBlack;
        private List<Texture2D> keys = new List<Texture2D>();
        private List<int> random_keys_X = new List<int>();
        private List<int> random_keys_Y = new List<int>();
        private int size = 200;
        private int ghost_key;
        KeyboardState OldKeyState;
        public int RandomNumber(int min, int max)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            return random.Next(min, max);
        }
        public bool DoOverlap(int r1x, int r1y, int r2x, int r2y, int l1x, int l1y, int l2x, int l2y)
        {
            if (l1x > r2x || l2x > r1x)
                return false;
            if (l1y < r2y || l2y < r1y)
                return false;
            return true;
        }
        public int pick()
        {
            if (RandomNumber(0, 100) % 2 == 0)
                return -1;
            return 1;
        }
        public int Pick_N()
        {
            if (RandomNumber(0, 100) % 2 == 0)
                return 0;
            return 1;
        }
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
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
            rect = new Texture2D(graphics.GraphicsDevice, GraphicsDevice.Viewport.Bounds.Width / 14, GraphicsDevice.Viewport.Bounds.Width / 14);
            heart = new Texture2D(graphics.GraphicsDevice, GraphicsDevice.Viewport.Bounds.Width / 14, GraphicsDevice.Viewport.Bounds.Width / 14);
            position_X = GraphicsDevice.Viewport.Bounds.Width / 2;
            position_Y = GraphicsDevice.Viewport.Bounds.Height / 2;
            ghost_key = size - 1;
            Color[] data = new Color[rect.Width * rect.Height];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.White;
            rect.SetData(data);
            heart.SetData(data);
            //ghost_key = RandomNumber(0, size);
            for(int x = 0; x < size; x++)
            {
                keys.Add(new Texture2D(GraphicsDevice, 1, 1));
                random_keys_X.Add(RandomNumber(200, 1500) * pick());
                random_keys_Y.Add(RandomNumber(200, 1500) * pick());
            }
            for(int x = 0; x < keys.Count; x++)
            {
                keys[x].SetData(new[] { Color.Yellow });
            }
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();
            int rate = 15;
            KeyboardState newKeyBoardState = Keyboard.GetState();
            // TODO: Add your update logic here
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer -= elapsed;
            //distance += pace * (negative);
            

            if (newKeyBoardState.IsKeyDown(Keys.A) && OldKeyState.IsKeyUp(Keys.A))
            {
               // Debug.WriteLine("LEFT");
                position_X += rate;
               // Debug.WriteLine((position_X + random_keys_X[ghost_key]) + " " + (position_Y + random_keys_Y[ghost_key]));
               // Debug.WriteLine(GraphicsDevice.Viewport.Bounds.Width / 2 + " " + GraphicsDevice.Viewport.Bounds.Height / 2);

            }
            else if(newKeyBoardState.IsKeyDown(Keys.W) && OldKeyState.IsKeyUp(Keys.W))
            {
               // Debug.WriteLine("UP");
                position_Y += rate;
                Debug.WriteLine((Math.Abs(Math.Abs(position_Y + random_keys_Y[ghost_key]) - Math.Abs(GraphicsDevice.Viewport.Bounds.Height / 2))));
                Debug.WriteLine((Math.Abs(Math.Abs(position_X + random_keys_X[ghost_key]) - Math.Abs(GraphicsDevice.Viewport.Bounds.Width / 2))));
                Debug.WriteLine("");
                // Debug.WriteLine((position_X + random_keys_X[ghost_key]) + " " + (position_Y + random_keys_Y[ghost_key]));
                // Debug.WriteLine(GraphicsDevice.Viewport.Bounds.Width / 2 + " " + GraphicsDevice.Viewport.Bounds.Height / 2);

            }
            else if(newKeyBoardState.IsKeyDown(Keys.S) && OldKeyState.IsKeyUp(Keys.S)) 
            {
                //Debug.WriteLine("DOWN");
                position_Y -= rate;
                //Debug.WriteLine((position_X + random_keys_X[ghost_key]) + " " + (position_Y + random_keys_Y[ghost_key]));
               // Debug.WriteLine(GraphicsDevice.Viewport.Bounds.Width / 2 + " " + GraphicsDevice.Viewport.Bounds.Height / 2);

            }
            else if(newKeyBoardState.IsKeyDown(Keys.D) && OldKeyState.IsKeyUp(Keys.D))
            {
                //Debug.WriteLine("RIGHT");
                position_X -= rate;
               // Debug.WriteLine((position_X + random_keys_X[ghost_key]) + " " + (position_Y + random_keys_Y[ghost_key]));
                //Debug.WriteLine(GraphicsDevice.Viewport.Bounds.Width / 2 + " " + GraphicsDevice.Viewport.Bounds.Height / 2);

            }
            
            if ((Math.Abs(Math.Abs(position_X + random_keys_X[ghost_key]) - Math.Abs(GraphicsDevice.Viewport.Bounds.Width / 2)) < 2000) && (Math.Abs(Math.Abs(position_X + random_keys_X[ghost_key]) - Math.Abs(GraphicsDevice.Viewport.Bounds.Width / 2)) > 1000))
            {
                if ((Math.Abs(Math.Abs(position_Y + random_keys_Y[ghost_key]) - Math.Abs(GraphicsDevice.Viewport.Bounds.Height / 2)) < 2000) && (Math.Abs(Math.Abs(position_Y + random_keys_Y[ghost_key]) - Math.Abs(GraphicsDevice.Viewport.Bounds.Height / 2)) > 1000))
                {
                    TIMER = 3.5f;
                    //Debug.WriteLine((Math.Abs(Math.Abs(position_Y + random_keys_Y[ghost_key]) - Math.Abs(GraphicsDevice.Viewport.Bounds.Height / 2))));
                    absolute = 1.5f;
                    absolute_color = Color.Yellow;
                    //Debug.WriteLine("OUTSIDE3");
                }
                else
                {
                    TIMER = 3.5f;
                    //Debug.WriteLine((Math.Abs(Math.Abs(position_Y + random_keys_Y[ghost_key]) - Math.Abs(GraphicsDevice.Viewport.Bounds.Height / 2))));
                    absolute = 1.5f;
                    absolute_color = Color.Yellow;
                    //Debug.WriteLine("OUTSIDE3");
                }

            }
            
            else if ((Math.Abs(Math.Abs(position_X + random_keys_X[ghost_key]) - Math.Abs(GraphicsDevice.Viewport.Bounds.Width / 2)) < 1000) && (Math.Abs(Math.Abs(position_X + random_keys_X[ghost_key]) - Math.Abs(GraphicsDevice.Viewport.Bounds.Width / 2)) > 500))
            {
                if ((Math.Abs(Math.Abs(position_Y + random_keys_Y[ghost_key]) - Math.Abs(GraphicsDevice.Viewport.Bounds.Height / 2)) < 1000) && (Math.Abs(Math.Abs(position_Y + random_keys_Y[ghost_key]) - Math.Abs(GraphicsDevice.Viewport.Bounds.Height / 2)) > 500))
                {
                    //Debug.WriteLine((Math.Abs(Math.Abs(position_Y + random_keys_Y[ghost_key]) - Math.Abs(GraphicsDevice.Viewport.Bounds.Height / 2))));
                    TIMER = 2.0f;
                    Debug.WriteLine("OUTSIDE2");
                    absolute = 0.5f;
                    absolute_color = Color.OrangeRed;
                }
                else
                {
                    TIMER = 3.5f;
                    //Debug.WriteLine((Math.Abs(Math.Abs(position_Y + random_keys_Y[ghost_key]) - Math.Abs(GraphicsDevice.Viewport.Bounds.Height / 2))));
                    absolute = 1.5f;
                    absolute_color = Color.Yellow;
                    //Debug.WriteLine("OUTSIDE3");
                }

            }
            else if ((Math.Abs(Math.Abs(position_X + random_keys_X[ghost_key]) - Math.Abs(GraphicsDevice.Viewport.Bounds.Width / 2)) < 500) && (Math.Abs(Math.Abs(position_X + random_keys_X[ghost_key]) - Math.Abs(GraphicsDevice.Viewport.Bounds.Width / 2)) > 0))
            {
                if ((Math.Abs(Math.Abs(position_Y + random_keys_Y[ghost_key]) - Math.Abs(GraphicsDevice.Viewport.Bounds.Height / 2)) < 500) && (Math.Abs(Math.Abs(position_Y + random_keys_Y[ghost_key]) - Math.Abs(GraphicsDevice.Viewport.Bounds.Height / 2)) > 0))
                {
                    //Debug.WriteLine((Math.Abs(Math.Abs(position_Y + random_keys_Y[ghost_key]) - Math.Abs(GraphicsDevice.Viewport.Bounds.Height / 2))));
                    TIMER = 1f;
                    absolute = 0.25f;
                    absolute_color = Color.Red;
                    //Debug.WriteLine("OUTSIDE1");
                }
                else
                {
                    TIMER = 3.5f;
                    //Debug.WriteLine((Math.Abs(Math.Abs(position_Y + random_keys_Y[ghost_key]) - Math.Abs(GraphicsDevice.Viewport.Bounds.Height / 2))));
                    absolute = 1.5f;
                    absolute_color = Color.Yellow;
                    //Debug.WriteLine("OUTSIDE3");
                }

            }
            else
            {
                TIMER = 3.5f;
                //Debug.WriteLine((Math.Abs(Math.Abs(position_Y + random_keys_Y[ghost_key]) - Math.Abs(GraphicsDevice.Viewport.Bounds.Height / 2))));
                absolute = 1.5f;
                absolute_color = Color.Yellow;
                //Debug.WriteLine("OUTSIDE3");
            }
            
            
            if (Math.Abs(Math.Abs(position_X + random_keys_X[ghost_key]) - Math.Abs(GraphicsDevice.Viewport.Bounds.Width / 2)) < 30)
            {
                if (Math.Abs(Math.Abs(position_Y + random_keys_Y[ghost_key]) - Math.Abs(GraphicsDevice.Viewport.Bounds.Height / 2)) < 30)
                {
                    Debug.WriteLine("HITTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT");
                    //gameover = true;
                }
            }
            if (timer < 0)
            {
                timer = absolute;
                if(!change_color)
                {
                    change_color = true;
                    heart_color = absolute_color;
                }
                else
                {
                    change_color = false;
                    heart_color = Color.TransparentBlack;
                    timer = TIMER;
                }
            }
            OldKeyState = newKeyBoardState;
            base.Update(gameTime);
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            //Texture2D rect = new Texture2D(graphics.GraphicsDevice, 100, 100);
            var textureCenter = new Vector2(rect.Width / 2, rect.Height / 2);
            var screenCenter = new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2, GraphicsDevice.Viewport.Bounds.Height / 2);
            var textureCenter3 = new Vector2(heart.Width / 2, heart.Height / 2);
            var screenCenter3 = new Vector2(GraphicsDevice.Viewport.Bounds.Width, GraphicsDevice.Viewport.Bounds.Height);
            spriteBatch.Begin();



            if (!gameover)
            {
                //Vector2 coor = new Vector2(10, 20);
                //spriteBatch.Draw(rect, coor, Color.White);
                for (int x = 0; x < size; x++)
                {
                    int n = Pick_N();
                    int t = 0;
                    if (n == 0)
                        t = 1;

                    var screenCenter2 = new Vector2(position_X + random_keys_X[x], position_Y + random_keys_Y[x]);
                    if (ghost_key == x)
                    {
                        spriteBatch.Draw(keys[x], screenCenter2, null, Color.Red, 0f, Vector2.Zero, textureCenter, SpriteEffects.None, 1f);

                    }
                    else
                        spriteBatch.Draw(keys[x], screenCenter2, null, Color.Yellow, 0f, Vector2.Zero, textureCenter, SpriteEffects.None, 1f);
                }
                spriteBatch.Draw(rect, screenCenter, null, Color.White, 0f, textureCenter, 1f, SpriteEffects.None, 1f);
                spriteBatch.Draw(heart, new Vector2(GraphicsDevice.Viewport.Bounds.Width - heart.Width / 2, GraphicsDevice.Viewport.Bounds.Height - heart.Height / 2), null, heart_color, 0f, textureCenter3, 1f, SpriteEffects.None, 1f);
                spriteBatch.Draw(heart, new Vector2(0 + heart.Width / 2, GraphicsDevice.Viewport.Bounds.Height - heart.Height / 2), null, heart_color, 0f, textureCenter3, 1f, SpriteEffects.None, 1f);
                spriteBatch.Draw(heart, new Vector2(0 + heart.Width / 2, 0 + heart.Height / 2), null, heart_color, 0f, textureCenter3, 1f, SpriteEffects.None, 1f);
                spriteBatch.Draw(heart, new Vector2(GraphicsDevice.Viewport.Bounds.Width - heart.Width / 2, 0 + heart.Height / 2), null, heart_color, 0f, textureCenter3, 1f, SpriteEffects.None, 1f);
                
                // TODO: Add your drawing code here
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
