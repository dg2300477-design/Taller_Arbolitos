using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Taller_Arbolitos
{
    public partial class Form1 : Form
    {
        private TreeBinary<int> miArbol = new TreeAVL<int>();
        private Panel panelArbol;
        private TextBox txtValorNodo;
        private TextBox txtBuscarNodo;
        private Label lblInfoArbol, lblRecorridos;
        private ComboBox cmbTipoArbol;

        private int? nodoBuscado = null;
        private Dictionary<NodeTree<int>, Rectangle> posicionesNodos = new Dictionary<NodeTree<int>, Rectangle>();

        public Form1()
        {
            this.Text = "Taller 7 - Árboles Binarios y AVL";
            this.Size = new Size(900, 650); // Ajustamos el tamaño de la ventana
            this.BackColor = Color.White;
            this.StartPosition = FormStartPosition.CenterScreen;
            configurarUiArbol();
        }

        private void configurarUiArbol()
        {
            panelArbol = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, AutoScroll = true };

            // Doble Buffer para evitar parpadeos
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, panelArbol, new object[] { true });

            panelArbol.Paint += panelArbolPaint;
            panelArbol.MouseClick += panelArbolMouseClick;

            // Panel de controles más alto para acomodar las filas de botones
            Panel panelControles = new Panel { Dock = DockStyle.Bottom, Height = 130, BackColor = Color.LightGray };

            // --- Fila 1: Gestión de Nodos ---
            Label lblValor = new Label { Text = "Valor:", Location = new Point(10, 15), Width = 40 };
            txtValorNodo = new TextBox { Location = new Point(50, 13), Width = 50 };
            Button btnInsertar = new Button { Text = "Insertar", Location = new Point(110, 10), Width = 65 };
            btnInsertar.Click += btnInsertarClick;

            Label lblBuscar = new Label { Text = "Buscar:", Location = new Point(190, 15), Width = 50 };
            txtBuscarNodo = new TextBox { Location = new Point(240, 13), Width = 50 };
            Button btnBuscar = new Button { Text = "Buscar", Location = new Point(300, 10), Width = 65 };
            btnBuscar.Click += btnBuscarClick;

            Button btnLimpiar = new Button { Text = "Limpiar", Location = new Point(380, 10), Width = 65 };
            btnLimpiar.Click += (s, e) => { miArbol.Clear(); nodoBuscado = null; actualizarInfoArbol(); };

            cmbTipoArbol = new ComboBox { Location = new Point(460, 13), Width = 170, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbTipoArbol.Items.AddRange(new string[] { "Árbol AVL (Balanceado)", "Árbol Binario (BST)" });
            cmbTipoArbol.SelectedIndex = 0;
            cmbTipoArbol.SelectedIndexChanged += (s, e) => {
                miArbol = cmbTipoArbol.SelectedIndex == 0 ? new TreeAVL<int>() : new TreeBinary<int>();
                nodoBuscado = null;
                actualizarInfoArbol();
            };

            // --- Fila 2: Recorridos (Añadidos Pre, In y Post) ---
            Button btnPreOrder = new Button { Text = "PreOrder", Location = new Point(10, 45), Width = 80 };
            btnPreOrder.Click += (s, e) => { miArbol.PreOrder(); actualizarInfoArbol(); };

            Button btnInOrder = new Button { Text = "InOrder", Location = new Point(100, 45), Width = 80 };
            btnInOrder.Click += (s, e) => { miArbol.InOrder(); actualizarInfoArbol(); };

            Button btnPostOrder = new Button { Text = "PostOrder", Location = new Point(190, 45), Width = 80 };
            btnPostOrder.Click += (s, e) => { miArbol.PostOrder(); actualizarInfoArbol(); };

            // --- Fila 3: Etiquetas de Información ---
            lblInfoArbol = new Label { Text = "Nodos: 0 | Altura: 0", Location = new Point(10, 80), Width = 400 };
            lblRecorridos = new Label { Text = "Recorrido: ", Location = new Point(10, 100), Width = 850, Font = new Font("Arial", 9, FontStyle.Bold) };

            panelControles.Controls.AddRange(new Control[] {
                lblValor, txtValorNodo, btnInsertar,
                lblBuscar, txtBuscarNodo, btnBuscar,
                btnLimpiar, cmbTipoArbol,
                btnPreOrder, btnInOrder, btnPostOrder,
                lblInfoArbol, lblRecorridos
            });

            this.Controls.Add(panelArbol);
            this.Controls.Add(panelControles);
        }

        private void btnInsertarClick(object sender, EventArgs e)
        {
            if (int.TryParse(txtValorNodo.Text, out int val))
            {
                miArbol.Insert(val);
                txtValorNodo.Clear();
                txtValorNodo.Focus();
                actualizarInfoArbol();
            }
        }

        private void btnBuscarClick(object sender, EventArgs e)
        {
            if (int.TryParse(txtBuscarNodo.Text, out int val))
            {
                if (miArbol.Contains(val))
                {
                    nodoBuscado = val;
                }
                else
                {
                    nodoBuscado = null;
                    MessageBox.Show("El nodo buscado no se encuentra en el árbol.", "Búsqueda Fallida");
                }
                actualizarInfoArbol();
            }
        }

        private void actualizarInfoArbol()
        {
            lblInfoArbol.Text = $"Nodos: {miArbol.Count()} | Altura: {miArbol.Height()} | Tipo: {TreeClassifier.classify(miArbol)}";
            lblRecorridos.Text = $"Recorrido: {miArbol.recorridoActual}";

            int alturaArbol = miArbol.Height();
            int anchoVirtual = (int)Math.Pow(2, alturaArbol) * 40;
            panelArbol.AutoScrollMinSize = new Size(Math.Max(panelArbol.Width, anchoVirtual), alturaArbol * 80 + 100);
            panelArbol.Invalidate();
        }

        private void panelArbolPaint(object sender, PaintEventArgs e)
        {
            posicionesNodos.Clear();
            if (miArbol != null && !miArbol.IsEmpty())
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.TranslateTransform(panelArbol.AutoScrollPosition.X, panelArbol.AutoScrollPosition.Y);
                int anchoVirtual = Math.Max(panelArbol.Width, panelArbol.AutoScrollMinSize.Width);
                dibujarNodo(e.Graphics, miArbol.GetRoot(), anchoVirtual / 2, 40, anchoVirtual / 4);
            }
        }

        private void dibujarNodo(Graphics g, NodeTree<int> nodo, int x, int y, int xOffset)
        {
            if (nodo == null) return;

            if (nodo.Left != null)
            {
                g.DrawLine(Pens.Gray, x, y, x - xOffset, y + 60);
                dibujarNodo(g, nodo.Left, x - xOffset, y + 60, xOffset / 2);
            }
            if (nodo.Right != null)
            {
                g.DrawLine(Pens.Gray, x, y, x + xOffset, y + 60);
                dibujarNodo(g, nodo.Right, x + xOffset, y + 60, xOffset / 2);
            }

            Color colorFondo = (nodoBuscado.HasValue && nodo.Data == nodoBuscado.Value) ? Color.Crimson : Color.FromArgb(44, 62, 80);

            g.FillEllipse(new SolidBrush(colorFondo), x - 15, y - 15, 30, 30);
            g.DrawEllipse(Pens.White, x - 15, y - 15, 30, 30);
            g.DrawString(nodo.Data.ToString(), new Font("Arial", 9, FontStyle.Bold), Brushes.White, x - 10, y - 7);

            posicionesNodos[nodo] = new Rectangle(x - 15, y - 15, 30, 30);
        }

        private void panelArbolMouseClick(object sender, MouseEventArgs e)
        {
            Point puntoClic = new Point(e.X - panelArbol.AutoScrollPosition.X, e.Y - panelArbol.AutoScrollPosition.Y);

            foreach (var parNodo in posicionesNodos)
            {
                if (parNodo.Value.Contains(puntoClic))
                {
                    NodeTree<int> nodoClic = parNodo.Key;
                    int alturaIzquierda = miArbol.obtenerAlturaExterna(nodoClic.Left);
                    int alturaDerecha = miArbol.obtenerAlturaExterna(nodoClic.Right);
                    int factorBalance = alturaIzquierda - alturaDerecha;

                    MessageBox.Show(
                        $"Información del Nodo:\n\n" +
                        $"Valor: {nodoClic.Data}\n" +
                        $"Altura (Subárbol): {nodoClic.Height}\n" +
                        $"Factor de Balance: {factorBalance}",
                        "Detalle de Nodo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
    }
}