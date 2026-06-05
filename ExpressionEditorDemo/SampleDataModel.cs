using System.ComponentModel;

namespace DxExpressionEditorDemo
{
    /// <summary>
    /// Sample data model representing laser ablation parameters.
    /// The ExpressionEditor will introspect this type and expose its properties as formula fields.
    /// </summary>
	[DisplayName("Ablation Parameters")]
	public class SampleAblationModel
	{
		[DisplayName("Average Power (W)")]
		[Description("Average laser power in Watts")]
		public double Power { get; set; } = 50.0;

		[DisplayName("Pulse Frequency (Hz)")]
		[Description("Pulse repetition rate in Hertz")]
		public double PulseFrequency { get; set; } = 200000.0;

		[DisplayName("Spot Size (µm)")]
		[Description("Laser spot diameter in micrometers")]
		public double SpotSize { get; set; } = 30.0;

		[DisplayName("Scan Speed (mm/s)")]
		[Description("Linear scan speed in mm/s")]
		public double ScanSpeed { get; set; } = 1000.0;

		[DisplayName("Pulse Duration (ns)")]
		[Description("Pulse width in nanoseconds")]
		public double PulseDuration { get; set; } = 10.0;

		[DisplayName("Number of Passes")]
		[Description("Total number of scan passes")]
		public int NumberOfPasses { get; set; } = 5;

		[DisplayName("Hatch Spacing (µm)")]
		[Description("Distance between scan lines in micrometers")]
		public double HatchSpacing { get; set; } = 15.0;

		[DisplayName("Material Density (g/cm³)")]
		[Description("Density of the target material")]
		public double MaterialDensity { get; set; } = 8.9;

		[DisplayName("Ablation Threshold (J/cm²)")]
		[Description("Minimum fluence required for material removal")]
		public double AblationThreshold { get; set; } = 0.5;

		[DisplayName("Wavelength (nm)")]
		[Description("Laser emission wavelength")]
		public double Wavelength { get; set; } = 1030.0;

		public override string ToString()
        {
            return $"{Power} W, {PulseFrequency} Hz, {SpotSize} µm, {ScanSpeed} mm/s, {PulseDuration} ns";
        }

		public string GetSummary()
        {
            return $"Power: {Power} W, Pulse Frequency: {PulseFrequency} Hz, Spot Size: {SpotSize} µm, Scan Speed: {ScanSpeed} mm/s, Pulse Duration: {PulseDuration} ns";
        }
    }
}
