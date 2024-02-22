using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoleproWpMediaRenamer
{
    public class FileInfo
    {
        // ファイルパス
        private string strFilePath;

        // オリジナルファイル名
        private string strBeforeFileName;

        // リネームファイル名
        private string strAfterFileName;

        // 除外フラグ
        private string strRemoveFlag;

        // 処理完了フラグ
        private string strRenameFlag;

        // 削除フラグ
        private string strDeleteFlag;

        public string FilePath { get => strFilePath; set => strFilePath = value; }
        public string BeforeFileName { get => strBeforeFileName; set => strBeforeFileName = value; }
        public string AfterFileName { get => strAfterFileName; set => strAfterFileName = value; }
        public string RemoveFlag { get => strRemoveFlag; set => strRemoveFlag = value; }
        public string RenameFlag { get => strRenameFlag; set => strRenameFlag = value; }
        public string DeleteFlag { get => strDeleteFlag; set => strDeleteFlag = value; }
    }
}
