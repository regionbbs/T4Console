using System;

namespace T4Console
{
    public static class ClrTypeHelper
    {
        public static string GetFriendlyNameFromClrType(Type ClrType, bool IsNullable)
        {
            string clrTypeName = ClrType.Name;
            bool isArray = ClrType.IsArray;

            if (isArray)
                clrTypeName = clrTypeName.Replace("[]", "");

            switch (clrTypeName)
            {
                case "Int32":
                    return (isArray) ? "int[]" : "int" + ((IsNullable) ? "?" : "");
                case "Int16":
                    return (isArray) ? "short[]" : "short" + ((IsNullable) ? "?" : "");
                case "Int64":
                    return (isArray) ? "long[]" : "long" + ((IsNullable) ? "?" : "");
                case "Single":
                    return (isArray) ? "float[]" : "float" + ((IsNullable) ? "?" : "");
                case "Double":
                    return (isArray) ? "double[]" : "double" + ((IsNullable) ? "?" : "");
                case "Boolean":
                    return (isArray) ? "bool[]" : "bool" + ((IsNullable) ? "?" : "");
                case "Decimal":
                    return (isArray) ? "decimal[]" : "decimal" + ((IsNullable) ? "?" : "");
                case "String":
                    return (isArray) ? "string[]" : "string";
                case "Byte":
                    return (isArray) ? "byte[]" : "byte" + ((IsNullable) ? "?" : "");
                case "Char":
                    return (isArray) ? "char[]" : "char" + ((IsNullable) ? "?" : "");
                case "DateTime":
                    return (isArray) ? "DateTime[]" : "DateTime" + ((IsNullable) ? "?" : "");
                default:
                    return ClrType.Name;
            }
        }
    }
}
