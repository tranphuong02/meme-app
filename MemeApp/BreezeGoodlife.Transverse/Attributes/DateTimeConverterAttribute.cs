﻿//////////////////////////////////////////////////////////////////////
// File Name    : DateTimeConverterAttribute
// System Name  : BreezeGoodlife
// Summary      :
// Author       : phuong.tran
// Change Log   : 12/29/2015 9:56:09 AM - Create Date
/////////////////////////////////////////////////////////////////////

using System;
using System.Globalization;
using Framework.Datatable.RequestParser;
using Framework.LinqSupport.LinqSupport;
using Framework.Utility;

namespace Transverse.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class DateTimeConverterAttribute : ValueConverterAttribute
    {
        public override void Parse(string inValue, FilterHelper.ColumnFilterInfo info)
        {
            if (string.IsNullOrEmpty(inValue)) return;
            var value = DateTimeHelper.ParseDateTimeForSearch(inValue.Trim(), new CultureInfo("vi-VN"));
            if (!value.HasValue) return;

            info.GreaterThanOrEqualValue = value.Value;
            info.LessThanValue = value.Value.AddDays(1);
        }
    }
}