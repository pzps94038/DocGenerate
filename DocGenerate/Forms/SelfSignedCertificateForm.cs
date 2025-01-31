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
            var hostSb = new StringBuilder();
            hostSb.AppendLine("*.localhost");
            hostSb.AppendLine("localhost");
            DNSTextBox.Text = hostSb.ToString();
            var ipSb = new StringBuilder();
            ipSb.AppendLine("127.0.0.1");
            IPTextBox.Text = ipSb.ToString();
        }

        private void GenerateBtn_Click(object sender, EventArgs e)
        {
            var valid = true;
            if (!CountryValidating())
            {
                valid = false;
            }
            if (!ProvinceValidating())
            {
                valid = false;
            }
            if (!CityValidating())
            {
                valid = false;
            }
            if (!OrganizationNameValidating())
            {
                valid = false;
            }
            if (!OrganizationalUnitValidating())
            {
                valid = false;
            }
            if (!EmailValidating())
            {
                valid = false;
            }
            if (!CommonNameValidating())
            {
                valid = false;
            }
            if (!DNSValidating())
            {
                valid = false;
            }
            if (!IPValidating())
            {
                valid = false;
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
                var dnsList = DNSTextBox.Lines.Select(a => a.Trim()).Where(a => !string.IsNullOrEmpty(a)).ToList();
                for (int i = 0; i < dnsList.Count; i++)
                {
                    sb.AppendLine(@$"DNS.{i + 1} = {dnsList[i]}");
                }
                var ipList = IPTextBox.Lines.Select(a => a.Trim()).Where(a => !string.IsNullOrEmpty(a)).ToList();
                for (int i = 0; i < ipList.Count; i++)
                {
                    sb.AppendLine(@$"IP.{i + 1} = {ipList[i]}");
                }
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
                        sw.WriteLine(@$"openssl pkcs12 -export -in {fileName}.crt -inkey {fileName}.key -out {fileName}.pfx");
                    }
                }
                cmd.WaitForExit();
                var keyFilePath = Path.Combine(cmdPath, $@"{fileName}.key");
                var crtFilePath = Path.Combine(cmdPath, $@"{fileName}.crt");
                var pfxFilePath = Path.Combine(cmdPath, $@"{fileName}.pfx");
                if (!File.Exists(pfxFilePath))
                {
                    _sharedHelper.ShowInfoMsg("錯誤", @$"雙重密碼驗證錯誤!");
                    // 刪除Temp產生資料
                    File.Delete(sslConfigFileName);
                    File.Delete(keyFilePath);
                    File.Delete(crtFilePath);
                }
                else
                {
                    using (var zipArchive = ZipFile.Open(zipFilePath, ZipArchiveMode.Create))
                    {
                        zipArchive.CreateEntryFromFile(keyFilePath, Path.GetFileName(keyFilePath));
                        zipArchive.CreateEntryFromFile(crtFilePath, Path.GetFileName(crtFilePath));
                        zipArchive.CreateEntryFromFile(pfxFilePath, Path.GetFileName(pfxFilePath));
                    }
                    // 刪除Temp產生資料
                    File.Delete(sslConfigFileName);
                    File.Delete(keyFilePath);
                    File.Delete(crtFilePath);
                    File.Delete(pfxFilePath);
                    _sharedHelper.ShowInfoMsg("文件產生完成", @$"文件已產生到{zipFilePath}!");
                }
            }
        }

        private bool CountryValidating()
        {
            if (string.IsNullOrEmpty(CountryTextBox.Text))
            {
                CountryErrorProvider.SetError(CountryTextBox, "請輸入國家代碼");
                return false;
            }
            else
            {
                CountryErrorProvider.SetError(CountryTextBox, "");
                return true;
            }
        }

        private bool ProvinceValidating()
        {
            if (string.IsNullOrEmpty(ProvinceTextBox.Text))
            {
                ProvinceErrorProvider.SetError(ProvinceTextBox, "請輸入所屬省/州");
                return false;
            }
            else
            {
                ProvinceErrorProvider.SetError(ProvinceTextBox, "");
                return true;
            }
        }

        private bool CityValidating()
        {
            if (string.IsNullOrEmpty(CityTextBox.Text))
            {
                CityErrorProvider.SetError(CityTextBox, "請輸入所在城市");
                return false;
            }
            else
            {
                CityErrorProvider.SetError(CityTextBox, "");
                return true;
            }
        }

        private bool OrganizationNameValidating()
        {
            if (string.IsNullOrEmpty(OrganizationNameTextBox.Text))
            {
                OrganizationNameErrorProvider.SetError(OrganizationNameTextBox, "請輸入組織名稱");
                return false;
            }
            else
            {
                OrganizationNameErrorProvider.SetError(OrganizationNameTextBox, "");
                return true;
            }
        }

        private bool OrganizationalUnitValidating()
        {
            if (string.IsNullOrEmpty(OrganizationalUnitTextBox.Text))
            {
                OrganizationalUnitErrorProvider.SetError(OrganizationalUnitTextBox, "請輸入組織單位");
                return false;
            }
            else
            {
                OrganizationalUnitErrorProvider.SetError(OrganizationalUnitTextBox, "");
                return true;
            }
        }
        private bool EmailValidating()
        {
            if (string.IsNullOrEmpty(EmailTextBox.Text))
            {
                EmailErrorProvider.SetError(EmailTextBox, "請輸入電子郵件");
                return false;
            }
            else
            {
                EmailErrorProvider.SetError(EmailTextBox, "");
                return true;
            }
        }

        private bool CommonNameValidating()
        {
            if (string.IsNullOrEmpty(CommonNameTextBox.Text))
            {
                CommonNameErrorProvider.SetError(CommonNameTextBox, "請輸入通用名稱");
                return false;
            }
            else
            {
                CommonNameErrorProvider.SetError(CommonNameTextBox, "");
                return true;
            }
        }

        private bool DNSValidating()
        {
            if (string.IsNullOrEmpty(DNSTextBox.Text))
            {
                DNSErrorProvider.SetError(DNSTextBox, "請輸入允許的域名");
                return false;
            }
            else
            {
                DNSErrorProvider.SetError(DNSTextBox, "");
                return true;
            }
        }

        private bool IPValidating()
        {
            if (string.IsNullOrEmpty(IPTextBox.Text))
            {
                IPErrorProvider.SetError(IPTextBox, "請輸入允許的IP地址");
                return false;
            }
            else
            {
                IPErrorProvider.SetError(IPTextBox, "");
                return true;
            }
        }


        private void CountryTextBox_Validating(object sender, CancelEventArgs e) => CountryValidating();

        private void ProvinceTextBox_Validating(object sender, CancelEventArgs e) => ProvinceValidating();

        private void CityTextBox_Validating(object sender, CancelEventArgs e) => CityValidating();

        private void OrganizationNameTextBox_Validating(object sender, CancelEventArgs e) => OrganizationNameValidating();

        private void OrganizationalUnitTextBox_Validating(object sender, CancelEventArgs e) => OrganizationalUnitValidating();

        private void EmailTextBox_Validating(object sender, CancelEventArgs e) => EmailValidating();

        private void CommonNameTextBox_Validating(object sender, CancelEventArgs e) => CommonNameValidating();

        private void DNSTextBox_Validating(object sender, CancelEventArgs e) => DNSValidating();

        private void IPTextBox_Validating(object sender, CancelEventArgs e) => IPValidating();
    }
}
