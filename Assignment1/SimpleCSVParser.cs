using System;
using System.IO;
using System.Text;
using Microsoft.VisualBasic.FileIO;

namespace Assignment1
{
    public class SimpleCSVParser
    {
        String[] headers = { "First Name", "Last Name", "Street Number", "Street", "City", "Province", "Postal Code", "Country", "Phone Number", "Email Address" };
        public void Parse(String fileName, ref int validRows, ref int skippedRows, ref StringBuilder sb)
        {
            StringBuilder sbRecords = new StringBuilder();
            Exceptions ex = new Exceptions();
            try
            {
                using (TextFieldParser parser = new TextFieldParser(fileName))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    while (!parser.EndOfData)
                    {
                        //Process row
                        string[] fields = parser.ReadFields();
                        if (fields[0] == "First Name")
                            continue;
                        int emptyFieldCheck = 0;
                        sbRecords = new StringBuilder();
                        for(int i= 0; i < fields.Length; i++)
                        {
                            if (!(String.IsNullOrEmpty(fields[i])))
                            {
                                sbRecords.Append(fields[i] + ",");
                            }
                            else
                            {
                                emptyFieldCheck = 1;
                                ex.WriteLog("Error in the data column : " + headers[i] + " in the file : " + fileName);
                                break;
                            }
                        }
                        if (emptyFieldCheck == 0)
                        {
                            validRows++;
                            sb.Append(sbRecords);
                            string[] splitFileName = fileName.Split('\\');
                            sb.Append(splitFileName[splitFileName.Length - 4] + "/" + splitFileName[splitFileName.Length - 3] + "/" + splitFileName[splitFileName.Length - 2] + Environment.NewLine);
                        }
                        else
                        {
                            skippedRows++;
                        }
                    }
                }
            }
            catch (IOException ioe)
            {
                ex.WriteLog(ioe.StackTrace);
            }
            //return sb;
        }
    }
}
