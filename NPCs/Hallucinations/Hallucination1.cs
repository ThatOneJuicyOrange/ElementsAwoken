using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Hallucinations
{
    public class Hallucination1 : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 110;
            npc.height = 86;

            npc.damage = 10;
            npc.defense = 5; 
            npc.lifeMax = 100;

            npc.HitSound = SoundID.NPCHit54;
            npc.DeathSound = SoundID.NPCDeath52;
            npc.knockBackResist = 0f;

            npc.noGravity = true;
            npc.noTileCollide = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hallucination");
        }
        public override void AI()
        {
            Player player = Main.player[(int)npc.ai[0]];
            AwakenedPlayer awakenedPlayer = player.GetModPlayer<AwakenedPlayer>();
            if (npc.ai[1] == 0)
            {
                npc.ai[2] = (int)MathHelper.Lerp(60, 255, MathHelper.Clamp((float)awakenedPlayer.sanity / awakenedPlayer.sanityMax * 0.5f, 0, 1));
                npc.alpha = (int)npc.ai[2];

            }
            else
            {
                npc.alpha = (int)MathHelper.Lerp(255, npc.ai[2], MathHelper.Clamp((Vector2.Distance(player.Center, npc.Center) - 100) / 200, 0, 1));
                if (npc.alpha >= 255) npc.active = false;
            }
            if (awakenedPlayer.sanity > awakenedPlayer.sanityMax * 0.5f)
            {
                npc.active = false;
            }
            else if (awakenedPlayer.sanity < awakenedPlayer.sanityMax * 0.2f)
            {

            }
            else
            {
                if (Vector2.Distance(player.Center,npc.Center) < 200)
                {
                    npc.ai[1] = 1;
                }
            }
            Vector2 toTarget = new Vector2(player.Center.X - npc.Center.X, player.Center.Y - npc.Center.Y);
            toTarget.Normalize();
            npc.velocity = toTarget * 2f;

        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.Slow, 60, false);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Player player = Main.LocalPlayer;
            AwakenedPlayer awakenedPlayer = player.GetModPlayer<AwakenedPlayer>();
            if (awakenedPlayer.sanity > awakenedPlayer.sanityMax * 0.5f)
            {
                if (npc.Hitbox.Contains(Main.MouseWorld.ToPoint())) player.GetModPlayer<MyPlayer>().cantSeeHoverText = true;
                return false;
            }
            return true;
        }
    }
}