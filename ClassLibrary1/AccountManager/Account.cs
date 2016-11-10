﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using Helpers.Interceptor_Package.Dispatchers;
using Helpers.Interceptor_Package;
using Helpers.Utils;
using Helpers.BankTransactions;

namespace Helpers.AccountManager
{
    public class Account
    {
        public State state;
        protected DatabaseManager databaseManager;

        [Required]
        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }

        [Required]
        [Display(Name = "Balance")]
        public double Balance { get; set; }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public Account(string _accountnumber)
        {
            this.AccountNumber = _accountnumber;
            databaseManager = new DatabaseManager();
            this.Balance = GetBalance();
            this.state = GetState();

        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void IncreaseBalance(double transferAmount)
        {UpdateAmount(Convert.ToInt32(transferAmount));}

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        public void DecreaseBalance(double transferAmount)
        {
            UpdateAmount(-Convert.ToInt32(transferAmount));
            state.PayInterest(transferAmount);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private State GetState()
        {
            if (Balance >= 0.0) return new BalancedState(this);
            else return new OverdrawnState(this);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private double GetBalance()
        {
            return databaseManager.GetAccountBalance(this.AccountNumber);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void UpdateAmount(Transaction t)
        {          
            state.UpdateAmount(t.amount());
            databaseManager.addTransactionToDatabase(t);
           this.Balance =  GetBalance();     
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void UpdateAmount(int amount)
        {          
            state.UpdateAmount(amount);
           this.Balance =  GetBalance();     
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool AreFundsAvailable(double Balance)
        {
                if (GetBalance() + 100 >= Balance) return true;
                else return false;         
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool AreFullFundsAvailable(double WithdrawAttempt)
        {
            if (GetBalance() >= WithdrawAttempt)
                return true;
            else
                return false;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
   }
}