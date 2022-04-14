using System;
using System.Windows.Forms;

namespace NavBuilder
{
    public partial class NewRegionDialog : Form
    {
        #region Constructor

        public NewRegionDialog()
        {
            InitializeComponent();
        }

        #endregion Constructor

        #region Properties

        public string RegionName { get; private set; }

        #endregion Properties

        #region Events

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Contains(",") || txtName.Text.Contains("|"))
            {
                MessageBox.Show("The region name contains invalid characters. Excluded characters are `,` `|`.", "Invalid region name", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("The region name can not be empty.", "Invalid region name", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            RegionName = txtName.Text;

            DialogResult = DialogResult.OK;
        }

        #endregion Events
    }
}