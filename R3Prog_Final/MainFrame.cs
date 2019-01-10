using System.Collections.Generic;
using FK_CLI;

namespace R3Prog_Final
{
	public class MainFrame
	{
		private static MainFrame _instance;
		public static MainFrame Instance => _instance;

		public static bool Create()
		{
			if (_instance != null) return false;
			_instance = new MainFrame();
			return true;
		}

		private readonly Stack<SceneBase> _sceneStack;

		private MainFrame()
		{
			fk_Material.InitDefault();
			_sceneStack = new Stack<SceneBase>();
		}

		public void PushNewScene(SceneBase scene)
		{
			_sceneStack.Push(scene);
			SetFkScene();
		}

		public void PopScene(int count)
		{
			for (int i = 0; i < count; i++)
				if (_sceneStack.Count > 1)
					_sceneStack.Pop();
			SetFkScene();
		}

		private void SetFkScene()
		{
			FKSettings.GetInstance().MainWindow.SetScene(_sceneStack.Peek().MainScene, false);
			FKSettings.GetInstance().StateWindow.SetScene(_sceneStack.Peek().StateScene, false);
		}

		public SceneBase NowScene => _sceneStack.Peek();

		public void MainLoop(string mainWindowName, fk_Dimension mainSize, string stateWindowName,
			fk_Dimension stateSize, SceneBase firstScene)
		{
			fk_Material.InitDefault();
			FKSettings.Create(new fk_AppWindow
			{
				Size = mainSize,
				BGColor = fk_Material.MatBlack.Diffuse,
				Scene = {BlendStatus = true},
				WindowName = mainWindowName,
			}, new fk_AppWindow
			{
				Size = stateSize,
				BGColor = fk_Material.DarkBlue.Diffuse,
				WindowName = stateWindowName,
			});
			PushNewScene(firstScene);

			FKSettings.WindowOpen();
			while (FKSettings.Update())
			{
				_sceneStack.Peek().Run();
			}
		}
	}
}
