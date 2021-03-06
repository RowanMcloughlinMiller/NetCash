﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.ATMHardware
{
    class CancelCard
    {
        public CancelCard(string cardnumber)
        {
            cancelCard(cardnumber);
        }
        private void cancelCard(string cardNumber)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"UPDATE [dbo].[ATMUsers] Set [Canceled]=@b WHERE [CardNumber] = @a ";
                var cmd = new SqlCommand(_sql, connection);
                cmd.Parameters
                    .Add(new SqlParameter("@a", SqlDbType.NVarChar))
                    .Value = cardNumber;
                cmd.Parameters
                    .Add(new SqlParameter("@b", SqlDbType.Int))
                    .Value = 1;
                connection.Open();
                cmd.ExecuteScalar();
                cmd.Dispose();
                connection.Dispose();
            }
            Debug.WriteLine("CARD NUMBER : " + cardNumber + " HAS JUST BEEN CANCELLED");
        }
    }
}
