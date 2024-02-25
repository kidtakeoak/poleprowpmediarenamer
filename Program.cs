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

            // アプリケーション開始をログに記録
            Logger.Log(Logger.GetLog("L0000"));

            // Config.txtファイルの情報をCommonInfoクラスに保持する
            CommonInfo.ReadConfig();

            // FileNameLog.txtファイルの情報をCommonInfoクラスに保持する
            CommonInfo.ReadFileNameLog();

            // FormMainを表示する
            ApplicationConfiguration.Initialize();
            Application.Run(new FormMain());
        }
    }
}