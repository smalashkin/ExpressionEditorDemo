# DevExpress ExpressionEditor API — Результаты исследования

## Обзор

`ExpressionEditorContext` (namespace `DevExpress.Data.Controls.ExpressionEditor`) — центральный класс конфигурации Expression Editor.  
Он содержит **5 коллекций**, определяющих разделы дерева в UI:

| Коллекция | Тип элемента | Раздел в UI |
|-----------|-------------|-------------|
| `Columns` | `List<ColumnInfo>` | Columns (поля данных) |
| `Parameters` | `List<ParameterInfo>` | Parameters (параметры отчёта/запроса) |
| `Functions` | `List<FunctionInfo>` | Functions (встроенные + custom функции) |
| `Operators` | `List<OperatorInfo>` | Operators (логические, арифметические, сравнения) |
| `Constants` | `List<ConstantInfo>` | Constants (True, False, ?, null и т.д.) |

---

## Иерархия типов

Все Info-классы наследуют от `ItemInfoBase`:

```
ItemInfoBase
├── Name        : string
├── Description : string
└── Category    : string

FunctionInfo : ItemInfoBase
├── FunctionCategory    : string   (группировка: "Math", "DateTime", "Text", "Logical", "Aggregate")
├── DisplayName         : string
├── UsageSample         : string
├── ArgumentTypes       : Type[]
├── CaretOffset         : int
└── CustomFunctionOperator : ICustomFunctionOperator

OperatorInfo : ItemInfoBase
   (наследует Name, Description, Category)

ConstantInfo : ItemInfoBase
   (наследует Name, Description, Category)

ParameterInfo : ItemInfoBase
├── Type : Type
└── ctor(IParameter parameter)

ColumnInfo
├── Name        : string
├── Type        : Type
└── Description : string
```

---

## Ключевой хелпер: `ExpressionEditorContextHelper`

**Namespace:** `DevExpress.DataAccess.ExpressionEditor`  
**Assembly:** `DevExpress.DataAccess.v25.2.dll`  
**Тип:** `public static class`

### Методы

```csharp
// Создаёт полностью заполненный контекст (Functions + Operators + Constants)
static ExpressionEditorContext CreateContext(
	bool includeAggregateFunctions,
	bool includeLikeOperator,
	IExpressionEditorColorProvider colorProvider);

// То же, но с возможностью передать дополнительные items
static ExpressionEditorContext CreateContext(
	bool includeAggregateFunctions,
	bool includeLikeOperator,
	IEnumerable<ConstantInfo> constants,
	IEnumerable<FunctionInfo> functions,
	IEnumerable<OperatorInfo> operators,
	IExpressionEditorColorProvider colorProvider);

// Расширенный вариант с custom aggregates
static ExpressionEditorContext CreateContext(
	bool includeAggregateFunctions,
	bool includeCustomAggregateFunctions,
	bool includeCustomAggregates,
	bool includeLikeOperator,
	IExpressionEditorColorProvider colorProvider);

// Получить список функций отдельно
static IEnumerable<FunctionInfo> GetFunctions(bool includeAggregateFunctions);

// Получить columns из контрола данных (Grid, Report и т.д.)
static IEnumerable<ColumnInfo> GetColumns(object sourceControl);
static IEnumerable<ColumnInfo> GetColumns(object sourceControl, IPropertiesProvider propertiesProvider);
static IEnumerable<ColumnInfo> GetColumns(object sourceControl, string dataMember);
static IEnumerable<ColumnInfo> GetColumns(object sourceControl, string dataMember,
	IPropertiesProvider propertiesProvider, Action<,> update);
```

---

## Что было в проекте (до исправления)

```csharp
var context = new ExpressionEditorContext
{
	ColorProvider = new CustomColorProvider()
};
context.Columns.AddRange(BuildColumnsFromType(typeof(SampleAblationModel)));
```

**Результат:** В UI отображался только раздел **Columns**.  
`Functions`, `Operators`, `Constants` — пустые списки.

---

## Исправленный вариант

```csharp
var colorProvider = new CustomColorProvider();

// CreateContext автоматически заполняет Functions, Operators, Constants
var context = ExpressionEditorContextHelper.CreateContext(
	includeAggregateFunctions: true,
	includeLikeOperator: true,
	colorProvider);

context.AutoCompleteItemsProvider = new AutoCompleteItemsProvider(context);
context.CriteriaOperatorValidatorProvider = new ValidatorProvider();
context.Columns.AddRange(BuildColumnsFromType(typeof(SampleAblationModel)));
```

**Результат:** В UI отображаются все разделы: Columns, Functions, Operators, Constants.

---

## Дополнительные настройки контекста

| Свойство | Назначение |
|----------|-----------|
| `ColorProvider` | Цветовая схема подсветки элементов |
| `AutoCompleteItemsProvider` | Автодополнение при вводе |
| `CriteriaOperatorValidatorProvider` | Валидация выражений в реальном времени |
| `ColumnDynamicProvider` | Динамические колонки (для runtime-источников) |
| `OptionsBehavior.AutoSelectFocusedItem` | Автовыбор элемента при фокусе |
| `OptionsBehavior.CapitalizeFunctionNames` | Заглавные буквы в именах функций |

---

## Категории функций (FunctionCategory)

Стандартные категории, заполняемые `CreateContext`:
- **Math** — Abs, Log, Round, Power, Sign, и т.д.
- **DateTime** — Now, Today, AddDays, GetYear, и т.д.
- **Text** — Concat, Substring, Upper, Lower, Trim, и т.д.
- **Logical** — Iif, IsNull, IsNullOrEmpty, и т.д.
- **Aggregate** — Sum, Count, Avg, Min, Max (если `includeAggregateFunctions = true`)

---

## Parameters

Раздел Parameters заполняется **вручную**. Это нужно, когда есть параметры отчёта или запроса:

```csharp
context.Parameters.Add(new ParameterInfo { Name = "StartDate", Type = typeof(DateTime) });
context.Parameters.Add(new ParameterInfo { Name = "Threshold", Type = typeof(double) });
```

Или через интерфейс `IParameter`:
```csharp
context.Parameters.Add(new ParameterInfo(myReportParameter));
```

---

## Используемые пакеты

- `DevExpress.Win.Design` 25.2.* (top-level, подтягивает всё остальное)
- `DevExpress.Data` 25.2.7 — содержит `ExpressionEditorContext`, `FunctionInfo`, etc.
- `DevExpress.DataAccess` 25.2.7 — содержит `ExpressionEditorContextHelper`
- `DevExpress.DataAccess.UI` 25.2.7 — содержит `ExpressionEditorView` (UI-форма)
