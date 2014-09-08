using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace D360
{
    public partial class D3BindingsForm : Form
    {
        public InputProcessor inputProcessor;

        private bool EditingBinding = false;

        private D3Bindings editedBindings;

        public D3BindingsForm()
        {
            InitializeComponent();
        }


        private void D3BindingsForm_Load(object sender, EventArgs e)
        {

        }

        private void bindingTextBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!EditingBinding)
            {

                if (editedBindings == null)
                {
                    editedBindings = CopyBindings(inputProcessor.d3Bindings);
                }

                EditingBinding = true;

                TextBox senderTextBox = sender as TextBox;
                senderTextBox.BackColor = Color.White;
                Refresh();

            }
        }

        private D3Bindings CopyBindings(D3Bindings d3Bindings)
        {
            D3Bindings result = new D3Bindings();
            result.actionBarSkill1Key = d3Bindings.actionBarSkill1Key;
            result.actionBarSkill2Key = d3Bindings.actionBarSkill2Key;
            result.actionBarSkill3Key = d3Bindings.actionBarSkill3Key;
            result.actionBarSkill4Key = d3Bindings.actionBarSkill4Key;
            result.forceMoveKey = d3Bindings.forceMoveKey;
            result.forceStandStillKey = d3Bindings.forceStandStillKey;
            result.gameMenuKey = d3Bindings.gameMenuKey;
            result.inventoryKey = d3Bindings.inventoryKey;
            result.mapKey = d3Bindings.mapKey;
            result.potionKey = d3Bindings.potionKey;
            result.townPortalKey = d3Bindings.townPortalKey;
            result.worldMapKey = d3Bindings.worldMapKey;

            return result;
        }



        private void D3BindingsForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)System.Windows.Forms.Keys.F12)
            {
                CancelEditing();

                this.Hide();
                e.Handled = true;
                return;
            }


            e.Handled = true;

        }

        private void CancelEditing()
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                {
                    TextBox controlTextBox = control as TextBox;
                    controlTextBox.BackColor = System.Drawing.SystemColors.Control;
                }
            }

            EditingBinding = false;
            editedBindings = null;
        }

        private void D3BindingsForm_KeyDown(object sender, KeyEventArgs e)
        {
            

            if (actionBarSkill1TextBox.BackColor == Color.White)
            {
                editedBindings.actionBarSkill1Key = e.KeyCode;
                actionBarSkill1TextBox.Text = editedBindings.actionBarSkill1Key.ToString();
                actionBarSkill1TextBox.BackColor = System.Drawing.SystemColors.Control;

            }

            if (actionBarSkill2TextBox.BackColor == Color.White)
            {
                editedBindings.actionBarSkill2Key = e.KeyCode;
                actionBarSkill2TextBox.Text = editedBindings.actionBarSkill2Key.ToString();
                actionBarSkill2TextBox.BackColor = System.Drawing.SystemColors.Control;
            }

            if (actionBarSkill3TextBox.BackColor == Color.White)
            {
                editedBindings.actionBarSkill3Key = e.KeyCode;
                actionBarSkill3TextBox.Text = editedBindings.actionBarSkill3Key.ToString();
                actionBarSkill3TextBox.BackColor = System.Drawing.SystemColors.Control;
            }

            if (actionBarSkill4TextBox.BackColor == Color.White)
            {
                editedBindings.actionBarSkill4Key = e.KeyCode;
                actionBarSkill4TextBox.Text = editedBindings.actionBarSkill4Key.ToString();
                actionBarSkill4TextBox.BackColor = System.Drawing.SystemColors.Control;
            }

            if (inventoryTextBox.BackColor == Color.White)
            {
                editedBindings.inventoryKey = e.KeyCode;
                inventoryTextBox.Text = editedBindings.inventoryKey.ToString();
                inventoryTextBox.BackColor = System.Drawing.SystemColors.Control;
            }

            if (mapTextBox.BackColor == Color.White)
            {
                editedBindings.mapKey = e.KeyCode;
                mapTextBox.Text = editedBindings.mapKey.ToString();
                mapTextBox.BackColor = System.Drawing.SystemColors.Control;
            }

            if (forceStandStillTextBox.BackColor == Color.White)
            {
                editedBindings.forceStandStillKey = e.KeyCode;
                forceStandStillTextBox.Text = editedBindings.forceStandStillKey.ToString();
                forceStandStillTextBox.BackColor = System.Drawing.SystemColors.Control;
            }

            if (forceMoveTextBox.BackColor == Color.White)
            {
                editedBindings.forceMoveKey = e.KeyCode;
                forceMoveTextBox.Text = editedBindings.forceMoveKey.ToString();
                forceMoveTextBox.BackColor = System.Drawing.SystemColors.Control;
            }

            if (potionTextBox.BackColor == Color.White)
            {
                editedBindings.potionKey = e.KeyCode;
                potionTextBox.Text = editedBindings.potionKey.ToString();
                potionTextBox.BackColor = System.Drawing.SystemColors.Control;
            }

            if (townPortalTextBox.BackColor == Color.White)
            {
                editedBindings.townPortalKey = e.KeyCode;
                townPortalTextBox.Text = editedBindings.townPortalKey.ToString();
                townPortalTextBox.BackColor = System.Drawing.SystemColors.Control;
            }

            if (gameMenuTextBox.BackColor == Color.White)
            {
                editedBindings.gameMenuKey = e.KeyCode;
                gameMenuTextBox.Text = editedBindings.gameMenuKey.ToString();
                gameMenuTextBox.BackColor = System.Drawing.SystemColors.Control;
            }

            if (worldMapTextBox.BackColor == Color.White)
            {
                editedBindings.worldMapKey = e.KeyCode;
                worldMapTextBox.Text = editedBindings.worldMapKey.ToString();
                worldMapTextBox.BackColor = System.Drawing.SystemColors.Control;
            }

            if (EditingBinding)
            {
                EditingBinding = false;
            }
        }

        private void D3BindingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            CancelEditing();
            this.Hide();
        }

        private void saveAndCloseButton_Click(object sender, EventArgs e)
        {
            if (editedBindings != null)
            {

                inputProcessor.d3Bindings = editedBindings;
                editedBindings = null;

            }
            SaveD3Bindings(inputProcessor.d3Bindings);

            Hide();
        }

        private void SaveD3Bindings(D3Bindings bindings)
        {
            var bindingsFileStream = new FileStream(Application.StartupPath + @"\D3Bindings.xml", FileMode.Create);
            var bindingsXMLSerializer = new XmlSerializer(typeof(D3Bindings));
            bindingsXMLSerializer.Serialize(bindingsFileStream, bindings);
            bindingsFileStream.Close();
        }

        private void D3BindingsForm_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                actionBarSkill1TextBox.Text = inputProcessor.d3Bindings.actionBarSkill1Key.ToString();
                actionBarSkill2TextBox.Text = inputProcessor.d3Bindings.actionBarSkill2Key.ToString();
                actionBarSkill3TextBox.Text = inputProcessor.d3Bindings.actionBarSkill3Key.ToString();
                actionBarSkill4TextBox.Text = inputProcessor.d3Bindings.actionBarSkill4Key.ToString();
                inventoryTextBox.Text = inputProcessor.d3Bindings.inventoryKey.ToString();
                mapTextBox.Text = inputProcessor.d3Bindings.mapKey.ToString();
                forceStandStillTextBox.Text = inputProcessor.d3Bindings.forceStandStillKey.ToString();
                forceMoveTextBox.Text = inputProcessor.d3Bindings.forceMoveKey.ToString();
                potionTextBox.Text = inputProcessor.d3Bindings.potionKey.ToString();
                townPortalTextBox.Text = inputProcessor.d3Bindings.townPortalKey.ToString();
                gameMenuTextBox.Text = inputProcessor.d3Bindings.gameMenuKey.ToString();
                worldMapTextBox.Text = inputProcessor.d3Bindings.worldMapKey.ToString();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            
            CancelEditing();
            Hide();
        }

        private void skillsTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void D3BindingsForm_Shown(object sender, EventArgs e)
        {
            editedBindings = CopyBindings(inputProcessor.d3Bindings);
            Refresh();
        }


    }
}
