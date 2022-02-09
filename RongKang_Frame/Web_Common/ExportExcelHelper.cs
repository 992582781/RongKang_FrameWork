using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Common
{
    public class ExportExcelHelper
    {
        /// <summary>
        /// 返回生成Excel文件byte流
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static byte[] GenExcelFileStream<T>(List<T> dataList) where T : class
        {
            var workbook = GenExcelWorkbook(dataList);
            var filePath = AppDomain.CurrentDomain.BaseDirectory + Guid.NewGuid() + ".xlsx";
            FileStream stream = new FileStream(filePath, FileMode.CreateNew, FileAccess.ReadWrite);
            workbook.Write(stream);
            workbook.Close();
            stream.Close();

            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);

            fileStream.Close();
            File.Delete(filePath);

            return bytes;

        }

        /// <summary>
        /// 返回生成Excel文件地址
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataList"></param>
        /// <returns></returns>
        public static string GenExcelFile<T>(List<T> dataList) where T : class
        {
            var workbook = GenExcelWorkbook(dataList);
            var filePath = AppDomain.CurrentDomain.BaseDirectory + "FileUpload/" + DateTime.Now.ToString("yyyy-MM-dd") +
                           "/";
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            var guid = Guid.NewGuid();
            var fileName = filePath + guid + ".xlsx";
            FileStream stream = new FileStream(fileName, FileMode.CreateNew);
            workbook.Write(stream);
            workbook.Close();
            stream.Close();
            return fileName;
        }

        /// <summary>
        /// 生成Workbook
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataList"></param>
        /// <returns></returns>
        private static HSSFWorkbook GenExcelWorkbook<T>(List<T> dataList)
        {
            var type = typeof(T);
            var props = type.GetProperties().ToArray();
            if (!props.Any())
            {
                throw new Exception("导出失败，导出模板中未标记要导出的数据列！");
            }
            var headerList = new List<ExportHeader>();
            foreach (var propertyInfo in props)
            {
                var exportAtt = (FieldNameAttribute)propertyInfo.GetCustomAttributes(typeof(FieldNameAttribute), false).FirstOrDefault();
                if (exportAtt?.View_Flag <= 1)
                {
                    ExportHeader header = new ExportHeader()
                    {
                        PropertyName = propertyInfo?.Name,
                        HeaderName = exportAtt?.View_Name,
                    };
                    headerList.Add(header);
                }
            }

            headerList = headerList.ToList();

            HSSFWorkbook workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet();

            //创建表头
            var rowIndex = 0;
            var sheetHeadRow = sheet.CreateRow(rowIndex);
            for (int i = 0; i < headerList.Count; i++)
            {
                var cell = sheetHeadRow.CreateCell(i);
                cell.SetCellValue(headerList[i].HeaderName);
            }
            rowIndex++;

            //写入数据
            foreach (var dataItem in dataList)
            {
                var dataProps = typeof(T).GetProperties();

                var sheetRow = sheet.CreateRow(rowIndex);
                for (int i = 0; i < headerList.Count; i++)
                {
                    var cell = sheetRow.CreateCell(i);
                    var value = dataProps.First(t => t.Name == headerList[i].PropertyName).GetValue(dataItem)?.ToString();
                    cell.SetCellValue(value);
                }

                rowIndex++;
            }
            return workbook;
        }

        private class ExportHeader
        {
            /// <summary>
            /// 导出字段属性名称
            /// </summary>
            public string PropertyName { get; set; }

            /// <summary>
            /// 导出字段标题名称
            /// </summary>
            public string HeaderName { get; set; }

        }
    }
}
