using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crawl_Instagram.BUS
{
    internal class UpdateDataGridView
    {
        public static void UpdateRow(DataGridViewRow row, AuraeColorEnum @enum, string column, string value)
        {
            try
            {
                AuraeColor auraeColor = new AuraeColor(@enum);
                row.DefaultCellStyle.ForeColor = auraeColor.AuraeForeColor;
                row.DefaultCellStyle.BackColor = auraeColor.AuraeBackColor;
                row.Cells[column].Value = value;
            }
            catch { }
        }

        public static void UpdateRowColor(DataGridViewRow row, AuraeColorEnum @enum)
        {
            try
            {
                AuraeColor auraeColor = new AuraeColor(@enum);
                row.DefaultCellStyle.ForeColor = auraeColor.AuraeForeColor;
                row.DefaultCellStyle.BackColor = auraeColor.AuraeBackColor;
            }
            catch { }
        }



        public static int SelectAll(DataGridView dataGridView, bool isSelect)
        {
            try
            {
                int SelectCount = 0;
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    dataGridView.Rows[i].SetValues(isSelect);
                    SelectCount++;
                }
                return SelectCount;
            }
            catch
            {
                return 0;
            }
        }

        public static int SelectSelected(DataGridView dataGridView)
        {
            int SelectCount = 0;
            try
            {
                List<int> selected = new List<int>();

                for (int i = 0; i < dataGridView.RowCount; i++)
                {
                    dataGridView.Rows[i].SetValues(false);
                }
                string Data = "";


                for (int i = 0; i < dataGridView.SelectedCells.Count; i++)
                {
                    try
                    {
                        int a = dataGridView.SelectedCells[i].RowIndex;
                        if (!Data.Contains("|" + a.ToString() + "|"))
                        {
                            SelectCount++;
                            dataGridView.Rows[a].SetValues(true);
                            Data += "|" + a.ToString() + "|";
                        }
                    }
                    catch
                    {

                    }
                }

                return SelectCount;
            }
            catch
            {
                return 0;
            }
        }
        public class AuraeColor
        {
            private Color SuccessBackColor = Color.FromArgb(198, 239, 206);
            private Color SuccessForeColor = Color.FromArgb(0, 97, 0);

            private Color DangerBackColor = Color.FromArgb(255, 199, 206);
            private Color DangerForeColor = Color.FromArgb(156, 0, 6);

            private Color WarningBackColor = Color.FromArgb(255, 235, 156);
            private Color WarningForeColor = Color.FromArgb(156, 101, 0);

            private Color InfoBackColor = Color.FromArgb(211, 225, 241);
            private Color InfoForeColor = Color.FromArgb(37, 82, 143);

            public Color AuraeBackColor { get; set; }
            public Color AuraeForeColor { get; set; }
            public AuraeColor(AuraeColorEnum @enum)
            {
                switch (@enum)
                {
                    case AuraeColorEnum.Success:
                        AuraeBackColor = SuccessBackColor;
                        AuraeForeColor = SuccessForeColor;
                        break;
                    case AuraeColorEnum.Warning:
                        AuraeBackColor = WarningBackColor;
                        AuraeForeColor = WarningForeColor;
                        break;
                    case AuraeColorEnum.Danger:
                        AuraeBackColor = DangerBackColor;
                        AuraeForeColor = DangerForeColor;
                        break;
                    case AuraeColorEnum.Info:
                        AuraeBackColor = InfoBackColor;
                        AuraeForeColor = InfoForeColor;
                        break;
                    default:
                        AuraeBackColor = InfoBackColor;
                        AuraeForeColor = InfoForeColor;
                        break;
                }
            }
        }

        public enum AuraeColorEnum
        {
            Success,
            Warning,
            Danger,
            Info
        }
    }
}
