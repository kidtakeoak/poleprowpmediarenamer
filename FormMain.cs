using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Windows.Forms;

namespace PoleproWpMediaRenamer
{
    public partial class FormMain : Form
    {
        private List<FileInfo> listFileInfo = new List<FileInfo>();
        private bool boolFormLoading = false;

        private bool boolCharAlert = true;
        private bool boolLengthAlert = true;

        public FormMain()
        {
            InitializeComponent();

            // pbDropArea�ւ̃h���b�v��������
            pbDropArea.AllowDrop = true;

            // �t�H�[�������l�̐ݒ�
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
            // �h���b�v���ꂽ�t�@�C���̃p�X���A�z��Ŏ擾
            string[] arrFilePath = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (arrFilePath.Length > 0)
            {
                // �������g���q�ꗗ���擾
                string[] arrExtension = CommonInfo.Extension;

                // �������g���q���ǂ���
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

                                if (listFileInfo[intC].BeforeFileName == strBeforeFileName)
                                {
                                    boolAdd = false;
                                    // ���b�Z�[�W�{�b�N�X������
                                    MessageBox.Show("���Ȃ�����");
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

            // dgvMediaFile���X�V
            UpdateDgvMediaFile();
        }

        private void UpdateDgvMediaFile()
        {
            // dgvMediaFile��������
            dgvMediaFile.Rows.Clear();

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

            for (int intA = 0; intA < arrDgvInfo.Length; intA++)
            {
                dgvMediaFile.Rows.Add(arrDgvInfo[intA].Split("\t")[0], arrDgvInfo[intA].Split("\t")[1]);
            }

            dgvMediaFile.CurrentCell = null;
            dgvMediaFile.ClearSelection();
        }

        /// <summary>
        /// �������t�@�����X���J��
        /// </summary>
        private void btnReference_Click(object sender, EventArgs e)
        {
            string strUrl = CommonInfo.ReferenceUrl;
            OpenUrl(strUrl);
        }

        /// <summary>
        /// �����Ŏ󂯎����URL���A����̃u���E�U�ŊJ��
        /// </summary>
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
        /// btnRemove���N���b�N���A�I�𒆂̃t�@�C�������X�g���珜��
        /// </summary>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvMediaFile.CurrentCell == null)
            {
                MessageBox.Show("�������傽�����傤�����񂽂������܂�");
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

            // dgvMediaFile���X�V
            UpdateDgvMediaFile();
        }

        /// <summary>
        /// tbDefineString�̓��͒l�ύX�C�x���g
        /// </summary>
        private void tbDefineString_TextChanged(object sender, EventArgs e)
        {
            string strInput = tbDefineString.Text;
            int intLength = strInput.Length;

            string strAllowChar = "abcdefghijklmnopqrstuvwxyz0123456789";
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
                MessageBox.Show("�����Ȃ���");
            }

            intLength = strNormalize.Length;

            if (intLength > 12)
            {
                if (boolLengthAlert == true)
                {
                    boolLengthAlert = false;
                    MessageBox.Show("�Ȃ��[��");
                }
                strNormalize = strNormalize.Substring(0, 12);
            }

            tbDefineString.Text = strNormalize;
            CommonInfo.DefineString = strNormalize;
        }

        private void rbPatternA_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPatternA.Checked == true)
            {
                CommonInfo.FileNamePattern = "0";
            }
        }

        private void rbPatternB_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPatternB.Checked == true)
            {
                CommonInfo.FileNamePattern = "1";
            }
        }

        private void rbPatternC_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPatternC.Checked == true)
            {
                CommonInfo.FileNamePattern = "2";
            }
        }

        private void rbPatternD_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPatternD.Checked == true)
            {
                CommonInfo.FileNamePattern = "3";
            }
        }

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
        /// �o�̓t�H���_�̎Q�ƃ{�^�����������Ƃ�
        /// </summary>
        private void btnDialog_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbdExport = new FolderBrowserDialog();

            fbdExport.Description = "�o�̓t�H���_���w��";

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
        /// �������Z�b�g�{�^�����������Ƃ�
        /// </summary>
        private void btnResetLog_Click(object sender, EventArgs e)
        {
            int intCount = 0;

            // �m�F���b�Z�[�W1���
            DialogResult drConfirm1 = MessageBox.Show("���������Z�b�g���܂����H", "����", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);

            if (drConfirm1 == DialogResult.Yes)
            {
                intCount++;
            }

            if (intCount == 1)
            {
                // �m�F���b�Z�[�W2���
                DialogResult drConfirm2 = MessageBox.Show("������Ȃ��̂ł��ˁH", "����", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);

                if (drConfirm2 == DialogResult.Yes)
                {
                    intCount++;
                }
            }

            if (intCount == 2)
            {
                if (File.Exists(CommonInfo.LogTxt))
                {
                    File.Delete(CommonInfo.LogTxt);
                }

                if (!File.Exists(CommonInfo.LogTxt))
                {
                    FileStream fsLog = File.Create(CommonInfo.LogTxt);
                    fsLog.Close();
                }

                CommonInfo.ReadLog();
            }
        }

        /// <summary>
        /// ���l�[�����s�{�^�����������Ƃ�
        /// </summary>
        private void btnRename_Click(object sender, EventArgs e)
        {
            // �ϊ��Ώۂ����邩
            if (dgvMediaFile.Rows.Count == 0)
            {
                MessageBox.Show("�ӂ��邾�Ȃ���");
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
                MessageBox.Show("�ӂ��邾�Ȃ���");
                return;
            }


            // �o�̓t�H���_�����݂��邩�`�F�b�N����
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
                MessageBox.Show("�ӂ��邾�Ȃ���");
                return;
            }

            // �o�͑Ώۂ̃t�@�C���ꗗ���擾
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

            // ���l�[����̃t�@�C���p�X�𐶐�
            for (int intA = 0; intA < arrRenameInfo.Length; intA++)
            {
                string strBeforeFileName = arrRenameInfo[intA].Split("\t")[0];
                string strFilePath = arrRenameInfo[intA].Split("\t")[1];
                string strExtension = arrRenameInfo[intA].Split("\t")[2];

                string strFileName = "";
                if (rbPatternA.Checked == true)
                {
                    strFileName = FileName.PatternA(strFilePath, tbDefineString.Text);
                }
                else if (rbPatternB.Checked == true)
                {
                    strFileName = FileName.PatternB(strFilePath);

                }
                else if (rbPatternC.Checked == true)
                {
                    strFileName = FileName.PatternC(strFilePath);
                }
                else
                {
                    strFileName = FileName.PatternD();
                }

                if (strFileName == "")
                {
                    arrRenameInfo[intA] = "";
                }
                else
                {
                    string strAfterFileName = strFileName + "." + strExtension;
                    arrRenameInfo[intA] = strBeforeFileName + "\t" + strAfterFileName + "\t" + strFilePath + "\t" + strExportDir + "\\" + strAfterFileName;
                    CommonInfo.AppendLog(strFileName);
                }
            }

            // ���l�[��
            for (int intA = 0; intA < arrRenameInfo.Length; intA++)
            {
                if (arrRenameInfo[intA] != "")
                {
                    string strBeforeFileName = arrRenameInfo[intA].Split("\t")[0];
                    string strAfterFileName = arrRenameInfo[intA].Split("\t")[1];
                    string strBeforePath = arrRenameInfo[intA].Split("\t")[2];
                    string strAfterPath = arrRenameInfo[intA].Split("\t")[3];

                    bool boolRename = false;
                    bool boolDelete = false;

                    if (File.Exists(strBeforePath) && !File.Exists(strAfterPath))
                    {
                        File.Copy(strBeforePath, strAfterPath);
                        boolRename = true;

                        if (chkbDeleteFile.Checked == true)
                        {
                            File.Delete(strBeforePath);
                            boolDelete = true;
                        }

                        // listFileInfo���X�V
                        for (int intB = 0; intB < listFileInfo.Count; intB++)
                        {
                            if (listFileInfo[intB].BeforeFileName == strBeforeFileName)
                            {
                                listFileInfo[intB].AfterFileName = strAfterFileName;
                                if (boolRename == true)
                                {
                                    listFileInfo[intB].RenameFlag = "1";
                                }
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

            if (chkbOpenDir.Checked == true)
            {
                Process.Start("EXPLORER.EXE", strExportDir);
            }

            MessageBox.Show("�����傤");
        }

        /// <summary>
        /// �A�v���P�[�V�����I���C�x���g
        /// </summary>
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            CommonInfo.WriteConfig();
            CommonInfo.WriteLog();
        }
    }
}
