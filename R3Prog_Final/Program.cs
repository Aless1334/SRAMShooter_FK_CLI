using System.Collections.Generic;
using FK_CLI;

namespace R3Prog_Final
{
	class Program
	{
		static void Main(string[] args)
		{
			MainFrame.Create();
			MainFrame.Instance.MainLoop("SRAMShooter DiveEdition", new fk_Dimension(540, 540), "SRAMShooter State",
				new fk_Dimension(420, 540), new TitleScene());
		}
	}
}
