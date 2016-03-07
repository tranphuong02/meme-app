//////////////////////////////////////////////////////////////////////
// File Name    : EnumConverterFromStringToIntAttribute
// System Name  : BreezeGoodlife
// Summary      :
// Author       : phuong.tran
// Change Log   : 12/29/2015 9:48:37 AM - Create Date
/////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Framework.Datatable.RequestParser;
using Framework.LinqSupport.LinqSupport;

namespace Transverse.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class EnumConverterFromStringToIntAttribute : ValueConverterAttribute
    {
        private readonly Type _enumType;

        public EnumConverterFromStringToIntAttribute(Type enumType)
        {
            _enumType = enumType;
        }

        public override void Parse(string inValue, FilterHelper.ColumnFilterInfo info)
        {
            if (!string.IsNullOrEmpty(inValue))
            {
                inValue = inValue.ToLower().Trim();

                if (_enumType != null)
                {
                    var equalValues = new List<object>();
                    foreach (var field in _enumType.GetFields(BindingFlags.Static | BindingFlags.Public))
                    {
                        var displayAttribute = field.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;
                        var display = displayAttribute == null ? field.Name : displayAttribute.Name;
                        if (display != null)
                        {
                            if (display.ToLower().Contains(inValue))
                            {
                                field.GetValue(_enumType);
                                equalValues.Add((int)Enum.Parse(_enumType, field.Name));
                            }
                        }
                    }

                    info.EqualValue = equalValues;
                }
            }
        }
    }
}