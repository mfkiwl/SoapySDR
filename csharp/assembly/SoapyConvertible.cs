// Copyright (c) 2020-2021 Nicholas Corgan
// SPDX-License-Identifier: BSL-1.0

using System;

namespace SoapySDR
{
    internal class SoapyConvertible : System.IConvertible
    {
        private string _value;

        public SoapyConvertible(SoapyConvertible other) => _value = other._value;

        public SoapyConvertible(object value)
        {
            switch (value)
            {
                case string _:
                    _value = (string)value;
                    break;
                case bool _:
                    _value = TypeConversionInternal.BoolToString((bool)value);
                    break;
                case float _:
                    _value = TypeConversionInternal.FloatToString((float)value);
                    break;
                case double _:
                    _value = TypeConversionInternal.DoubleToString((double)value);
                    break;
                case decimal _:
                    _value = TypeConversionInternal.DoubleToString(Convert.ToDouble(value));
                    break;
                case sbyte _:
                    _value = TypeConversionInternal.SByteToString((sbyte)value);
                    break;
                case short _:
                    _value = TypeConversionInternal.ShortToString((short)value);
                    break;
                case int _:
                    _value = TypeConversionInternal.IntToString((int)value);
                    break;
                case long _:
                    _value = TypeConversionInternal.LongToString((long)value);
                    break;
                case byte _:
                    _value = TypeConversionInternal.ByteToString((byte)value);
                    break;
                case ushort _:
                    _value = TypeConversionInternal.UShortToString((ushort)value);
                    break;
                case uint _:
                    _value = TypeConversionInternal.UIntToString((uint)value);
                    break;
                case ulong _:
                    _value = TypeConversionInternal.ULongToString((ulong)value);
                    break;
                default:
                    _value = value.ToString(); // Good luck
                    break;
            }
        }

        public object ToArgType(ArgInfo.Type argType)
        {
            switch (argType)
            {
                case ArgInfo.Type.BOOL: return ToType(typeof(bool), null);
                case ArgInfo.Type.INT: return ToType(typeof(long), null);
                case ArgInfo.Type.FLOAT: return ToType(typeof(double), null);
                default: return _value;
            }
        }

        //
        // IConvertible overrides
        //

        public TypeCode GetTypeCode() => TypeCode.Object;

        public bool ToBoolean(IFormatProvider provider) => TypeConversionInternal.StringToBool(_value);

        public byte ToByte(IFormatProvider provider) => TypeConversionInternal.StringToByte(_value);

        public char ToChar(IFormatProvider provider) => throw new NotImplementedException();

        public DateTime ToDateTime(IFormatProvider provider) => throw new NotImplementedException();

        public decimal ToDecimal(IFormatProvider provider) => (decimal)TypeConversionInternal.StringToDouble(_value);

        public double ToDouble(IFormatProvider provider) => TypeConversionInternal.StringToDouble(_value);

        public short ToInt16(IFormatProvider provider) => TypeConversionInternal.StringToShort(_value);

        public int ToInt32(IFormatProvider provider) => TypeConversionInternal.StringToInt(_value);

        public long ToInt64(IFormatProvider provider) => TypeConversionInternal.StringToLong(_value);

        public sbyte ToSByte(IFormatProvider provider) => TypeConversionInternal.StringToSByte(_value);

        public float ToSingle(IFormatProvider provider) => TypeConversionInternal.StringToFloat(_value);

        public string ToString(IFormatProvider provider) => _value;

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            if (conversionType.Equals(typeof(string))) return ToString(provider);
            if (conversionType.Equals(typeof(bool))) return ToBoolean(provider);
            if (conversionType.Equals(typeof(sbyte))) return ToSByte(provider);
            if (conversionType.Equals(typeof(short))) return ToInt16(provider);
            if (conversionType.Equals(typeof(int))) return ToInt32(provider);
            if (conversionType.Equals(typeof(long))) return ToInt64(provider);
            if (conversionType.Equals(typeof(byte))) return ToByte(provider);
            if (conversionType.Equals(typeof(ushort))) return ToUInt16(provider);
            if (conversionType.Equals(typeof(uint))) return ToUInt32(provider);
            if (conversionType.Equals(typeof(ulong))) return ToUInt64(provider);
            if (conversionType.Equals(typeof(float))) return ToSingle(provider);
            if (conversionType.Equals(typeof(double))) return ToDouble(provider);
            if (conversionType.Equals(typeof(decimal))) return ToDecimal(provider);

            throw new NotImplementedException(conversionType.FullName);
        }

        public ushort ToUInt16(IFormatProvider provider) => TypeConversionInternal.StringToUShort(_value);

        public uint ToUInt32(IFormatProvider provider) => TypeConversionInternal.StringToUInt(_value);

        public ulong ToUInt64(IFormatProvider provider) => TypeConversionInternal.StringToULong(_value);

        //
        // Object overrides
        //

        public override string ToString() => ToString(null);

        public override int GetHashCode() => GetType().GetHashCode() ^ _value.GetHashCode();

        public override bool Equals(object obj) => (obj as SoapyConvertible)?._value.Equals(_value) ?? false;
    }
}