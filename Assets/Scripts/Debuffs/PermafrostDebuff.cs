using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Debuffs
{
    class PermafrostDebuff : Debuff
    {
        public float MySpeedReduction { get; set; }

        public override string Name => "Permafrost";

        public PermafrostDebuff()
        {
            MyDuration = 3;
        }

        public override Debuff Clone()
        {
            PermafrostDebuff clone = (PermafrostDebuff)this.MemberwiseClone();

            return clone;
        }
    }
}
