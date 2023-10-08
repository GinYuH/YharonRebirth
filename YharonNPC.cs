using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria.ModLoader.IO;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria.GameContent;
using CalamityMod.NPCs.Yharon;
using CalamityMod;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using ReLogic.Utilities;

namespace YharonRebirth
{
    public class YharonNPC : GlobalNPC
    {
        public int invtimer = 0;

        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }

        public override void SetDefaults(NPC npc)
        {
            if (npc.type == ModContent.NPCType<Yharon>())
            {
                npc.lifeMax /= 2;
                npc.life = npc.lifeMax;
            }
        }

        public override void AI(NPC npc)
        {
            if (npc.type == ModContent.NPCType<Yharon>())
            {
                int time = 900;
                if (npc.ai[0] == 17)
                {
                    npc.dontTakeDamage = true;
                }
                if (invtimer < time && CalamityMod.NPCs.CalamityGlobalNPC.yharonP2 > -1)
                {
                    npc.dontTakeDamage = true;
                    invtimer++;
                    if (npc.life < npc.lifeMax)
                    {
                        npc.life += (int)(npc.lifeMax / time);
                        npc.HealEffect((int)(npc.lifeMax / time), true);
                        npc.netUpdate = true;
                    }
                    else
                    {
                        npc.life = npc.lifeMax;
                    }
                }
            }
        }
        public override void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
        {
            if (npc.type == ModContent.NPCType<Yharon>())
                binaryWriter.Write(invtimer);
        }
        public override void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
        {
            if (npc.type == ModContent.NPCType<Yharon>())
            invtimer = binaryReader.ReadInt32();
        }
    }
}
