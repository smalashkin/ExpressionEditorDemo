using System.ComponentModel;

namespace DxExpressionEditorDemo;

/// <summary>
/// Physical and mathematical constants used in laser ablation calculations.
/// These are exposed in the Expression Editor's Constants section.
/// </summary>
[DisplayName("Constants")]
public static class SampleAblationConstants
{
    [Description("Mathematical constant Pi (π)")]
    public const double Pi = 3.14159265358979323846;

    [Description("Euler's number (e)")]
    public const double E = 2.71828182845904523536;

    [Description("Speed of light in vacuum (m/s)")]
    public const double SpeedOfLight = 299_792_458.0;

    [Description("Boltzmann constant (J/K)")]
    public const double BoltzmannConstant = 1.380649e-23;

    [Description("Planck constant (J·s)")]
    public const double PlanckConstant = 6.62607015e-34;

    [Description("Lorenz number for thermal conductivity (W·Ω/K²)")]
    public const double LorenzNumber = 2.44e-8;

    [Description("Stefan-Boltzmann constant (W/(m²·K⁴))")]
    public const double StefanBoltzmann = 5.670374419e-8;
}
