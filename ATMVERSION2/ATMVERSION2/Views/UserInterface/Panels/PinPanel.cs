﻿using ATMVERSION2.Interfaces;
using ATMVERSION2.UserInterface.Buttons;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATMVERSION2.UserInterface.Panels
{
   public class PinPanel : ATMPanel , Subject 
    {
        protected static TextBox pinEntryBox;
        protected static Label messageLabel;
        protected static Label netCashLabel;
        public PinPanel()
        {
            this.name = "PinPanel";
            this.BackColor = System.Drawing.Color.White;
            this.Location = new System.Drawing.Point(109, 57);
            this.Name = "panel1";
            this.Size = new System.Drawing.Size(351, 194);
            this.TabIndex = 12;

            pinEntryBox = new System.Windows.Forms.TextBox();

            pinEntryBox.ReadOnly = true;
            pinEntryBox.Name = "ENTER PIN";
            pinEntryBox.Text = "";
            pinEntryBox.SetBounds(((this.Width / 2) - 50), this.Height / 2, 100, 40);
            this.Controls.Add(pinEntryBox);

            netCashLabel = new Label();
            netCashLabel.Text = "NET-CASH";
            netCashLabel.SetBounds(((this.Width / 2) - 30), ((this.Height / 2) - 30), 100, 40);
            this.Controls.Add(netCashLabel);

            messageLabel = new Label();
            messageLabel.Text = "";
            messageLabel.ForeColor = System.Drawing.Color.Red;
            messageLabel.SetBounds(((this.Width / 2) - 70), ((this.Height / 2) - 70), 150, 40);
            this.Controls.Add(messageLabel);

            navData.addNavigaion("MAIN");
        }  
                

        //PART OF OBSERVER DESIGN PATTERN -- SUBJECT PASSES ITSELF AS PARAMETER TO GET TEXT FROM AND UPDATES

        public override void update(Subject e)
        {
            ATMButton b = (ATMButton)e;
            pinEntryBox.Text += b.Text;
            pinEntryBox.Update();
        }
        public override void cancel()
        {
            this.navData.setNavigationPanelName("LOGOUT");
            notifyObservers();
        }
        public override void clear()
        {
            pinEntryBox.Clear();
            pinEntryBox.Update();
        }
        public override void enter()
        {
                //pinEntryBox.Clear();
                this.navData.setNavigationPanelName("MAIN");
                notifyObservers();           
        }
        public override TextBox getInput()
        {
            return pinEntryBox;
        }
        public void DisplayMessage(string message)
        {
            messageLabel.Text = message;
            Debug.WriteLine(messageLabel.Text);
            messageLabel.Update();
        }
    }
}
