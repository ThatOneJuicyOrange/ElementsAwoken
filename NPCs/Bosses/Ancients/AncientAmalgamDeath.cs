using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.Ancients
{
    public class AncientAmalgamDeath : ModNPC
    {
        public override string Texture
        {
            get
            {
                return "ElementsAwoken/NPCs/Bosses/Ancients/AncientAmalgam";
            }
        }
        public override void SetDefaults()
        {
            npc.width = 44;
            npc.height = 102;

            npc.aiStyle = -1;

            npc.lifeMax = 10;
            npc.damage = 0;
            npc.defense = 0;
            npc.knockBackResist = 0f;

            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.netAlways = true;
            npc.immortal = true;
            npc.dontTakeDamage = true;

            npc.HitSound = SoundID.NPCHit5;
            npc.DeathSound = SoundID.NPCDeath6;

            npc.scale *= 1.3f;
            npc.npcSlots = 1f;

            music = MusicID.LunarBoss;

        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Ancient Amalgamate");
            Main.npcFrameCount[npc.type] = 5;
        }

        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            npc.frameCounter++;

            if (npc.frameCounter > 6)
            {
                npc.frame.Y = npc.frame.Y + frameHeight;
                npc.frameCounter = 0.0;
            }

            if (npc.frame.Y >= frameHeight * Main.npcFrameCount[npc.type])
            {
                npc.frame.Y = 0;
            }
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            return false;
        }
        public override bool? CanBeHitByProjectile(Projectile projectile)
        {
            return false;
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override bool PreNPCLoot()
        {
            return false;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = ElementsAwoken.AADeathBall;
            Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            spriteBatch.Draw(texture, npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY), null, Color.White, npc.rotation, origin, npc.ai[0] / 450f, SpriteEffects.None, 0.0f);
        }

        public override void AI()
        {
            Player P = Main.player[npc.target];
            Lighting.AddLight(npc.Center, 1f, 1f, 1f);

            npc.ai[0]++;

            float intensity = MathHelper.Lerp(0f, 1f, npc.ai[0] / 450f);
            MoonlordDeathDrama.RequestLight(intensity, npc.Center);
            if (npc.ai[1] == 0)
            {
                for (int i = 0; i < 15; i++)
                {
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("Lightbeam"), 0, 0f, 0, i);
                }
                npc.ai[1]++;
            }
            if (npc.ai[0] > 450)
            {
                npc.immortal = false;
                npc.dontTakeDamage = false;
                npc.StrikeNPCNoInteraction(npc.life, 0f, 0, false, false, false);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("DeathShockwave"), 0, 0f);
                if (Main.netMode == 0 && !MyWorld.downedAncients) Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("CreditsStarter"), 0, 0f);
                MyWorld.downedAncients = true;
            }
            if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
        }
    }
}
