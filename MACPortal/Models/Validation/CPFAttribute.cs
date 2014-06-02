using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WellaMates.Models.Validation
{
    public class CPFAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return false;
            return Validate(value.ToString());
        }

        private bool Validate(string cpf)
        {
            if (cpf == null)
                return false;

            cpf = cpf.Replace(".", String.Empty).Replace("-", String.Empty).Trim();

            if (cpf.Length != 11)
                return false;

            switch (cpf)
            {
                case "00000000000":
                case "11111111111":
                case "22222222222":
                case "33333333333":
                case "44444444444":
                case "55555555555":
                case "66666666666":
                case "77777777777":
                case "88888888888":
                case "99999999999":
                    return false;
            }

            int sum = 0;
            for (int i = 0, j = 10, d; i < 9; i++, j--)
            {
                if (!Int32.TryParse(cpf[i].ToString(), out d))
                    return false;
                sum += d * j;
            }

            int remainder = sum % 11;

            string digit = (remainder < 2 ? 0 : 11 - remainder).ToString();
            string prefix = cpf.Substring(0, 9) + digit;

            sum = 0;
            for (int i = 0, j = 11; i < 10; i++, j--)
                sum += Int32.Parse(prefix[i].ToString()) * j;

            remainder = sum % 11;
            digit += (remainder < 2 ? 0 : 11 - remainder).ToString();

            return cpf.EndsWith(digit);
        }

        private string Format(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return string.Empty;

            cpf = cpf.Trim();

            if (cpf.Length != 11)
                return string.Empty;

            return string.Format("{0}.{1}.{2}-{3}", cpf.Substring(0, 3), cpf.Substring(3, 3), cpf.Substring(6, 3), cpf.Substring(9));
        }
    }
}