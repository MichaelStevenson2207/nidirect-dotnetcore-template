﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace nidirect_app_frontend.Attributes;

public class NumericOnlyAttribute : ValidationAttribute
{
    private readonly Regex _regex;

    public NumericOnlyAttribute()
    {
        _regex = new Regex(@"^\d+$", RegexOptions.None, TimeSpan.FromMilliseconds(100));

        ErrorMessage = "Only numeric characters are permitted";
    }

    public override bool IsValid(object value)
    {
        var stringValue = value as string;

        if (string.IsNullOrWhiteSpace(stringValue)) return true;

        return _regex.IsMatch(stringValue);
    }
}