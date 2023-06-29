using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SerialLogCore : MonoBehaviour
{
	[Header("Components")]
	[SerializeField] TextMeshProUGUI logMessageText;
	[SerializeField] Image image;

	[Header("TweenParameters")]
	[SerializeField] float duration;
	[SerializeField] Ease ease;

	public TextMeshProUGUI LogMessageText => logMessageText;
	public Image Image => image;

	public float Duration => duration;
	public Ease Ease => ease;
}
