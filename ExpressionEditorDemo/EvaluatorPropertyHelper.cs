using System.ComponentModel;
using System.Reflection;

namespace ExpressionEditorDemo;

/// <summary>
/// Provides constant values as virtual property descriptors for expression evaluation.
/// When the evaluator encounters a property name matching a constant, it returns its value.
/// </summary>
internal sealed class ConstantPropertyDescriptor : PropertyDescriptor
{
    private readonly object _value;
    private readonly Type _propertyType;

    public ConstantPropertyDescriptor(string name, object value, Type propertyType)
        : base(name, [])
    {
        _value = value;
        _propertyType = propertyType;
    }

    public override Type ComponentType => typeof(object);
    public override bool IsReadOnly => true;
    public override Type PropertyType => _propertyType;
    public override bool CanResetValue(object component) => false;
    public override object? GetValue(object? component) => _value;
    public override void ResetValue(object component) { }
    public override void SetValue(object? component, object? value) { }
    public override bool ShouldSerializeValue(object component) => false;
}

/// <summary>
/// Builds a <see cref="PropertyDescriptorCollection"/> that includes both model properties
/// and constant values, allowing the evaluator to resolve constant names.
/// </summary>
internal static class EvaluatorPropertyHelper
{
    /// <summary>
    /// Creates a merged collection containing properties from <paramref name="modelType"/>
    /// and constant fields from <paramref name="constantsType"/>.
    /// </summary>
    public static PropertyDescriptorCollection BuildProperties(Type modelType, Type constantsType)
    {
        ArgumentNullException.ThrowIfNull(modelType);
        ArgumentNullException.ThrowIfNull(constantsType);

        var descriptors = new List<PropertyDescriptor>();

        // Add model properties
        PropertyDescriptorCollection modelProps = TypeDescriptor.GetProperties(modelType);
        foreach (PropertyDescriptor prop in modelProps)
        {
            descriptors.Add(prop);
        }

        // Add constants as virtual read-only properties
        FieldInfo[] fields = constantsType.GetFields(BindingFlags.Public | BindingFlags.Static);
        foreach (FieldInfo field in fields)
        {
            if (!field.IsLiteral && !field.IsInitOnly)
                continue;

            object? value = field.GetValue(null);
            if (value is not null)
            {
                descriptors.Add(new ConstantPropertyDescriptor(field.Name, value, field.FieldType));
            }
        }

        return new PropertyDescriptorCollection([.. descriptors]);
    }
}
