using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.Volcanox
{
    public class VolcanoxSpawner : ModNPC
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            npc.width = 2;
            npc.height = 2;
            npc.lifeMax = 10000;
            npc.immortal = true;
            npc.dontTakeDamage = true;
            npc.noGravity = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
        }
        public override void AI()
        {
            MyPlayer modPlayer = Main.LocalPlayer.GetModPlayer<MyPlayer>();
            modPlayer.screenshakeAmount = MathHelper.Lerp(0, 10, MathHelper.Clamp(npc.ai[0] / 600, 0, 1));

            if (npc.ai[0] == 0 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Projectile.NewProjectile(npc.Center, new Vector2(0, 10), ModContent.ProjectileType<Projectiles.Environmental.SuperGeyser>(), 9999, 0, Main.myPlayer);
            }
            npc.ai[0]++;
            if (npc.ai[0] < 600)
            {
                float lightStrength = MathHelper.Lerp(0, 1, npc.ai[0] / 600);
                Terraria.GameContent.Events.MoonlordDeathDrama.RequestLight(lightStrength, npc.Center);
            }
            else if (npc.ai[0] == 600)
            {

                NPC volc = Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y + 1000, ModContent.NPCType<Volcanox>(), npc.whoAmI, 0f, 0f, 0f, 0f, 255)];
                //ElementsAwoken.SetEACameraLerp(volc.Center - new Vector2(Main.screenWidth / 2, Main.screenHeight / 2), 0.01f, 20, 120);
                Main.PlaySound(SoundID.Item, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/Bell"), pitchOffset: -0.5f);
                ElementsAwoken.DrawScreenText("---------------------", 270, 0.6f, new Vector2(0, 150), true);
            }
            else if (npc.ai[0] == 630)
            {
                Main.PlaySound(SoundID.Item, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/Bell"), pitchOffset: -0.4f);
                ElementsAwoken.DrawScreenText("VOLCANOX", 300, 1, new Vector2(0, 100), true);
            }
            else if (npc.ai[0] == 660)
            {
                Main.PlaySound(SoundID.Item, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/Bell"), pitchOffset: -0.6f);
                ElementsAwoken.DrawScreenText("King of the Broken Hell", 270, 0.6f, new Vector2(0, 180), true);
                npc.active = false;
            }
        }
    }
}
