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

namespace ElementsAwoken.NPCs.Liftable
{
    public abstract class HeldNPCBase : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 32;
            npc.height = 32;

            npc.aiStyle = -1;

            npc.lifeMax = 50;

            npc.immortal = true;
            npc.dontTakeDamage = true;
            npc.GetGlobalNPC<NPCsGLOBAL>().liftable = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pick Up Base");
        }
       
        public override void AI()
        {
            Player player = Main.LocalPlayer;
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (npc.GetGlobalNPC<NPCsGLOBAL>().liftable)
            {
                if (modPlayer.npcHeld == npc.whoAmI)
                {
                    npc.GivenName = "";
                    npc.noGravity = true;
                    npc.noTileCollide = true;
                    player.bodyFrame.Y = 5 * player.bodyFrame.Height;
                }
                else
                {
                    npc.noGravity = false;
                    npc.noTileCollide = false;
                    if (npc.getRect().Contains(Main.MouseWorld.ToPoint()) && Vector2.Distance(player.Center, npc.Center) < 100)
                    {
                        npc.GivenName = "Pick up (Right Mouse)";
                        if (Main.mouseRight && Main.mouseRightRelease) modPlayer.npcHeld = npc.whoAmI;
                    }
                    else npc.GivenName = "";
                }
                if (Math.Abs(npc.velocity.Y) <= 0.2f)
                {
                    npc.velocity.X *= 0.9f;
                }
            }
            ExtraAI(modPlayer.npcHeld == npc.whoAmI, player);
        }
        public virtual void ExtraAI(bool held, Player player)
        {

        }
    }
}
