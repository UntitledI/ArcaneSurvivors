using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class randomEventSystem : MonoBehaviour
{
	[Header ("Main Settings")]
	[Space]

	public bool randomEventsEnabled = true;

	public bool useSameIndexValue;
	public int sameIndexValueToUse;

	[Space]
	[Header ("Debug")]
	[Space]

	public bool showDebugPrint;

	[Space]
	[Header ("Random Events Settings")]
	[Space]

	public List<randomEventInfo> randomEventInfoList = new List<randomEventInfo> ();


	randomEventInfo currentEventInfo;


	public void callRandomEvent ()
	{
		if (!randomEventsEnabled) {
			return;
		}

		int randomIndex = Random.Range (0, randomEventInfoList.Count);

		if (useSameIndexValue) {
			randomIndex = sameIndexValueToUse;
		}

		if (randomIndex <= randomEventInfoList.Count - 1) {
			currentEventInfo = randomEventInfoList [randomIndex];

			if (currentEventInfo.eventEnabled) {
				if (showDebugPrint) {
					print (currentEventInfo.Name);
				}

				currentEventInfo.eventToActive.Invoke ();

				if (currentEventInfo.disableEventAfterActivation) {
					currentEventInfo.eventEnabled = false;
				}
			}
		}
	}

	[System.Serializable]
	public class randomEventInfo
	{
		public string Name;

		public bool eventEnabled = true;

		public bool disableEventAfterActivation;

		public UnityEvent eventToActive;
	}
}
