﻿using ATMVERSION2.AccountManager;
using System;

namespace WindowsFormsApplication1.Interceptor_Package
{
   public  class TransactionInfo : ContextObject
    {
        Account account;
        string description;
        int transactionAmount;
    public TransactionInfo(Account account, string description, int amount)
        {
            this.account = account;
            this.description = description;
            this.transactionAmount = amount;
        }


      public string  getAccountNumber()
        { return this.account.AccountNumber; }

        public string getAccountBalance()
        { return this.account.Balance.ToString(); }

        public string getAccountCard()
        { return this.account.cardNumber; }

        public string getDescription()
        { return this.description; }
        public string getAmount()
        { return this.transactionAmount.ToString();
        }

        public string getObj()
        {
            return "TransactionInfo: ";
        }

       

        public string getShortDescription()
        { return this.description; }

        public string getVerboseDescription()
        { return "Account: "+ getAccountNumber() + " Description: " + description+ " Amount: € " + getAmount()+ " "  + DateTime.Now; }
    }
}