using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    public class PlayerController : ObjectComponentBase<PlayerCore> {

		[Header("Components")]
		[SerializeField] DeviceDataReceiver deviceDataReceiver;

		[SerializeField] DeviceThreasholds threasholds;

		/* Properties */
		KeyController key;
		DeviceController device;

		public ControllerBase ActiveController { get; private set; }

		//--------------------------------------------------

		[Serializable]
		public class DeviceThreasholds
		{
			[Header("デバイス")]
			[SerializeField,Tooltip("上傾き")] float upThreashold	= .7f;
			[SerializeField,Tooltip("下傾き")] float downThreashold	= .1f;
			[SerializeField,Tooltip("横傾き")] float sideThreashold	= .5f;

			public float Up => upThreashold;
			public float Down => downThreashold;
			public float Side => sideThreashold;
		}

		//--------------------------------------------------

		public abstract class ControllerBase {
			public abstract bool IsUp		{ get; }
			public abstract bool IsDown		{ get; }
            public abstract bool IsLeft		{ get; }
            public abstract bool IsRight	{ get; }
            
			public abstract bool IsPressed	{ get; }
			//public abstract bool IsReleased { get; }

			public abstract bool IsAxisX { get; }

			public bool IsInputAnyKey()
			{
				if (IsUp || IsDown || IsLeft || IsRight || IsPressed) {
					return true;
				}

				return false;
			}
		}

        /// <summary> キー入力 </summary>
        public class KeyController:ControllerBase
        {
			public override bool IsUp		=> Input.GetKeyDown(KeyCode.UpArrow);
			public override bool IsDown		=> Input.GetKeyDown(KeyCode.DownArrow);
			public override bool IsLeft		=> Input.GetKeyDown(KeyCode.LeftArrow);
			public override bool IsRight	=> Input.GetKeyDown(KeyCode.RightArrow);

			public override bool IsPressed	=> Input.GetKeyDown(KeyCode.Space);
			//public override bool IsReleased => Input.GetKeyUp  (KeyCode.Space);

			public override bool IsAxisX => (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1);
		}

		/// <summary> デバイス入力 </summary>
		public class DeviceController : ControllerBase {
			DeviceDataReceiver receiver;
			DeviceThreasholds  threasholds;

			public DeviceController(DeviceDataReceiver receiver, DeviceThreasholds threasholds)
			{
				this.receiver = receiver;		this.threasholds = threasholds;
			}

			public override bool IsUp		=> (receiver.Gyro.y >= threasholds.Up);
			public override bool IsDown		=> (receiver.Gyro.y <= threasholds.Down && receiver.Gyro.y != 0);
			public override bool IsLeft		=> (receiver.Gyro.x >= threasholds.Side);
			public override bool IsRight	=> (receiver.Gyro.x <= -threasholds.Side);

			public override bool IsPressed	=> (receiver.IsPressed);
			//public override bool IsReleased => (receiver.IsPressed);

			public override bool IsAxisX	=> (Mathf.Abs(receiver.Gyro.x) >= threasholds.Side);
		}

		//--------------------------------------------------

		protected override void OnStart()
		{
			key = new KeyController();
			device = new DeviceController(deviceDataReceiver, threasholds);

			ActiveController = key;
		}

		protected override void OnUpdate()
		{
			// デバイスが入力されたら、そのデバイスに切り替え
			if (key.IsInputAnyKey()) {
				ActiveController = key;
			}

			else if (device.IsInputAnyKey()) {
				ActiveController = device;
			}
		}
	}
}
