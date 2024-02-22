using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoleproWpMediaRenamer
{
    public static class CommonInfo
    {
        // ==== アプリケーション基礎情報 ====

        // アプリケーション実行ディレクトリ
        private static string strAppDir;

        // AppDataフォルダパス
        private static string strAppDataDir;

        // Config.txtファイルパス
        private static string strConfigTxt;

        // Log.txtファイルパス
        private static string strLogTxt;

        public static string AppDir { get => strAppDir; set => strAppDir = value; }

        public static string AppDataDir { get => strAppDataDir; set => strAppDataDir = value; }

        public static string ConfigTxt { get => strConfigTxt; set => strConfigTxt = value; }

        public static string LogTxt { get => strLogTxt; set => strLogTxt = value; }

        // ==== 設定情報 ====

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

        // ==== Log情報 ====

        // 過去に生成済みのファイル名
        private static string[] arrLog;

        public static string[] Log { get => arrLog; set => arrLog = value; }

        /// <summary>
        /// Log.txtの情報を静的メンバーに保持する
        /// </summary>
        public static void ReadLog()
        {
            // Log.txtの内容を格納する配列
            string[] arrLogRecord = [];

            StreamReader srLog = new StreamReader(LogTxt, Encoding.GetEncoding("UTF-8"));

            while (srLog.Peek() != -1)
            {
                string strReadLine = srLog.ReadLine();

                if (strReadLine != "")
                {
                    Array.Resize(ref arrLogRecord, arrLogRecord.Length + 1);
                    arrLogRecord[arrLogRecord.Length - 1] = strReadLine;
                }
            }

            srLog.Close();

            // 静的メンバーに保持
            Log = arrLogRecord;
        }

        /// <summary>
        /// 静的メンバーにファイル名を追加する
        /// </summary>
        public static void AppendLog(string strFileName)
        {
            // Log.txtの内容を格納する配列
            string[] arrLogRecord = Log;

            Array.Resize(ref arrLogRecord, arrLogRecord.Length + 1);
            arrLogRecord[arrLogRecord.Length - 1] = strFileName;

            // 静的メンバーに保持
            Log = arrLogRecord;
        }


        /// <summary>
        /// 静的メンバーの情報をLog.txtに書き込む
        /// </summary>
        public static void WriteLog()
        {
            // Log.txtに書き込む配列
            string[] arrLogRecord = Log;

            Array.Sort(arrLogRecord);

            // ==== Log.txtに書き込み ====
            StreamWriter swLog = new StreamWriter(LogTxt, false, Encoding.GetEncoding("UTF-8"));

            for (int intA = 0; intA < arrLogRecord.Length; intA++)
            {
                if (arrLogRecord[intA] != "")
                {
                    swLog.WriteLine(arrLogRecord[intA]);
                }
            }

            swLog.Close();
        }

        // ==== WordPressにアップロードできるファイル拡張子 ====

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

        public static string[] AllowExtension()
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

        // ==== 公式リファレンス ====
        private static string strReferenceUrl = "https://polepro.blog/poleprowpmediarenamer/";

        public static string ReferenceUrl { get => strReferenceUrl; }

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

            // Config.txt
            string strConfigTxt = AppDataDir + "\\Config.txt";
            if (!File.Exists(strConfigTxt))
            {
                FileStream fsConfig = File.Create(strConfigTxt);
                fsConfig.Close();
            }
            ConfigTxt = strConfigTxt;

            // Log.txt
            string strLogTxt = AppDataDir + "\\Log.txt";
            if (!File.Exists(strLogTxt))
            {
                FileStream fsLog = File.Create(strLogTxt);
                fsLog.Close();
            }
            LogTxt = strLogTxt;
        }
    }
}
