using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADO_NET_ДЗ_Модуль_02_часть_02
{
    public partial class Form1 : Form
    {
        private NpgsqlConnection _connection;
        private string _connString = "";
        private NpgsqlDataAdapter _da = null;
        private DataTable _dt;
        private string _tableName = "";
        public Form1()
        {
            InitializeComponent();
            _connection = new NpgsqlConnection();
            _connString = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;
            _connection.ConnectionString = _connString;
            comboBox_tables.Items.AddRange(new string[]
            { "stationery", "buyer_company", "sales_manager","type_of_stationery","sales","arch_stationery",
                "arch_buyer_company", "arch_sales_manager", "arch_type_of_stationery"});
        }

        private void btn_show_Click(object sender, EventArgs e)
        {
            try
            {
                _dt = new DataTable();
                string SelectSql = $"select * from public.{_tableName}";
                _da = new NpgsqlDataAdapter(SelectSql, _connection);
                _da.Fill(_dt);
                dgv.DataSource = _dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erorr!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox_tables.SelectedIndex == 0) EditStationery();
                else if (comboBox_tables.SelectedIndex == 1) EditBuyerCompany();
                else if (comboBox_tables.SelectedIndex == 2) EditSalesManager();
                else if (comboBox_tables.SelectedIndex == 3) EditTypeOfStationery();
                else if (comboBox_tables.SelectedIndex == 4) EditSales();
                _da.Update(_dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erorr!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        private void EditStationery()
        {
            NpgsqlCommand insert_cmd = new NpgsqlCommand("insert_stationery", _connection);
            insert_cmd.CommandType = CommandType.StoredProcedure;
            insert_cmd.Parameters.Add("p_name", NpgsqlTypes.NpgsqlDbType.Varchar, 20, "name");
            insert_cmd.Parameters.Add("p_price", NpgsqlTypes.NpgsqlDbType.Numeric, 32, "price");
            insert_cmd.Parameters.Add("p_id_type", NpgsqlTypes.NpgsqlDbType.Integer, 32, "id_type");
            _da.InsertCommand = insert_cmd;
            NpgsqlCommand update_cmd = new NpgsqlCommand("update_stationery", _connection);
            update_cmd.CommandType = CommandType.StoredProcedure;
            update_cmd.Parameters.Add("p_id", NpgsqlTypes.NpgsqlDbType.Integer, 0, "id");
            update_cmd.Parameters["p_id"].SourceVersion = DataRowVersion.Original;
            update_cmd.Parameters.Add("p_name", NpgsqlTypes.NpgsqlDbType.Varchar, 20, "name");
            update_cmd.Parameters.Add("p_price", NpgsqlTypes.NpgsqlDbType.Numeric, 32, "price");
            update_cmd.Parameters.Add("p_id_type", NpgsqlTypes.NpgsqlDbType.Integer, 32, "id_type");
            _da.UpdateCommand = update_cmd;
            NpgsqlCommand delete_cmd = new NpgsqlCommand("delete_stationery", _connection);
            delete_cmd.CommandType = CommandType.StoredProcedure;
            delete_cmd.Parameters.Add("p_id", NpgsqlTypes.NpgsqlDbType.Integer, 0, "id");
            delete_cmd.Parameters["p_id"].SourceVersion = DataRowVersion.Original;
            _da.DeleteCommand = delete_cmd;
        }
        private void EditBuyerCompany()
        {
            NpgsqlCommand insert_cmd = new NpgsqlCommand("insert_buyer_company", _connection);
            insert_cmd.CommandType = CommandType.StoredProcedure;
            insert_cmd.Parameters.Add("p_name", NpgsqlTypes.NpgsqlDbType.Varchar, 20, "name");
            insert_cmd.Parameters.Add("p_adress", NpgsqlTypes.NpgsqlDbType.Varchar, 100, "adress");
            _da.InsertCommand = insert_cmd;
            NpgsqlCommand update_cmd = new NpgsqlCommand("update_buyer_company", _connection);
            update_cmd.CommandType = CommandType.StoredProcedure;
            update_cmd.Parameters.Add("p_id", NpgsqlTypes.NpgsqlDbType.Integer, 0, "id");
            update_cmd.Parameters["p_id"].SourceVersion = DataRowVersion.Original;
            update_cmd.Parameters.Add("p_name", NpgsqlTypes.NpgsqlDbType.Varchar, 20, "name");
            update_cmd.Parameters.Add("p_adress", NpgsqlTypes.NpgsqlDbType.Varchar, 100, "adress");
            _da.UpdateCommand = update_cmd;
            NpgsqlCommand delete_cmd = new NpgsqlCommand("delete_buyer_company", _connection);
            delete_cmd.CommandType = CommandType.StoredProcedure;
            delete_cmd.Parameters.Add("p_id", NpgsqlTypes.NpgsqlDbType.Integer, 0, "id");
            delete_cmd.Parameters["p_id"].SourceVersion = DataRowVersion.Original;
            _da.DeleteCommand = delete_cmd;
        }
        private void EditSalesManager()
        {
            NpgsqlCommand insert_cmd = new NpgsqlCommand("insert_sales_manager", _connection);
            insert_cmd.CommandType = CommandType.StoredProcedure;
            insert_cmd.Parameters.Add("p_firstname", NpgsqlTypes.NpgsqlDbType.Varchar, 20, "firstname");
            insert_cmd.Parameters.Add("p_lastname", NpgsqlTypes.NpgsqlDbType.Varchar, 20, "lastname");
            insert_cmd.Parameters.Add("p_profit", NpgsqlTypes.NpgsqlDbType.Numeric, 32, "profit");
            _da.InsertCommand = insert_cmd;
            NpgsqlCommand update_cmd = new NpgsqlCommand("update_sales_manager", _connection);
            update_cmd.CommandType = CommandType.StoredProcedure;
            update_cmd.Parameters.Add("p_id", NpgsqlTypes.NpgsqlDbType.Integer, 0, "id");
            update_cmd.Parameters["p_id"].SourceVersion = DataRowVersion.Original;
            update_cmd.Parameters.Add("p_firstname", NpgsqlTypes.NpgsqlDbType.Varchar, 20, "firstname");
            update_cmd.Parameters.Add("p_lastname", NpgsqlTypes.NpgsqlDbType.Varchar, 20, "lastname");
            update_cmd.Parameters.Add("p_profit", NpgsqlTypes.NpgsqlDbType.Numeric, 32, "profit");
            _da.UpdateCommand = update_cmd;
            NpgsqlCommand delete_cmd = new NpgsqlCommand("delete_sales_manager", _connection);
            delete_cmd.CommandType = CommandType.StoredProcedure;
            delete_cmd.Parameters.Add("p_id", NpgsqlTypes.NpgsqlDbType.Integer, 0, "id");
            delete_cmd.Parameters["p_id"].SourceVersion = DataRowVersion.Original;
            _da.DeleteCommand = delete_cmd;
        }
        private void EditTypeOfStationery()
        {
            NpgsqlCommand insert_cmd = new NpgsqlCommand("insert_type_of_stationery", _connection);
            insert_cmd.CommandType = CommandType.StoredProcedure;
            insert_cmd.Parameters.Add("p_name", NpgsqlTypes.NpgsqlDbType.Varchar, 20, "name");
            _da.InsertCommand = insert_cmd;
            NpgsqlCommand update_cmd = new NpgsqlCommand("update_type_of_stationery", _connection);
            update_cmd.CommandType = CommandType.StoredProcedure;
            update_cmd.Parameters.Add("p_id", NpgsqlTypes.NpgsqlDbType.Integer, 0, "id");
            update_cmd.Parameters["p_id"].SourceVersion = DataRowVersion.Original;
            update_cmd.Parameters.Add("p_name", NpgsqlTypes.NpgsqlDbType.Varchar, 20, "name");
            _da.UpdateCommand = update_cmd;
            NpgsqlCommand delete_cmd = new NpgsqlCommand("delete_type_of_stationery", _connection);
            delete_cmd.CommandType = CommandType.StoredProcedure;
            delete_cmd.Parameters.Add("p_id", NpgsqlTypes.NpgsqlDbType.Integer, 0, "id");
            delete_cmd.Parameters["p_id"].SourceVersion = DataRowVersion.Original;
            _da.DeleteCommand = delete_cmd;
        }
        private void EditSales()
        {
            NpgsqlCommand insert_cmd = new NpgsqlCommand("insert_sales", _connection);
            insert_cmd.CommandType = CommandType.StoredProcedure;
            insert_cmd.Parameters.Add("p_date_of_sale", NpgsqlTypes.NpgsqlDbType.Date, 20, "date_of_sale");
            insert_cmd.Parameters.Add("p_id_stationery", NpgsqlTypes.NpgsqlDbType.Integer, 16, "id_stationery");
            insert_cmd.Parameters.Add("p_id_sales_manager", NpgsqlTypes.NpgsqlDbType.Integer, 16, "id_sales_manager");
            insert_cmd.Parameters.Add("p_id_buyer_company", NpgsqlTypes.NpgsqlDbType.Integer, 16, "id_buyer_company");
            insert_cmd.Parameters.Add("p_quantity_products", NpgsqlTypes.NpgsqlDbType.Integer, 16, "quantity_products");
            _da.InsertCommand = insert_cmd;
            NpgsqlCommand update_cmd = new NpgsqlCommand("update_sales", _connection);
            update_cmd.CommandType = CommandType.StoredProcedure;
            update_cmd.Parameters.Add("p_id", NpgsqlTypes.NpgsqlDbType.Integer, 0, "id");
            update_cmd.Parameters["p_id"].SourceVersion = DataRowVersion.Original;
            update_cmd.Parameters.Add("p_date_of_sale", NpgsqlTypes.NpgsqlDbType.Date, 20, "date_of_sale");
            update_cmd.Parameters.Add("p_id_stationery", NpgsqlTypes.NpgsqlDbType.Integer, 16, "id_stationery");
            update_cmd.Parameters.Add("p_id_sales_manager", NpgsqlTypes.NpgsqlDbType.Integer, 16, "id_sales_manager");
            update_cmd.Parameters.Add("p_id_buyer_company", NpgsqlTypes.NpgsqlDbType.Integer, 16, "id_buyer_company");
            update_cmd.Parameters.Add("p_quantity_products", NpgsqlTypes.NpgsqlDbType.Integer, 16, "quantity_products");
            _da.UpdateCommand = update_cmd;
            NpgsqlCommand delete_cmd = new NpgsqlCommand("delete_sales", _connection);
            delete_cmd.CommandType = CommandType.StoredProcedure;
            delete_cmd.Parameters.Add("p_id", NpgsqlTypes.NpgsqlDbType.Integer, 0, "id");
            delete_cmd.Parameters["p_id"].SourceVersion = DataRowVersion.Original;
            _da.DeleteCommand = delete_cmd;
        }
        private void comboBox_tables_SelectedIndexChanged(object sender, EventArgs e)
        {
            _tableName = comboBox_tables.SelectedItem as string;
        }

    }
}
