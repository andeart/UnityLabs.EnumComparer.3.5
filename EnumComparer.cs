using System;
using System.Collections;
using System.Collections.Generic;



namespace Andeart.Utilities.EnumComparisons
{

    /// <summary>
    /// An optimised type-safe enum comparer that doesn't box enum values, created specifically for .NET Framework 3.5.
    /// .NET Framework 4.7.x fixes the enum-value boxing, so this is not as relevant there.
    /// A hybrid of:
    /// 1) Tyler Brinkley's Enums.NET at https://github.com/TylerBrinkley/Enums.NET (Copyright (c) 2016 Tyler Brinkley).
    /// 2) .NET Framework 3.5's EqualityComparer code, found by peeking at the source (https://referencesource.microsoft.com/ now only shows the latest .NET Framework version, which implicitly fixes this problem).
    /// </summary>
    /// <typeparam name="TEnum">The type of the enum.</typeparam>
    /// <seealso cref="T:System.Collections.IEqualityComparer" />
    /// <seealso cref="T:System.Collections.IComparer" />
    public sealed class EnumComparer<TEnum> : IEqualityComparer<TEnum>, IComparer<TEnum>, IEqualityComparer, IComparer where TEnum : struct, IConvertible, IComparable, IFormattable
    {
        /// <summary>
        /// The default comparer for the Enum type.
        /// </summary>
        private static EnumComparer<TEnum> _defaultComparer;

        /// <summary>
        /// Gets the default comparer for the Enum type. If it doesn't already exist, a new one is created.
        /// </summary>
        public static EnumComparer<TEnum> Default
        {
            get
            {
                EnumComparer<TEnum> comparer = _defaultComparer;
                if (comparer != null)
                {
                    return comparer;
                }

                comparer = new EnumComparer<TEnum> ();
                _defaultComparer = comparer;
                return comparer;
            }
        }

        /// <summary>
        /// <see cref="EnumComparer{TEnum}"/> constructor.
        /// Callers should use singleton property <see cref="_defaultComparer"/> instead.
        /// </summary>
        private EnumComparer () { }

        /// <inheritdoc />
        /// <summary>
        /// Indicates if <paramref name="x" /> equals <paramref name="y" /> without boxing the values.
        /// </summary>
        /// <param name="x">The first enum value.</param>
        /// <param name="y">The second enum value.</param>
        /// <returns>Indication if <paramref name="x" /> equals <paramref name="y" /> without boxing the values.</returns>
        public bool Equals (TEnum x, TEnum y)
        {
            return x.Equals (y);
        }

        /// <inheritdoc />
        /// <summary>
        /// Retrieves a hash code for <paramref name="value" /> without boxing the value.
        /// </summary>
        /// <param name="value">The enum value.</param>
        /// <returns>Hash code for <paramref name="value" /> without boxing the value.</returns>
        public int GetHashCode (TEnum value)
        {
            return value.GetHashCode ();
        }

        /// <inheritdoc />
        /// <summary>
        /// Compares <paramref name="x" /> to <paramref name="y" /> without boxing the values.
        /// </summary>
        /// <param name="x">The first enum value.</param>
        /// <param name="y">The second enum value.</param>
        /// <returns>1 if <paramref name="x" /> is greater than <paramref name="y" />, 0 if <paramref name="x" /> equals <paramref name="y" />,
        /// and -1 if <paramref name="x" /> is less than <paramref name="y" />.</returns>
        public int Compare (TEnum x, TEnum y)
        {
            return x.CompareTo (y);
        }


        #region Explicit Interface Implementation

        bool IEqualityComparer.Equals (object x, object y)
        {
            return x is TEnum && y is TEnum && Equals ((TEnum) x, (TEnum) y);
        }

        int IEqualityComparer.GetHashCode (object obj)
        {
            return obj is TEnum ? GetHashCode ((TEnum) obj) : 0;
        }

        int IComparer.Compare (object x, object y)
        {
            return (x is TEnum && y is TEnum) ? Compare ((TEnum) x, (TEnum) y) : 0;
        }

        #endregion
    }

}