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
    public partial class ConfigForm : Form
    {
        public InputProcessor inputProcessor;

        private bool EditingConfig = false;

        private Configuration editedConfig;

        public ConfigForm()
        {
            InitializeComponent();
        }

        private void saveAndCloseButton_Click(object sender, EventArgs e)
        {
            if (editedConfig != null)
            {

                inputProcessor.config = editedConfig;
                editedConfig = null;


            }
            EditingConfig = false;

            SaveConfig(inputProcessor.config);

            inputProcessor.loadChanges();

            Hide();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {

            CancelEditing();
            Hide();
        }

        private Configuration copyConfig(Configuration config)
        {
            Configuration resultConfig = new Configuration();
            resultConfig.leftTriggerBinding = config.leftTriggerBinding;
            resultConfig.rightTriggerBinding = config.rightTriggerBinding;

            return resultConfig;

        }

        private void CancelEditing()
        {
            EditingConfig = false;
            editedConfig = null;
        }

        private void SaveConfig(Configuration configuration)
        {
            var configurationFileStream = new FileStream(Application.StartupPath + @"\Config.xml", FileMode.Create);
            var bindingsXMLSerializer = new XmlSerializer(typeof(Configuration));
            bindingsXMLSerializer.Serialize(configurationFileStream, configuration);
            configurationFileStream.Close();
        }

        private void LeftTriggerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LeftTriggerComboBox.SelectedItem != null)
            {
                EditingConfig = true;

                if (editedConfig == null)
                {
                    editedConfig = copyConfig(inputProcessor.config);
                }

                string selectedItem = (LeftTriggerComboBox.SelectedItem).ToString();

                switch (selectedItem)
                {
                    case "Action Bar Skill 1": editedConfig.leftTriggerBinding = "actionBarSkill1Key"; break;
                    case "Action Bar Skill 2": editedConfig.leftTriggerBinding = "actionBarSkill2Key"; break;
                    case "Action Bar Skill 3": editedConfig.leftTriggerBinding = "actionBarSkill3Key"; break;
                    case "Action Bar Skill 4": editedConfig.leftTriggerBinding = "actionBarSkill4Key"; break;
                    case "Inventory": editedConfig.leftTriggerBinding = "inventoryKey"; break;
                    case "Map": editedConfig.leftTriggerBinding = "mapKey"; break;
                    case "Potion": editedConfig.leftTriggerBinding = "potionKey"; break;
                    case "Town Portal": editedConfig.leftTriggerBinding = "townPortalKey"; break;
                    default: break;
                }
            }
        }

        private void RightTriggerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RightTriggerComboBox.SelectedItem != null)
            {
                EditingConfig = true;

                if (editedConfig == null)
                {
                    editedConfig = copyConfig(inputProcessor.config);
                }

                string selectedItem = (RightTriggerComboBox.SelectedItem).ToString();

                switch (selectedItem)
                {
                    case "Action Bar Skill 1": editedConfig.rightTriggerBinding = "actionBarSkill1Key"; break;
                    case "Action Bar Skill 2": editedConfig.rightTriggerBinding = "actionBarSkill2Key"; break;
                    case "Action Bar Skill 3": editedConfig.rightTriggerBinding = "actionBarSkill3Key"; break;
                    case "Action Bar Skill 4": editedConfig.rightTriggerBinding = "actionBarSkill4Key"; break;
                    case "Inventory": editedConfig.rightTriggerBinding = "inventoryKey"; break;
                    case "Map": editedConfig.rightTriggerBinding = "mapKey"; break;
                    case "Potion": editedConfig.rightTriggerBinding = "potionKey"; break;
                    case "Town Portal": editedConfig.rightTriggerBinding = "townPortalKey"; break;
                    default: break;
                }
            }
        }

        private void ConfigForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                LeftTriggerComboBox.SelectedIndex = LeftTriggerComboBox.Items.IndexOf(BindingToString(inputProcessor.config.leftTriggerBinding));
                RightTriggerComboBox.SelectedIndex = RightTriggerComboBox.Items.IndexOf(BindingToString(inputProcessor.config.rightTriggerBinding));
            }
        }

        private string BindingToString(string p)
        {
            switch (p)
            {
                case "actionBarSkill1Key": return "Action Bar Skill 1";
                case "actionBarSkill2Key": return "Action Bar Skill 2";
                case "actionBarSkill3Key": return "Action Bar Skill 3";
                case "actionBarSkill4Key": return "Action Bar Skill 4";
                case "inventoryKey": return "Inventory";
                case "mapKey": return "Map";
                case "potionKey": return "Potion";
                case "townPortalKey": return "Town Portal";
                default: return "";
            }
        }

        private void ConfigForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            CancelEditing();
            this.Hide();
        }
    }
}
