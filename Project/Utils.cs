using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public static class Utils
    {
        public static bool CheckUI(string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;
            return true;
        }
        public static string PreformOperation(string str1, string str2)
        {
            string result = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(str1))
                    throw new Exception("Input 1 is empty");

                if (string.IsNullOrEmpty(str2))
                    throw new Exception("Input 2 is empty");
                return AdditionOperation(str1,str2);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message.ToString(), "Exception", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
            }
            
            return result;
            
        }
        public static int ConvertToDecimal(string str)
        {
            int decNumber = 0;
            try
            {
                string validChar = "IVXLCDMvxlcdm";
                int nextIsBigflag = 0;
                int prevIsSameFlag = 0;
                bool checkPrevForFive = false;
                int prevNumber = 0;
                int number = 0;
                var romanToDecMap = GetRomanToNumberMapping();
               
                bool ret = true;
                foreach (char num in str)
                {
                    if (!validChar.Contains(num.ToString()))
                    {
                        ret = false;
                        break;
                    }
                    else
                        prevNumber = number;

                    foreach (var element in romanToDecMap)
                    {
                        if (element.Item2.ToString() == num.ToString())
                        {
                            number = element.Item1;
                            checkPrevForFive = element.Item3;
                            break;
                        }
                    }

                    if (number == prevNumber)
                    {
                        if (checkPrevForFive == true)
                        {
                            ret = false;
                            break;
                        }
                        prevIsSameFlag += 1;
                        if (prevIsSameFlag == 3)
                        {
                            ret = false;
                            break;
                        }
                    }
                    if (number > prevNumber && prevNumber != 0)
                    {
                        nextIsBigflag = nextIsBigflag + 1;
                    }
                    if ((prevIsSameFlag > 0 && nextIsBigflag > 0) || nextIsBigflag > 1)
                    {
                        ret = false;
                        break;
                    }
                    if (number < prevNumber)
                    {
                        prevIsSameFlag = 0;
                        nextIsBigflag = 0;
                    }
                    if (nextIsBigflag == 1)
                    {
                        decNumber = decNumber + (number - prevNumber) - prevNumber;
                    }

                    else
                    {
                        decNumber = decNumber + number;
                    }
                }

                if (!ret)
                {
                    decNumber = 0;
                    throw new Exception("Input value : " + str + "is not a valid roman format");
                }
                //Now to calculate Roman from deciaml
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message.ToString(), "Exception", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
            }
            return decNumber;
        }
        public static string ConvertToRoman(int sum)
        {         
            try
            {
                var romanNumeral = GetNumberToRomanMapping();
                StringBuilder sumRoman = new StringBuilder();
                foreach (var element in romanNumeral)
                {
                    while (sum >= element.Item1)
                    {
                        sumRoman.Append(element.Item2);
                        sum = sum - element.Item1;
                    }
                }
                return sumRoman.ToString();
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message.ToString(), "Exception", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
            }
            return string.Empty;
        }
        
        public static string AdditionOperation(string str1, string str2)
        {
            int a = ConvertToDecimal(str1);
            int b = ConvertToDecimal(str2);
            if (a == 0 || b == 0)
                return "--NA--";
            else
                return ConvertToRoman(a + b);
        }
        private static List<Tuple<int, String>> GetNumberToRomanMapping()
        {
            List<Tuple<int, String>> romanNumeral = new List<Tuple<int, String>>();
            romanNumeral.Add(new Tuple<int, String>(10000, "x"));
            romanNumeral.Add(new Tuple<int, String>(9000, "Mx"));
            romanNumeral.Add(new Tuple<int, String>(5000, "v"));
            romanNumeral.Add(new Tuple<int, String>(4000, "Mv"));
            romanNumeral.Add(new Tuple<int, String>(1000, "M"));
            romanNumeral.Add(new Tuple<int, String>(900, "CM"));
            romanNumeral.Add(new Tuple<int, String>(500, "D"));
            romanNumeral.Add(new Tuple<int, String>(400, "CD"));
            romanNumeral.Add(new Tuple<int, String>(100, "C"));
            romanNumeral.Add(new Tuple<int, String>(90, "XC"));
            romanNumeral.Add(new Tuple<int, String>(50, "L"));
            romanNumeral.Add(new Tuple<int, String>(40, "XL"));
            romanNumeral.Add(new Tuple<int, String>(10, "X"));
            romanNumeral.Add(new Tuple<int, String>(9, "IX"));
            romanNumeral.Add(new Tuple<int, String>(5, "V"));
            romanNumeral.Add(new Tuple<int, String>(4, "IV"));
            romanNumeral.Add(new Tuple<int, String>(1, "I"));

            return romanNumeral;
        }

        private static List<Tuple<int, String, bool>> GetRomanToNumberMapping()
        {
            List<Tuple<int, String, bool>> romanToDecMap = new List<Tuple<int, String, bool>>();
            romanToDecMap.Add(new Tuple<int, String, bool>(10000, "x", false));
            romanToDecMap.Add(new Tuple<int, String, bool>(5000, "v", true));
            romanToDecMap.Add(new Tuple<int, String, bool>(1000, "M", false));
            romanToDecMap.Add(new Tuple<int, String, bool>(500, "D", true));
            romanToDecMap.Add(new Tuple<int, String, bool>(100, "C", false));
            romanToDecMap.Add(new Tuple<int, String, bool>(50, "L", true));
            romanToDecMap.Add(new Tuple<int, String, bool>(10, "X", false));
            romanToDecMap.Add(new Tuple<int, String, bool>(5, "V", true));
            romanToDecMap.Add(new Tuple<int, String, bool>(1, "I", false));

            return romanToDecMap;
        }
    }
}
