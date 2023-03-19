using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class showGameInfoHud : MonoBehaviour
{
	public List<hudElementInfo> hudElements = new List<hudElementInfo> ();

	public enum elementType
	{
		Text,
		Slider,
		Panel
	}

	hudElementInfo currentHudElementInfo;

	rectTransformInfo currentRectTransformInfo;


	public GameObject getHudElement (string parentName, string elementName)
	{
		int hudElementsCount = hudElements.Count;

		for (int i = 0; i < hudElementsCount; i++) {
			currentHudElementInfo = hudElements [i];

			if (currentHudElementInfo.name.Equals (parentName)) {

				int rectTransformListCount = currentHudElementInfo.rectTransformList.Count;

				for (int j = 0; j < rectTransformListCount; j++) {
					currentRectTransformInfo = currentHudElementInfo.rectTransformList [j];

					if (currentRectTransformInfo.name.Equals (elementName)) {
						return currentRectTransformInfo.rectTransform.gameObject;
					}
				}
			}
		}

		return null;
	}

	public List<GameObject> getHudElements (string parentName, List<string> elementNames)
	{
		List<GameObject> hudElementList = new  List<GameObject> ();

		int hudElementsCount = hudElements.Count;

		for (int i = 0; i < hudElementsCount; i++) {
			currentHudElementInfo = hudElements [i];

			if (currentHudElementInfo.name.Equals (parentName)) {
				int rectTransformListCount = currentHudElementInfo.rectTransformList.Count;

				for (int j = 0; j < rectTransformListCount; j++) {
					currentRectTransformInfo = currentHudElementInfo.rectTransformList [j];

					if (elementNames.Contains (currentRectTransformInfo.name)) {
						hudElementList.Add (currentRectTransformInfo.rectTransform.gameObject);
					}
				}
			}
		}

		return hudElementList;
	}

	public GameObject getHudElementParent (string parentName)
	{
		int hudElementsCount = hudElements.Count;

		for (int i = 0; i < hudElementsCount; i++) {
			currentHudElementInfo = hudElements [i];

			if (currentHudElementInfo.name.Equals (parentName)) {
				return currentHudElementInfo.hudParent.gameObject;
			}
		}

		return null;
	}

	[System.Serializable]
	public class hudElementInfo
	{
		public string name;
		public GameObject hudParent;
		public List<rectTransformInfo> rectTransformList = new List<rectTransformInfo> ();
	}

	[System.Serializable]
	public class rectTransformInfo
	{
		public string name;
		public RectTransform rectTransform;
		public elementType hudElementyType;
	}
}