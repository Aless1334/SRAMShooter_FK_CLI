using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FK_CLI;

namespace R3Prog_Final
{
	class NormalBullet : BulletBase
	{
		private const double BulletSpeed = 2;

		private const int LifeMax = 1;

		public NormalBullet(CharacterSide side, fk_Vector position, fk_Vector velocity, fk_Material material,
			double scale = 1) : base(side, LifeMax, position,
			velocity)
		{
			Model = new fk_Model()
			{
				Shape = new fk_Block(scale, scale, scale),
				Material = material,
				BMode = fk_BoundaryMode.CAPSULE,
				//BDraw = true,
				BLineColor = fk_Material.Green.Diffuse
			};
			Model.SetCapsule(new fk_Vector(0, 0, 0), new fk_Vector(0, 0, 0.01), scale);
		}

		protected override void BulletMove()
		{
			Position += Velocity;
		}
	}
}
