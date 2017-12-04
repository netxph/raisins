using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raisins.Client.Models;
using RestSharp.Deserializers;
using System.IO;
using Excel;
using System.Data;
using System.Web.Script.Serialization;
using System.Globalization;

namespace Raisins.Client.Services
{
    public class FileUploader : IFileUploader
    {
        public IEnumerable<Payment> ExcelUpload(HttpPostedFileBase upload)
        {
            if (upload != null && upload.ContentLength > 0)
            {
                Stream stream = upload.InputStream;
                IExcelDataReader reader = null;

                if (upload.FileName.EndsWith(".xls"))
                {
                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                }
                else if (upload.FileName.EndsWith(".xlsx"))
                {
                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }
                reader.IsFirstRowAsColumnNames = true;
                DataSet result = reader.AsDataSet();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                List<Payment> payments = new List<Payment>();
                Payment payment;
                
                foreach (DataRow row in result.Tables[0].Rows)
                {
                    if (!string.IsNullOrEmpty(row.ToString()))
                    {
                        payment = new Payment();

                        ColumnChecker(upload, result, payment, row);

                        payments.Add(payment);
                    }
                }
                return payments;
            }
            else
            {
                return null;
            }          
        }

        private static void ColumnChecker(HttpPostedFileBase upload, DataSet result, Payment payment, DataRow row)
        {
            try
            {
                foreach (DataColumn col in result.Tables[0].Columns)
                {
                    if (payment.Locked == false)
                    {
                        // check if ID exists in db and if published already = do not update
                        // if exists in db but not yet published = overwrite data
                        //if(publishdate.hasvalue)
                        // TODO

                        if (col.ColumnName == "ID")
                        {
                            if (string.IsNullOrWhiteSpace(row[col].ToString()))
                            {
                                payment.PaymentID = 0;
                            }
                            else
                            {
                                payment.PaymentID = int.Parse(row[col].ToString());
                            }
                        }
                        if (col.ColumnName == "Donor Name")
                        {
                            payment.Name = row[col].ToString();
                        }
                        if (col.ColumnName == "Email")
                        {
                            payment.Email = row[col].ToString();
                        }
                        if (col.ColumnName == "Amount")
                        {
                            payment.Amount = decimal.Parse(row[col].ToString());
                        }
                        if (col.ColumnName == "Beneficiary")
                        {
                            Beneficiary beneficiary = new Beneficiary();
                            beneficiary.Name = row[col].ToString();
                            payment.Beneficiary = beneficiary;
                        }
                        if (col.ColumnName == "Currency")
                        {
                            Currency currency = new Currency();
                            currency.CurrencyCode = row[col].ToString();
                            payment.Currency = currency;
                        }
                        if (col.ColumnName == "Type")
                        {
                            payment.Type = new PaymentType(row[col].ToString());
                        }
                        if (col.ColumnName == "Source")
                        {
                            payment.Source = new PaymentSource(row[col].ToString());
                        }
                        if (col.ColumnName == "Date")
                        {
                            if (upload.FileName.EndsWith(".xls"))
                            {
                                payment.PaymentDate = DateTime.FromOADate(double.Parse(row[col].ToString()));
                            }
                            else
                            {
                                payment.PaymentDate = DateTime.Parse(row[col].ToString(), new CultureInfo("en-US", true));
                            }
                        }
                        if (col.ColumnName == "Opt Out")
                        {
                            if (row[col].ToString().ToLower() == "yes")
                            {
                                payment.OptOut = true;
                            }
                            else
                            {
                                payment.OptOut = false;
                            }
                        }
                        if (col.ColumnName == "Remarks")
                        {
                            payment.Remarks = row[col].ToString();
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}