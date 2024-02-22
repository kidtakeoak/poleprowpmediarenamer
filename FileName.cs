using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskBand;

namespace PoleproWpMediaRenamer
{
    public static class FileName
    {
        /// <summary>
        /// 任意文字列-連番で、ユニークなファイル名をリターンする
        /// </summary>
        public static string PatternA(string strFilePath, string strDefineString)
        {
            // 結果を格納する変数
            string strFileName = "";

            for (int intA = 1; intA < 10000; intA++)
            {
                string strNumber = "";

                if (intA < 10)
                {
                    strNumber = "000" + intA.ToString();
                }
                else if (intA < 100)
                {
                    strNumber = "00" + intA.ToString();
                }
                else if (intA < 1000)
                {
                    strNumber = "0" + intA.ToString();
                }
                else
                {
                    strNumber = intA.ToString();
                }

                string strTempName = strDefineString + "-" + strNumber;

                if (!CommonInfo.Log.Contains(strTempName))
                {
                    strFileName = strTempName;
                    break;
                }
            }

            return strFileName;
        }

        /// <summary>
        /// 今日の日付-連番で、ユニークなファイル名をリターンする
        /// </summary>
        public static string PatternB(string strFilePath)
        {
            // 結果を格納する変数
            string strFileName = "";

            // 今日の日付
            string strToday = DateTime.Now.ToString("yyyyMMdd");

            for (int intA = 1; intA < 10000; intA++)
            {
                string strNumber = "";

                if (intA < 10)
                {
                    strNumber = "000" + intA.ToString();
                }
                else if (intA < 100)
                {
                    strNumber = "00" + intA.ToString();
                }
                else if (intA < 1000)
                {
                    strNumber = "0" + intA.ToString();
                }
                else
                {
                    strNumber = intA.ToString();
                }

                string strTempName = strToday + "-" + strNumber;

                if (!CommonInfo.Log.Contains(strTempName))
                {
                    strFileName = strTempName;
                    break;
                }
            }

            return strFileName;
        }

        /// <summary>
        /// ファイルの最終更新日でユニークなファイル名を生成する
        /// </summary>
        public static string PatternC(string strFilePath)
        {
            // 結果を格納する変数
            string strFileName = "";

            // ファイルの最終更新日時
            string strLastUpdate = File.GetLastWriteTime(strFilePath).ToString("yyyyMMdd-HHmmss");

            if (!CommonInfo.Log.Contains(strLastUpdate))
            {
                strFileName = strLastUpdate;
            }

            return strFileName;
        }

        /// <summary>
        /// ランダムでユニークなファイル名を生成する
        /// </summary>
        public static string PatternD()
        {
            // 結果を格納する変数
            string strFileName;

            // 使用可能な文字
            string strAllowChar = "abcdefghijklmnopqrstuvwxyz0123456789";

            Random rand = new Random();

            while (true)
            {
                string strResult = "";

                for (int intA = 0; intA < 12; intA++)
                {
                    int intRandomIndex = rand.Next(0, strAllowChar.Length);
                    char charLetter = strAllowChar[intRandomIndex];

                    if (strResult == "")
                    {
                        strResult = charLetter.ToString();
                    }
                    else
                    {
                        strResult = strResult + charLetter.ToString();
                    }

                }

                if (!CommonInfo.Log.Contains(strResult))
                {
                    strFileName = strResult;
                    break;
                }
            }

            return strFileName;
        }



    }
}
