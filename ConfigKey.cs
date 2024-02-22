using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoleproWpMediaRenamer
{
    public static class ConfigKey
    {
        // ファイル名パターン
        private static string strFileNamePattern = "FileNamePattern";

        // 任意文字列
        private static string strDefineString = "DefaultString";

        // 出力フォルダ
        private static string strExportDir = "ExportDir";

        // 出力フォルダを開く
        private static string strOpenDir = "OpenDir";

        // オリジナルファイルを削除
        private static string strDeleteFile = "DeleteFile";

        public static string FileNamePattern { get => strFileNamePattern; }

        public static string DefineString { get => strDefineString; }

        public static string ExportDir { get => strExportDir; }

        public static string OpenDir { get => strOpenDir; }

        public static string DeleteFile { get => strDeleteFile; }
    }
}
