using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace PoleproWpMediaRenamer
{
    public partial class FormMain : Form
    {
        private List<FileInfo> listFileInfo = new List<FileInfo>();
        private bool boolCharAlert = true;
        private bool boolLengthAlert = true;

        public FormMain()
        {
            InitializeComponent();

            // pbDropAreaへのドロップを許可する
            pbDropArea.AllowDrop = true;

            // フォーム初期値の設定
            if (CommonInfo.FileNamePattern == "0")
            {
                rbPatternA.Checked = true;
            }
            else if (CommonInfo.FileNamePattern == "1")
            {
                rbPatternB.Checked = true;
            }
            else if (CommonInfo.FileNamePattern == "2")
            {
                rbPatternC.Checked = true;
            }
            else
            {
                rbPatternD.Checked = true;
            }

            tbDefineString.Text = CommonInfo.DefineString;

            tbExportDir.Text = CommonInfo.ExportDir;

            if (CommonInfo.OpenDir == "1")
            {
                chkbOpenDir.Checked = true;
            }

            if (CommonInfo.DeleteFile == "1")
            {
                chkbDeleteFile.Checked = true;
            }
        }



        // ====================================================
        // ==== ドロップエリアへのファイルドロップイベント ====
        // ====================================================
        private void pbDropArea_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void pbDropArea_DragDrop(object sender, DragEventArgs e)
        {
            // ドロップされたファイルのパスを、配列で取得
            string[] arrFilePath = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (arrFilePath.Length > 0)
            {
                // 許可される拡張子一覧を取得
                string[] arrExtension = CommonInfo.Extension;

                // 許可される拡張子かどうか
                for (int intA = 0; intA < arrFilePath.Length; intA++)
                {
                    string strFilePath = arrFilePath[intA];
                    string strBeforeFileName = strFilePath.Split("\\")[strFilePath.Split("\\").Length - 1];

                    string strToLower = strBeforeFileName.ToLower();

                    for (int intB = 0; intB < arrExtension.Length; intB++)
                    {
                        if (strToLower.EndsWith(arrExtension[intB]))
                        {
                            bool boolAdd = true;

                            for (int intC = 0; intC < listFileInfo.Count; intC++)
                            {
                                if (listFileInfo[intC].FilePath == strFilePath)
                                {
                                    boolAdd = false;
                                    break;
                                }

                                // 同じ名前のファイルは許可しない
                                if (listFileInfo[intC].BeforeFileName == strBeforeFileName)
                                {
                                    boolAdd = false;
                                    System.Windows.Forms.MessageBox.Show(Message.GetMessage("M0000"), Message.GetCaption("C0000"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                    break;
                                }
                            }

                            if (boolAdd == true)
                            {
                                FileInfo fiAdd = new FileInfo();

                                fiAdd.FilePath = strFilePath;
                                fiAdd.BeforeFileName = strBeforeFileName;
                                fiAdd.AfterFileName = "";
                                fiAdd.RemoveFlag = "0";
                                fiAdd.RenameFlag = "0";
                                fiAdd.DeleteFlag = "0";

                                listFileInfo.Add(fiAdd);
                            }

                            break;
                        }
                    }
                }

            }

            // dgvMediaFileを更新
            UpdateDgvMediaFile();
        }



        /// <summary>
        /// DataGridViewを更新
        /// </summary>
        private void UpdateDgvMediaFile()
        {
            // dgvMediaFileを初期化
            dgvMediaFile.Rows.Clear();

            // DataGridViewに追加する情報を生成
            string[] arrDgvInfo = [];

            for (int intA = 0; intA < listFileInfo.Count; intA++)
            {
                if (listFileInfo[intA].RemoveFlag == "0")
                {
                    string strBeforeFileName = listFileInfo[intA].BeforeFileName;
                    string strAfterFileName = listFileInfo[intA].AfterFileName;
                    if (strAfterFileName == "")
                    {
                        strAfterFileName = "-";
                    }
                    string strRenameFlag = listFileInfo[intA].RenameFlag;

                    Array.Resize(ref arrDgvInfo, arrDgvInfo.Length + 1);
                    arrDgvInfo[arrDgvInfo.Length - 1] = strBeforeFileName + "\t" + strAfterFileName + "\t" + strRenameFlag;
                }
            }

            Array.Sort(arrDgvInfo);

            // DataGridViewを更新
            for (int intA = 0; intA < arrDgvInfo.Length; intA++)
            {
                dgvMediaFile.Rows.Add(arrDgvInfo[intA].Split("\t")[0], arrDgvInfo[intA].Split("\t")[1]);

                // 完了済みセルはライトグリーンにする
                if (arrDgvInfo[intA].Split("\t")[2] == "1" && arrDgvInfo[intA].Split("\t")[1] != "-")
                {
                    dgvMediaFile[0, intA].Style.BackColor = Color.LightGreen;
                    dgvMediaFile[1, intA].Style.BackColor = Color.LightGreen;
                }
            }

            dgvMediaFile.CurrentCell = null;
            dgvMediaFile.ClearSelection();
        }



        // ================================================
        // ==== 公式リファレンスを既定のブラウザで開く ====
        // ================================================
        private void btnReference_Click(object sender, EventArgs e)
        {
            string strUrl = CommonInfo.ReferenceUrl;
            OpenUrl(strUrl);
        }

        private Process OpenUrl(string strUrl)
        {
            ProcessStartInfo psiOpenUrl = new ProcessStartInfo()
            {
                FileName = strUrl,
                UseShellExecute = true,
            };

            return Process.Start(psiOpenUrl);
        }



        /// <summary>
        /// btnRemoveをクリック時、選択中のファイルをリストから除く
        /// </summary>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvMediaFile.CurrentCell == null)
            {
                System.Windows.Forms.MessageBox.Show(Message.GetMessage("M0001"), Message.GetCaption("C0000"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            int intSelectRow = dgvMediaFile.CurrentCell.RowIndex;
            string strSelectFileName = dgvMediaFile[0, intSelectRow].Value.ToString();

            for (int intA = 0; intA < listFileInfo.Count; intA++)
            {
                if (listFileInfo[intA].BeforeFileName == strSelectFileName)
                {
                    listFileInfo[intA].RemoveFlag = "1";
                    break;
                }
            }

            // dgvMediaFileを更新
            UpdateDgvMediaFile();
        }



        /// <summary>
        /// tbDefineStringの入力値変更イベント
        /// </summary>
        private void tbDefineString_TextChanged(object sender, EventArgs e)
        {
            string strInput = tbDefineString.Text;
            int intLength = strInput.Length;

            string strAllowChar = CommonInfo.AllowChar;
            string strNormalize = "";

            bool boolAlert = false;

            for (int intA = 0; intA < intLength; intA++)
            {
                string strCheckChar = strInput.Substring(intA, 1);

                if (strAllowChar.Contains(strCheckChar))
                {
                    if (strNormalize == "")
                    {
                        strNormalize = strCheckChar;
                    }
                    else
                    {
                        strNormalize = strNormalize + strCheckChar;
                    }
                }
                else
                {
                    boolAlert = true;
                }
            }

            if (boolAlert == true && boolCharAlert == true)
            {
                boolCharAlert = false;
                MessageBox.Show(Message.GetMessage("M0002"), Message.GetCaption("C0000"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }

            intLength = strNormalize.Length;

            if (intLength > 12)
            {
                if (boolLengthAlert == true)
                {
                    boolLengthAlert = false;
                    MessageBox.Show(Message.GetMessage("M0003"), Message.GetCaption("C0000"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                strNormalize = strNormalize.Substring(0, 12);
            }

            tbDefineString.Text = strNormalize;
            CommonInfo.DefineString = strNormalize;
            SetSampleName();
        }



        // ======================================
        // ==== ラジオボタン切り替えイベント ====
        // ======================================
        private void rbPatternA_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPatternA.Checked == true)
            {
                CommonInfo.FileNamePattern = "0";
                SetSampleName();
            }
        }

        private void rbPatternB_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPatternB.Checked == true)
            {
                CommonInfo.FileNamePattern = "1";
                SetSampleName();
            }
        }

        private void rbPatternC_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPatternC.Checked == true)
            {
                CommonInfo.FileNamePattern = "2";
                SetSampleName();
            }
        }

        private void rbPatternD_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPatternD.Checked == true)
            {
                CommonInfo.FileNamePattern = "3";
                SetSampleName();
            }
        }



        /// <summary>
        /// 出力フォルダの参照ボタンを押したとき
        /// </summary>
        private void SetSampleName()
        {
            labelSampleFileName.Text = FileName.Create(CommonInfo.AppLogTxt) + ".[拡張子]";
        }



        /// <summary>
        /// 出力フォルダを開くチェックボックスの切り替えイベント
        /// </summary>
        private void chkbOpenDir_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbOpenDir.Checked == true)
            {
                CommonInfo.OpenDir = "1";
            }
            else
            {
                CommonInfo.OpenDir = "0";
            }
        }



        /// <summary>
        /// オリジナルファイルを削除するチェックボックスの切り替えイベント
        /// </summary>
        private void chkbDeleteFile_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbDeleteFile.Checked == true)
            {
                CommonInfo.DeleteFile = "1";
            }
            else
            {
                CommonInfo.DeleteFile = "0";
            }
        }



        /// <summary>
        /// 出力フォルダの参照ボタンを押したとき
        /// </summary>
        private void btnDialog_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbdExport = new FolderBrowserDialog();

            fbdExport.Description = "出力フォルダを指定";

            if (Directory.Exists(CommonInfo.ExportDir))
            {
                fbdExport.SelectedPath = CommonInfo.ExportDir;
            }
            else
            {
                fbdExport.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            }
            fbdExport.ShowNewFolderButton = true;

            if (fbdExport.ShowDialog() == DialogResult.OK)
            {
                CommonInfo.ExportDir = fbdExport.SelectedPath;
            }

            fbdExport.Dispose();

            tbExportDir.Text = CommonInfo.ExportDir;
        }



        /// <summary>
        /// 履歴リセットボタンを押したとき
        /// </summary>
        private void btnResetLog_Click(object sender, EventArgs e)
        {
            int intCount = 0;

            // 確認メッセージ1回目
            DialogResult drConfirm1 = MessageBox.Show(Message.GetMessage("M0004"),Message.GetMessage("C0001"), MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);

            if (drConfirm1 == DialogResult.Yes)
            {
                intCount++;
            }

            if (intCount == 1)
            {
                // 確認メッセージ2回目
                DialogResult drConfirm2 = MessageBox.Show(Message.GetMessage("M0005"), Message.GetMessage("C0001"), MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);

                if (drConfirm2 == DialogResult.Yes)
                {
                    intCount++;
                }
            }

            if (intCount == 2)
            {
                if (File.Exists(CommonInfo.FileNameLogTxt))
                {
                    File.Delete(CommonInfo.FileNameLogTxt);
                }

                if (!File.Exists(CommonInfo.FileNameLogTxt))
                {
                    FileStream fsFileNameLog = File.Create(CommonInfo.FileNameLogTxt);
                    fsFileNameLog.Close();
                }

                CommonInfo.ReadFileNameLog();
            }
        }



        /// <summary>
        /// リネーム実行ボタンを押したとき
        /// </summary>
        private void btnRename_Click(object sender, EventArgs e)
        {
            // 変換対象があるか
            if (dgvMediaFile.Rows.Count == 0)
            {
                MessageBox.Show(Message.GetMessage("M0006"), Message.GetCaption("C0000"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            bool boolExistRenameFile = false;
            for (int intA = 0; intA < listFileInfo.Count; intA++)
            {
                if (listFileInfo[intA].RemoveFlag == "0" && listFileInfo[intA].RenameFlag == "0" && listFileInfo[intA].DeleteFlag == "0")
                {
                    boolExistRenameFile = true;
                    break;
                }
            }

            if (boolExistRenameFile == false)
            {
                MessageBox.Show(Message.GetMessage("M0007"), Message.GetCaption("C0000"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            // 任意文字列を使用するのにカラのとき
            if (CommonInfo.FileNamePattern == "0" && CommonInfo.DefineString == "")
            {
                MessageBox.Show(Message.GetMessage("M0008"), Message.GetCaption("C0000"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            // 出力フォルダが存在するかチェックする
            string strExportDir = tbExportDir.Text;

            if (!Directory.Exists(strExportDir))
            {
                if (strExportDir.Contains("\\"))
                {
                    string strPrevDir = strExportDir.Replace("\\" + strExportDir.Split("\\")[strExportDir.Split("\\").Length - 1], "");

                    if (Directory.Exists(strPrevDir))
                    {
                        Directory.CreateDirectory(strExportDir);
                    }
                }
            }

            if (!Directory.Exists(strExportDir))
            {
                MessageBox.Show(Message.GetMessage("M0009"), Message.GetCaption("C0000"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            // 出力対象のファイル一覧を取得
            string[] arrRenameInfo = [];

            for (int intA = 0; intA < listFileInfo.Count; intA++)
            {
                if (listFileInfo[intA].RemoveFlag == "0" && listFileInfo[intA].RenameFlag == "0" && listFileInfo[intA].DeleteFlag == "0")
                {
                    string strFilePath = listFileInfo[intA].FilePath;
                    string strBeforeFileName = listFileInfo[intA].BeforeFileName;
                    string strExtension = strBeforeFileName.Split(".")[strBeforeFileName.Split(".").Length - 1].ToLower();

                    Array.Resize(ref arrRenameInfo, arrRenameInfo.Length + 1);
                    arrRenameInfo[arrRenameInfo.Length - 1] = strBeforeFileName + "\t" + strFilePath + "\t" + strExtension;
                }
            }

            Array.Sort(arrRenameInfo);

            // リネーム後のファイルパスを生成
            for (int intA = 0; intA < arrRenameInfo.Length; intA++)
            {
                string strBeforeFileName = arrRenameInfo[intA].Split("\t")[0];
                string strFilePath = arrRenameInfo[intA].Split("\t")[1];
                string strExtension = arrRenameInfo[intA].Split("\t")[2];
                string strFileName = FileName.Create(strFilePath);

                if (strFileName == "")
                {
                    arrRenameInfo[intA] = "";
                }
                else
                {
                    string strAfterFileName = strFileName + "." + strExtension;
                    arrRenameInfo[intA] = strBeforeFileName + "\t" + strAfterFileName + "\t" + strFilePath + "\t" + strExportDir + "\\" + strAfterFileName;
                    CommonInfo.AppendFileNameLog(strFileName);
                }
            }

            // リネーム
            for (int intA = 0; intA < arrRenameInfo.Length; intA++)
            {
                if (arrRenameInfo[intA] != "")
                {
                    string strBeforeFileName = arrRenameInfo[intA].Split("\t")[0];
                    string strAfterFileName = arrRenameInfo[intA].Split("\t")[1];
                    string strBeforePath = arrRenameInfo[intA].Split("\t")[2];
                    string strAfterPath = arrRenameInfo[intA].Split("\t")[3];

                    bool boolDelete = false;

                    if (File.Exists(strBeforePath) && !File.Exists(strAfterPath))
                    {
                        File.Copy(strBeforePath, strAfterPath);

                        if (chkbDeleteFile.Checked == true)
                        {
                            File.Delete(strBeforePath);
                            boolDelete = true;
                        }

                        // listFileInfoを更新
                        for (int intB = 0; intB < listFileInfo.Count; intB++)
                        {
                            if (listFileInfo[intB].BeforeFileName == strBeforeFileName)
                            {
                                listFileInfo[intB].AfterFileName = strAfterFileName;
                                listFileInfo[intB].RenameFlag = "1";

                                if (boolDelete == true)
                                {
                                    listFileInfo[intB].DeleteFlag = "1";
                                }

                                break;
                            }
                        }
                    }
                }
            }

            UpdateDgvMediaFile();

            // チェックボックスに応じて、フォルダを開く
            if (chkbOpenDir.Checked == true)
            {
                Process.Start("EXPLORER.EXE", strExportDir);
            }

            // 完了メッセージ
            MessageBox.Show(Message.GetMessage("M0010"), Message.GetCaption("C0002"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }



        /// <summary>
        /// アプリケーション終了イベント
        /// </summary>
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            CommonInfo.WriteConfig();
            CommonInfo.WriteFileNameLog();

            // アプリケーション終了をログに記録
            Logger.Log(Logger.GetLog("L0001"));
        }
    }
}
