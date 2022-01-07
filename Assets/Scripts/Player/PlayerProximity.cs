using FMOD.Studio;
using UnityEngine;

public class PlayerProximity : MonoBehaviour
//Sound use only
{
	public GameObject threat;
	private EventInstance instance;
	public FMODUnity.EventReference fmodEvent;
	private PlayerStats playerStats;
	private void Start() 
	{
		instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
		instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
		instance.start();
		playerStats = GetComponent<PlayerStatsLoader>().playerStats;
	}
	private void Update() 
	{
		if (!playerStats.PlayerDied) 
		{
			float distance = Vector3.Distance(transform.position, threat.transform.position);
			float hp = playerStats.Health / playerStats.MaxHealth * 100;
			instance.setParameterByName("Hp",hp);
			instance.setParameterByName("How Far To Enemy", distance);
		}
		else 
		{
			instance.stop(STOP_MODE.IMMEDIATE);
			instance.start();
		}
	}
	public void StopMusic() {
		instance.stop(STOP_MODE.ALLOWFADEOUT);
	}
}
