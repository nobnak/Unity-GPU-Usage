using GpuUsageSys;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPUUsageLogger : MonoBehaviour {

	[SerializeField]
	protected Tuner tuner = new Tuner();

	#region unity
	private void OnEnable() {
		StartCoroutine(Process());
	}
	private void OnDisable() {
		StopAllCoroutines();
	}
	#endregion

	#region methods
	IEnumerator Process() {
		while (true) {
			var time = System.DateTime.Now;
			var stats = GPUStatList.Create();
			Debug.Log($"{time}\n{stats}");
			yield return new WaitForSeconds(tuner.interval);
		}
	}
	#endregion

	#region definition
	[System.Serializable]
	public class Tuner {
		public float interval = 3600f;
	}
	#endregion
}
