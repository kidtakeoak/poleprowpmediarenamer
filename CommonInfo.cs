using System.Text;

namespace PoleproWpMediaRenamer
{
    public static class CommonInfo
    {
        // ==================
        // ==== 汎用変数 ====
        // ==================

        // ファイル名で使用可能な文字
        private static string strAllowChar = "abcdefghijklmnopqrstuvwxyz0123456789";

        public static string AllowChar { get => strAllowChar; }



        // ==================================
        // ==== アプリケーション基礎情報 ====
        // ==================================

        // アプリケーション実行ディレクトリ
        private static string strAppDir;

        // AppDataフォルダパス
        private static string strAppDataDir;

        // AppLog.txtファイルパス
        private static string strAppLogTxt;

        // Config.txtファイルパス
        private static string strConfigTxt;

        // Log.txtファイルパス
        private static string strFileNameLogTxt;

        public static string AppDir { get => strAppDir; set => strAppDir = value; }

        public static string AppDataDir { get => strAppDataDir; set => strAppDataDir = value; }

        public static string AppLogTxt { get => strAppLogTxt; set => strAppLogTxt = value; }

        public static string ConfigTxt { get => strConfigTxt; set => strConfigTxt = value; }

        public static string FileNameLogTxt { get => strFileNameLogTxt; set => strFileNameLogTxt = value; }



        // ==================
        // ==== 設定情報 ====
        // ==================

        // ファイル名パターン
        private static string strFileNamePattern;

        // 任意文字列
        private static string strDefineString;

        // 出力フォルダ
        private static string strExportDir;

        // 出力フォルダを開く
        private static string strOpenDir;

        // オリジナルファイルを削除
        private static string strDeleteFile;

        public static string FileNamePattern { get => strFileNamePattern; set => strFileNamePattern = value; }

        public static string DefineString { get => strDefineString; set => strDefineString = value; }

        public static string ExportDir { get => strExportDir; set => strExportDir = value; }

        public static string OpenDir { get => strOpenDir; set => strOpenDir = value; }

        public static string DeleteFile { get => strDeleteFile; set => strDeleteFile = value; }



        /// <summary>
        /// Config.txtを読み取り、内容を静的メンバーに保持する
        /// </summary>
        public static void ReadConfig()
        {
            // ==== デフォルト値をセット ====

            // ファイル名パターン
            FileNamePattern = "0";

            // 任意文字列
            DefineString = "media";

            // 出力フォルダ
            ExportDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\Media";

            // 出力フォルダを開く
            OpenDir = "1";

            // オリジナルファイルを削除
            DeleteFile = "0";

            // Config.txtの値を反映
            StreamReader srConfig = new StreamReader(ConfigTxt, Encoding.GetEncoding("UTF-8"));

            while (srConfig.Peek() != -1)
            {
                string strReadLine = srConfig.ReadLine();

                if (strReadLine.Contains("\t"))
                {
                    // ファイル名パターン
                    if (strReadLine.Split("\t")[0] == ConfigKey.FileNamePattern)
                    {
                        FileNamePattern = strReadLine.Split("\t")[1];
                    }

                    // 任意文字列
                    if (strReadLine.Split("\t")[0] == ConfigKey.DefineString)
                    {
                        if (strReadLine.Split("\t")[1] != "")
                        {
                            DefineString = strReadLine.Split("\t")[1];
                        }
                    }

                    // 出力フォルダ
                    if (strReadLine.Split("\t")[0] == ConfigKey.ExportDir)
                    {
                        ExportDir = strReadLine.Split("\t")[1];
                    }

                    // 出力フォルダを開く
                    if (strReadLine.Split("\t")[0] == ConfigKey.OpenDir)
                    {
                        OpenDir = strReadLine.Split("\t")[1];
                    }

                    // オリジナルファイルを削除
                    if (strReadLine.Split("\t")[0] == ConfigKey.DeleteFile)
                    {
                        DeleteFile = strReadLine.Split("\t")[1];
                    }
                }
            }

            srConfig.Close();
        }



        /// <summary>
        /// 静的メンバーに保持した設定情報を、Config.txtに書き込む
        /// </summary>
        public static void WriteConfig()
        {
            // ==== Config.txtに書き込む情報を生成 ====
            string[] arrConfigRecord = [];

            // ファイル名パターン
            Array.Resize(ref arrConfigRecord, arrConfigRecord.Length + 1);
            arrConfigRecord[arrConfigRecord.Length - 1] = ConfigKey.FileNamePattern + "\t" + FileNamePattern;

            // 任意文字列
            Array.Resize(ref arrConfigRecord, arrConfigRecord.Length + 1);
            arrConfigRecord[arrConfigRecord.Length - 1] = ConfigKey.DefineString + "\t" + DefineString;

            // 出力フォルダ
            Array.Resize(ref arrConfigRecord, arrConfigRecord.Length + 1);
            arrConfigRecord[arrConfigRecord.Length - 1] = ConfigKey.ExportDir + "\t" + ExportDir;

            // 出力フォルダを開く
            Array.Resize(ref arrConfigRecord, arrConfigRecord.Length + 1);
            arrConfigRecord[arrConfigRecord.Length - 1] = ConfigKey.OpenDir + "\t" + OpenDir;

            // オリジナルファイルを削除
            Array.Resize(ref arrConfigRecord, arrConfigRecord.Length + 1);
            arrConfigRecord[arrConfigRecord.Length - 1] = ConfigKey.DeleteFile + "\t" + DeleteFile;

            // ==== Config.txtに書き込み ====
            StreamWriter swConfig = new StreamWriter(ConfigTxt, false, Encoding.GetEncoding("UTF-8"));

            for (int intA = 0; intA < arrConfigRecord.Length; intA++)
            {
                swConfig.WriteLine(arrConfigRecord[intA]);
            }

            swConfig.Close();
        }



        // =====================================
        // ==== 生成したファイル名のLog情報 ====
        // =====================================

        // 過去に生成済みのファイル名
        private static string[] arrFileNameLog;

        public static string[] FileNameLog { get => arrFileNameLog; set => arrFileNameLog = value; }



        /// <summary>
        /// Log.txtの情報を静的メンバーに保持する
        /// </summary>
        public static void ReadFileNameLog()
        {
            // Log.txtの内容を格納する配列
            string[] arrLogRecord = [];

            StreamReader srFileNameLog = new StreamReader(FileNameLogTxt, Encoding.GetEncoding("UTF-8"));

            while (srFileNameLog.Peek() != -1)
            {
                string strReadLine = srFileNameLog.ReadLine();

                if (strReadLine != "")
                {
                    Array.Resize(ref arrLogRecord, arrLogRecord.Length + 1);
                    arrLogRecord[arrLogRecord.Length - 1] = strReadLine;
                }
            }

            srFileNameLog.Close();

            // 静的メンバーに保持
            FileNameLog = arrLogRecord;
        }



        /// <summary>
        /// 静的メンバーにファイル名を追加する
        /// </summary>
        public static void AppendFileNameLog(string strFileName)
        {
            // Log.txtの内容を格納する配列
            string[] arrLogRecord = FileNameLog;

            Array.Resize(ref arrLogRecord, arrLogRecord.Length + 1);
            arrLogRecord[arrLogRecord.Length - 1] = strFileName;

            // 静的メンバーに保持
            FileNameLog = arrLogRecord;
        }



        /// <summary>
        /// 静的メンバーの情報をLog.txtに書き込む
        /// </summary>
        public static void WriteFileNameLog()
        {
            // Log.txtに書き込む配列
            string[] arrLogRecord = FileNameLog;

            Array.Sort(arrLogRecord);

            // ==== Log.txtに書き込み ====
            StreamWriter swFileNameLog = new StreamWriter(FileNameLogTxt, false, Encoding.GetEncoding("UTF-8"));

            for (int intA = 0; intA < arrLogRecord.Length; intA++)
            {
                if (arrLogRecord[intA] != "")
                {
                    swFileNameLog.WriteLine(arrLogRecord[intA]);
                }
            }

            swFileNameLog.Close();
        }



        // =====================================================
        // ==== WordPressにアップロードできるファイル拡張子 ====
        // =====================================================
        public static string[] Extension
        {
            get => AllowExtension();
        }

        // 画像の拡張子一覧
        private static string strImageExtension = ".gif .heic .jpeg .jpg .png .svg .webp";

        // 文書の拡張子一覧
        private static string strDocumentExtension = ".doc .docx .key .odt .pdf .ppt .pptx .pps .ppsx .xls .xlsx";

        // 音声の拡張子一覧
        private static string strMusicExtension = ".mp3 .m4a .ogg .wav";

        // 動画の拡張子一覧
        private static string strMovieExtension = ".avi .mpg .mp4 .m4v .mov .ogv .vtt .wmv .3gp .3g2";

        private static string[] AllowExtension()
        {
            string[] arrExtension = [];

            // 画像の拡張子を配列に追加
            for (int intA = 0; intA < strImageExtension.Split(" ").Length; intA++)
            {
                if (strImageExtension.Split(" ")[intA] != "")
                {
                    Array.Resize(ref arrExtension, arrExtension.Length + 1);
                    arrExtension[arrExtension.Length - 1] = strImageExtension.Split(" ")[intA];
                }
            }

            // 文書の拡張子を配列に追加
            for (int intA = 0; intA < strDocumentExtension.Split(" ").Length; intA++)
            {
                if (strDocumentExtension.Split(" ")[intA] != "")
                {
                    Array.Resize(ref arrExtension, arrExtension.Length + 1);
                    arrExtension[arrExtension.Length - 1] = strDocumentExtension.Split(" ")[intA];
                }
            }

            // 音声の拡張子を配列に追加
            for (int intA = 0; intA < strMusicExtension.Split(" ").Length; intA++)
            {
                if (strMusicExtension.Split(" ")[intA] != "")
                {
                    Array.Resize(ref arrExtension, arrExtension.Length + 1);
                    arrExtension[arrExtension.Length - 1] = strMusicExtension.Split(" ")[intA];
                }
            }

            // 動画の拡張子を配列に追加
            for (int intA = 0; intA < strMovieExtension.Split(" ").Length; intA++)
            {
                if (strMovieExtension.Split(" ")[intA] != "")
                {
                    Array.Resize(ref arrExtension, arrExtension.Length + 1);
                    arrExtension[arrExtension.Length - 1] = strMovieExtension.Split(" ")[intA];
                }
            }

            return arrExtension;
        }



        // ==========================
        // ==== 公式リファレンス ====
        // ==========================
        private static string strReferenceUrl = "https://polepro.blog/poleprowpmediarenamer/";

        public static string ReferenceUrl { get => strReferenceUrl; }



        // ========================================
        // ==== アプリケーション開始時メソッド ====
        // ========================================

        /// <summary>
        /// アプリケーション基礎情報を静的メンバーに保持する
        /// </summary>
        public static void AppInit()
        {
            // アプリケーションの実行ディレクトリ
            string strAppDir = Application.ExecutablePath.Replace("\\" + Application.ExecutablePath.Split("\\")[Application.ExecutablePath.Split("\\").Length - 1], "");
            AppDir = strAppDir;

            // AppDataフォルダパス
            string strAppDataDir = AppDir + "\\AppData";
            if (!Directory.Exists(strAppDataDir))
            {
                Directory.CreateDirectory(strAppDataDir);
            }
            AppDataDir = strAppDataDir;

            // AppLog.txt
            string strAppLogTxt = AppDataDir + "\\AppLog-" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".txt";
            if (File.Exists(strAppLogTxt))
            {
                File.Delete(strAppLogTxt);
            }

            FileStream fsAppLog = File.Create(strAppLogTxt);
            fsAppLog.Close();

            AppLogTxt = strAppLogTxt;

            // 古いAppLog.txtを削除する
            DeleteOldAppLog();

            // Config.txt
            string strConfigTxt = AppDataDir + "\\Config.txt";
            if (!File.Exists(strConfigTxt))
            {
                FileStream fsConfig = File.Create(strConfigTxt);
                fsConfig.Close();
            }
            ConfigTxt = strConfigTxt;

            // FileNameLog.txt
            string strFileNameLogTxt = AppDataDir + "\\FineNameLog.txt";
            if (!File.Exists(strFileNameLogTxt))
            {
                FileStream fsFileNameLog = File.Create(strFileNameLogTxt);
                fsFileNameLog.Close();
            }
            FileNameLogTxt = strFileNameLogTxt;
        }



        /// <summary>
        /// 古いAppLog.txtを削除する
        /// </summary>
        private static void DeleteOldAppLog()
        {
            DirectoryInfo diAppData = new DirectoryInfo(AppDataDir);

            System.IO.FileInfo[] fiAppLogTxt = diAppData.GetFiles("*AppLog-*");
            foreach (System.IO.FileInfo f in fiAppLogTxt)
            {
                string strAppLogTxt = f.FullName;
                string strAppLogTxtName = strAppLogTxt.Split("\\")[strAppLogTxt.Split("\\").Length - 1];

                int intCheckTxtDay = int.Parse(strAppLogTxtName.Split("-")[1]);
                int intToday = int.Parse(DateTime.Now.ToString("yyyyMMdd"));

                // ログの保有期間は、2日前までとする
                if (intToday - intCheckTxtDay > 2)
                {
                    File.Delete(strAppLogTxt);
                }
            }
        }
    }
}
