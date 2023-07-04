using UnityEngine;
using System.IO.Ports;
using System.Threading;
using System;
using Cysharp.Threading.Tasks;

/// <summary>
/// �f�[�^�𑗎�M����A�V���A���ʐM�N���X
/// </summary>
public class SerialHandler : MonoBehaviour
{
	/// <summary>
	/// �V���A���ʐM�ŁA�f�[�^���󂯎�������̃C�x���g
	/// </summary>
	public event Action OnDataReceived;

    [Header("Serial Options")]
    [SerializeField, Tooltip("�J�����|�[�g�̃{�[���[�g")] int baudRate = 9600;

    SerialPort serialPort;      // �V���A���|�[�g

    // thread
    Thread readingThread;       // �ǂݎ��p�̃X���b�h
    bool isThreadRunning;       // �X���b�h���s���t���O

    // message
    string message;
    bool isNewMessageReceived;  // �V�������b�Z�[�W���󂯎�������ǂ���

    CancellationToken token;

    /// <summary> �|�[�g���L���� </summary>
    public bool IsPortEnable => serialPort != null;

	//--------------------------------------------------

	// �J�n���Ƀ|�[�g���J��
	async void Awake()
    {
        token = this.GetCancellationTokenOnDestroy();

		// �|�[�g�I�������܂őҋ@
		await UniTask.WaitUntil(() => SerialSelector.TargetPortName != null, cancellationToken: token);

        Open();
    }

    void Update()
    {
        // �󂯎������A�󂯎�莞�̏��������s
        if(isNewMessageReceived) {
             OnDataReceived();
        }

        isNewMessageReceived = false;
    }

    // �I�����Ƀ|�[�g�����
	private void OnDestroy()
	{
		Close();
	}

    //--------------------------------------------------

    // �V���A���|�[�g���J��
    void Open()
    {
        if (SerialSelector.TargetPortName != null) {
            serialPort = new SerialPort(SerialSelector.TargetPortName, baudRate, Parity.None, 8, StopBits.One);        // �|�[�g�C���X�^���X�쐬
            serialPort.Open();                                      // �쐬�����|�[�g���J��

            isThreadRunning = true;                                 // ���s���t���O���Ă�

            readingThread = new Thread(Read);                              // �X���b�h�쐬
            readingThread.Start();                                         // �X���b�h�J�n(�ǂݍ���)

            print("port was setuped.");
        }
    }

    // �V���A���|�[�g�����
    void Close()
    {
        // �t���O�~�낷
        isNewMessageReceived = false;
        isThreadRunning = false;

        if (readingThread != null && readingThread.IsAlive) {
            // �X���b�h���I������܂őҋ@
            print("waiting");
            readingThread.Join();

            print("Thread was joined.");
        }

        // �|�[�g���J���Ă�����A����
        if (serialPort != null && serialPort.IsOpen) {
            serialPort.Close();         // ����
            serialPort.Dispose();       // ���\�[�X�J��

            print("port was closed.");
        }

        else {
            Debug.LogWarning("����ɏI�����܂���ł���");
        }
    }

	//--------------------------------------------------

	// �V���A���|�[�g�ɓǂݍ���
	void Read()
    {
        while (isThreadRunning && serialPort != null && serialPort.IsOpen) {

            try {
                // �|�[�g����̃o�C�g����0��葽��������A�ǂݍ���
                if (serialPort.BytesToRead > 0) {
                    message = serialPort.ReadLine();            // �f�[�^�ǂݍ���
                    isNewMessageReceived = true;                // ���b�Z�[�W�󂯎��t���O���Ă�
                }
            }

            // ��O
            catch(Exception exception) {
                Debug.LogWarning(exception.Message);
                break;
            }
        }
    }

	/// <summary>
	/// �V���A���|�[�g�ɏ�������
	/// </summary>
	/// <param name="message">�������ޕ�����</param>
	public void Write(string message)
    {
        // ��������
        try {
            serialPort.Write(message);
        }

        // �x��
        catch (Exception exception){
            Debug.LogWarning(exception.Message);
        }
    }

    //--------------------------------------------------

    /// <summary>
    /// �R���}�ŋ�؂�ꂽ���b�Z�[�W��Ԃ�
    /// </summary>
    public string[] GetSplitedData()
    {
        // �󂯎�������b�Z�[�W����؂�
        return message.Split(",");
    }
}
