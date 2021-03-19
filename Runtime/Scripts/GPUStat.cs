using NvAPIWrapper.GPU;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GpuUsageSys {



	public struct GPUStat {

		public readonly uint gpuId;
		public readonly string name;
		public readonly int GPUPercentage;
		public readonly int VideoPercentage;
		public readonly uint AvailableMemory;
		public readonly uint TotalMemory;

		public GPUStat(PhysicalGPU gpu) {
			var usage = gpu.UsageInformation;
			var mem = gpu.MemoryInformation;

			this.gpuId = gpu.GPUId;
			this.name = gpu.FullName;

			this.GPUPercentage = usage.GPU.Percentage;
			this.VideoPercentage = usage.VideoEngine.Percentage;
			this.AvailableMemory = mem.CurrentAvailableDedicatedVideoMemoryInkB;
			this.TotalMemory = mem.AvailableDedicatedVideoMemoryInkB;
		}

		#region interface

		#region object
		public override string ToString() {
			return $"{name} (id={gpuId}) : GPU {GPUPercentage}%, Video {VideoPercentage}%, Available Memory {AvailableMemory * 1e-3f:f1}/{TotalMemory * 1e-3f:f1}MB";
		}
		#endregion

		#endregion
	}

	public struct GPUStatList {

		public readonly GPUStat[] list;

		public GPUStatList(GPUStat[] list) { this.list = list; }

		#region interface

		#region object
		public override string ToString() {
			var tmp = new StringBuilder();
			foreach (var g in list)
				tmp.AppendLine(g.ToString());
			return tmp.ToString();
		}
		#endregion

		public static IEnumerable<GPUStat> Stats {
			get {
				foreach (var v in PhysicalGPU.GetPhysicalGPUs()) {
					var created = false;
					var stat = default(GPUStat);

					try {
						stat = new GPUStat(v);
						if (!stat.Equals(default))
							created = true;
					} catch (System.Exception e) {
						//Debug.LogWarning(e);
					}

					if (created)
						yield return stat;
				}
			}
		}
		public static GPUStatList Create() {
			var list = Stats.ToArray();
			return new GPUStatList(list);
		}
		#endregion
	}
}
