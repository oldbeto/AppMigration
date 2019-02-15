using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

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
            CTEGOLEntities2 Cte = new CTEGOLEntities2();
            List<tbMigrationFile1> OpenAwbs = Cte.tbMigrationFile1.ToList();

            ProcessOpenAWBs(OpenAwbs, Cte);
        }

        private void InsertMassData()
        {
           //string connString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
           // var data = System.IO.File.ReadAllLines(@"C:\TEMP\teste.csv");

           // for (int i = 1; i < data.Count() - 1; i++)
                
        }

        private void ProcessOpenAWBs(List<tbMigrationFile1> OpenAwbs, CTEGOLEntities2 conn)
        {
            List<tbMigrationFile8> AwbsCompletas = new List<tbMigrationFile8>();
            int count = 0;
            foreach (tbMigrationFile1 OpenAwb in OpenAwbs)
            {

                if ((!string.IsNullOrEmpty(OpenAwb.AWB_ID)) && (!string.IsNullOrEmpty(OpenAwb.ODN)))
                {
                    Int64 erpCode = Int64.Parse(OpenAwb.ODN.Substring(3));
                    try
                    {
                        var awb = conn.tbctes
                            .Where(w => w.erpCode.Equals(erpCode)).First();

                        tbMigrationFile8 file8 = new tbMigrationFile8();
                        
                        file8.OND = OpenAwb.ODN;
                        file8.ChaveCte = awb.eKey;
                        file8.AWB_ID = OpenAwb.AWB_ID;
                        count++;

                        conn.tbMigrationFile8.Add(file8);
                        //conn.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Erro: " + e.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            conn.SaveChanges();        

        MessageBox.Show("Operação Concluída.\n " + count + " Registros processados.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
        }
}
}
