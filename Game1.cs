using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _bg;
    private Texture2D _ballTexture;
    private Vector2 _ballPos;
    private Vector2 _ballDir;
    Random _random;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {

        base.Initialize();
    
        _random = new Random();

        _ballPos.X = (_graphics.PreferredBackBufferWidth / 2.0f) - (_ballTexture.Width / 2.0f);
        _ballPos.Y = (_graphics.PreferredBackBufferHeight / 2.0f) - (_ballTexture.Height / 2.0f);

        _ballDir = new Vector2(1.0f, GetRandomY());
        _ballDir.Normalize();

        
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _bg = Content.Load<Texture2D>("campo");

        _ballTexture = Content.Load<Texture2D>("bola_insta");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

            //-----------------------------//

        const float speed = 50.0f; // pra bola se mover pra direita

        _ballPos  = _ballPos + (_ballDir * speed); // entender melhor dps
        if(_ballPos.X + _ballTexture.Width > _graphics.PreferredBackBufferWidth){
            _ballDir.X = -1.0f;
            _ballDir.Y = (_random.NextSingle() * 2.0f) - 1.0f;
        } else if(_ballPos.X < 0.0f){
            _ballDir.X = 1.0f;
            _ballDir.Y = (_random.NextSingle() * 2.0f) - 1.0f;
        } else if(_ballPos.Y > _graphics.PreferredBackBufferHeight - _ballTexture.Height){
	        _ballPos.Y = _graphics.PreferredBackBufferHeight - _ballTexture.Height;
            _ballDir.Y = -_ballDir.Y;
	    } else if(_ballPos.Y < 0.0f){
            _ballPos.Y = 0.0f;
            _ballDir.Y = -_ballDir.Y;
        }
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        _spriteBatch.Draw(_bg, Vector2.Zero, Color.White);
        _spriteBatch.Draw(_ballTexture, _ballPos, Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private float GetRandomY(){
        return (_random.NextSingle() * 2.0f) - 1.0f;
    }
}
