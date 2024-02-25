namespace PoleproWpMediaRenamer
{
    internal static class Program
    {
        /// <summary>
        /// �A�v���P�[�V�����̃��C���G���g���[�|�C���g
        /// </summary>
        [STAThread]
        static void Main()
        {
            // �A�v���P�[�V�����̋N��������
            CommonInfo.AppInit();

            // �A�v���P�[�V�����J�n�����O�ɋL�^
            Logger.Log(Logger.GetLog("L0000"));

            // Config.txt�t�@�C���̏���CommonInfo�N���X�ɕێ�����
            CommonInfo.ReadConfig();

            // FileNameLog.txt�t�@�C���̏���CommonInfo�N���X�ɕێ�����
            CommonInfo.ReadFileNameLog();

            // FormMain��\������
            ApplicationConfiguration.Initialize();
            Application.Run(new FormMain());
        }
    }
}