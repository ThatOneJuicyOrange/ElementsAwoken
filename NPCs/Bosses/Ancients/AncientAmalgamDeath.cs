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
            npc.value = Item.buyPrice(1, 0, 0, 0);
            npc.npcSlots = 1f;

            music = MusicID.LunarBoss;
            //music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/InfernaceTheme");
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
        public override void NPCLoot()
        {
            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("DeathShockwave"), 0, 0f);
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = mod.ItemType("SuperHealingPotion");
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
            if (npc.localAI[0] == 0)
            {
                for (int i = 0; i < 15; i++)
                {
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("Lightbeam"), 0, 0f, 0, i);
                }
                npc.localAI[0]++;
            }
            if (npc.ai[0] > 450)
            {
                npc.immortal = false;
                npc.dontTakeDamage = false;
                npc.StrikeNPCNoInteraction(npc.life, 0f, 0, false, false, false);
            }
        }
    }
}
