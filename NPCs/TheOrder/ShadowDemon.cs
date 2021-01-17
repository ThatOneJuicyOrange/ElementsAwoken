using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using static Terraria.ModLoader.ModContent;
using ElementsAwoken.Projectiles.NPCProj;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Audio;
using System.Linq;
using Terraria.Graphics.Shaders;

namespace ElementsAwoken.NPCs.TheOrder
{
    public class ShadowDemon : ModNPC
    {
        private float aiState
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        private float aiTimer
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }
        private float shootTimer
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }
        private float visualsAI
        {
            get => npc.ai[3];
            set => npc.ai[3] = value;
        }
        public override void SetDefaults()
        {
            npc.width = 60;
            npc.height = 88;

            npc.aiStyle = -1;

            npc.defense = 20;
            npc.lifeMax = 1200;
            npc.damage = 30;
            npc.knockBackResist = 0f;

            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/ShadowDemon");

            npc.value = Item.buyPrice(0, 15, 0, 0);
            npc.knockBackResist = 0f;
            npc.lavaImmune = true;
            npc.noGravity = false;
            npc.noTileCollide = true;

            //npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadow Demon");
        }
        public override void NPCLoot()
        {
            //if (Main.rand.NextBool())Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<Items.Placeable.Tiles.Plateau.SulfuricSedimentItem>(), Main.rand.Next(1, 3));
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            Main.PlaySound(SoundID.NPCHit, (int)npc.position.X, (int)npc.position.Y, 36, 1, -0.8f);   
            if (npc.life <= 0)
            {
                Main.PlaySound(SoundID.NPCKilled, (int)npc.position.X, (int)npc.position.Y, 55, 1, -0.8f);
                Main.PlaySound(SoundID.NPCKilled, (int)npc.position.X, (int)npc.position.Y, 6, 1, -0.8f);
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = Main.npcTexture[npc.type];
            SpriteEffects effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            int shader = GameShaders.Armor.GetShaderIdFromItemId(ItemID.VoidDye);

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            GameShaders.Armor.ApplySecondary(shader, Main.player[Main.myPlayer], null);

            Main.spriteBatch.Draw(texture, npc.Center - Main.screenPosition, new Rectangle?(), drawColor, npc.rotation, texture.Size() / 2, npc.scale, effects, 0);

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            return false;
        }
        public override void AI()
        {
            /*int numDust = 6;
            if (GetInstance<Config>().lowDust) numDust = 2;
            for (int i = 0; i < numDust; i++)
            {
                Vector2 position = npc.Center + Main.rand.NextVector2Circular(npc.width * 0.5f, npc.height * 0.5f);
                Dust dust = Dust.NewDustPerfect(position, 54, Vector2.Zero);
                dust.noGravity = true;
                dust.fadeIn = 1.2f;
            }*/
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            npc.spriteDirection = Math.Sign(npc.Center.X - player.Center.X);
 float dist = Vector2.Distance(player.Center, npc.Center);
            if (dist < 1000)
            {
                aiTimer++;
                shootTimer--;
                if (aiTimer % 5 == 0)
                {
                    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                    string nameString = new string(Enumerable.Repeat(chars, 20)
                      .Select(s => s[Main.rand.Next(s.Length)]).ToArray());
                    npc.GivenName = nameString;
                }
                if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    float Speed = Main.expertMode ? MyWorld.awakenedMode ? 8 : 7 : 5;
                    Speed *= MathHelper.Lerp(1.5f, 1, (float)npc.life / (float)npc.lifeMax);
                    float rotation = (float)Math.Atan2(npc.Center.Y - player.Center.Y, npc.Center.X - player.Center.X);
                    Vector2 projSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                    Projectile proj = Main.projectile[Projectile.NewProjectile(npc.Center, projSpeed, ProjectileType<ShadowTentacle>(), npc.damage / 2, 0f, Main.myPlayer,Main.rand.NextFloat(0.01f,0.08f), Main.rand.NextFloat(0.01f, 0.08f))];
                    if (Main.rand.NextBool())
                    {
                        proj.ai[0] *= -1;
                        proj.ai[1] *= -1;
                    }
                    shootTimer = (int)MathHelper.Lerp(30, 180, (float)npc.life / (float)npc.lifeMax);
                }
                /*if (npc.soundDelay <= 0)
                {
                    Main.PlaySound(SoundID.Item, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/MusicBoxNote"), 0.5f, MathHelper.Lerp(-0.3f, -1f, MathHelper.Clamp(dist / 800f, 0, 1)));
                    npc.soundDelay = (int)MathHelper.Lerp(12, 90, dist / 800f);
                }*/
                Vector2 toTarget = new Vector2(player.Center.X - npc.Center.X, player.Center.Y - npc.Center.Y);
                toTarget.Normalize();
                float moveSpeed = Main.expertMode ? MyWorld.awakenedMode ? 3 : 2.5f : 2;
                moveSpeed *= MathHelper.Lerp(1.25f, 1, (float)npc.life / (float)npc.lifeMax);
                npc.velocity = toTarget * moveSpeed;
            }
            Point npcTile = npc.Center.ToTileCoordinates();
            for (int i = -5; i < 5; i++)
            {
                for (int j = -5; j < 5; j++)
                {
                    Tile t = Framing.GetTileSafely(npcTile.X + i, npcTile.Y + j);
                    if (t.type == TileID.Torches)
                    {
                        WorldGen.KillTile(npcTile.X + i, npcTile.Y + j);
                    }
                }
            }
                for (int p = 0; p < Main.maxPlayers; p++)
            {
                if (Main.player[p].active && !Main.player[p].dead)
                {
                    if (Vector2.Distance(player.Center, npc.Center) < 600)
                    {
                        Main.player[p].AddBuff(BuffID.Blackout, 60);
                        Main.player[p].AddBuff(BuffID.Darkness, 60);
                    }
                }
            }
        }
    }
}
