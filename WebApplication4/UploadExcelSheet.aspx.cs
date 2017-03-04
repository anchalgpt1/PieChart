using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WebApplication4
{
    public class UploadExcelSheet : System.Web.UI.Page
    {
        protected HtmlInputFile inpExcel;
        protected HiddenField hdnData;
        public string json;

        protected void UploadExcel_Click(object sender, EventArgs e)
        {
            var fileNameArray = inpExcel.Value.Split('.');
            var correctFileName = fileNameArray[0];
            correctFileName = Regex.Replace(correctFileName, @"[^0-9a-zA-Z]+", "-").TrimEnd('-');
            string filePath = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, @"\Temp\");
            Savefile(inpExcel, string.Empty, filePath, correctFileName);
            var file = string.Format("{0}{1}", filePath, correctFileName);
            var data = GetDataFromExcel(file);
            json = SerializeData(data);
            hdnData.Value = json;
        }

        private void Savefile(HtmlInputFile inpEx, string absolutePath, string filePath, string fileName)
        {  //Get the uploaded excel file and save it to a local folder
            string fullPath = absolutePath + filePath + fileName;

            if (!Directory.Exists(absolutePath + filePath))
                Directory.CreateDirectory(absolutePath + filePath);

            inpEx.PostedFile.SaveAs(fullPath);
        }

        private DataTable GetDataFromExcel(string file)
        {  // Read data from the saved excel
            DataSet dset = new DataSet();
            DataTable dtable = new DataTable();
            try
            {
                OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + file + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";");
                OleDbDataAdapter adapter = new OleDbDataAdapter("select top 100 * from [Sales$]", conn);
                adapter.Fill(dset);
                dtable = dset.Tables[0];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Msg:" + ex);
                throw;
            }
            return dtable;
        }

        private string SerializeData(DataTable obj)
        {   //Serialize the data to be passed to the front end
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<List<object>> parentRow = new List<List<object>>();
            List<object> childRow;
            foreach (DataRow row in obj.Rows)
            {
                childRow = new List<object>();
                foreach (DataColumn col in obj.Columns)
                {
                    if (col.ColumnName != "Sales") {
                        if (row[col].ToString()=="Total")
                            break;
                        else
                        childRow.Add(row[col]);
                    }
                }
                if (childRow.Count > 0)
                {
                    parentRow.Add(childRow);
                }
            }
            return jsSerializer.Serialize(parentRow);
        }
    }
}