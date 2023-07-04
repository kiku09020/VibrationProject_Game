using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameController.UI {
    public class UIGroup : MonoBehaviour {

		/// <summary> UI�̏����� </summary>
		public virtual void Initialize() { }

		/// <summary> UI���\���ɂ��� </summary>
		public virtual void Hide() => gameObject.SetActive(false);

		/// <summary> UI��\������ </summary>
		public virtual void Show() => gameObject.SetActive(true);
	}
}