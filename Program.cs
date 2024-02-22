using System.Diagnostics;

namespace PoleproWpMediaRenamer
{
    internal static class Program
    {
        /// <summary>
        /// アプリケーションのメインエントリーポイント
        /// </summary>
        [STAThread]
        static void Main()
        {
            // アプリケーションの起動時処理
            CommonInfo.AppInit();

            // Config.txtファイルの情報をCommonInfoクラスに保持する
            CommonInfo.ReadConfig();

            // Log.txtファイルの情報をCommonInfoクラスに保持する
            CommonInfo.ReadLog();

            // FormMainを表示する
            ApplicationConfiguration.Initialize();
            Application.Run(new FormMain());
        }
    }
}