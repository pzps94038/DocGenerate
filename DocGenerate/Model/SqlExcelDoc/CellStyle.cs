using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocGenerate.Model.SqlExcelDoc
{
    public class CellStyle
    {
        public double FontHeightInPoints = 10;
        public string FontName = "微軟正黑體";
        public bool IsBold = false;
        public short FontColor = IndexedColors.Black.Index;
        public NPOI.SS.UserModel.BorderStyle BorderStyle = NPOI.SS.UserModel.BorderStyle.Medium;
        public short BorderColor = IndexedColors.Black.Index;
        public NPOI.SS.UserModel.HorizontalAlignment Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;
        public VerticalAlignment VerticalAlignment = VerticalAlignment.Center;
        public short FillForegroundColor = IndexedColors.White.Index;
        public FillPattern FillPattern = FillPattern.SolidForeground;
        public FontUnderlineType Underline = FontUnderlineType.None;
    }
}
