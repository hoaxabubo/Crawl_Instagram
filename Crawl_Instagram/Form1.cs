using AliceSeleniumHelper;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Crawl_Instagram.BUS;
using System.Windows.Forms;
using static Crawl_Instagram.BUS.UpdateDataGridView;
using System.Runtime.InteropServices;
using System.Net;

namespace Crawl_Instagram
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string[] keyWords = richTextBox1.Lines.ToArray();
                foreach (string keyWord in keyWords)
                {

                    int iRow = dataGridView1.Rows.Add();

                    dataGridView1.Rows[iRow].Cells["cIndex"].Value = iRow + 1;
                    dataGridView1.Rows[iRow].Cells["cInstagram"].Value = keyWord;

                }
                LabelTotal.Text = "Totals:" + keyWords.Count().ToString();
            }
            catch
            {


            }
        }
        List<Thread> threads = new List<Thread>();
        Random rnd = new Random();
        private static WebClient download = new WebClient();
        private void button2_Click(object sender, EventArgs e)
        {
            Semaphore semaphore;
            semaphore = new Semaphore(1, 1);
            if (string.IsNullOrEmpty(richTextBox1.Text))
            {
                MessageBox.Show(" Điền tài khoản Instagram ", "Thông báo",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                return;
            }
            Thread mainThread = new Thread(() =>
            {
                try
                {
                    ChromeDriver chrome = AliceChrome.OpenChrome();
                    BUS.CrawlImage.SignInInstagram(chrome, "thay74536", "121211223212Anh");
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        row.Cells["cStatus"].Value = $"Đang Crawl";
                        string instaAcc = row.Cells["cInstagram"].Value.ToString();
                        UpdateDataGridView.UpdateRow(row, AuraeColorEnum.Warning, "cStatus", "Chuẩn bị Crawl");

                        semaphore.WaitOne(-1);

                        UpdateDataGridView.UpdateRow(row, AuraeColorEnum.Info, "cStatus", "Đang Crawl Keyword");
                        Thread.Sleep(3000);
                        chrome.Navigate().GoToUrl(instaAcc);
                        Thread.Sleep(2000);
                        var re2 = AliceSeleniumHelper.AliceClick.BySelenium(chrome, "//*[contains(@href,'/p/')]");
                        if (!re2.Status)
                        {
                            continue;
                        }
                        Thread childThread = new Thread(() =>
                    {

                       
                        AliceSeleniumHelperReturn re = new AliceSeleniumHelperReturn();
                        while (true)
                        {
                           List<string> links = CrawlImage.DownloadOnePost(chrome, txtsv.Text);
                            if (CrawlImage.ClickNextPost(chrome) == false)
                            {
                                break;
                            }
                            foreach (var link in links)
                            {


                                download.DownloadFile(link, $@"{txtsv.Text}\{rnd.Next(1111, 9999)}.png");


                            }
                            links.Clear();
                         
                            Thread.Sleep(2000);
                        }
                      
                        if (re.Status)
                        {
                            UpdateDataGridView.UpdateRow(row, AuraeColorEnum.Success, "cStatus", $"Crawl  thành công");
                        }


                        semaphore.Release();

                    });
                        threads.Add(childThread);
                        childThread.IsBackground = true;
                        childThread.Start();


                    }
                }
                catch
                {


                }


            });

            threads.Add(mainThread);
            mainThread.IsBackground = true;
            mainThread.Start();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
                txtsv.Text = fbd.SelectedPath;
        }
    }
}
