using Newtonsoft.Json.Linq;

namespace PoleproWpMediaRenamer
{
    internal class Message
    {
        /// <summary>
        /// 引数で指定されたMessageIdに対応する文字列を返す
        /// </summary>
        public static string GetMessage(string strMessageId)
        {
            JObject jsonMessage = new JObject();

            jsonMessage["M0000"] = "同じ名前のファイルは、同時にドロップできません。";
            jsonMessage["M0001"] = "除外対象が選択されていません。";
            jsonMessage["M0002"] = "使用可能な文字は、半角アルファベット(小文字)、半角数字のみです。";
            jsonMessage["M0003"] = "任意文字列は、最大12文字です。";
            jsonMessage["M0004"] = "これまでに生成したファイル名の履歴をリセットしますか？";
            jsonMessage["M0005"] = "後悔しないのですね？";
            jsonMessage["M0006"] = "まずはファイルをドラッグ＆ドロップしてください。";
            jsonMessage["M0007"] = "変換できるファイルがありません。";
            jsonMessage["M0008"] = "任意文字列が適切に入力されていません。";
            jsonMessage["M0009"] = "出力フォルダを確認してください。";
            jsonMessage["M0010"] = "リネーム処理が完了しました";

            if (jsonMessage[strMessageId] != null)
            {
                return jsonMessage[strMessageId].ToString();
            }
            else
            {
                return "FATAL ERROR - GETMESSAGE";
            }
        }



        /// <summary>
        /// 引数で指定されたCaptionIdに対応する文字列を返す
        /// </summary>
        public static string GetCaption(string strCaptionId)
        {
            JObject jsonCaption = new JObject();

            jsonCaption["C0000"] = "WPメディア変換くん - エラー";
            jsonCaption["C0001"] = "WPメディア変換くん - 確認";
            jsonCaption["C0002"] = "WPメディア変換くん - 完了";

            if (jsonCaption[strCaptionId] != null)
            {
                return jsonCaption[strCaptionId].ToString();
            }
            else
            {
                return "FATAL ERROR - GETCAPTION";
            }
        }
    }
}
