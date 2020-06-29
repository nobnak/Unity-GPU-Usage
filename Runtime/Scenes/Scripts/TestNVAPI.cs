using NvAPIWrapper.GPU;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GpuUsageSys.Examples {

	[ExecuteAlways]
	public class TestNVAPI : MonoBehaviour {

		#region unity
		private void OnEnable() {
			var tmp = new StringBuilder();

			var stats = GPUStatList.Create();
			Debug.Log(stats);
		}
		#endregion
	}
}
