using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Utilities;

namespace ElementsAwoken.Effects
{
    // thanks yuyu  :D
    public class AshOverlay : Overlay
    {
        private Ash[] _ashes;

        public AshOverlay(EffectPriority priority = 0, RenderLayers layer = RenderLayers.TilesAndNPCs) : base(priority, layer)
        {
            _ashes = new Ash[1000];
        }

        public override void Activate(Vector2 position, params object[] args)
        {
            this.Mode = OverlayMode.FadeIn;
        }

        public override void Deactivate(params object[] args)
        {
            this.Mode = OverlayMode.FadeOut;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Mod mod = ModLoader.GetMod("ElementsAwoken");
            for (int i = 0; i < _ashes.Length; i++)
            {
                if (_ashes[i].active)
                {
                    Ash ash = _ashes[i];
                    float alpha = ash.timeLeft < 1f ? ash.timeLeft : 1f;
                    float scale = 1.2f;
                    Texture2D spriteTexture = mod.GetTexture("Effects/Ash2");
                    Rectangle spriteSource = new Rectangle(8, 1, 6, 6); // what the hell does this line do. was: new Rectangle(761, 1, 6, 6)
                    Color c = Lighting.GetColor((int)(ash.center.X), (int)(ash.center.Y));

                    spriteBatch.Draw(spriteTexture, ash.center - Main.screenPosition, spriteSource, c * alpha, ash.rotation, new Vector2(spriteTexture.Width * 0.5f, spriteTexture.Height * 0.5f), scale, SpriteEffects.None, 0f);

                }
            }
        }

        public override bool IsVisible()
        {
            return true;
        }

        public override void Update(GameTime gameTime)
        {
            if (!Main.gameMenu && Main.netMode != 2)
            {
                int ashToSpawn = Main.rand.Next(2);
                for (int i = 0; i < ashToSpawn; i++)
                {
                    int index = 0;
                    while (index < _ashes.Length && _ashes[index].active)
                    {
                        index++;
                    }

                    if (index >= _ashes.Length) break;

                    float spawnX = Main.screenPosition.X + Main.screenWidth / 2  + Main.rand.Next(-(Main.screenWidth / 2 + 100), Main.screenWidth / 2 + 100);
                    float spawnY = Main.screenPosition.Y + Main.rand.NextFloat(- 500f, 500);

                    _ashes[index].active = true;
                    _ashes[index].center = new Vector2(spawnX, spawnY);
                    _ashes[index].velocity = new Vector2(Main.windSpeed * 30f, Main.rand.NextFloat(0.2f, 2f));
                    _ashes[index].rotation = Main.rand.NextFloat(0f, MathHelper.TwoPi);
                    _ashes[index].cinder = Main.rand.Next(150) == 0;
                    _ashes[index].timeLeft = 6f;
                }
                for (int i = 0; i < _ashes.Length; i++)
                {
                    if (_ashes[i].active)
                    {
                        _ashes[i].UpdatePosition(gameTime);
                        Point tilePos = _ashes[i].center.ToTileCoordinates();
                        Vector2 worldMapSize = new Vector2(Main.maxTilesX - 1, Main.maxTilesY - 1);
                        if (tilePos.X < 0 || tilePos.Y < 0 || tilePos.X > worldMapSize.X || tilePos.Y > worldMapSize.Y)
                        {
                            _ashes[i].active = false;
                            continue;
                        }
                        if (tilePos.X < 0) tilePos.X = 0;
                        else if (tilePos.X > worldMapSize.X) tilePos.X = (int)worldMapSize.X;
                        if (tilePos.Y < 0) tilePos.Y = 0;
                        else if (tilePos.Y > worldMapSize.Y) tilePos.Y = (int)worldMapSize.Y;

                        if (_ashes[i].timeLeft <= 0f || (Main.tile[tilePos.X, tilePos.Y] != null && Main.tile[tilePos.X, tilePos.Y].active() && Main.tileSolid[Main.tile[tilePos.X, tilePos.Y].type]))
                        {
                            _ashes[i].active = false;
                        }
                        else if (Framing.GetTileSafely(tilePos.X, tilePos.Y).liquid > 230)
                        {
                            _ashes[i].velocity *= 0.9f;
                            _ashes[i].center.Y -= 0.3f;
                            _ashes[i].timeLeft -= 0.03f;
                            _ashes[i].cinder = false;
                        }
                        else
                        {
                            _ashes[i].velocity.Y += Main.rand.NextFloat(0.005f, 0.02f);
                            _ashes[i].rotation += Main.windSpeed * 0.142f;
                        }
                    }
                }
            }
        }

        private struct Ash
        {
            public bool active;
            public float rotation;
            public bool cinder;
            public Vector2 center;
            public Vector2 velocity;
            public float timeLeft;

            public void UpdatePosition(GameTime time)
            {
                center += velocity;
                timeLeft -= (float)time.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}