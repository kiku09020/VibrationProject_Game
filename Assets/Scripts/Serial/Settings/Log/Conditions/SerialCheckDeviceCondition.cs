public class SerialCheckDeviceCondition : SerialSettingCondition {

	bool isOnce;

	public override void CheckCondition()
	{
		if(!isOnce) {
			Condition = true;
		}

		if (SerialSelector.IsMultiple) {
			Condition = false;
			isOnce = false;
		}

		//コントローラーがデバイスのときのみ判定
		if (PlayerController.ActiveCtrlIsDevice) {
			if(PlayerController.ActiveController.IsPressed) {
				Condition = false;
				isOnce = true;
			}
		}
	}
}
