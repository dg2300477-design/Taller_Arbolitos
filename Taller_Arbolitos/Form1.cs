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
            this.Size = new Size(1000, 850);
            this.BackColor = Color.White;
            this.StartPosition = FormStartPosition.CenterScreen;
            configurarUiArbol();
        }

        private void configurarUiArbol()
        {
            panelArbol = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, AutoScroll = true };
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, panelArbol, new object[] { true });

            panelArbol.Paint += panelArbolPaint;
            panelArbol.MouseClick += panelArbolMouseClick;

            Panel panelControles = new Panel { Dock = DockStyle.Bottom, Height = 300, BackColor = Color.FromArgb(240, 240, 240), BorderStyle = BorderStyle.FixedSingle };

            int fila1Y = 25;
            int fila2Y = 85;
            int fila3Y = 145;
            int filaInfoY = 205;

            // --- Fila 1: Gestión e Inserción (Se añadieron los botones Eliminar y Aleatorio) ---
            Label lblValor = new Label { Text = "Valor Nodo:", Location = new Point(20, fila1Y + 5), Width = 75 };
            txtValorNodo = new TextBox { Location = new Point(95, fila1Y + 2), Width = 50 };

            Button btnInsertar = new Button { Text = "Insertar", Location = new Point(155, fila1Y), Width = 70, Height = 30 };
            btnInsertar.Click += btnInsertarClick;

            Button btnEliminar = new Button { Text = "Eliminar", Location = new Point(230, fila1Y), Width = 70, Height = 30 };
            btnEliminar.Click += btnEliminarClick;

            Button btnAleatorio = new Button { Text = "Aleatorio", Location = new Point(305, fila1Y), Width = 80, Height = 30 };
            btnAleatorio.Click += btnAleatorioClick;

            Button btnLimpiar = new Button { Text = "Limpiar Todo", Location = new Point(390, fila1Y), Width = 100, Height = 30 };
            btnLimpiar.Click += (s, e) => { miArbol.Clear(); nodoBuscado = null; actualizarInfoArbol(); };

            Label lblTipo = new Label { Text = "Tipo Árbol:", Location = new Point(510, fila1Y + 5), Width = 80 };
            cmbTipoArbol = new ComboBox { Location = new Point(590, fila1Y + 2), Width = 180, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbTipoArbol.Items.AddRange(new string[] { "Árbol AVL (Balanceado)", "Árbol Binario (BST)" });
            cmbTipoArbol.SelectedIndex = 0;
            cmbTipoArbol.SelectedIndexChanged += (s, e) => {
                miArbol = cmbTipoArbol.SelectedIndex == 0 ? new TreeAVL<int>() : new TreeBinary<int>();
                nodoBuscado = null;
                actualizarInfoArbol();
            };

            // --- Fila 2: Búsqueda ---
            Label lblBuscar = new Label { Text = "Buscar Nodo:", Location = new Point(20, fila2Y + 5), Width = 85 };
            txtBuscarNodo = new TextBox { Location = new Point(110, fila2Y + 2), Width = 60 };
            Button btnBuscar = new Button { Text = "Buscar y Resaltar", Location = new Point(180, fila2Y), Width = 140, Height = 30 };
            btnBuscar.Click += btnBuscarClick;

            // --- Fila 3: Recorridos ---
            Label lblTitRec = new Label { Text = "Recorridos:", Location = new Point(20, fila3Y + 5), Width = 80, Font = new Font("Arial", 9, FontStyle.Bold) };
            Button btnPre = new Button { Text = "PreOrder", Location = new Point(105, fila3Y), Width = 90, Height = 30 };
            btnPre.Click += (s, e) => { miArbol.PreOrder(); actualizarInfoArbol(); };
            Button btnIn = new Button { Text = "InOrder", Location = new Point(205, fila3Y), Width = 90, Height = 30 };
            btnIn.Click += (s, e) => { miArbol.InOrder(); actualizarInfoArbol(); };
            Button btnPost = new Button { Text = "PostOrder", Location = new Point(305, fila3Y), Width = 90, Height = 30 };
            btnPost.Click += (s, e) => { miArbol.PostOrder(); actualizarInfoArbol(); };
            Button btnLevel = new Button { Text = "LevelOrder", Location = new Point(405, fila3Y), Width = 100, Height = 30 };
            btnLevel.Click += (s, e) => { miArbol.LevelOrder(); actualizarInfoArbol(); };

            // --- Fila 4: Información ---
            lblInfoArbol = new Label { Text = "Información: Nodos: 0 | Altura: 0", Location = new Point(20, filaInfoY), Width = 800, ForeColor = Color.DarkBlue };
            lblRecorridos = new Label { Text = "Resultado Recorrido: ", Location = new Point(20, filaInfoY + 25), Width = 940, Height = 45, Font = new Font("Consolas", 10, FontStyle.Bold), BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle, TextAlign = ContentAlignment.MiddleLeft };

            panelControles.Controls.AddRange(new Control[] { lblValor, txtValorNodo, btnInsertar, btnEliminar, btnAleatorio, btnLimpiar, lblTipo, cmbTipoArbol, lblBuscar, txtBuscarNodo, btnBuscar, lblTitRec, btnPre, btnIn, btnPost, btnLevel, lblInfoArbol, lblRecorridos });

            this.Controls.Add(panelArbol);
            this.Controls.Add(panelControles);
        }

        private void btnInsertarClick(object sender, EventArgs e)
        {
            if (int.TryParse(txtValorNodo.Text, out int val)) { miArbol.Insert(val); txtValorNodo.Clear(); txtValorNodo.Focus(); actualizarInfoArbol(); }
        }

        // ---Evento para Eliminar ---
        private void btnEliminarClick(object sender, EventArgs e)
        {
            if (int.TryParse(txtValorNodo.Text, out int val))
            {
                if (!miArbol.Contains(val))
                {
                    MessageBox.Show("El nodo a eliminar no existe.");
                }
                else
                {
                    miArbol.Remove(val);
                    if (nodoBuscado == val) nodoBuscado = null; // Quita el resaltado si eliminamos el buscado
                }
                txtValorNodo.Clear();
                txtValorNodo.Focus();
                actualizarInfoArbol();
            }
        }

        // ---Evento para Generar 15 números al azar ---
        private void btnAleatorioClick(object sender, EventArgs e)
        {
            Random rnd = new Random();
            for (int i = 0; i < 15; i++)
            {
                miArbol.Insert(rnd.Next(1, 100));
            }
            actualizarInfoArbol();
        }

        private void btnBuscarClick(object sender, EventArgs e)
        {
            if (int.TryParse(txtBuscarNodo.Text, out int val))
            {
                if (miArbol.Contains(val)) { nodoBuscado = val; }
                else { nodoBuscado = null; MessageBox.Show("El nodo buscado no se encuentra en el árbol.", "Búsqueda Fallida"); }
                actualizarInfoArbol();
            }
        }

        private void actualizarInfoArbol()
        {
            TreeBinary<int> arbolReal = (TreeBinary<int>)miArbol;
            string tipoArbol = miArbol is TreeAVL<int> ? "Árbol AVL (Balanceado)" : "Árbol Binario de Búsqueda (BST)";

            lblInfoArbol.Text = $"Información: Nodos: {miArbol.Count()} | Altura: {miArbol.Height()} | Tipo: {tipoArbol}";
            lblRecorridos.Text = $"Resultado Recorrido: {arbolReal.recorridoActual}";

            int alturaArbol = miArbol.Height();
            int anchoVirtual = (int)Math.Pow(2, alturaArbol) * 50;
            panelArbol.AutoScrollMinSize = new Size(Math.Max(panelArbol.Width, anchoVirtual), alturaArbol * 90 + 100);
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
                g.DrawLine(Pens.Gray, x, y, x - xOffset, y + 70);
                dibujarNodo(g, nodo.Left, x - xOffset, y + 70, xOffset / 2);
            }
            if (nodo.Right != null)
            {
                g.DrawLine(Pens.Gray, x, y, x + xOffset, y + 70);
                dibujarNodo(g, nodo.Right, x + xOffset, y + 70, xOffset / 2);
            }

            Color colorFondo = (nodoBuscado.HasValue && nodo.Data == nodoBuscado.Value) ? Color.Crimson : Color.FromArgb(44, 62, 80);

            g.FillEllipse(new SolidBrush(colorFondo), x - 18, y - 18, 36, 36);
            g.DrawEllipse(Pens.White, x - 18, y - 18, 36, 36);
            g.DrawString(nodo.Data.ToString(), new Font("Arial", 10, FontStyle.Bold), Brushes.White, x - 12, y - 8);

            posicionesNodos[nodo] = new Rectangle(x - 18, y - 18, 36, 36);
        }

        private void panelArbolMouseClick(object sender, MouseEventArgs e)
        {
            Point puntoClic = new Point(e.X - panelArbol.AutoScrollPosition.X, e.Y - panelArbol.AutoScrollPosition.Y);

            foreach (var parNodo in posicionesNodos)
            {
                if (parNodo.Value.Contains(puntoClic))
                {
                    NodeTree<int> nodoClic = parNodo.Key;
                    TreeBinary<int> arbolReal = (TreeBinary<int>)miArbol;
                    int alturaIzquierda = arbolReal.obtenerAlturaExterna(nodoClic.Left);
                    int alturaDerecha = arbolReal.obtenerAlturaExterna(nodoClic.Right);
                    int factorBalance = alturaIzquierda - alturaDerecha;

                    MessageBox.Show(
                        $"Detalles del Nodo:\n\n" +
                        $"• Valor: {nodoClic.Data}\n" +
                        $"• Altura Subárbol: {nodoClic.Height}\n" +
                        $"• Factor de Balance: {factorBalance}",
                        "Información del Nodo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                }
            }
        }
    }
}