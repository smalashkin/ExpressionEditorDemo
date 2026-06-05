using DevExpress.XtraEditors;
using DevExpress.Data.Controls.ExpressionEditor;
using DevExpress.Data.Filtering;
using System.ComponentModel;
using DevExpress.Data.Filtering.Helpers;
using DevExpress.DataAccess.ExpressionEditor;
using DevExpress.DataAccess.Native.ExpressionEditor;

namespace DxExpressionEditorDemo
{
    public partial class frmExpressionEditorDemo : XtraForm
    {
        private static readonly Type _modelType = typeof(SampleAblationModel);
        private static readonly Type _constantsType = typeof(SampleAblationConstants);

        private readonly SampleAblationModel _sampleData = new();
        private static readonly PropertyDescriptorCollection _evaluatorProperties =
            EvaluatorPropertyHelper.BuildProperties(_modelType, _constantsType);

        public static void Run() => new frmExpressionEditorDemo()?.Show();

        public frmExpressionEditorDemo()
        {
            InitializeComponent();
        }

        private static ExpressionEditorContext CreateExpressionEditorContext()
        {
            var colorProvider = new CustomColorProvider();

            // CreateContext populates built-in Functions, Operators, and Constants automatically
            var context = ExpressionEditorContextHelper.CreateContext(
                includeAggregateFunctions: true,
                includeLikeOperator: true,
                colorProvider);

            context.AutoCompleteItemsProvider = new AutoCompleteItemsProvider(context);
            context.CriteriaOperatorValidatorProvider = new ValidatorProvider();

            // Populate from model type: Properties → Columns, Methods → Functions
            ExpressionEditorTypeHelper.PopulateColumns(context, _modelType);
            ExpressionEditorTypeHelper.PopulateFunctions(context, _modelType);

            // Populate domain-specific constants from dedicated class
            ExpressionEditorTypeHelper.PopulateConstants(context, _constantsType);

            return context;
        }

        private static string? ValidateExpression(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression)) return "⚠ Expression is empty.";

            try
            {
                var criteria = CriteriaOperator.Parse(expression);
                if (criteria is null) return "⚠ Parsed to null (empty expression).";
            }
            catch (Exception ex)
            {
                return $"✗ Validation error:\r\n{ex.Message}";
            }

            return null;
        }

        private void sbEditExpression_Click(object sender, EventArgs e)
        {
            string expression = meExpression.Text;
            var context = CreateExpressionEditorContext();

            using var view = new DevExpress.DataAccess.UI.ExpressionEditor.ExpressionEditorView(this.LookAndFeel, this);
            bool accepted = ExpressionEditorUIHelper.RunExpressionEditor(ref expression, view, context, ValidateExpression);
            if (accepted) meExpression.Text = expression;
            if (accepted) sbValidate_Click(sender, e);
        }

        private void sbValidate_Click(object sender, EventArgs e)
        {
            meResult.Text = ValidateExpression(meExpression.Text) ?? $"✓ Valid expression.";
        }

        private void sbEvaluate_Click(object sender, EventArgs e)
        {
            string expression = meExpression.Text;

            if (string.IsNullOrWhiteSpace(expression))
            {
                meResult.Text = "⚠ Expression is empty.";
                return;
            }

            try
            {
                var criteria = CriteriaOperator.Parse(expression);

                if (criteria is null)
                {
                    meResult.Text = "⚠ Parsed to null (empty expression).";
                    return;
                }

                // Merge model properties + constants so evaluator resolves both
                ExpressionEvaluator evaluator = new(_evaluatorProperties, criteria);
                object result = evaluator.Evaluate(_sampleData);

                meResult.Text =
                    $"✓ Result: {result}\r\n" +
                    $"   Type: {result?.GetType().Name ?? "null"}\r\n\r\n" +
                    $"Expression: {criteria}\r\n\r\n" +
                    $"Sample data:\r\n" +
                    $"   Power = {_sampleData.Power} W\r\n" +
                    $"   PulseFrequency = {_sampleData.PulseFrequency} Hz\r\n" +
                    $"   SpotSize = {_sampleData.SpotSize} µm\r\n" +
                    $"   ScanSpeed = {_sampleData.ScanSpeed} mm/s\r\n" +
                    $"   PulseDuration = {_sampleData.PulseDuration} ns";
            }
            catch (Exception ex)
            {
                meResult.Text = $"✗ Evaluation error:\r\n{ex.Message}";
            }
        }

        class ValidatorProvider : ICriteriaOperatorValidatorProvider
        {
            public ErrorsEvaluatorCriteriaValidator GetCriteriaOperatorValidator(ExpressionEditorContext context)
            {
                return new Validator(context);
            }
        }

        class Validator(ExpressionEditorContext context) : CriteriaOperatorValidator(context, supportsAggregates: true)
        {
            // Names of user-defined constants that are valid in expressions
            private static readonly HashSet<string> _knownConstants =
                new(GetConstantNames(typeof(SampleAblationConstants)), StringComparer.OrdinalIgnoreCase);

            public override void Visit(OperandProperty theOperand)
            {
                // Allow constants to pass validation without being in the Columns list
                if (theOperand is not null && _knownConstants.Contains(theOperand.PropertyName))
                    return;

                base.Visit(theOperand);
            }

            public override void Visit(FunctionOperator @operator)
            {
                //if (@operator.OperatorType == FunctionOperatorType.Now)
                //    this.errors.Add(new CriteriaValidatorError("Invalid function: now()"));
                base.Visit(@operator);
            }

            private static IEnumerable<string> GetConstantNames(Type type)
            {
                return type.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
                    .Where(f => f.IsLiteral || f.IsInitOnly)
                    .Select(f => f.Name);
            }
        }

        class CustomColorProvider : IExpressionEditorColorProvider
        {
            public Color GetColorForElement(ExpressionElementKind elementKind)
            {
                return elementKind switch
                {
                    ExpressionElementKind.Column => Color.BlueViolet,
                    ExpressionElementKind.Function => Color.FromArgb(128, 0, 128),
                    ExpressionElementKind.Constant => Color.FromArgb(0, 128, 0),
                    ExpressionElementKind.Operator => Color.FromArgb(64, 64, 64),
                    ExpressionElementKind.Group => Color.FromArgb(128, 128, 128),
                    ExpressionElementKind.Error => Color.Red,
                    ExpressionElementKind.Parenthesis => Color.FromArgb(128, 128, 128),
                    _ => Color.Black,
                };
            }
        }
    }
}
