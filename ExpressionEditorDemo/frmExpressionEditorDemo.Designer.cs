namespace DxExpressionEditorDemo
{
    partial class frmExpressionEditorDemo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lcInfo = new DevExpress.XtraEditors.LabelControl();
            ttc = new DevExpress.Utils.ToolTipController(components);
            sbValidate = new DevExpress.XtraEditors.SimpleButton();
            sbEvaluate = new DevExpress.XtraEditors.SimpleButton();
            sbEditExpression = new DevExpress.XtraEditors.SimpleButton();
            meResult = new DevExpress.XtraEditors.MemoEdit();
            meExpression = new DevExpress.XtraEditors.MemoEdit();
            bottomPanel = new Panel();
            ((System.ComponentModel.ISupportInitialize)meResult.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)meExpression.Properties).BeginInit();
            bottomPanel.SuspendLayout();
            SuspendLayout();
            // 
            // lcInfo
            // 
            lcInfo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            lcInfo.Location = new Point(0, 8);
            lcInfo.Name = "lcInfo";
            lcInfo.Padding = new Padding(8, 8, 8, 4);
            lcInfo.Size = new Size(671, 25);
            lcInfo.TabIndex = 0;
            lcInfo.Text = "Enter a formula using fields from SampleAblationModel. Click \"Edit Expression...\" to open the Expression Editor dialog.";
            lcInfo.ToolTipController = ttc;
            // 
            // ttc
            // 
            ttc.AutoPopDelay = 10000;
            ttc.CloseOnClick = DevExpress.Utils.DefaultBoolean.True;
            ttc.Rounded = true;
            ttc.RoundRadius = 4;
            // 
            // sbValidate
            // 
            sbValidate.Location = new Point(8, 8);
            sbValidate.Name = "sbValidate";
            sbValidate.Size = new Size(100, 30);
            sbValidate.TabIndex = 0;
            sbValidate.Text = "Validate";
            sbValidate.ToolTipController = ttc;
            sbValidate.Click += sbValidate_Click;
            // 
            // sbEvaluate
            // 
            sbEvaluate.Location = new Point(116, 8);
            sbEvaluate.Name = "sbEvaluate";
            sbEvaluate.Size = new Size(100, 30);
            sbEvaluate.TabIndex = 1;
            sbEvaluate.Text = "Evaluate";
            sbEvaluate.ToolTipController = ttc;
            sbEvaluate.Click += sbEvaluate_Click;
            // 
            // sbEditExpression
            // 
            sbEditExpression.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            sbEditExpression.Location = new Point(677, 8);
            sbEditExpression.Name = "sbEditExpression";
            sbEditExpression.Size = new Size(133, 30);
            sbEditExpression.TabIndex = 0;
            sbEditExpression.Text = "Edit Expression...";
            sbEditExpression.ToolTipController = ttc;
            sbEditExpression.Click += sbEditExpression_Click;
            // 
            // meResult
            // 
            meResult.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            meResult.Location = new Point(8, 46);
            meResult.Name = "meResult";
            meResult.Properties.ReadOnly = true;
            meResult.Size = new Size(802, 212);
            meResult.TabIndex = 2;
            meResult.ToolTipController = ttc;
            // 
            // meExpression
            // 
            meExpression.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            meExpression.EditValue = "[Power] / [PulseFrequency] / (3.14159265 * Sqr([SpotSize] / 20000.0))";
            meExpression.Location = new Point(8, 46);
            meExpression.Name = "meExpression";
            meExpression.Size = new Size(802, 150);
            meExpression.TabIndex = 1;
            meExpression.ToolTipController = ttc;
            // 
            // bottomPanel
            // 
            bottomPanel.Controls.Add(meResult);
            bottomPanel.Controls.Add(sbEvaluate);
            bottomPanel.Controls.Add(sbValidate);
            bottomPanel.Dock = DockStyle.Bottom;
            bottomPanel.Location = new Point(0, 202);
            bottomPanel.Name = "bottomPanel";
            bottomPanel.Size = new Size(822, 266);
            bottomPanel.TabIndex = 2;
            // 
            // frmExpressionEditorDemo
            // 
            ClientSize = new Size(822, 468);
            Controls.Add(meExpression);
            Controls.Add(sbEditExpression);
            Controls.Add(bottomPanel);
            Controls.Add(lcInfo);
            MinimumSize = new Size(700, 500);
            Name = "frmExpressionEditorDemo";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Expression Editor Demo";
            ((System.ComponentModel.ISupportInitialize)meResult.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)meExpression.Properties).EndInit();
            bottomPanel.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton sbValidate;
        private DevExpress.XtraEditors.SimpleButton sbEvaluate;
        private DevExpress.XtraEditors.SimpleButton sbEditExpression;
        private DevExpress.XtraEditors.MemoEdit meResult;
        private DevExpress.XtraEditors.MemoEdit meExpression;
        private DevExpress.XtraEditors.LabelControl lcInfo;
        private System.Windows.Forms.Panel bottomPanel;
        private DevExpress.Utils.ToolTipController ttc;
    }
}
