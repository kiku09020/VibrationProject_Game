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

		//�R���g���[���[���f�o�C�X�̂Ƃ��̂ݔ���
		if (PlayerController.ActiveCtrlIsDevice) {
			if(PlayerController.ActiveController.IsPressed) {
				Condition = false;
				isOnce = true;
			}
		}
	}
}
