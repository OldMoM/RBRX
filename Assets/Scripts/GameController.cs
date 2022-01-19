using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.VisualDebugging.Unity;

public class GameController : MonoBehaviour
{
	private static Systems _feature;

	public void Start()
	{
		var contexts = Contexts.sharedInstance;
		DontDestroyOnLoad(gameObject);

#if UNITY_EDITOR
		ContextObserverHelper.ObserveAll(contexts);
#endif

		// create random entity
		var rand = new System.Random();
		var context = Contexts.Default;
		var e = context.CreateEntity("PlayerDataHandleEntity");
		e.Add<VelocityCompnent>();
		e.Add<PlayerStateComponent>();
		e.Add<InputComponent>();


#if UNITY_EDITOR
		_feature = FeatureObserverHelper.CreateFeature(null);
#else
		// init systems, auto collect matched systems, no manual Systems.Add(ISystem) required
		_feature = new Feature(null);
#endif
		_feature.Initialize();
	}

	public void Update()
	{
		_feature.Execute();
		_feature.Cleanup();
	}
}
