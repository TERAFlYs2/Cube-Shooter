using System;
public interface IAppStatsNotifier
{
	event Action<int> OnUpdateFps;
	event Action<int> OnUpdatePing;
}
