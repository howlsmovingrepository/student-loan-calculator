using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    ///
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_calc_Click(object sender, RoutedEventArgs e)
        {
            //Tuition Fees
            string strFees = txt_fees.Text;
            double dFees;
            bool bFees;
            bFees = double.TryParse(strFees, out dFees);
            if (bFees == false)
            {
                MessageBox.Show("Please input a valid number.");
                return;
             
            }
            if (dFees < 0)
            {
                MessageBox.Show("Please input a valid number:  non-negative.");
                return;
            }

            //Student Type
            string strType = txt_type.Text;
            strType.ToLower().Trim();
            if (strType != "u" && strType != "g")
                {
                MessageBox.Show("Please enter a lowercase \"u\" or \"g\"!");
                return;
            }

            //Residential Status
            string strStatus = cbx_status.Text;
            if (strStatus == "")
            {
                MessageBox.Show("Please choose a residential status!");
                return;
            }

            //Email
            string strEmail = txt_email.Text;
            if (!strEmail.Contains("@"))
            {
                MessageBox.Show("Please entaer an email containing \"@\"!");
                return;
            }

            // Calculate Loan Amount

            double dLoantAmount = 0;
            double dDefaultLoan = 0;

            if (strType == "u")
            {
                dDefaultLoan = 30000;
                if (strStatus == "in-state")
                {
                    dLoantAmount = dDefaultLoan * 1.2;
                }
                else if (strStatus == "out-of-state")
                {
                    dLoantAmount = dDefaultLoan * 0.8;
                }
                else if (strStatus == "foreign")
                {
                    dLoantAmount = dDefaultLoan * 0.5;
                }

            }

            else
            {
                dDefaultLoan = 50000;
                if (strStatus == "in-state")
                {
                    dLoantAmount = dDefaultLoan * 1.2;
                }
                else if (strStatus == "out-of-state")
                {
                    dLoantAmount = dDefaultLoan * 0.8;
                }
                else if (strStatus == "foreign")
                {
                    dLoantAmount = dDefaultLoan * 0.5;
                }

            }
            
            //From IU?

            string strHoosier = strEmail.Substring(strEmail.IndexOf('@') + 1);
            if (strHoosier == "iu.edu")
            {
                if (txt_type.Text == "u")
                {
                    dLoantAmount += 3000;
                }
                else
                {
                    dLoantAmount += 5000;
                }

            }

            //Calculate Interest Rate
            double dInterest = 0;
            if (dFees >=0 && dFees <= 20000)
            {
                if (dLoantAmount <= 30000)
                {
                    dInterest = .0452;
                }
                else
                {
                    dInterest = .0421;
                }
            }
            else if(dFees > 20000 && dFees <= 50000)
            {
                if (dLoantAmount <= 30000)
                {
                    dInterest = .0383;
                }
                else
                {
                    dInterest = .03765;
                }
            }
            else if (dFees > 50000)
            {
                if (dLoantAmount <= 30000)
                {
                    dInterest = .0359;
                }
                else
                {
                    dInterest = .0315;
                }
            }

            //lucky draw!!!!!
            string strBday = txt_birthdate.Text;
            string[] strBdayArray = strBday.Split('/');
            double dSumBday = Convert.ToDouble(strBdayArray[0]) + Convert.ToDouble(strBdayArray[1]) + Convert.ToDouble(strBdayArray[2]);

            if (dSumBday % 4 == 0)
            {
                dInterest = dInterest - .002;
            }

            //output!!
            txt_LoanAmount.Text = dLoantAmount.ToString("C2");
            txt_YearlyInterest.Text = dInterest.ToString("P2");

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            txt_birthdate.Clear();
            txt_email.Clear();
            txt_fees.Clear();
            txt_type.Clear();
            cbx_status.SelectedIndex = -1;
            txt_LoanAmount.Clear();
            txt_YearlyInterest.Clear();
        }
    }
}
