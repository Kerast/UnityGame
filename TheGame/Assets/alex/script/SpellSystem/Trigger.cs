using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]public class Triggers {

	public enum SpatialTrigger{none,OnPhysicalCollide,OnTargetEntered,OnTargetInside,OnTargetExited};
	public enum TimeTrigger{Always,OnStart,OnUpdate,OnFinish};

	public SpatialTrigger spatialTrigger;
	public bool isSpatialTriggered =false;

	public TimeTrigger timeTrigger;
	public bool isTimeTriggered=false;
}
