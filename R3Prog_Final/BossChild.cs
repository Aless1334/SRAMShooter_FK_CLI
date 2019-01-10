using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FK_CLI;

namespace R3Prog_Final
{
	class BossChild : Character
	{
		private const int LifeMax = 1;
		private Boss _parent;
		private fk_Vector _relativePosition;

		public BossChild(fk_Vector position, Boss parent, fk_Vector relativePosition) : base(CharacterSide.Enemy,
			LifeMax, position)
		{
			_parent = parent;
			_relativePosition = relativePosition;

			Model = new fk_Model()
			{
				Shape = new fk_Cone(4, 2.0, 4),
				Material = fk_Material.BambooGreen,
				BMode = fk_BoundaryMode.CAPSULE,
				BDraw = false,
				BLineColor = fk_Material.Red.Diffuse
			};
			Model.SetCapsule(new fk_Vector(0, 0, 0), new fk_Vector(0, 0, 0.01), 3);

			GameInfo.GetInstance().AddEnemySide(this);
		}

		protected override void UniqueMove()
		{
			Position = _parent.Position + _relativePosition;
		}

		protected override void DeadProcess()
		{
			_parent.DeadChild(this);
			base.DeadProcess();
		}
	}
}
