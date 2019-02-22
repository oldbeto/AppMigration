using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using System.ComponentModel;

namespace AppMigration
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void btConsulta_Click(object sender, EventArgs e)
        {
            CTEGOLEntities Cte = new CTEGOLEntities();
            List<tbMigrationFile1> OpenAwbs = Cte.tbMigrationFile1.ToList();


            string inicioHora = string.Empty;
            string fimHora = string.Empty;
            string inicioDataTable = string.Empty;
            string fimDataTable = string.Empty;
            string inicioBulk = string.Empty;
            string fimBulk = string.Empty;

            inicioHora = DateTime.Now.ToLongTimeString();
            ProcessOpenAWBs(OpenAwbs, Cte, ref inicioDataTable, ref fimDataTable, ref inicioBulk, ref fimBulk);
            fimHora = DateTime.Now.ToLongTimeString();

            MessageBox.Show("Inicio do Processo: " + inicioHora +
                            "\nInicio Data Table: " + inicioDataTable +
                            "\nFim Data Table: " + fimDataTable +
                            "\nInicio Bulk: " + inicioBulk +
                            "\nFim Bulk: " + fimBulk +
                            "\nFim do Processo: " + fimHora);

        }

        public static DataTable ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        //Popula DataTable com base em formato .csv
        private void InsertMassData()
        {
            CTEGOLEntities cte = new CTEGOLEntities();
            var data = System.IO.File.ReadAllText(@"C:\TEMP\teste1000.csv");

            DataTable dt = ToDataTable(cte.tbMigrationFile1.ToList());

            foreach (var item in data.Split('\n'))
            {
                if (!string.IsNullOrEmpty(item))
                {
                    dt.Rows.Add();
                    int i = 0;

                    foreach (var cell in item.Split(','))
                    {
                        dt.Rows[dt.Rows.Count - 1][i] = cell;
                        i++;
                    }
                }
            }

            DatatableBulkCopy(dt, "dbo.tbMigrationFile1");
        }

        private void ProcessOpenAWBs(List<tbMigrationFile1> OpenAwbs, CTEGOLEntities conn, ref string inicioDataTable, ref string fimDataTable, ref string inicioBulk, ref string fimBulk)
        {
            List<tbMigrationFile8> file8List = new List<tbMigrationFile8>();
            int count = 0;

            inicioDataTable = DateTime.Now.ToLongTimeString();

            try
            {
                var chavesCTe = (from t in conn.tbctes
                                 join x in conn.tbMigrationFile1 on t.erpCode.ToString() equals x.ODN.Substring(3)
                                 //where t.erpCode.Equals(erpCode)
                                 select new
                                 {
                                     x.ODN,
                                     t.eKey,
                                     x.AWB_ID
                                 });

                foreach (var linha in chavesCTe)
                {
                    tbMigrationFile8 file8 = new tbMigrationFile8();
                    file8.OND = linha.ODN;
                    file8.AWB_ID = linha.AWB_ID;
                    file8.ChaveCte = linha.eKey;
                    count++;

                    file8List.Add(file8);
                }

                MessageBox.Show("Foram processados " + count + " itens.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (Exception e)
            {
                throw;
            }

            fimDataTable = DateTime.Now.ToLongTimeString();

            InsertMassDataProcessed(file8List, ref inicioBulk, ref fimBulk);
            
        }

        private void DatatableBulkCopy(DataTable dt, string tableTarget)
        {
            CTEGOLEntities cte = new CTEGOLEntities();
            var connString = cte.Database.Connection.ConnectionString;

            using (SqlConnection sqlConn = new SqlConnection(connString))
            {
                using (SqlBulkCopy bulk = new SqlBulkCopy(sqlConn))
                {
                    bulk.DestinationTableName = tableTarget;
                    try
                    {
                        sqlConn.Open();
                        bulk.WriteToServer(dt);
                    }
                    catch (Exception e)
                    {

                        throw;
                    }
                    finally
                    {
                        sqlConn.Close();
                    }
                }
            }
        }

        private void InsertMassDataProcessed(List<tbMigrationFile8> file8List, ref string inicioBulk, ref string fimBulk)
        {

            CTEGOLEntities cte = new CTEGOLEntities();
            inicioBulk = DateTime.Now.ToLongTimeString();

            DataTable dt = ToDataTable(cte.tbMigrationFile8.ToList());

            foreach (var item in file8List)
            {
                dt.Rows.Add();

                dt.Rows[dt.Rows.Count - 1][1] = item.OND;
                dt.Rows[dt.Rows.Count - 1][2] = item.AWB_ID;
                dt.Rows[dt.Rows.Count - 1][3] = item.ChaveCte;
            }

            DatatableBulkCopy(dt, "dbo.tbMigrationFile8");
            fimBulk = DateTime.Now.ToLongTimeString();
        }



        private void btImport_Click(object sender, EventArgs e)
        {
            string inicioHora = string.Empty;
            string fimHora = string.Empty;

            inicioHora = DateTime.Now.ToLongTimeString();
            InsertMassData();
            fimHora = DateTime.Now.ToLongTimeString();

            MessageBox.Show("Inicio: " + inicioHora + "\nFim: " + fimHora);
        }
    }
}
