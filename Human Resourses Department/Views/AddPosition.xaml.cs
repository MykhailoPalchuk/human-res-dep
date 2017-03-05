using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Controllers;

namespace Views
{
    /// <summary>
    /// Logic of interaction for AddPosition.xaml
    /// </summary>
    public partial class AddPosition : Window
    {
        private int positionId;

        public AddPosition()
        {
            positionId = 0;
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
        }

        //For changing data
        public AddPosition(int positionId)
        {
            this.positionId = positionId;
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
            PositionController positionController = new PositionController();
            var position = positionController.GetPositionInfo(positionId);
            string name, hours, payment;
            position.TryGetValue("name", out name);
            position.TryGetValue("hours", out hours);
            position.TryGetValue("payment", out payment);
            nameTextBox.Text = name;
            hoursTextBox.Text = hours;
            paymentTextBox.Text = payment;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            if(nameTextBox.Text.Equals("") || !Input.IsName(nameTextBox.Text))
            {
                nameLabel.Content = "Start with capital letter";
                nameTextBox.BorderBrush = Brushes.Red;
                flag = false;        
            }
            if (hoursTextBox.Text.Equals("") || !Input.IsNumber(hoursTextBox.Text))
            {
                hoursLabel.Content = "Only integer numbers";
                hoursTextBox.BorderBrush = Brushes.Red;
                flag = false;
            }
            if (paymentTextBox.Text.Equals("") || !Input.IsDouble(paymentTextBox.Text))
            {
                paymentLabel.Content = "Only float numbers(ex. 5,5)";
                paymentTextBox.BorderBrush = Brushes.Red;
                flag = false;
            }
            if(flag == false)
                MessageBox.Show("Enter correct data", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                try
                {
                    int hours = int.Parse(hoursTextBox.Text);
                    double payment = double.Parse(paymentTextBox.Text);
                    PositionController positionController = new PositionController();
                    if (positionId == 0)
                    {
                        positionController.AddPosition(nameTextBox.Text, hours, payment);
                        MessageBox.Show("Position added successfully!", "Success");
                    }
                    else
                    {
                        if(positionController.ChangeName(positionId, nameTextBox.Text) &&
                                positionController.ChangeHours(positionId, hours) &&
                                positionController.ChangePayment(positionId, payment))
                            MessageBox.Show("Position data changed successfully!", "Success");
                        else
                            MessageBox.Show("Something went wrong", "Error");
                    }
                    TableWindow parent = Owner as TableWindow;
                    if (parent != null)
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
        
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            Owner = null;
        }
        
        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                OK_Click(new object(), new RoutedEventArgs());
        }
    }
}
