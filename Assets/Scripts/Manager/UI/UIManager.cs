using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameController.UI {
    public class UIManager : MonoBehaviour {

		//--------------------------------------------------
		// inspector List
		[SerializeField, Tooltip("���߂ɗL���ɂȂ�UIGroup")] UIGroup startUIGroup;
		[SerializeField] List<UIGroup> _uiGroupList = new List<UIGroup>();

		// static List
		static readonly List<UIGroup> uiGroupList = new List<UIGroup>();

		// ���ݗL����UIGroup
		static UIGroup currentUIGroup;

		// UIGroup�̗���
		static Stack<UIGroup> histroy = new Stack<UIGroup>();

		//--------------------------------------------------
		private void Awake()
		{
			ResetUIHistory();			// �������Z�b�g

			foreach (var uiGroup in _uiGroupList) {
				uiGroup.Hide();					// �o�^���ꂽUI���A�S�Ĕ�\���ɂ���
				uiGroup.Initialize();			
				uiGroupList.Add(uiGroup);		// �C���X�y�N�^�[�̃��X�g��static���X�g�ɓo�^
			}

			// ����UIGroup��\��
			if (startUIGroup != null) {
				ShowUIGroup(startUIGroup);
			}
		}

		private void OnDestroy()
		{
			uiGroupList.Clear();
		}

		//--------------------------------------------------
		/// <summary>
		/// <typeparamref name="T"/>�^��UIGroup���擾����
		/// </summary>
		/// <typeparam name="T">UIGroup�̌^</typeparam>
		/// <returns><typeparamref name="T"/>�^��UIGroup�̃C���X�^���X</returns>
		public static T GetUIGroup<T>() where T : UIGroup
		{
			// T�^��UIGroup����������
			foreach (var uiGroup in uiGroupList) {
				if (uiGroup is T targetUI) {
					return targetUI;
				}
			}

			return null;
		}

		//--------------------------------------------------
		/// <summary>
		/// UIGroup��\������
		/// </summary>
		/// <typeparam name="T">UIGroup�̌^</typeparam>
		/// <param name="remember">�����Ɏc����</param>
		public static void ShowUIGroup<T>(bool remember = true) where T : UIGroup
		{
			foreach (var uiGroup in uiGroupList) {
				if (uiGroup is T) {
					if (currentUIGroup) {
						// �����Ɏc���ꍇ�AStack�ɒǉ�
						if (remember) {
							histroy.Push(currentUIGroup);
						}

						currentUIGroup.Hide();      // ���݂�UI���\���ɂ���
					}

					uiGroup.Show();                 // UI�\��
					currentUIGroup = uiGroup;       // ���݂�UI���w�肳�ꂽUI�ɂ���

					ShowCommon();
					return;
				}
			}
		}

		/// <summary>
		/// UIGroup��\������
		/// </summary>
		/// <param name="uiGroup">�\������UIGroup�̃C���X�^���X</param>
		public static void ShowUIGroup(UIGroup uiGroup)
		{
			if (currentUIGroup) {
				histroy.Push(currentUIGroup);
				currentUIGroup.Hide();
			}

			if (uiGroup != null) {
				uiGroup.Show();
				currentUIGroup = uiGroup;
			}

			ShowCommon();
		}

		/// <summary>
		/// UIGroup��\������
		/// </summary>
		/// <param name="uiGroup">�\������UIGroup�̃C���X�^���X</param>
		/// <param name="remember">�����Ɏc����</param>
		public static void ShowUIGroup(UIGroup uiGroup, bool remember = true)
		{
			if (currentUIGroup) {
				if (remember) {
					histroy.Push(currentUIGroup);
				}

				currentUIGroup.Hide();
			}

			uiGroup.Show();
			currentUIGroup = uiGroup;

			ShowCommon();
		}

		/// <summary>
		/// ��O��UIGroup��\������
		/// </summary>
		public static void ShowLastUIGroup()
		{
			if (histroy.Count != 0) {
				ShowUIGroup(histroy.Pop(), false);      // ����������o���ĕ\��
			}
		}

		//--------------------------------------------------
		/// <summary>
		/// �S�Ă�UI���\���ɂ���
		/// </summary>
		public static void HideAllUIGroups()
		{
			foreach (var uiGroup in uiGroupList) {
				uiGroup.Hide();
				currentUIGroup = null;      // ���݂�UI�����Z�b�g
			}
		}

		/// <summary>
		/// ���������Z�b�g����
		/// </summary>
		public static void ResetUIHistory()
		{
			histroy.Clear();
		}

		//-------------------------------------------
		// ���ʏ���
		static void ShowCommon()
		{
			print($"{nameof(currentUIGroup)}={currentUIGroup}");
		}
	}
}