namespace PoleproWpMediaRenamer
{
    public static class FileName
    {
        /// <summary>
        /// 選択中のファイル名パターンに従い、ユニークなファイル名をリターンする
        /// </summary>
        public static string Create(string strFilePath)
        {
            string strFileName = "";

            // (1)任意文字列 + 連番
            if (CommonInfo.FileNamePattern == "0")
            {
                for (int intA = 1; intA < 10000; intA++)
                {
                    // 4桁の連番を生成
                    string strNumber;

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

                    string strTempName = CommonInfo.DefineString + "-" + strNumber;

                    if (!CommonInfo.FileNameLog.Contains(strTempName))
                    {
                        strFileName = strTempName;
                        break;
                    }
                }
            }

            // (2)今日の日付 + 連番
            else if (CommonInfo.FileNamePattern == "1")
            {
                // 今日の日付を8桁の文字列で生成
                string strToday = DateTime.Now.ToString("yyyyMMdd");

                for (int intA = 1; intA < 10000; intA++)
                {
                    // 4桁の連番を生成
                    string strNumber;

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

                    if (!CommonInfo.FileNameLog.Contains(strTempName))
                    {
                        strFileName = strTempName;
                        break;
                    }
                }
            }

            // (3)ファイル更新時刻
            else if (CommonInfo.FileNamePattern == "2")
            {
                // ファイルの最終更新日時
                string strTempName = File.GetLastWriteTime(strFilePath).ToString("yyyyMMdd-HHmmss");

                if (!CommonInfo.FileNameLog.Contains(strTempName))
                {
                    strFileName = strTempName;
                }
            }

            // (4)ランダム
            else
            {
                // 使用可能な文字
                string strAllowChar = CommonInfo.AllowChar;

                Random rand = new Random();

                while (true)
                {
                    string strTempName = "";

                    for (int intA = 0; intA < 12; intA++)
                    {
                        int intRandomIndex = rand.Next(0, strAllowChar.Length);
                        char charLetter = strAllowChar[intRandomIndex];

                        if (strTempName == "")
                        {
                            strTempName = charLetter.ToString();
                        }
                        else
                        {
                            strTempName = strTempName + charLetter.ToString();
                        }

                    }

                    if (!CommonInfo.FileNameLog.Contains(strTempName))
                    {
                        strFileName = strTempName;
                        break;
                    }
                }
            }

            // 結果をリターン
            return strFileName;
        }
    }
}
