using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FK_CLI;

namespace R3Prog_Final
{
	class PierceBullet : BulletBase
	{
		private const double BulletSpeed = 2;

		private const int LifeMax = 99;

		public PierceBullet(CharacterSide side, fk_Vector position, fk_Vector velocity) : base(side, LifeMax, position,
			velocity)
		{
			Model = new fk_Model()
			{
				Shape = new fk_Block(1, 1, 1),
				Material = fk_Material.DimYellow,
				BMode = fk_BoundaryMode.CAPSULE,
				//BDraw = true,
				BLineColor = fk_Material.Green.Diffuse
			};
			Model.SetCapsule(new fk_Vector(0, 0, 0), new fk_Vector(0, 0, 0.01), 1);
			Model.GlRotate(Position, fk_Axis.Y, FK.PI / 4.0);
		}

		protected override void BulletMove()
		{
			Position += Velocity;
		}
	}
}
