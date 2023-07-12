using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialSelectedCondition : SerialSettingCondition {
	public override void CheckCondition()
	{
		Condition = SerialSelector.IsMultiple;
	}
}
