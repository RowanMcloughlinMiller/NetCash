﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetCash.Controllers
{
    public class TransferController : Controller
    {
        // GET: Transfer
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Transfer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Transfer(Models.Transfer transfer)
        {
            Models.Account CurrentAccount = new Models.Account("12345678");
            Models.Account TargetAccount = new Models.Account(transfer.TargetAccountNumber);

            if(CurrentAccount.AreFundsAvailable(transfer.TransferAmount))
            {
                CurrentAccount.UpdateAmount(-transfer.TransferAmount);
                TargetAccount.UpdateAmount(transfer.TransferAmount);
            }
            return View();
        }
    }
}