﻿using DocGenerate.Helper;
using DocGenerate.Model.Shared;
using NPOI.OpenXmlFormats.Vml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocGenerate.Forms
{
    public partial class SelfSignedCertificateForm : Form
    {
        private readonly ISharedHelper _sharedHelper;
        public SelfSignedCertificateForm(ISharedHelper sharedHelper)
        {
            _sharedHelper = sharedHelper;
            InitializeComponent();
        }

        private void SelfSignedCertificateForm_Load(object sender, EventArgs e)
        {
            Text = "自簽憑證設定";
        }

        private void GenerateBtn_Click(object sender, EventArgs e)
        {
            var valid = true;
            if (string.IsNullOrEmpty(CountryTextBox.Text))
            {
                valid = false;
                CountryErrorProvider.SetError(CountryTextBox, "請輸入國家代碼");
            }
            if (string.IsNullOrEmpty(ProvinceTextBox.Text))
            {
                valid = false;
                ProvinceErrorProvider.SetError(ProvinceTextBox, "請輸入所屬省/州");
            }
            if (string.IsNullOrEmpty(CityTextBox.Text))
            {
                valid = false;
                CityErrorProvider.SetError(CityTextBox, "請輸入所在城市");
            }
            if (string.IsNullOrEmpty(OrganizationNameTextBox.Text))
            {
                valid = false;
                OrganizationNameErrorProvider.SetError(OrganizationNameTextBox, "請輸入組織名稱");
            }
            if (string.IsNullOrEmpty(OrganizationalUnitTextBox.Text))
            {
                valid = false;
                OrganizationalUnitErrorProvider.SetError(OrganizationalUnitTextBox, "請輸入組織單位");
            }
            if (string.IsNullOrEmpty(CommonNameTextBox.Text))
            {
                valid = false;
                CommonNameErrorProvider.SetError(CommonNameTextBox, "請輸入通用名稱");
            }
            if (valid)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "ZIP files (*.zip)|*.zip",
                    Title = "Save the ZIP File"
                };
                if (saveFileDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                string zipFilePath = saveFileDialog.FileName;
                if (File.Exists(zipFilePath))
                {
                    File.Delete(zipFilePath);
                }
                string basePath = AppContext.BaseDirectory;
                string relativePath = "Asset\\Openssl";
                var sslConfigFileName = "ssl.conf";
                var cmdPath = Path.Combine(basePath, relativePath);
                string sslConfigFullPath = Path.Combine(cmdPath, sslConfigFileName);
                var sb = new StringBuilder();
                sb.AppendLine("[req]");
                sb.AppendLine("prompt = no");
                sb.AppendLine("default_md = sha256");
                sb.AppendLine("default_bits = 2048");
                sb.AppendLine("distinguished_name = dn");
                sb.AppendLine("x509_extensions = v3_req");
                sb.AppendLine();
                sb.AppendLine("[dn]");
                sb.AppendLine($"C = {CountryTextBox.Text}");
                sb.AppendLine($"ST = {ProvinceTextBox.Text}");
                sb.AppendLine($"L = {CityTextBox.Text}");
                sb.AppendLine($"O = {OrganizationNameTextBox.Text}");
                sb.AppendLine($"OU = {OrganizationalUnitTextBox.Text}");
                sb.AppendLine($"emailAddress = {EmailTextBox.Text}");
                sb.AppendLine($"CN = {CommonNameTextBox.Text}");
                sb.AppendLine();
                sb.AppendLine("[v3_req]");
                sb.AppendLine("subjectAltName = @alt_names");
                sb.AppendLine();
                sb.AppendLine("[alt_names]");
                sb.AppendLine("DNS.1 = *.localhost");
                sb.AppendLine("DNS.2 = localhost");
                sb.AppendLine("IP.1 = 192.168.1.1");
                string sslConfig = sb.ToString();
                File.WriteAllText(sslConfigFullPath, sslConfig, Encoding.UTF8);
                Process cmd = new Process();
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.UseShellExecute = false;
                cmd.Start();
                var fileName = Path.GetFileNameWithoutExtension(zipFilePath);

                using (StreamWriter sw = cmd.StandardInput)
                {
                    if (sw.BaseStream.CanWrite)
                    {
                        sw.WriteLine(@$"cd {cmdPath}");
                        sw.WriteLine(@$"openssl req -x509 -new -nodes -sha256 -utf8 -days 3650 -newkey rsa:2048 -keyout {fileName}.key -out {fileName}.crt -config ssl.conf");
                    }
                }
                cmd.WaitForExit();
                var keyFilePath = Path.Combine(cmdPath, $@"{fileName}.key");
                var crtFilePath = Path.Combine(cmdPath, $@"{fileName}.crt");
                using (var zipArchive = ZipFile.Open(zipFilePath, ZipArchiveMode.Create))
                {
                    zipArchive.CreateEntryFromFile(keyFilePath, Path.GetFileName(keyFilePath));
                    zipArchive.CreateEntryFromFile(crtFilePath, Path.GetFileName(crtFilePath));
                }
                // 刪除Temp產生資料
                File.Delete(sslConfigFileName);
                File.Delete(keyFilePath);
                File.Delete(crtFilePath);
                _sharedHelper.ShowInfoMsg("文件產生完成", @$"文件已產生到{zipFilePath}!");
            }
        }
    }
}