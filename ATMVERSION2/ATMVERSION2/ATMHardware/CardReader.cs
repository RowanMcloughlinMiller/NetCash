﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMVERSION2.ATMHardware
{    
    class CardReader
    {
        private Card currentCard;

        public CardReader(string cardLocation)
        {
            if (cardLocation != "")
            {
                readCardFromFile(cardLocation);
            }
            else
            {
                var path = Directory.GetCurrentDirectory();
                path += "\\NetCash_Debit_Card.txt";
                Debug.WriteLine("+++" + path);
                readCardFromFile(path);
 
            }
        }
        private void readCardFromFile(string cardLocation)
        {           
            string[] lines = System.IO.File.ReadAllLines(@cardLocation);
            string CN = lines[0].Replace("Card Number: ", "");
            string E = lines[1].Replace("Expiry Date: ", "");
            Debug.WriteLine(lines[0]);
            Debug.WriteLine(lines[1]);
            currentCard = new Card(CN, E);
        }
        public string getCardNumber()
        {
            return currentCard.getCardNumber();
        }
    }
}