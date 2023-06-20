using UnityEngine;
using System.IO.Ports;
using System.Threading;
using System;

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
    [SerializeField,Tooltip("�J�����|�[�g��")] string openedPortName = "COM1";

    [SerializeField, Tooltip("�J�����|�[�g�̃{�[���[�g")] int baudRate = 9600;

    SerialPort serialPort;      // �V���A���|�[�g

    // thread
    Thread readingThread;       // �ǂݎ��p�̃X���b�h
    bool isThreadRunning;       // �X���b�h���s���t���O

    // message
    string message;
    bool isNewMessageReceived;  // �V�������b�Z�[�W���󂯎�������ǂ���

    //--------------------------------------------------

    // �J�n���Ƀ|�[�g���J��
    void Awake()
    {
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
        serialPort = new SerialPort(openedPortName, baudRate, Parity.None, 8, StopBits.One);        // �|�[�g�C���X�^���X�쐬
        serialPort.Open();                                      // �쐬�����|�[�g���J��

        isThreadRunning = true;                                 // ���s���t���O���Ă�

        readingThread = new Thread(Read);                              // �X���b�h�쐬
        readingThread.Start();                                         // �X���b�h�J�n(�ǂݍ���)

        print("port was setuped.");
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

            readingThread.Join(TimeSpan.FromSeconds(.5f));
        }

        // �|�[�g���J���Ă�����A����
        if (serialPort != null && serialPort.IsOpen) {
            serialPort.Close();         // ����
            serialPort.Dispose();       // ���\�[�X�J��

            print("port was closed.");
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
