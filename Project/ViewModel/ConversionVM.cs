
using Project.Command;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ViewModel
{
    public class ConversionVM:BaseClassNotificationChanged
    {
      
        public ExecuteCommand ExecuteConversion { get; private set; }
        private MainWindow _UI;
        public ConversionVM(MainWindow UI)
        {
            ExecuteConversion = new ExecuteCommand(this);
            _UI = UI;
        }
        private string _strInput1 = string.Empty;
        private string _strInput2 = string.Empty;
       
        private List<char> ListOfValidCharter = new List<char> { 'I', 'V', 'X', 'L', 'C', 'D', 'M', 'v', 'x', 'l', 'c', 'd', 'm' };

        public string Input1
        {
            get { return _strInput1; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    foreach (char item in value)
                    {
                        if (!ListOfValidCharter.Contains(item))
                        {
                            Input1Valid = false;
                            break;
                        }
                        Input1Valid = true;

                    }
                }
                _strInput1 = value;
                OnPropertyChanged("Input1");
            }
        }
        public string Input2
        {
            get { return _strInput2; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    foreach (char item in value)
                    {
                        if (!ListOfValidCharter.Contains(item))
                        {
                            Input2Valid = false;
                            break;
                        }
                        Input1Valid = true;

                    }
                }
                _strInput2 = value;
                OnPropertyChanged("Input2");
            }
        }

        private bool _input1Valid;
        public bool Input1Valid
        {
            get { return _input1Valid; }
            private set
            {
                if(!value)
                {
                    System.Windows.MessageBox.Show("Value 1 is invalid");
                }
                _input1Valid = value;
            }
        }
     
        private bool _input2Valid;
        public bool Input2Valid
        {
            get { return _input2Valid; }
            private set
            {
                if (!value)
                {
                    System.Windows.MessageBox.Show("Value 2 is invalid");
                }
                _input2Valid = value;
            }
        }

        private string _strOutputResult = string.Empty;
        public string MyOutputValue
        {
            get { return _strOutputResult; }
            set
            {
                _strOutputResult = value;
                OnPropertyChanged("MyOutputValue");
            }
        }

        private string _isValidValues;

        public string IsValidValidValues
        {
            get { return _isValidValues; }
            set
            {
                _isValidValues = value;
                OnPropertyChanged("IsValidValidValues");
            }
        }
        
        public void Calculate()
        {
            MyOutputValue =  Utils.PreformOperation(Input1, Input2);
            if (MyOutputValue == "--NA--")
                IsValidValidValues = "Invalid Input(s)";
            else
                IsValidValidValues = "Valid Input(s)";
            _UI.Out.Text = MyOutputValue;
            _UI.isValid.Content = IsValidValidValues;
        }
    }
}
