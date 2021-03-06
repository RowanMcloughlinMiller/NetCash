﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetCash.Models
{
    public class Loan
    {
        public string AccountNumber { get; set; }

        public DateTime DateOfApplication { get; set; }

        public bool Discussed { get; set; }

        string Title { get; set; }

        public class LoanType
        {
            public int LoanTypeID { get; set; }
            public string Value { get; set; }
        }

        public bool PendingApplicationExists()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"SELECT * FROM [dbo].[LoanApplications] WHERE [AccountNumber] = @an AND [Discussed] = 0 ";

                var cmd = new SqlCommand(_sql, connection);

                cmd.Parameters
                    .Add(new SqlParameter("@an", SqlDbType.NVarChar))
                    .Value = AccountNumber;

                connection.Open();

                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    cmd.Dispose();
                    connection.Dispose();
                    return true;
                }
                else
                {
                    cmd.Dispose();
                    connection.Dispose();
                    return false;
                }
            }
        }

        [Required(ErrorMessage = "You mest provide a reason for your Loan")]
        [Display(Name = "Reason for Loan:")]
        public string LoanChoice { get; set; }

        public IEnumerable<LoanType> LoanTypeOptions =
            new List<LoanType>
            {
                new LoanType {LoanTypeID = 0, Value = "Mortgage" },
                new LoanType {LoanTypeID = 1, Value = "Car Loan" },
                new LoanType {LoanTypeID = 2, Value = "Education Costs" },
                new LoanType {LoanTypeID = 3, Value = "Other" },
            };

        [Required(ErrorMessage = "Please specifiy an amount required")]
        [Display(Name = "Amount Required:")]
        public string AmountRequired { get; set; }

        [Required]
        [Display(Name = "Period Of Repayment")]
        [DataType(DataType.Time)]
        public string PeriodOfRepayment { get; set; }

        public void SubmitApplication(object AccountNumber)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {       
                string _sql =@"INSERT INTO [dbo].[LoanApplications] (GUID, AccountNumber, LoanType, AmountRequired, RepaymentPeriod, DateOfApplication, Discussed) VALUES (@id, @an, @lt, @ar, @rp, @doa, @d) ";

                var cmd = new SqlCommand(_sql, connection);

                cmd.Parameters
                    .Add(new SqlParameter("@id", SqlDbType.UniqueIdentifier))
                    .Value = Guid.NewGuid();
                cmd.Parameters
                    .Add(new SqlParameter("@an", SqlDbType.NVarChar))
                    .Value = AccountNumber.ToString();
                cmd.Parameters
                    .Add(new SqlParameter("@lt", SqlDbType.NVarChar))
                    .Value = LoanChoice;
                cmd.Parameters
                    .Add(new SqlParameter("@ar", SqlDbType.NVarChar))
                    .Value = AmountRequired;
                cmd.Parameters
                    .Add(new SqlParameter("@rp", SqlDbType.NVarChar))
                    .Value = PeriodOfRepayment;
                cmd.Parameters
                   .Add(new SqlParameter("@doa", SqlDbType.DateTime))
                   .Value = DateTime.Now;
                cmd.Parameters
                   .Add(new SqlParameter("@d", SqlDbType.Bit))
                   .Value = 0;

                connection.Open();

                cmd.ExecuteScalar();

                cmd.Dispose();
                connection.Dispose();
            }
        }

        public bool PendingApplicationExists(string AccountNumber)
        {
            this.AccountNumber = AccountNumber;
            return PendingApplicationExists();
        }

        public void GetLoanDataByAccountNumber()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"SELECT * FROM [dbo].[LoanApplications] " +
                                  @"WHERE [AccountNumber] = @an AND [Discussed] = 0";

                Debug.WriteLine(AccountNumber);
                var cmd = new SqlCommand(_sql, connection);
                cmd.Parameters
                    .Add(new SqlParameter("@an", SqlDbType.NVarChar))
                    .Value = this.AccountNumber;
                connection.Open();

                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    this.LoanChoice = reader.GetString(reader.GetOrdinal("LoanType"));
                    this.AmountRequired = reader.GetString(reader.GetOrdinal("AmountRequired"));
                    this.PeriodOfRepayment = reader.GetString(reader.GetOrdinal("RepaymentPeriod"));
                    this.DateOfApplication = reader.GetDateTime(reader.GetOrdinal("DateOfApplication"));
                    reader.Dispose();
                    cmd.Dispose();
                }
                else
                {
                    reader.Dispose();
                    cmd.Dispose();
                }
            }
        }

        public void MarkLoanAsDiscussed()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"UPDATE [dbo].[LoanApplications] " + @"SET [Discussed] = 1 " +
                                  @"WHERE [AccountNumber] = @an";

                var cmd = new SqlCommand(_sql, connection);
                cmd.Parameters
                    .Add(new SqlParameter("@an", SqlDbType.NVarChar))
                    .Value = this.AccountNumber;
                connection.Open();

                var reader = cmd.ExecuteScalar();

                cmd.Dispose();
                connection.Close();
            }
        }
    }
}