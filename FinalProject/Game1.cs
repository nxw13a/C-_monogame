using System;
using System.Collections.Generic;
using System.Diagnostics;
//using Android.Media;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using OurGame;

namespace FinalProject
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private OurCharacter ourCharacter;
        private Rectangle char_rect;
        private int boundary_up = -2000;
        private int boundary_down = 2000;
        private int boundary_left = -2000;
        private int boundary_right = 2000;

        private Texture2D texture;
        private Texture2D up_button;
        private Texture2D down_button;
        private Texture2D left_button;
        private Texture2D right_button;
        private Texture2D inter_button;
        private Texture2D fog;
        private Texture2D torch;
        private Texture2D stage;
        private Texture2D background;
        private Texture2D ghost;
        private Texture2D chest;
        private Rectangle chest_rect;

        private List<Texture2D> chest_list = new List<Texture2D>();
        private List<Rectangle> chestlist_rect = new List<Rectangle>();
        private Texture2D message;

        private Rectangle up_rect;
        private Rectangle left_rect;
        private Rectangle right_rect;
        private Rectangle down_rect;
        private Rectangle inter_rect;
        private string position;

        private int DIRECTX = 0;
        private int DIRECTY = 0;
        private int rate = 5;

        private int x = 0;
        private int y = 0;

        private SoundEffect effect;
        private SoundEffectInstance soundEffectInstance;
        private SoundEffect effect2;
        private SoundEffectInstance soundEffectInstance2;
        private SoundEffect effect3;
        private SoundEffectInstance soundEffectInstance3;
        private Song song;
        private Boolean GameDec = false;
        private Boolean transition = false;
        private Boolean GameEnd = false;

        private SpriteFont text;

        private String first = "Beware of Her...";
        private String second = "Stay Still...";
        private String press = "Touch Any Where to Start the Game...";

        private float clock = 5;
        private float clock2 = 5;
        private float timer = 5;
        private float timer2 = 3;
        private float timer3 = 2;
        private float ghost_timer;
        private Boolean ghost_appear = false;
        private float song_time = 20;
        public int RandomNumber(int min, int max)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            return random.Next(min, max);
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
        protected bool Tilemap_collision(Rectangle a, Rectangle b)
        {
            var x_overlaps = (a.Left < b.Right - 20) && (a.Right > b.Left + 20);
            var y_overlaps = (a.Top < b.Bottom - 10) && (a.Bottom > b.Top + 40);
            //Debug.WriteLine("chest "+ a.Top);
           // Debug.WriteLine("char " + b.Bottom);
            return x_overlaps && y_overlaps;
        }
        protected bool Overlap(Rectangle character, string side)
        {
            if(Tilemap_collision(chest_rect,character))
            {
                if(side == "left" && character.Left < chest_rect.Left)
                {
                    return false;
                }
                else if (side == "right" && character.Right > chest_rect.Right)
                {
                    return false;
                }
                else if (side == "up" && character.Top < chest_rect.Top)
                {
                    return false;
                }
                else if (side == "down" && character.Bottom > chest_rect.Bottom)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        /// 
        protected void Game()
        {
            DIRECTX = 0;
            DIRECTY = 0;
            clock = 5;
            clock2 = 5;
            boundary_up = -2000;
            boundary_down = 2000;
            boundary_left = -2000;
            boundary_right = 2000;
            ghost_timer = RandomNumber(10, 30);
            song_time = 20;
            var height_view = GraphicsDevice.Viewport.Bounds.Height - 280;
            var width_view = 20;
            var width_view2 = GraphicsDevice.Viewport.Bounds.Width - 280;
            fog = Content.Load<Texture2D>("fog");
            torch = Content.Load<Texture2D>("torchlight");

            spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = Content.Load<Texture2D>("main_character/stand");

            up_button = Content.Load<Texture2D>("button2");
            up_rect = new Rectangle(70 + width_view, 0 + height_view, 70, 70);

            down_button = Content.Load<Texture2D>("button2");
            down_rect = new Rectangle(70 + width_view, 140 + height_view, 70, 70);

            left_button = Content.Load<Texture2D>("button2");
            left_rect = new Rectangle(0 + width_view, 70 + height_view, 70, 70);

            right_button = Content.Load<Texture2D>("button2");
            right_rect = new Rectangle(140 + width_view, 70 + height_view, 70, 70);

            ghost = Content.Load<Texture2D>("ghost");

            inter_button = Content.Load<Texture2D>("button2");
            inter_rect = new Rectangle(140 + width_view2, 70 + height_view, 70, 70);
            //ghost_song = Content.Load<Song>("ghost_song");
            chest = Content.Load<Texture2D>("chest");
            x = RandomNumber(-1500, 1500);
            y = RandomNumber(-1500, 1500);
            chest_rect = new Rectangle(x + DIRECTX, y + DIRECTY, 100, 100);
            ourCharacter = new OurCharacter(texture, 1, 1);
            char_rect = new Rectangle((GraphicsDevice.Viewport.Bounds.Width / 2) - (75 / 2), (GraphicsDevice.Viewport.Bounds.Height / 2) - (80 / 2), 75, 80);
            position = "stand";
            stage = Content.Load<Texture2D>("black2");
            effect = Content.Load<SoundEffect>("footsteps");
            soundEffectInstance = effect.CreateInstance();
            effect2 = Content.Load<SoundEffect>("ghost_song");
            soundEffectInstance2 = effect2.CreateInstance();

            effect3 = Content.Load<SoundEffect>("laugh");
            soundEffectInstance3 = effect3.CreateInstance();

            soundEffectInstance.Volume = 0.1f;
        }
        protected void First_Scene()
        {
            clock2 = 5;
            first = "Beware of Her...";
            second = "Stay Still...";
            press = "Touch Any Where to Start the Game...";
            timer = 5;
            timer2 = 3;
            timer3 = 2;
            text = Content.Load<SpriteFont>("test");
            song = Content.Load<Song>("song");
            background = Content.Load<Texture2D>("creepy_background");
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;
            Game();
        }
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            First_Scene();
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
        protected void UPDATE_chest()
        {
            chest_rect = new Rectangle(x + DIRECTX, y + DIRECTY, 100, 100);
            Debug.WriteLine((x + DIRECTX) + " " + (y + DIRECTY));
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
            TouchCollection touchCollection = TouchPanel.GetState();

            // TODO: Add your update logic here
            var newKeyBoardState = Keyboard.GetState();
            if (transition)
            { 
                if (!GameEnd)
                {
                    float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                    ghost_timer -= elapsed;

                    if (newKeyBoardState.IsKeyDown(Keys.A) || CheckRectangleTouch(left_rect, touchCollection))
                    {

                        if (position != "left")
                        {
                            texture = Content.Load<Texture2D>("main_character/left");
                            ourCharacter = new OurCharacter(texture, 1, 3);
                            position = "left";
                            //Debug.WriteLine("OUT");
                        }
                        if (boundary_left == 0)
                        {
                            Debug.WriteLine("STOPLEFT");
                        }
                        else
                        {
                            boundary_left += rate;
                            boundary_right += rate;
                            if (!Overlap(char_rect, "left"))
                            {
                                DIRECTX += rate;
                            }
                            else
                            {
                                DIRECTX -= rate;
                            }


                        }
                        soundEffectInstance.Play();
                        left_button = Content.Load<Texture2D>("button");
                        ourCharacter.Update(gameTime);
                    }
                    else if (newKeyBoardState.IsKeyDown(Keys.D) || CheckRectangleTouch(right_rect, touchCollection))
                    {
                        if (position != "right")
                        {
                            texture = Content.Load<Texture2D>("main_character/right");
                            ourCharacter = new OurCharacter(texture, 1, 3);
                            position = "right";
                            //Debug.WriteLine("OUT" + count++);
                        }
                        if (boundary_right == 0)
                        {
                            Debug.WriteLine("STOPRIGHT");
                        }
                        else
                        {
                            //Debug.WriteLine(boundary_right);
                            boundary_right -= rate;
                            boundary_left -= rate;
                            //DIRECTX -= rate;
                            if (!Overlap(char_rect, "right"))
                            {
                                DIRECTX -= rate;
                            }
                            else
                            {
                                DIRECTX += rate;
                            }
                        }
                        soundEffectInstance.Play();
                        right_button = Content.Load<Texture2D>("button");
                        ourCharacter.Update(gameTime);
                    }
                    else if (newKeyBoardState.IsKeyDown(Keys.S) || CheckRectangleTouch(down_rect, touchCollection))
                    {
                        if (position != "down")
                        {
                            texture = Content.Load<Texture2D>("main_character/backward");
                            ourCharacter = new OurCharacter(texture, 1, 3);
                            position = "down";

                            //Debug.WriteLine("OUT" + count++);
                        }
                        if (boundary_down == 0)
                        {
                            Debug.WriteLine("STOPDOWN");
                        }
                        else
                        {
                            //Debug.WriteLine(boundary_down);
                            boundary_down -= rate;
                            boundary_up -= rate;
                            //DIRECTY -= rate;
                            if (!Overlap(char_rect, "down"))
                            {
                                DIRECTY -= rate;
                            }
                            else
                            {
                                DIRECTY += rate;
                            }

                        }
                        soundEffectInstance.Play();
                        down_button = Content.Load<Texture2D>("button");
                        ourCharacter.Update(gameTime);
                    }
                    else if (newKeyBoardState.IsKeyDown(Keys.W) || CheckRectangleTouch(up_rect, touchCollection))
                    {
                        if (position != "forward")
                        {

                            texture = Content.Load<Texture2D>("main_character/forward");
                            up_button = Content.Load<Texture2D>("button2");
                            down_button = Content.Load<Texture2D>("button2");
                            left_button = Content.Load<Texture2D>("button2");
                            right_button = Content.Load<Texture2D>("button2");
                            ourCharacter = new OurCharacter(texture, 1, 3);
                            position = "forward";
                            //Debug.WriteLine("OUT" + count++);
                        }
                        if (boundary_up == 0)
                        {
                            Debug.WriteLine("STOPUP");
                        }
                        else
                        {
                            //Debug.WriteLine(boundary_up);
                            boundary_up += rate;
                            boundary_down += rate;
                            //DIRECTY += rate;
                            if (!Overlap(char_rect, "up"))
                            {
                                DIRECTY += rate;
                            }
                            else
                            {
                                DIRECTY -= rate;
                            }
                        }


                        soundEffectInstance.Play();
                        up_button = Content.Load<Texture2D>("button");
                        ourCharacter.Update(gameTime);
                    }
                    else if (newKeyBoardState.IsKeyDown(Keys.Enter) || CheckRectangleTouch(inter_rect, touchCollection))
                    {
                        inter_button = Content.Load<Texture2D>("button");
                        if (Overlap(char_rect, "up") || Overlap(char_rect, "left") || Overlap(char_rect, "right") || Overlap(char_rect, "down"))
                        {
                            Debug.WriteLine("YOU SURVIVE!!!");
                            message = Content.Load<Texture2D>("win");
                            GameEnd = true;
                            GameDec = true;
                        }

                    }
                    else
                    {
                        soundEffectInstance.Stop();
                        left_button = Content.Load<Texture2D>("button2");
                        up_button = Content.Load<Texture2D>("button2");
                        right_button = Content.Load<Texture2D>("button2");
                        down_button = Content.Load<Texture2D>("button2");
                        inter_button = Content.Load<Texture2D>("button2");
                        if (position == "down")
                        {
                            texture = Content.Load<Texture2D>("main_character/stand");
                            ourCharacter = new OurCharacter(texture, 1, 1);
                            position = "stand";
                            //Debug.WriteLine("OUT" + count++);
                        }
                        else if (position == "left")
                        {
                            texture = Content.Load<Texture2D>("main_character/left_stand");
                            ourCharacter = new OurCharacter(texture, 1, 1);
                            position = "left_stand";
                        }
                        else if (position == "forward")
                        {
                            texture = Content.Load<Texture2D>("main_character/forward_stand");
                            ourCharacter = new OurCharacter(texture, 1, 1);
                            position = "forward_stand";
                        }
                        else if (position == "right")
                        {
                            texture = Content.Load<Texture2D>("main_character/right_stand");
                            ourCharacter = new OurCharacter(texture, 1, 1);
                            position = "right_stand";
                        }
                    }
                    if (ghost_timer < 0)
                    {

                        //MediaPlayer.Play(ghost_song);
                        soundEffectInstance2.Play();
                        song_time -= elapsed;
                        soundEffectInstance.Stop();
                        ghost_appear = true;
                        if (song_time < 0)
                        {
                            ghost_timer = RandomNumber(10, 30);
                            song_time = 20;
                            ghost_appear = false;
                            soundEffectInstance2.Stop();
                            soundEffectInstance.Stop();
                        }
                        if ((CheckRectangleTouch(new Rectangle(0, 0, GraphicsDevice.Viewport.Bounds.Width, GraphicsDevice.Viewport.Bounds.Height), touchCollection) || CheckRectangleTouch(left_rect, touchCollection) || CheckRectangleTouch(right_rect, touchCollection) || CheckRectangleTouch(up_rect, touchCollection) || CheckRectangleTouch(down_rect, touchCollection) || CheckRectangleTouch(inter_rect, touchCollection)) && song_time <= 17)
                        {
                            Debug.WriteLine("YOU DIED!!!");
                            message = Content.Load<Texture2D>("lose");
                            GameEnd = true;
                            GameDec = true;
                            ghost_appear = false;
                            soundEffectInstance2.Stop();
                            soundEffectInstance.Stop();
                        }

                    }
                }
                else
                {
                    float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                    clock -= elapsed;
                    soundEffectInstance3.Play();
                    if (CheckRectangleTouch(new Rectangle(0, 0, GraphicsDevice.Viewport.Bounds.Width, GraphicsDevice.Viewport.Bounds.Height), touchCollection) && clock < 0)
                    {
                        First_Scene();
                        //soundEffectInstance3.Pause();
                        soundEffectInstance3.Stop();
                        transition = false;
                        GameEnd = false;
                        GameDec = false;

                    }
                }
            }
            else
            {
                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                clock2 -= elapsed;
                timer2 -= elapsed;
                timer -= elapsed;
                timer3 -= elapsed;
                if (timer < 0)
                {
                    if (first == "")
                    {
                        timer = 5;
                        first = "Beware of Her...";
                    }
                    else
                    {
                        timer = 2;
                        first = "";
                    }
                }
                if (timer2 < 0)
                {
                    if (second == "")
                    {
                        timer2 = 3;
                        second = "Stay Still...";
                    }
                    else
                    {
                        timer2 = 1;
                        second = "";
                    }
                }
                if (timer3 < 0)
                {
                    if (second == "")
                    {
                        timer3 = 2;
                        press = "Touch Any Where to Start the Game...";
                    }
                    else
                    {
                        timer3 = 1;
                        press = "";
                    }
                }
                if (clock2 < 0)
                {
                    TouchCollection touches = TouchPanel.GetState();
                    foreach (TouchLocation tl in touches)
                    {
                        transition = true;
                    }
                }
            }
            base.Update(gameTime);
            
        }
        private bool CheckRectangleTouch(Rectangle target, TouchCollection touchCollection)
        {
            if (touchCollection.Count > 0)
            {
                foreach (var touch in touchCollection)
                {
                    if (target.Contains(touch.Position))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.TransparentBlack);

            var screenCenter = new Vector2((GraphicsDevice.Viewport.Bounds.Width / 2 ) - (75/2), (GraphicsDevice.Viewport.Bounds.Height / 2) - (80/2));
            UPDATE_chest();
            spriteBatch.Begin();
            if (transition)
            {
                MediaPlayer.Stop();
                if (!GameEnd)
                {
                    spriteBatch.Draw(stage, new Rectangle((GraphicsDevice.Viewport.Bounds.Width / 2) - (4000 / 2) + DIRECTX, (GraphicsDevice.Viewport.Bounds.Height / 2) - (4008 / 2) + DIRECTY, 4008, 4036), Color.White);
                    spriteBatch.Draw(chest, chest_rect, Color.White);
                    if(ghost_appear)
                    {
                        spriteBatch.Draw(ghost, new Rectangle((GraphicsDevice.Viewport.Bounds.Width / 2) - (75 / 2), (160) - (85 / 2), 75,85) , Color.White);
                    }
                    ourCharacter.Draw(spriteBatch, screenCenter, 75, 80);

                    spriteBatch.Draw(torch, new Rectangle(0, 0, GraphicsDevice.Viewport.Bounds.Width, GraphicsDevice.Viewport.Bounds.Height), Color.White);

                    spriteBatch.Draw(fog, new Rectangle(0, 0, GraphicsDevice.Viewport.Bounds.Width, GraphicsDevice.Viewport.Bounds.Height), Color.White);

                    spriteBatch.Draw(up_button, up_rect, Color.White);
                    spriteBatch.Draw(left_button, left_rect, Color.White);
                    spriteBatch.Draw(right_button, right_rect, Color.White);
                    spriteBatch.Draw(down_button, down_rect, Color.White);

                    spriteBatch.Draw(inter_button, inter_rect, Color.White);
                }
                else
                {
                    if (GameDec)
                    {
                        soundEffectInstance.Stop();
                        spriteBatch.Draw(message, new Rectangle(0, 0, GraphicsDevice.Viewport.Bounds.Width, GraphicsDevice.Viewport.Bounds.Height), Color.White);
                    }
                }
            }
            else
            {
                Debug.WriteLine("FIRST SCREEN");
                spriteBatch.Draw(background, new Rectangle(0, 0, GraphicsDevice.Viewport.Bounds.Width, GraphicsDevice.Viewport.Bounds.Height), Color.White);
                spriteBatch.DrawString(text, first, new Vector2((GraphicsDevice.Viewport.Bounds.Width / 4), GraphicsDevice.Viewport.Bounds.Height / 4), Color.Red);
                spriteBatch.DrawString(text, second, new Vector2(3 * (GraphicsDevice.Viewport.Bounds.Width / 5), 3 * (GraphicsDevice.Viewport.Bounds.Height / 5)), Color.Red);
                Debug.WriteLine(text.MeasureString(press));
                spriteBatch.DrawString(text, press, new Vector2((GraphicsDevice.Viewport.Bounds.Width / 2) - text.MeasureString(press).X / 2, (GraphicsDevice.Viewport.Bounds.Height) - GraphicsDevice.Viewport.Bounds.Height / 5), Color.White);
            }
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
