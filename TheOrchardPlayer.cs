using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
using Terraria.GameInput;
using System.Linq;
using Terraria.ModLoader.IO;
using ReLogic.Graphics;
using Terraria.Graphics.Effects;
using ElementsAwoken.NPCs;
using Microsoft.Xna.Framework.Input;
using Terraria.Social;

namespace ElementsAwoken
{
    public class TheOrchardPlayer : ModPlayer
    {
        public bool inGame = false;
        public int oranges = 0;
        public float money = 0;
        public override void ResetEffects()
        {

        }
        public override void PreUpdate()
        {
            if (inGame)
            {
                player.controlJump = false;
            }
        }
        public override void PreUpdateMovement()
        {
        }
        public override TagCompound Save()
        {
            TagCompound tag = new TagCompound {
            {"oranges", oranges},
            {"money", money}
            };
            return tag;
        }

        public override void Load(TagCompound tag)
        {
            oranges = tag.GetInt("oranges");
            money = tag.GetFloat("money");
        }
        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            ExitGame();
        }
        public override void OnHitByProjectile(Projectile proj, int damage, bool crit)
        {
            ExitGame();
        }
        private void ExitGame()
        {
            inGame = false;
            Main.PlaySound(SoundID.MenuClose);
        }
        public override void PostUpdateMiscEffects()
        {
            if (inGame)
            {
                player.GetModPlayer<MyPlayer>().cantUseItems = true;
                player.GetModPlayer<MyPlayer>().cantGrapple = true;
                player.velocity.X = 0;
                player.controlLeft = false;
                player.controlRight = false;
                player.controlDown = false;
                player.controlJump = false;
                if (player.controlInv)
                {
                    ExitGame();
                }
                else Main.playerInventory = false;
            }
        }
    }
}