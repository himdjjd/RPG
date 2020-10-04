using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace Assets.Scripts.Debuffs
{
    class PermafrostDebuff : Debuff
    {
        public float MySpeedReduction { get; set; }

        public override string Name => "Permafrost";

        private float originalSpeed;

        public PermafrostDebuff(Image icon) : base(icon)
        {
            MyDuration = 3;
        }

        public override void Apply(Character character)
        {
            originalSpeed = character.Speed;
            character.Speed = character.Speed - (character.Speed * (MySpeedReduction / 100));
            base.Apply(character);
        }

        public override void Remove()
        {
            MyCharacter.Speed = originalSpeed;
            base.Remove();
        }

        public override Debuff Clone()
        {
            PermafrostDebuff clone = (PermafrostDebuff)this.MemberwiseClone();

            return clone;
        }
    }
}
