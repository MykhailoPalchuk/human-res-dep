﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;
using Controllers;

namespace Views
{
    /// <summary>
    /// Logic of interaction for AddPosition.xaml
    /// </summary>
    public partial class AddPosition : Window
    {
        public AddPosition()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
        }

        /*
         * Logic for OK button:
         * check input
         * highlight wrong input
         * get correct input
         * create new project in database
         * show message
         * close
         */
        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if(nameTextBox.Text.Equals(""))
            {
                MessageBox.Show("Fill all lines to continue", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                nameTextBox.BorderBrush = Brushes.Red;                
            }
            else if (hoursTextBox.Text.Equals(""))
            {
                MessageBox.Show("Fill all lines to continue", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                hoursTextBox.BorderBrush = Brushes.Red;
            }
            else if (paymentTextBox.Text.Equals(""))
            {
                MessageBox.Show("Fill all lines to continue", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                paymentTextBox.BorderBrush = Brushes.Red;
            }
            else
            {
                try
                {
                    int hours = int.Parse(hoursTextBox.Text);
                    double payment = double.Parse(paymentTextBox.Text);
                    PositionController positionController = new PositionController();
                    positionController.AddPosition(nameTextBox.Text, hours, payment);
                    MessageBox.Show("Position added successfully!", "Success");
                    TableWindow parent = Owner as TableWindow;
                    parent.RefreshTable(sender, e);
                    Close();
                }
                catch
                {
                    MessageBox.Show("Enter correct values for Hours and Payment", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    hoursTextBox.BorderBrush = Brushes.Red;
                    paymentTextBox.BorderBrush = Brushes.Red;
                }
            }
        }

        /*
         * Logic for Cancel button
         */
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /*
         * Avoid bug with minimizing owner window
         */
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            Owner = null;
        }

        /*
         * Set enter button as OK button click
         */
        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                OK_Click(new object(), new RoutedEventArgs());
        }
    }
}
