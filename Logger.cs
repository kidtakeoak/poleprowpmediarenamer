using Newtonsoft.Json.Linq;
using System.Text;

namespace PoleproWpMediaRenamer
{
    internal class Logger
    {
        /// <summary>
        /// 引数で指定されたLogIdに対応する文字列を返す
        /// </summary>
        public static string GetLog(string strLogId)
        {
            JObject jsonLog = new JObject();

            jsonLog["L0000"] = "アプリケーション起動";
            jsonLog["L0001"] = "アプリケーション終了";

            if (jsonLog[strLogId] != null)
            {
                return jsonLog[strLogId].ToString();
            }
            else
            {
                return "FATAL ERROR - GETLOG";
            }
        }



        /// <summary>
        /// AppLog.txtに追記する
        /// </summary>
        public static void Log(string strLog)
        {
            // App.txtの内容を読み取る
            string[] arrAppLog = [];

            StreamReader srAppLog = new StreamReader(CommonInfo.AppLogTxt, Encoding.GetEncoding("UTF-8"));

            while (srAppLog.Peek() != -1)
            {
                string strReadLine = srAppLog.ReadLine();

                if (strReadLine != "")
                {
                    Array.Resize(ref arrAppLog, arrAppLog.Length + 1);
                    arrAppLog[arrAppLog.Length - 1] = strReadLine;
                }
            }

            srAppLog.Close();

            // strLogの内容を書き込む
            string strNow = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            string strLogRecord = strNow + " | " + strLog;

            Array.Resize(ref arrAppLog, arrAppLog.Length + 1);
            arrAppLog[arrAppLog.Length - 1] = strLogRecord;

            StreamWriter swAppLog = new StreamWriter(CommonInfo.AppLogTxt, false, Encoding.GetEncoding("UTF-8"));

            for (int intA = 0; intA < arrAppLog.Length; intA++)
            {
                if (arrAppLog[intA] != "")
                {
                    swAppLog.WriteLine(arrAppLog[intA]);
                }
            }

            swAppLog.Close();
        }
    }
}
