using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FK_CLI;

namespace R3Prog_Final
{
	class ClusterBullet : BulletBase
	{
		private const int LifeMax = 1;

		public ClusterBullet(CharacterSide side, fk_Vector position, fk_Vector velocity) : base(side, LifeMax, position,
			velocity)
		{
			Model = new fk_Model()
			{
				Shape = new fk_Block(2, 2, 2),
				Material = fk_Material.Red,
				BMode = fk_BoundaryMode.CAPSULE,
				//BDraw = true,
				BLineColor = fk_Material.Green.Diffuse
			};
			Model.SetCapsule(new fk_Vector(0, 0, 0), new fk_Vector(0, 0, 0.01), 1);
		}

		protected override void BulletMove()
		{
			Position += Velocity;
		}

		protected override void DeadProcess()
		{
			var normalBullet = new PierceBullet(Side, new fk_Vector(Position), new fk_Vector(0, 2, 0));
			normalBullet = new PierceBullet(Side, new fk_Vector(Position), new fk_Vector(2, 0, 0));
			normalBullet = new PierceBullet(Side, new fk_Vector(Position), new fk_Vector(0, -2, 0));
			normalBullet = new PierceBullet(Side, new fk_Vector(Position), new fk_Vector(-2, 0, 0));
			normalBullet = new PierceBullet(Side, new fk_Vector(Position), new fk_Vector(0, 0, 2));
			normalBullet = new PierceBullet(Side, new fk_Vector(Position), new fk_Vector(0, 0, -2));
		}
	}
}
