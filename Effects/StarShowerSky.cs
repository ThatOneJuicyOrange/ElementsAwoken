using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ElementsAwoken.Effects
{
    public class StarShowerSky : CustomSky
    {
        private struct Meteor
        {
            public Vector2 Position;

            public float Depth;


            public float Scale;

            public float StartX;
        }

        private UnifiedRandom _random = new UnifiedRandom();

        private Texture2D _meteorTexture;

        private bool isActive;

        private Meteor[] _meteors;

        private float _fadeOpacity;

        public override void OnLoad()
        {
            _meteorTexture = ModContent.GetTexture("ElementsAwoken/Extra/ShootingStar");
        }

        public override void Update(GameTime gameTime)
        {
            if (isActive)
            {
                _fadeOpacity = Math.Min(1f, 0.01f + _fadeOpacity);
            }
            else
            {
                _fadeOpacity = Math.Max(0f, _fadeOpacity - 0.01f);
            }
            for (int i = 0; i < _meteors.Length; i++)
            {
                Meteor[] what = _meteors;
                float num = Math.Min((1 / what[i].Depth) * 4400f,3000f);
                what[i].Position.X = what[i].Position.X - num * (float)gameTime.ElapsedGameTime.TotalSeconds;
                what[i].Position.Y = what[i].Position.Y + num * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if ((double)_meteors[i].Position.Y > Main.worldSurface * 16.0)
                {
                    _meteors[i].Position.X = _meteors[i].StartX;
                    _meteors[i].Position.Y = -10000f;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            int num = -1;
            int num2 = 0;
            for (int i = 0; i < _meteors.Length; i++)
            {
                float depth = _meteors[i].Depth;
                if (num == -1 && depth < maxDepth)
                {
                    num = i;
                }
                if (depth <= minDepth)
                {
                    break;
                }
                num2 = i;
            }
            if (num == -1)
            {
                return;
            }
            float scale = Math.Min(1f, (Main.screenPosition.Y - 1000f) / 1000f);
            Vector2 value3 = Main.screenPosition + new Vector2((float)(Main.screenWidth >> 1), (float)(Main.screenHeight >> 1));
            Rectangle rectangle = new Rectangle(-1000, -1000, 4000, 4000);
            for (int j = num; j < num2; j++)
            {
                float depthColorScale = Math.Min((1 / _meteors[j].Depth), 0.2f);
                Vector2 vector = new Vector2(1f / _meteors[j].Depth, 0.9f / _meteors[j].Depth);
                Vector2 vector2 = (_meteors[j].Position - value3) * vector + value3 - Main.screenPosition;
                int num3 = 0;
                if (rectangle.Contains((int)vector2.X, (int)vector2.Y))
                {
                    spriteBatch.Draw(_meteorTexture, vector2, new Rectangle?(new Rectangle(0, num3 * _meteorTexture.Height, _meteorTexture.Width, _meteorTexture.Height)), Color.White * scale * depthColorScale * _fadeOpacity, 0f, Vector2.Zero, vector.X * 5f * _meteors[j].Scale * 0.14f, SpriteEffects.None, 0f);
                }
            }
        }

        public override float GetCloudAlpha()
        {
            return (1f - _fadeOpacity) * 0.3f + 0.7f;
        }

        public override void Activate(Vector2 position, params object[] args)
        {
            isActive = true;
            //_fadeOpacity = 0.002f;
            _meteors = new Meteor[150];
            for (int i = 0; i < _meteors.Length; i++)
            {
                float num = (float)i / (float)_meteors.Length;
                _meteors[i].Position.X = num * ((float)Main.maxTilesX * 16f) + _random.NextFloat() * 40f - 20f;
                _meteors[i].Position.Y = _random.NextFloat() * -((float)Main.worldSurface * 16f + 10000f) - 10000f;
                if (_random.Next(3) != 0)
                {
                    _meteors[i].Depth = _random.NextFloat() * 3f + 1.8f;
                }
                else
                {
                    _meteors[i].Depth = _random.NextFloat() * 5f + 4.8f;
                }
                _meteors[i].Scale = _random.NextFloat() * 0.5f + 1f;
                _meteors[i].StartX = _meteors[i].Position.X;
            }
            Array.Sort<Meteor>(_meteors, new Comparison<Meteor>(SortMethod));
        }

        private int SortMethod(Meteor meteor1, Meteor meteor2)
        {
            return meteor2.Depth.CompareTo(meteor1.Depth);
        }

        public override void Deactivate(params object[] args)
        {
            isActive = false;
        }

        public override void Reset()
        {
            _fadeOpacity = 0f;
            isActive = false;
        }
        public override bool IsActive()
        {
            return isActive || _fadeOpacity > 0.001f;
        }
    }
}
