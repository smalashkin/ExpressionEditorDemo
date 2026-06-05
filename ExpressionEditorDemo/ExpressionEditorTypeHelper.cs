using System.ComponentModel;
using System.Reflection;
using DevExpress.Data.Controls.ExpressionEditor;

namespace ExpressionEditorDemo;

/// <summary>
/// Populates an <see cref="ExpressionEditorContext"/> from .NET types via reflection.
/// Properties → Columns, public methods → Functions, const/static readonly → Constants.
/// </summary>
internal static class ExpressionEditorTypeHelper
{
    /// <summary>
    /// Adds public instance properties of <paramref name="type"/> as Columns in the context.
    /// </summary>
    public static void PopulateColumns(ExpressionEditorContext context, Type type)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(type);

        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(type);

        foreach (PropertyDescriptor prop in properties)
        {
            context.Columns.Add(new ColumnInfo
            {
                Name = prop.Name,
                Type = prop.PropertyType,
                Description = prop.Description,
            });
        }
    }

    /// <summary>
    /// Adds public instance methods of <paramref name="type"/> as Functions in the context.
    /// Methods inherited from <see cref="object"/> are excluded.
    /// Displayed under "Methods" / TypeName in the EE tree.
    /// </summary>
    public static void PopulateFunctions(ExpressionEditorContext context, Type type)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(type);

        string typeName = GetDisplayName(type);

        MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

        foreach (MethodInfo method in methods)
        {
            // Skip property accessors and special methods
            if (method.IsSpecialName)
                continue;

            System.Reflection.ParameterInfo[] parameters = method.GetParameters();
            string displayName = FormatMethodSignature(method, parameters);
            string description = method.GetCustomAttribute<DescriptionAttribute>()?.Description
                ?? $"Method {method.Name} from {type.Name}";

            var functionInfo = new FunctionInfo("Methods")
            {
                Name = method.Name,
                DisplayName = displayName,
                FunctionCategory = typeName,
                Description = description,
                ArgumentTypes = parameters.Select(p => p.ParameterType).ToArray(),
            };

            context.Functions.Add(functionInfo);
        }
    }

    /// <summary>
    /// Adds const and static readonly fields of <paramref name="constantsType"/> 
    /// as Constants in the context.
    /// </summary>
    public static void PopulateConstants(ExpressionEditorContext context, Type constantsType)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(constantsType);

        string category = GetDisplayName(constantsType);

        FieldInfo[] fields = constantsType.GetFields(BindingFlags.Public | BindingFlags.Static);

        foreach (FieldInfo field in fields)
        {
            if (!field.IsLiteral && !field.IsInitOnly)
                continue;

            object? value = field.GetValue(null);
            string description = field.GetCustomAttribute<DescriptionAttribute>()?.Description
                ?? $"{field.Name} = {value}";

            var constantInfo = new ConstantInfo(category)
            {
                Name = field.Name,
                Description = $"{description}\r\nValue: {value}",
            };

            context.Constants.Add(constantInfo);
        }
    }

    private static string GetDisplayName(Type type)
    {
        return type.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? type.Name;
    }

    private static string FormatMethodSignature(MethodInfo method, System.Reflection.ParameterInfo[] parameters)
    {
        string parms = string.Join(", ", parameters.Select(p => $"{p.ParameterType.Name} {p.Name}"));
        return $"{method.Name}({parms})";
    }
}
