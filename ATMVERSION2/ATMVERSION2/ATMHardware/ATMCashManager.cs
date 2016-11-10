﻿using System;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Helpers.Utils;

namespace ATMVERSION2.ATMHardware
{
    class ATMCashManager
    {

        private double total = 0;
        private int[] DenominationsAmounts = new int[6];
        DatabaseManager databaseManager;

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ATMCashManager()
        {
            databaseManager = new DatabaseManager();
            setLocalCash();
        }

       
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void setLocalCash()
        {
            string[] denominations = { "10Euro", "20Euro", "50Euro", "100Euro", "200Euro", "500Euro" };
            double[] values = { 10, 20, 50, 100, 200, 500 };
            int i = 0;
            while (i < denominations.Length)
            {
                int attempted = setEachDenomination(denominations[i]);
                DenominationsAmounts[i] = attempted;
                this.total += attempted * values[i];
                i++;
            }
           
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private int setEachDenomination(string current)
        {
            int returnedValue = databaseManager.retrieveDenominationAmounts(current);
            return returnedValue;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool isWithdrawable(double attemptedWithdrawal)
        {
            bool ReturnVal = CheckBank(attemptedWithdrawal);
            setLocalCash();
            return ReturnVal;
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        private bool CheckBank(double doubleattempt)
        {
            int attempted = Convert.ToInt32(doubleattempt);
            int[] Denominations = { 10, 20, 50, 100, 200, 500 };
           
            if (attempted <= this.total)
            {
                for (int i = Denominations.Length - 1; i >= 0; i--)
                {
                   
                    if (Denominations[i] > attempted)
                    {
                    }
                                    
                    else if (Denominations[i] <= attempted)
                    {
                        if (DenominationsAmounts[i] != 0)
                        {
                            attempted -= Denominations[i];
                            DenominationsAmounts[i]--;
                            if (attempted != 0)
                                i++;
                        }
                    }
                }
            }
            if (attempted == 0)
            {  return true; }
            else
            {  return false;}
        }
        

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        public void UpdateAmountWithdrawal(double updateAmount)
        {
            updatecashWithdrawal(updateAmount);
            int i = 0;
            string[] denominations = { "10Euro", "20Euro", "50Euro", "100Euro", "200Euro", "500Euro" };

            while (i < DenominationsAmounts.Length)
            {
                updateCashAmounts(denominations[i], DenominationsAmounts[i]);
                i++;
            }
        }
        
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void UpdateAmountDeposit(double updateAmount)
        {
            updatecashDeposit(updateAmount);
            int i = 0;
            string[] denominations = { "10Euro", "20Euro", "50Euro", "100Euro", "200Euro", "500Euro" };
            while (i < DenominationsAmounts.Length)
            {
                updateCashAmounts(denominations[i], DenominationsAmounts[i]);
                i++;
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        private void updateCashAmounts(string note, int amount)
        {
            databaseManager.updateATMCashAmount(note, amount);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void updatecashWithdrawal(double doubleattempt)
        {
            int attempted = Convert.ToInt32(doubleattempt);
            int[] Denominations = { 10, 20, 50, 100, 200, 500 };
           
            for (int i = Denominations.Length - 1; i >= 0; i--)
            {
               
                if (Denominations[i] > attempted)
                {
                }                
                else if (Denominations[i] <= attempted)
                {
                    if (DenominationsAmounts[i] != 0)
                    {
                        attempted -= Denominations[i];
                        DenominationsAmounts[i]--;
                        if (attempted != 0)
                            i++;
                    }
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void updatecashDeposit(double doubleattempt)
        {
            int attempted = Convert.ToInt32(doubleattempt);
            int[] Denominations = { 10, 20, 50, 100, 200, 500 };
           
            for (int i = Denominations.Length - 1; i >= 0; i--)
            {
                if (Denominations[i] > attempted)
                {
                }
               
                else if (Denominations[i] <= attempted)
                {
                   attempted -= Denominations[i];
                    DenominationsAmounts[i]++;
                    if (attempted != 0)
                        i++;
                }
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    }
}
