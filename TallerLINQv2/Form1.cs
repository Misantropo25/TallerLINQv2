using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TallerLINQv2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        NorthWindDCDataContext nwdc = new NorthWindDCDataContext();

        //
        bool pProductosClicked = false;
        bool pCategoriasClicked = false;
        bool pProveedoresClicked = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            //Color de fondo
            this.BackColor = Color.FromArgb(244, 245, 250);
            //Cambia el tamaño
            this.Size = new Size(1000, 600);

            //
            pProductos.Visible = false;
            pCategorias.Visible = false;
            pProveedores.Visible = false;

            pProductos.Location = new Point(70, 10);
            pCategorias.Location = new Point(70, 10);
            pProveedores.Location = new Point(70, 10);

            dgvTablaProductos.BackColor = Color.FromArgb(244, 245, 250);
        }

        //Metodos
        public void mostrarPanelProductos()
        {
            pProveedores.Visible = false;
            pCategorias.Visible = false;
            pProductos.Visible = true;

            // CONSULTA CON PROYECCIONES
            var consulta = from Prod in nwdc.Products select new { Prod.ProductID, Prod.ProductName, Prod.SupplierID, Prod.CategoryID, Prod.QuantityPerUnit, Prod.UnitPrice, Prod.UnitsInStock, Prod.UnitsOnOrder, Prod.ReorderLevel, Prod.Discontinued };

            dgvTablaProductos.DataSource = consulta;
        }
        public void mostrarPanelCategorias()
        {
            pProveedores.Visible = false;
            pProductos.Visible = false;
            pCategorias.Visible = true;

            var consulta = from Cate in nwdc.Categories select Cate;
            //dgvTablaCategorias.DataSource = consulta;
        }
        public void mostrarPanelProveedores()
        {
            pCategorias.Visible = false;
            pProductos.Visible = false;
            pProveedores.Visible = true;

            var consulta = from Prov in nwdc.Suppliers select Prov;
            dgvProveedor.DataSource = consulta;
        }

        private void pbCargar_Click(object sender, EventArgs e)
        {
            if (dgvTablaProductos.SelectedCells.Count > 0)
            {
                DataGridViewRow selectedRow = dgvTablaProductos.Rows[dgvTablaProductos.SelectedCells[0].RowIndex];
                MessageBox.Show(Convert.ToString(selectedRow.Cells["ProductID"].Value));
            }
        }









        //_____________________________________________________________________________________________________________________________________________
        //Mouse events
        //_____________________________________________________________________________________________________________________________________________
        private void mouseClick_pbProductos(object sender, EventArgs e)
        {
            mostrarPanelProductos();
            pProductosClicked = true;
            pCategoriasClicked = false;     pbCategorias.Image = Properties.Resources.categoria_bw;
            pProveedoresClicked = false;    pbProveedores.Image = Properties.Resources.proveedores_bw;
        }
        private void mouseEnter_pbProductos(object sender, EventArgs e)     { pbProductos.Image = Properties.Resources.productos_color; }
        private void mouseLeave_pbProductos(object sender, EventArgs e)     { if (pProductosClicked == false) {pbProductos.Image = Properties.Resources.productos_bw; } }
        //_____________________________________________________________________________________________________________________________________________
        private void mouseClick_pbCategorias(object sender, EventArgs e)
        {
            mostrarPanelCategorias();
            pCategoriasClicked = true;
            pProveedoresClicked = false;    pbProveedores.Image = Properties.Resources.proveedores_bw;
            pProductosClicked = false;      pbProductos.Image = Properties.Resources.productos_bw;
        }
        private void mouseEnter_pbCategorias(object sender, EventArgs e)    { pbCategorias.Image = Properties.Resources.categoria_color; }
        private void mouseLeave_pbCategorias(object sender, EventArgs e)    { if (pCategoriasClicked == false) { pbCategorias.Image = Properties.Resources.categoria_bw; } }

        //_____________________________________________________________________________________________________________________________________________
        private void mouseClick_pbProveedores(object sender, EventArgs e)
        {
            mostrarPanelProveedores();
            pProveedoresClicked = true;
            pCategoriasClicked = false;     pbCategorias.Image = Properties.Resources.categoria_bw;
            pProductosClicked = false;      pbProductos.Image = Properties.Resources.productos_bw;
        }
        private void mouseEnter_pbProveedores(object sender, EventArgs e)   { pbProveedores.Image = Properties.Resources.proveedores_color; }
        private void mouseLeave_pbProveedores(object sender, EventArgs e)   { if (pProveedoresClicked == false) { pbProveedores.Image = Properties.Resources.proveedores_bw; } }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        //Consultas de Proyeccion




        //Consultas con Expresiones lambda




        //Consultas con Clases parciales




        //Consultas con Join 





        //CRUD IMPLEMENTACION
        //____________________________________________________________________
        // METODOS PROVEEDORES
        //____________________________________________________________________
        private void cargarTablaProveedores() 
        {
            var consulta = from Prov in nwdc.Suppliers select Prov;
            dgvProveedor.DataSource = consulta;

        }


        private void btnAgregarProveedor_Click(object sender, EventArgs e)
        {
            Suppliers proveedor = new Suppliers();
            proveedor.SupplierID = (int)cbIDProveedorInterface.Value;
            proveedor.CompanyName = tbNombreCompania.Text;
            proveedor.ContactName = tbNombreContacto.Text;
            proveedor.ContactTitle = tbTituloContacto.Text;
            proveedor.Address = tbDireccion.Text;
            proveedor.City = tbCiudad.Text;
            proveedor.Region = tbRegion.Text;
            proveedor.PostalCode = tbCodigoPostal.Text;
            proveedor.Country = tbPais.Text;
            proveedor.Phone = tbTelefono.Text;
            proveedor.Fax = tbFax.Text;
            proveedor.HomePage = tbPaginaInicio.Text;

            nwdc.Suppliers.InsertOnSubmit(proveedor);

            try
            {
                nwdc.SubmitChanges(); // Confirmar la transaccion
                MessageBox.Show("Proveedor agregado con exito");
                cargarTablaProveedores();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        //Actualizar
        private void button2_Click(object sender, EventArgs e)
        {

            Suppliers proveedor = nwdc.Suppliers.Single(S => S.SupplierID == cbIDProveedorInterface.Value);

            proveedor.CompanyName = tbNombreCompania.Text;
            proveedor.ContactName = tbNombreContacto.Text;
            proveedor.ContactTitle = tbTituloContacto.Text;
            proveedor.Address = tbDireccion.Text;
            proveedor.City = tbCiudad.Text;
            proveedor.Region = tbRegion.Text;
            proveedor.PostalCode = tbCodigoPostal.Text;
            proveedor.Country = tbPais.Text;
            proveedor.Phone = tbTelefono.Text;
            proveedor.Fax = tbFax.Text;
            proveedor.HomePage = tbPaginaInicio.Text;

            try
            {
                nwdc.SubmitChanges(); // Confirmar la transaccion
                MessageBox.Show("Proveedor actualizado con exito");
                cargarTablaProveedores();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void pbBuscarProveedor_Click(object sender, EventArgs e)
        {
            try
            {
                var buscarProveedor = (from P in nwdc.Suppliers
                                       where P.SupplierID == Int16.Parse(txtBuscarProveedor.Text)
                                       select P);

                dgvProveedor.DataSource = buscarProveedor;
            }
            catch (Exception) {
                MessageBox.Show("Ingrese informacion");
            }
        }

        private void pbCargarProveedor_Click(object sender, EventArgs e)
        {
            if (dgvProveedor.SelectedCells.Count > 0)
            {
                DataGridViewRow selectedRow = dgvProveedor.Rows[dgvProveedor.SelectedCells[0].RowIndex];

                var buscarProveedor = (from S in nwdc.Suppliers
                                       where S.SupplierID == Int16.Parse(Convert.ToString(selectedRow.Cells["SupplierID"].Value))
                                       select S);

                cbIDProveedorInterface.Value = (decimal)buscarProveedor.First().SupplierID;
                tbNombreCompania.Text = buscarProveedor.First().CompanyName.ToString();
                tbNombreContacto.Text = buscarProveedor.First().ContactName.ToString();
                tbTelefono.Text = buscarProveedor.First().Phone.ToString();
                tbTituloContacto.Text = buscarProveedor.First().ContactTitle.ToString();
                tbDireccion.Text = buscarProveedor.First().Address.ToString();
                tbCiudad.Text = buscarProveedor.First().City.ToString();
                tbFax.Text = buscarProveedor.First().Fax.ToString();
                tbRegion.Text = buscarProveedor.First().Region.ToString();
                tbCodigoPostal.Text = buscarProveedor.First().PostalCode.ToString();
                tbPais.Text = buscarProveedor.First().Country.ToString();
                tbPaginaInicio.Text = buscarProveedor.First().HomePage.ToString();

            }
        }

        private void pbBorrarProveedor_Click(object sender, EventArgs e)
        {
            try
            {
                var ProveedorEliminado = (from P in nwdc.Suppliers
                                          where P.SupplierID == cbIDProveedorInterface.Value
                                          select P).First();

                nwdc.Suppliers.DeleteOnSubmit(ProveedorEliminado);

                try
                {
                    nwdc.SubmitChanges(); // Confirmar la transaccion
                    MessageBox.Show("Eliminado exitosamente");
                    cargarTablaProveedores();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            catch (Exception) {
                MessageBox.Show("Revise la informacion ingresada");
            }
        }

        




    }
}
