namespace Exportdataprocessing
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.urlDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.titleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.skunumDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.minpriceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maxpriceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.minchengjiaoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.minyishouDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reviewquantityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.keywordDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.marketDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.yiyilandbDataSet = new Exportdataprocessing.yiyilandbDataSet();
            this.productsTableAdapter = new Exportdataprocessing.yiyilandbDataSetTableAdapters.productsTableAdapter();
            this.btndaoru = new System.Windows.Forms.Button();
            this.btnrefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yiyilandbDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.urlDataGridViewTextBoxColumn,
            this.titleDataGridViewTextBoxColumn,
            this.skunumDataGridViewTextBoxColumn,
            this.minpriceDataGridViewTextBoxColumn,
            this.maxpriceDataGridViewTextBoxColumn,
            this.minchengjiaoDataGridViewTextBoxColumn,
            this.minyishouDataGridViewTextBoxColumn,
            this.reviewquantityDataGridViewTextBoxColumn,
            this.keywordDataGridViewTextBoxColumn,
            this.marketDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.productsBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(472, 30);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(304, 386);
            this.dataGridView1.TabIndex = 0;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "id";
            this.idDataGridViewTextBoxColumn.HeaderText = "id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // urlDataGridViewTextBoxColumn
            // 
            this.urlDataGridViewTextBoxColumn.DataPropertyName = "url";
            this.urlDataGridViewTextBoxColumn.HeaderText = "url";
            this.urlDataGridViewTextBoxColumn.Name = "urlDataGridViewTextBoxColumn";
            // 
            // titleDataGridViewTextBoxColumn
            // 
            this.titleDataGridViewTextBoxColumn.DataPropertyName = "title";
            this.titleDataGridViewTextBoxColumn.HeaderText = "title";
            this.titleDataGridViewTextBoxColumn.Name = "titleDataGridViewTextBoxColumn";
            // 
            // skunumDataGridViewTextBoxColumn
            // 
            this.skunumDataGridViewTextBoxColumn.DataPropertyName = "skunum";
            this.skunumDataGridViewTextBoxColumn.HeaderText = "skunum";
            this.skunumDataGridViewTextBoxColumn.Name = "skunumDataGridViewTextBoxColumn";
            // 
            // minpriceDataGridViewTextBoxColumn
            // 
            this.minpriceDataGridViewTextBoxColumn.DataPropertyName = "minprice";
            this.minpriceDataGridViewTextBoxColumn.HeaderText = "minprice";
            this.minpriceDataGridViewTextBoxColumn.Name = "minpriceDataGridViewTextBoxColumn";
            // 
            // maxpriceDataGridViewTextBoxColumn
            // 
            this.maxpriceDataGridViewTextBoxColumn.DataPropertyName = "maxprice";
            this.maxpriceDataGridViewTextBoxColumn.HeaderText = "maxprice";
            this.maxpriceDataGridViewTextBoxColumn.Name = "maxpriceDataGridViewTextBoxColumn";
            // 
            // minchengjiaoDataGridViewTextBoxColumn
            // 
            this.minchengjiaoDataGridViewTextBoxColumn.DataPropertyName = "minchengjiao";
            this.minchengjiaoDataGridViewTextBoxColumn.HeaderText = "minchengjiao";
            this.minchengjiaoDataGridViewTextBoxColumn.Name = "minchengjiaoDataGridViewTextBoxColumn";
            // 
            // minyishouDataGridViewTextBoxColumn
            // 
            this.minyishouDataGridViewTextBoxColumn.DataPropertyName = "minyishou";
            this.minyishouDataGridViewTextBoxColumn.HeaderText = "minyishou";
            this.minyishouDataGridViewTextBoxColumn.Name = "minyishouDataGridViewTextBoxColumn";
            // 
            // reviewquantityDataGridViewTextBoxColumn
            // 
            this.reviewquantityDataGridViewTextBoxColumn.DataPropertyName = "review_quantity";
            this.reviewquantityDataGridViewTextBoxColumn.HeaderText = "review_quantity";
            this.reviewquantityDataGridViewTextBoxColumn.Name = "reviewquantityDataGridViewTextBoxColumn";
            // 
            // keywordDataGridViewTextBoxColumn
            // 
            this.keywordDataGridViewTextBoxColumn.DataPropertyName = "keyword";
            this.keywordDataGridViewTextBoxColumn.HeaderText = "keyword";
            this.keywordDataGridViewTextBoxColumn.Name = "keywordDataGridViewTextBoxColumn";
            // 
            // marketDataGridViewTextBoxColumn
            // 
            this.marketDataGridViewTextBoxColumn.DataPropertyName = "market";
            this.marketDataGridViewTextBoxColumn.HeaderText = "market";
            this.marketDataGridViewTextBoxColumn.Name = "marketDataGridViewTextBoxColumn";
            // 
            // productsBindingSource
            // 
            this.productsBindingSource.DataMember = "products";
            this.productsBindingSource.DataSource = this.yiyilandbDataSet;
            // 
            // yiyilandbDataSet
            // 
            this.yiyilandbDataSet.DataSetName = "yiyilandbDataSet";
            this.yiyilandbDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // productsTableAdapter
            // 
            this.productsTableAdapter.ClearBeforeFill = true;
            // 
            // btndaoru
            // 
            this.btndaoru.Location = new System.Drawing.Point(51, 54);
            this.btndaoru.Name = "btndaoru";
            this.btndaoru.Size = new System.Drawing.Size(75, 23);
            this.btndaoru.TabIndex = 1;
            this.btndaoru.Text = "导入";
            this.btndaoru.UseVisualStyleBackColor = true;
            this.btndaoru.Click += new System.EventHandler(this.btndaoru_Click);
            // 
            // btnrefresh
            // 
            this.btnrefresh.Location = new System.Drawing.Point(132, 54);
            this.btnrefresh.Name = "btnrefresh";
            this.btnrefresh.Size = new System.Drawing.Size(75, 23);
            this.btnrefresh.TabIndex = 2;
            this.btnrefresh.Text = "刷新";
            this.btnrefresh.UseVisualStyleBackColor = true;
            this.btnrefresh.Click += new System.EventHandler(this.btnrefresh_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 500);
            this.Controls.Add(this.btnrefresh);
            this.Controls.Add(this.btndaoru);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.productsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yiyilandbDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private yiyilandbDataSet yiyilandbDataSet;
        private System.Windows.Forms.BindingSource productsBindingSource;
        private yiyilandbDataSetTableAdapters.productsTableAdapter productsTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn urlDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn titleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn skunumDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn minpriceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn maxpriceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn minchengjiaoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn minyishouDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn reviewquantityDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn keywordDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn marketDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btndaoru;
        private System.Windows.Forms.Button btnrefresh;
    }
}

