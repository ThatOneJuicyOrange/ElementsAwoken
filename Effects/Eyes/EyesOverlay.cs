using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace ElementsAwoken.Effects.Eyes
{
    // thanks yuyu  :D
    public class EyesOverlay : Overlay
    {
        private Eyes[] _eyes;
        private int eyeSpawnCD = 0;

        public EyesOverlay(EffectPriority priority = 0, RenderLayers layer = RenderLayers.TilesAndNPCs) : base(priority, layer)
        {
            _eyes = new Eyes[30];
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
            for (int i = 0; i < _eyes.Length; i++)
            {
                if (_eyes[i].active)
                {
                    Eyes eyes = _eyes[i];
                    Texture2D spriteTexture = mod.GetTexture("Effects/Eyes/Eyes" + eyes.type);

                    spriteBatch.Draw(spriteTexture, eyes.center - Main.screenPosition, null, Color.White * eyes.alpha, 0, spriteTexture.Size() / 2, new Vector2(1, eyes.yScale), SpriteEffects.None, 0f);
                }
            }
        }

        public override bool IsVisible()
        {
            return MyWorld.awakenedMode;
        }

        public override void Update(GameTime gameTime)
        {
            if (!Main.gameMenu && Main.netMode != 2)
            {
                int maxLight = 6;
                Player player = Main.player[Main.myPlayer];
                AwakenedPlayer modPlayer = player.GetModPlayer<AwakenedPlayer>();
                if (modPlayer.sanity < modPlayer.sanityMax * 0.6f)
                {
                    eyeSpawnCD--;
                    if (eyeSpawnCD <= 0)
                    {
                        int index = 0;
                        while (index < _eyes.Length && _eyes[index].active)
                        {
                            index++;
                        }
                        bool spawn = true;
                        if (index >= _eyes.Length) spawn = false;

                        float spawnX = player.Center.X + Main.rand.Next(-500,500);
                        float spawnY = player.Center.Y + Main.rand.Next(-500, 500);
                        Color c = Lighting.GetColor((int)spawnX / 16, (int)spawnY / 16);
                        if (c.R > maxLight || c.G > maxLight || c.B > maxLight || Vector2.Distance(player.Center, new Vector2(spawnX,spawnY)) < 200) spawn = false;
                        if (spawn)
                        {
                            _eyes[index].active = true;
                            _eyes[index].despawning = false;
                            _eyes[index].center = new Vector2(spawnX, spawnY);
                            _eyes[index].timeLeft = 600;
                            _eyes[index].alphaMax = Main.rand.NextFloat(0.6f, 1f);
                            _eyes[index].alpha = 0;
                            _eyes[index].type = Main.rand.Next(5);
                            _eyes[index].yScale = 0;
                            _eyes[index].blinkingState = 0;
                            eyeSpawnCD = (int)MathHelper.Lerp(60, 600, modPlayer.sanity / (modPlayer.sanityMax * 0.6f));
                            eyeSpawnCD = (int)(eyeSpawnCD * Main.rand.NextFloat(0.5f, 1.5f));
                        }
                    }
                }
                for (int i = 0; i < _eyes.Length; i++)
                {
                    if (!_eyes[i].active) continue;
                    _eyes[i].timeLeft--;

                    //Main.NewText(Lighting.GetColor((int)Main.MouseWorld.X / 16, (int)Main.MouseWorld.Y / 16));
                    Point tilePos = _eyes[i].center.ToTileCoordinates();
                    Tile curTile = Framing.GetTileSafely(tilePos.X, tilePos.Y);
                    Color c = Lighting.GetColor((int)_eyes[i].center.X / 16, (int)_eyes[i].center.Y / 16);
                    if (c.R > maxLight || c.G > maxLight || c.B > maxLight || Vector2.Distance(player.Center,_eyes[i].center) < 200 || _eyes[i].timeLeft <= 0) _eyes[i].despawning = true;
                    if (_eyes[i].blinkingState != 0)
                    {
                        if (_eyes[i].blinkingState == 1)
                        {
                            _eyes[i].yScale -= 1f / 10f;
                            if (_eyes[i].yScale <= 0) _eyes[i].blinkingState = 2;
                        }
                        else
                        {
                            _eyes[i].yScale += 1f / 10f;
                            if (_eyes[i].yScale >= 1)
                            {
                                _eyes[i].blinkingState = 0;
                                _eyes[i].yScale = 1;
                            }
                        }
                    }
                    else if (Main.rand.NextBool(120)) _eyes[i].blinkingState = 1;
                    if (_eyes[i].despawning)
                    {
                        _eyes[i].alpha -= _eyes[i].alphaMax / 20f;
                        _eyes[i].yScale -= 1f / 20f;
                        if (_eyes[i].yScale <= 0) _eyes[i].active = false;
                    }
                    else if (_eyes[i].blinkingState == 0)
                    {
                        if (_eyes[i].alpha < _eyes[i].alphaMax) _eyes[i].alpha += _eyes[i].alphaMax / 20f;
                        else _eyes[i].alpha = _eyes[i].alphaMax;

                        if (_eyes[i].yScale < 1) _eyes[i].yScale += 1f / 20f;
                        else _eyes[i].yScale = 1;
                    }
                }
            }
        }

        private struct Eyes
        {
            public bool active;
            public Vector2 center;
            public int timeLeft;
            public int type;
            public float alpha;
            public float alphaMax;
            public bool despawning;
            public int blinkingState;
            public float yScale;
        }
    }
}