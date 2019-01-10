using System;
using FK_CLI;

namespace R3Prog_Final
{
	class FKSettings
	{
		private static FKSettings _instance;

		public static bool Create(fk_AppWindow mainWindow, fk_AppWindow stateWindow, bool force = false)
		{
			if (_instance != null && !force) return false;
			_instance = new FKSettings(mainWindow, stateWindow);
			return true;
		}

		public static FKSettings GetInstance()
		{
			return _instance;
		}

		private FKSettings(fk_AppWindow mainWindow, fk_AppWindow stateWindow)
		{
			MainWindow = mainWindow;
			StateWindow = stateWindow;
		}

		public fk_AppWindow MainWindow { get; }
		public fk_AppWindow StateWindow { get; }

		public static void WindowOpen()
		{
			if (_instance == null) return;
			_instance.StateWindow.Open();
			_instance.MainWindow.Open();
		}

		public static bool Update()
		{
			if (_instance == null) return false;
			if (!_instance.MainWindow.Update()) return false;
			if (!_instance.StateWindow.Update()) return false;
			return true;
		}
	}
}
