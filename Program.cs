using System.Diagnostics;

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

            // Config.txt�t�@�C���̏���CommonInfo�N���X�ɕێ�����
            CommonInfo.ReadConfig();

            // Log.txt�t�@�C���̏���CommonInfo�N���X�ɕێ�����
            CommonInfo.ReadLog();

            // FormMain��\������
            ApplicationConfiguration.Initialize();
            Application.Run(new FormMain());
        }
    }
}