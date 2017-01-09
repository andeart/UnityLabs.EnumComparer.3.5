# EnumComparer

An optimised type-safe enum comparer that doesn't box enum values, created specifically for .NET Framework 3.5 and older.
Newer .NET Framework versions fix the enum-value boxing, so this is not relevant in those environments.
This is a hybrid of:
1) Tyler Brinkley's Enums.NET at https://github.com/TylerBrinkley/Enums.NET (Copyright (c) 2016 Tyler Brinkley). Go give this beautiful repo a star.
2) .NET Framework 3.5's EqualityComparer code, found by peeking at the source (https://referencesource.microsoft.com/ now only shows the latest .NET Framework version, which implicitly fixes this very problem).

This is best used as a comparer in `Dictionary<T1,T2>` and `Hashset<T>` objects, as they both resort to old-school `ObjectEqualityComparer<T>` when created via their default constructors (via `EqualityComparer<T>.Default`).
Both these types do a comparison check each time you deserialize, lookup, insert, or remove an entry. `EnumComparer` saves boxing allocations in these cases.

## Usage

* Drop `EnumComparer.cs` in your project.
* In your IDE, when creating a `Dictionary<T, T2>` or a `Hashset<T>`, pass in `EnumComparer<T>.Default` as the constructor argument.
    * If you use Intellisense, it should automatically fill out the correct argument as you start typing `Default` as the constructor argument.

## Benchmark Results

Coming soon...