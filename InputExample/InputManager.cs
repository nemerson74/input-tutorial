﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace InputExample
{
    public class InputManager
    {
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        MouseState currentMouseState;
        MouseState previousMouseState;

        GamePadState currentGamePadState;
        GamePadState previousGamePadState;

        /// <summary>
        /// Direction of the input
        /// </summary>
        public Vector2 Direction { get; private set; }

        /// <summary>
        /// Position of the mouse cursor
        /// </summary>
        public Vector2 Position { get; private set; }

        /// <summary>
        /// If warp function is requested
        /// </summary>
        public bool Warp { get; private set; }

        /// <summary>
        /// To exit the game
        /// </summary>
        public bool Exit { get; private set; } = false;

        public void Update(GameTime gameTime)
        {
        #region updating states
                    previousKeyboardState = currentKeyboardState;
                    currentKeyboardState = Keyboard.GetState();
                    previousMouseState = currentMouseState;
                    currentMouseState = Mouse.GetState();
                    previousGamePadState = currentGamePadState;
                    currentGamePadState = GamePad.GetState(0);
                    Warp = false;
                    #endregion
        #region GamePad
                    //Get position from GamePad
                    Direction = currentGamePadState.ThumbSticks.Right * 100
                    * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (currentGamePadState.IsButtonDown(Buttons.A)
                        && previousGamePadState.IsButtonUp(Buttons.A))
                    {
                        Warp = true;
                    }

                    #endregion
        #region Keyboard
                    //Get position from Keyboard
                    if ((currentKeyboardState.IsKeyDown(Keys.Left)) ||
                        currentKeyboardState.IsKeyDown(Keys.A))
                    {
                        Direction += new Vector2(-100 * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);
                    }
                    if ((currentKeyboardState.IsKeyDown(Keys.Right)) ||
                        currentKeyboardState.IsKeyDown(Keys.D))
                    {
                        Direction += new Vector2(100 * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);
                    }
                    if ((currentKeyboardState.IsKeyDown(Keys.Up)) ||
                        currentKeyboardState.IsKeyDown(Keys.W))
                    {
                        Direction += new Vector2(0, -100 * (float)gameTime.ElapsedGameTime.TotalSeconds);
                    }
                    if ((currentKeyboardState.IsKeyDown(Keys.Down)) ||
                        currentKeyboardState.IsKeyDown(Keys.S))
                    {
                        Direction += new Vector2(0, 100 * (float)gameTime.ElapsedGameTime.TotalSeconds);
                    }
                    if (currentKeyboardState.IsKeyDown(Keys.Space) && previousKeyboardState.IsKeyUp(Keys.Space))
                    {
                        Warp = true;
                    }
                    #endregion
        #region Mouse
                    //Get position from Mouse
            
                    Position = new Vector2(
                        currentMouseState.X,
                        currentMouseState.Y
                        ) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            
                    if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
                    {
                        Warp = true;
                    }
            
                    #endregion
        #region exit
                    if (currentGamePadState.Buttons.Back == ButtonState.Pressed || currentKeyboardState.IsKeyDown(Keys.Escape))
                    {
                        Exit = true;
                    }
                    #endregion
        }
    }
}
