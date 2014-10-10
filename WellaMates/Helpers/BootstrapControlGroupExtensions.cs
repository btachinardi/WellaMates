using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using TwitterBootstrapMVC.BootstrapMethods;
using TwitterBootstrapMVC.Controls;

namespace WellaMates.Helpers
{
    public static class BootstrapControlGroupExtensions
    {



        public static BootstrapControlGroupDropDownListFromEnum<TModel> DropDownListFromEnumFor<TModel, TValue>(
            this BootstrapControlGroupBase<TModel> controlGroup, Expression<Func<TModel, TValue>> expression, object[] excludeParams)
        {
            return controlGroup.DropDownListFromEnumFor(expression);
        }

        /// <summary>
        /// return the items of enum paired with its description.
        /// </summary>
        /// <param name="enumeration">enumeration type to be processed.</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetDescription(this Type enumeration)
        {
            if (!enumeration.IsEnum)
            {
                throw new ArgumentException("Passed type must be of Enum type", "enumeration");
            }

            var descriptions = new Dictionary<string, string>();
            var members = enumeration.GetMembers().Where(m => m.MemberType == MemberTypes.Field);

            foreach (MemberInfo member in members)
            {
                var attrs = member.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs.Count() != 0)
                    descriptions.Add(member.Name, ((DescriptionAttribute)attrs[0]).Description);
            }
            return descriptions;
        }
    }
}